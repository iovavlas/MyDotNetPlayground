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
            CreateMap<Camp, CampDto>();
            CreateMap<CampDto, Camp>();
        }
    }
}