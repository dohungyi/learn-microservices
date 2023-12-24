namespace Catalog.Application.DTOs;

public class CategoryHierarchyDto
{
    public CategoryDto CurrentCategory { get; set; }
    public IList<CategoryDto> AncestorCategories { get; set; }
    public IList<CategoryDto> ChildCategories { get; set; }
}