using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<Entities.Library, Models.LibraryForReturnDto>();
            CreateMap<Models.LibraryForManipulation, Entities.Library>().ReverseMap();
        }
    }
}
