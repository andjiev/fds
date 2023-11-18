namespace FDS.Package.Domain.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;

    public class PackageProfile : Profile, IProfile
    {
        public PackageProfile()
        {
            CreateMap<DbResults.PackageDbResult, Entities.Package>();
        }
    }
}
