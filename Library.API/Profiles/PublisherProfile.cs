using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<Entities.Publisher, Models.PublisherForReturnDto>();
            CreateMap<Models.PublisherForManipulationDto, Entities.Publisher>().ReverseMap();
        }
    }
}
