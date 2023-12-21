using Catalog.Application.DTOs;
using Catalog.Application.Features.VersionOne;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application;

namespace Catalog.Api.Controllers.VersionOne;

[ApiVersion("1.0")]
public class SupplierController : BaseController
{
    [AllowAnonymous]
    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")]Guid supplierId, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetSupplierByIdQuery(supplierId), cancellationToken);
        return Ok(new ApiSimpleResult(result));
    }
    
    [AllowAnonymous]
    [HttpGet("get-by-alias/{alias}")]
    public async Task<IActionResult> GetByAliasAsync([FromRoute(Name = "alias")]string alias, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetSupplierByAliasQuery(alias), cancellationToken);
        return Ok(new ApiSimpleResult(result));
    }
    
    [AllowAnonymous]
    [HttpGet("paging")]
    public async Task<IActionResult> GetPagingAsync([FromQuery]PagingRequest pagingRequest, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetSupplierPagingQuery(pagingRequest), cancellationToken);
        return Ok(new ApiSimpleResult(result));
    }
    
    [AllowAnonymous]
    [HttpPost("create-supplier")]
    public async Task<IActionResult> CreateAsync([FromBody]CreateSupplierDto supplierDto, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new CreateSupplierCommand(supplierDto), cancellationToken);
        return Ok(new ApiSimpleResult(result));
    }
    
}