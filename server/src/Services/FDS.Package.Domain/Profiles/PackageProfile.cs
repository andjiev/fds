namespace FDS.Package.Domain.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;
    using Entities = FDS.Common.Entities;

    public class PackageProfile : Profile, IProfile
    {
        public PackageProfile()
        {
            CreateMap<DbResults.PackageDbResult, Entities.Package>();
        }
    }
}
