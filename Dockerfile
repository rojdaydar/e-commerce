FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EcommerceService.API/EcommerceService.API.csproj", "EcommerceService.API/"]
COPY ["EcommerceService.Core/EcommerceService.Core.csproj", "EcommerceService.Core/"]
COPY ["EcommerceService.Data/EcommerceService.Data.csproj", "EcommerceService.Data/"]
COPY ["EcommerceService.Service/EcommerceService.Service.csproj", "EcommerceService.Service/"]
COPY ["EcommerceService.Test/EcommerceService.Test.csproj", "EcommerceService.Test/"]

RUN dotnet restore "EcommerceService.API/EcommerceService.API.csproj"

COPY . .
WORKDIR "/src/EcommerceService.API"
RUN dotnet build "EcommerceService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet EcommerceService.API.dll
