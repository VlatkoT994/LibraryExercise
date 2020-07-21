using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.BookForReturnDto>().ReverseMap();
            CreateMap<Models.BookForManipulationDto, Entities.Book>().ReverseMap();
        }
    }
}
