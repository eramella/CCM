using AutoMapper;
using CCM.Business.Utilities;
using CCM.Data.Enums;
using CCM.Data.Models;
using CCM.ViewModels;
using System.Linq;

namespace CCM.Services
{
    public class ModelMapping : Profile
    {
        public ModelMapping()
        {
            CreateMap<CCMUser, UserVm>()
                .ForMember(dst => dst.MobilePhone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dst => dst.UploadedImage, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CCMUser, SpeakerVm>();
            CreateMap<CCMUser, TeamMemberVm>();

            CreateMap<Session, SessionVm>()
                .ForMember(dst => dst.Tags, opt => opt.MapFrom(src => src.TagSessions.Select(t => t.Tag.Name)));
            CreateMap<Camp, CampVm>()
                .ForMember(dst => dst.State, opt => opt.MapFrom(src => EnumUtilities.GetEnumDescription(src.State)))
                .ReverseMap().ForMember(dst => dst.State, opt => opt.MapFrom(src => EnumUtilities.EnumFromString<CampState>(src.State)));
            CreateMap<Tag, TagVm>();
            CreateMap<TagSession, TagSessionVm>().ReverseMap();
            CreateMap<AppSettings, AppSettingsVm>()
                .ForMember(dst => dst.NextCamp, opt => opt.MapFrom(src => src.NextCampId))
                .ForMember(dst => dst.Image1Url, opt => opt.ResolveUsing<SettingsImageUrlResolver,byte[]>(src => src.Pic1))
                .ForMember(dst => dst.Image2Url, opt => opt.ResolveUsing<SettingsImageUrlResolver, byte[]>(src => src.Pic2))
                .ForMember(dst => dst.Image3Url, opt => opt.ResolveUsing<SettingsImageUrlResolver, byte[]>(src => src.Pic3))
                .ForMember(dst => dst.Image4Url, opt => opt.ResolveUsing<SettingsImageUrlResolver, byte[]>(src => src.Pic4))
                .ForMember(dst => dst.Image5Url, opt => opt.ResolveUsing<SettingsImageUrlResolver, byte[]>(src => src.Pic5))
                .ReverseMap()
                .ForMember(dst => dst.NextCampId, opt => opt.MapFrom(src => src.NextCamp));

        }
    }    
}
