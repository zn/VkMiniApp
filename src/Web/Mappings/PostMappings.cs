using ApplicationCore.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Mappings
{
    public class PostMappings : Profile
    {
        public PostMappings()
        {
            CreateMap<CreatePostViewModel, Post>()
                .ForMember(vm => vm.AuthorVkId,
                           action => action.MapFrom(source => source.UserId))
                .ForMember(vm=>vm.
                .ReverseMap();
            CreateMap<UpdatePostViewModel, Post>()
                .ReverseMap();
        }
    }
}
