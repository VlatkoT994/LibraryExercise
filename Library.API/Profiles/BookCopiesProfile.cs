using AutoMapper;
using Library.API.Entities;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class BookCopiesProfile : Profile
    {
        public BookCopiesProfile()
        {
            CreateMap<BookCopiesForCreationDto, BookCopies>();
            CreateMap<BookCopies, BookCopiesForReturnDto>();
            CreateMap<BookCopiesForUpdateDto, BookCopies>().ReverseMap();

        }
    }
}
