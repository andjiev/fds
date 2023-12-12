namespace FDS.Package.Service.Queries.Handlers
{
    using AutoMapper;
    using FDS.Package.Domain.Repositories;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models = FDS.Common.Models;

    public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, Models.Settings>
    {
        private readonly ISettingsRepository repository;
        private readonly IMapper mapper;

        public GetSettingsQueryHandler(ISettingsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Models.Settings> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAsync();
            return mapper.Map<Models.Settings>(result);
        }
    }
}
