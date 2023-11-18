namespace FDS.Package.Service.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;
    using Entities = Domain.Entities;

    public class PackageProfile : Profile, IProfile
    {
        public PackageProfile()
        {
            CreateMap<Entities.Package, Models.Package>()
                .ReverseMap();
        }
    }
}
