using System.Diagnostics;
using Microsoft.IO;

namespace EcommerceService.API.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
    private readonly RecyclableMemoryStreamManager _memoryStreamManager;

    public RequestResponseLoggingMiddleware
    (
        RequestDelegate next,
        ILoggerFactory loggerFactory
    )
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
        _memoryStreamManager = new RecyclableMemoryStreamManager();
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();

        string requestMessage = await logRequest(context);

        var originalBodyStream = context.Response.Body;
        var responseStream = _memoryStreamManager.GetStream();
        context.Response.Body = responseStream;

        Exception? exception = null;
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        watch.Stop();
        int responseTime = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds).Seconds;

        string responseMessage = string.Join(Environment.NewLine, "Http Response Information",
            $"Schema: {context.Request.Scheme} ", $"Host: {context.Request.Host} ", $"Path: {context.Request.Path} ",
            $"QueryString: {context.Request.QueryString} ", $"Response Body: {text}");

        var message = string.Join(Environment.NewLine, $"RequestId: {context.TraceIdentifier}",
            $"Response time for completed request: {responseTime} sn", requestMessage,
            exception == null ? null : "Exception:" + exception.Message, responseMessage);

        await responseStream.CopyToAsync(originalBodyStream);

        if (exception == null)
        {
            _logger.LogInformation(message);
        }
        else
        {
            _logger.LogError(message);
        }
    }

    private async Task<string> logRequest(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestStream = _memoryStreamManager.GetStream();
        await context.Request.Body.CopyToAsync(requestStream);

        var message = string.Join(Environment.NewLine, "#### Http Request Information:",
            $"Schema: {context.Request.Scheme}", $"Host: {context.Request.Host}", $"Method: {context.Request.Method}",
            $"Path: {context.Request.Path}", $"QueryString: {context.Request.QueryString}",
            $"Request Body: {readStreamInChunks(requestStream)}");

        context.Request.Body.Position = 0;
        return message;
    }

    private static string readStreamInChunks(Stream stream)
    {
        const int readChunkBufferLength = 4096;

        stream.Seek(0, SeekOrigin.Begin);

        var textWriter = new StringWriter();
        var reader = new StreamReader(stream);

        var readChunk = new char[readChunkBufferLength];
        int readChunkLength;

        do
        {
            readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
            textWriter.Write(readChunk, 0, readChunkLength);
        } while (readChunkLength > 0);

        return textWriter.ToString();
    }
}