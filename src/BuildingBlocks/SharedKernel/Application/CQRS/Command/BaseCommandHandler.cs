using SharedKernel.Domain;

namespace SharedKernel.Application
{
    public abstract class BaseCommandHandler
    {
        protected readonly IServiceProvider _provider;
        protected readonly IAuthService _authService;

        public BaseCommandHandler(IServiceProvider provider, IAuthService authService)
        {
            _provider = provider;
            _authService = authService;
        }
    }
}
