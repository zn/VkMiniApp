using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UpdateUserInfoViewModel, User>()
                .ForMember(dest => dest.VkontakteId, opts => opts.MapFrom(p => p.Id))
                .ForMember(dest => dest.BirthDate,
                           opts => opts.MapFrom(src => src.BirthDate == null ? null : (DateTime?)DateTime.Parse(src.BirthDate)));
        }
    }
}
