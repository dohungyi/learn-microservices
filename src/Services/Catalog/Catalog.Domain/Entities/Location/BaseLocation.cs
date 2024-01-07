using SharedKernel.Domain;

public class BaseLocation : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public LocationType Type { get; set; }
}