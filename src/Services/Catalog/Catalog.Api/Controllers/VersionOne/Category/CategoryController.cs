using Catalog.Application.Features.VersionOne;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application;

namespace Catalog.Api.Controllers.VersionOne;

[ApiVersion("1.0")]
public class CategoryController : BaseController
{
    [AllowAnonymous]
    [HttpGet("get-navigation")]
    public async Task<IActionResult> GetCategoryNavigationAsync(CancellationToken cancellationToken = default)
    {
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpGet("get-hierarchy-by-id/{id}")]
    public async Task<IActionResult> GetCategoryWithHierarchyByIdAsync([FromRoute(Name = "id")]string categoryId, CancellationToken cancellationToken = default)
    {
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")]string categoryId, CancellationToken cancellationToken = default)
    {
       //  var result = await Mediator.Send(new GetSupplierByIdQuery(supplierId), cancellationToken);
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpGet("get-by-alias/{alias}")]
    public async Task<IActionResult> GetByAliasAsync([FromRoute(Name = "alias")]string alias, CancellationToken cancellationToken = default)
    {
        // Handler 
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpPost("get-paging")]
    public async Task<IActionResult> GetPagingAsync([FromBody]PagingRequest pagingRequest, CancellationToken cancellationToken = default)
    {
        // Handler 
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody]CreateCategoryCommand command, CancellationToken cancellationToken = default)
    {
        // Handler 

        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpPut("update/{id:guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")]Guid categoryId, [FromBody]UpdateSupplierCommand command, CancellationToken cancellationToken = default)
    {
        if (categoryId != command.Id)
        {
            return BadRequest();
        }
        // Handler 
        return Ok(new ApiSimpleResult());
    }
    
    [AllowAnonymous]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")]string categoryId, CancellationToken cancellationToken = default)
    {
        // Handler 
        return Ok(new ApiSimpleResult());
    }
    
}