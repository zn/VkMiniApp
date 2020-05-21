using ApplicationCore.Entities;
using AutoMapper;
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
                .ForMember(p => p.Attachments, opts => opts.Ignore())
                .ReverseMap();
            CreateMap<UpdatePostViewModel, Post>()
                .ReverseMap();
        }
    }
}
