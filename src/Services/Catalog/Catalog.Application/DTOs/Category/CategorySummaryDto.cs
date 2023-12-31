namespace Catalog.Application.DTOs;

public class CategorySummaryDto
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Alias  { get; set; }
    public string Description { get; set; }
    public int Level { get; set; }
    
    public bool IsSelected { get; set; }
}