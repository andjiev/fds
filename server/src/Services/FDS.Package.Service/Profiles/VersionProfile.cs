namespace FDS.Package.Service.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;
    using Entities = Domain.Entities;

    public class VersionProfile : Profile, IProfile
    {
        public VersionProfile()
        {
            CreateMap<Entities.Version, Models.Version>();
        }
    }
}
