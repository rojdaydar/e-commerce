namespace EcommerceService.Core.Domain;

public abstract class Base
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}