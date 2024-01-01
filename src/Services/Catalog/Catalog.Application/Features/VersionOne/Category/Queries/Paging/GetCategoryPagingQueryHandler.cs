using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Repositories;
using MediatR;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class GetCategoryPagingQueryHandler : BaseQueryHandler, IRequestHandler<GetCategoryPagingQuery, IPagedList<CategoryDto>>
{
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    public GetCategoryPagingQueryHandler(
        IMapper mapper,
        ICategoryReadOnlyRepository categoryReadOnlyRepository
        ) : base(mapper)
    {
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
    }

    public async Task<IPagedList<CategoryDto>> Handle(GetCategoryPagingQuery request, CancellationToken cancellationToken)
    {
        return await _categoryReadOnlyRepository.GetPagingResultAsync(request.PagingRequest, cancellationToken);
    }
}