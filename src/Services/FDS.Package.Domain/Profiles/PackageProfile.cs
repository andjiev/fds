namespace FDS.Package.Domain.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;

    public class PackageProfile : Profile, IProfile
    {
        public PackageProfile()
        {
            CreateMap<DbResults.PackageDbResult, Entities.Package>()
                .ForMember(x => x.Version, x => x.MapFrom(z => new Entities.Version(z.VersionId, z.VersionName)));
        }
    }
}
