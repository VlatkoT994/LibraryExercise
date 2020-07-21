using AutoMapper;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class LendingProfile : Profile
    {
        public LendingProfile()
        {
            CreateMap<Entities.Lending, LendingForReturnDto>();
            CreateMap<LendingForCreateDto,Entities.Lending>();
            CreateMap<LendingForUpdateDto, Entities.Lending>().ReverseMap();

        }
    }
}
