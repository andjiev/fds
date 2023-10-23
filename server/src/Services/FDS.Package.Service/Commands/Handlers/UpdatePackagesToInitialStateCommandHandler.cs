namespace FDS.Package.Service.Commands.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class UpdatePackagesToInitialStateCommandHandler : IRequestHandler<UpdatePackagesToInitialStateCommand, List<Models.Package>>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public UpdatePackagesToInitialStateCommandHandler(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Models.Package>> Handle(UpdatePackagesToInitialStateCommand request, CancellationToken cancellationToken)
        {
            await repository.UpdatePackagesToInitialStateAsync();

            var packages = await repository.GetAsync();
            return mapper.Map<List<Models.Package>>(packages);
        }
    }
}
