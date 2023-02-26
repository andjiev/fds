namespace FDS.Package.Service.Queries.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using FDS.Package.Service.Models;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetPackagesQueryHandler : IRequestHandler<GetPackagesQuery, List<Package>>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public GetPackagesQueryHandler(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Package>> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAsync();
            return mapper.Map<List<Package>>(result);
        }
    }
}
