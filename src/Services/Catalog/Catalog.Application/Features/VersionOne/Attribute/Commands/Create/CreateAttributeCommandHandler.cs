using AutoMapper;
using Catalog.Application.Properties;
using Catalog.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Localization;
using SharedKernel.Application;

namespace Catalog.Application.Features.VersionOne;

public class CreateAttributeCommandHandler : BaseCommandHandler, IRequestHandler<CreateAttributeCommand, Guid>
{
    private readonly IAttributeWriteOnlyRepository _attributeWriteOnlyRepository;
    private readonly IAttributeReadOnlyRepository _attributeReadOnlyRepository;
    private readonly IStringLocalizer<Resources> _localizer;
    private readonly IMapper _mapper;
    public CreateAttributeCommandHandler(
        IServiceProvider provider
        ) : base(provider)
    {
        
    }

    public async Task<Guid> Handle(CreateAttributeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}