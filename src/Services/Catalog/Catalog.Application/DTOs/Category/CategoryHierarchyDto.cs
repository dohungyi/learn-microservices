namespace Catalog.Application.DTOs;

public class CategoryHierarchyDto
{
    public CategoryDto CurrentCategory { get; set; }
    public IEnumerable<CategoryDto> ParentCategories { get; set; }
    public IEnumerable<CategoryDto> ChildCategories { get; set; }
}