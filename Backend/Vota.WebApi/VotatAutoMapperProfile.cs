using AutoMapper;
using System;
using Vota.Domain.UserDetails;
using Vota.WebApi.Models.Auth;
using Vota.WebApi.Models.Settings;

namespace Vota.WebApi
{
    class VotatAutoMapperProfile : Profile
    {
        public VotatAutoMapperProfile()
        {
            CreateAuthMaps();
        }


        private void CreateAuthMaps()
        {
            CreateMap<SignUpRequestViewModel, UserDetail>();

            CreateMap<UserDetail, SignInResponseViewModel>()
                .ForMember(x => x.Email, o => o.MapFrom(x => x.User.Email))
                .ForMember(x => x.UserName, o => o.MapFrom(x => x.User.UserName));
        }
        
        private static DateTime? SpecifyUtc(DateTime? dateTime)
        {
            return dateTime.HasValue ? DateTime.SpecifyKind(dateTime.Value, DateTimeKind.Utc) : (DateTime?)null;
        }
    }
}
