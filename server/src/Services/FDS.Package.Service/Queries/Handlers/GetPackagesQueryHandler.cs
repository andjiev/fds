namespace FDS.Package.Service.Queries.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class GetPackagesQueryHandler : IRequestHandler<GetPackagesQuery, List<Models.Package>>
    {
        private readonly IPackageRepository repository;
        private readonly IMapper mapper;

        public GetPackagesQueryHandler(IPackageRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<Models.Package>> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAsync();
            return mapper.Map<List<Models.Package>>(result);
        }
    }
}
