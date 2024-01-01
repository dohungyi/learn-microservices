using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;
using SharedKernel.Runtime.Exceptions;

namespace Catalog.Application.Features.VersionOne;

public class GetCategoryByIdQueryHandler : BaseQueryHandler, IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly IStringLocalizer<Resources> _localizer;
    private readonly IMapper _mapper;
    
    public GetCategoryByIdQueryHandler(IMapper mapper, ICategoryReadOnlyRepository categoryReadOnlyRepository, IStringLocalizer<Resources> localizer) : base(mapper)
    {
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _localizer = localizer;
        _mapper = mapper;
    }
  

    public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryReadOnlyRepository.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
        if (category == null)
        {
            throw new BadRequestException(_localizer["category_does_not_exist_or_was_deleted"].Value);
        }
        
        var categoryDto = _mapper.Map<CategoryDto>(category);
        
        return categoryDto;
    }
}