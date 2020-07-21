using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Entities.Client, Models.ClientForReturnDto>();
            CreateMap<Models.ClientForManipulation, Entities.Client>();
        }
    }
}
