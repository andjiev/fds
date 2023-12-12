namespace FDS.Package.Domain.Profiles
{
    using AutoMapper;
    using FDS.Common.Interfaces;
    using Entities = FDS.Common.Entities;
    using Models = FDS.Common.Models;

    public class SettingsProfile : Profile, IProfile
    {
        public SettingsProfile()
        {
            CreateMap<Entities.Settings, Models.Settings>();
        }
    }
}
