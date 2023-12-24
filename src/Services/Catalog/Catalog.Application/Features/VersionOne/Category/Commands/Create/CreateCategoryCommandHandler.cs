using Catalog.Application.DTOs;
using MediatR;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class CreateCategoryCommandHandler : BaseCommandHandler, IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public CreateCategoryCommandHandler(
        IServiceProvider provider) : base(provider)
    {
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}