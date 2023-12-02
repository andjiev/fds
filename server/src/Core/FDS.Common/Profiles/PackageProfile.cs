namespace FDS.Common.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;
    using Entities = FDS.Common.Entities;
    using Models = FDS.Common.Models;

    public class PackageProfile : Profile, IProfile
    {
        public PackageProfile()
        {
            CreateMap<Entities.Package, Models.Package>()
                .ReverseMap();
        }
    }
}
