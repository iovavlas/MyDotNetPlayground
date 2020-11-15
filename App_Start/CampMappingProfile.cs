using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.App_Start
{
    public class CampMappingProfile: Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampDto>()
                .ForMember(camp => camp.Venue, option => option.MapFrom(m => m.Location.VenueName))         // Get the Venue from the Camp.Location.VenueName
                .ReverseMap();


            CreateMap<Talk, TalkDto>()
                .ReverseMap()
                .ForMember(talkDto => talkDto.Speaker, option => option.Ignore())                           // Do not update the speaker and the camp when updating the talk
                .ForMember(talkDto => talkDto.Camp, option => option.Ignore());


            CreateMap<Speaker, SpeakerDto>()
                .ReverseMap();
        }
    }
}