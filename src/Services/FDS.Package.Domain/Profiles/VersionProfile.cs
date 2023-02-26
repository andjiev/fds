namespace FDS.Package.Domain.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;

    public class VersionProfile : Profile, IProfile
    {
        public VersionProfile()
        {
            CreateMap<DbResults.VersionDbResult, Entities.Version>();
        }
    }
}
