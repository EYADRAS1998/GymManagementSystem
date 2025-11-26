using AutoMapper;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Plan
            CreateMap<Plan, PlanReadDto>().ReverseMap();
            CreateMap<PlanCreateDto, Plan>();
            CreateMap<PlanUpdateDto, Plan>();

            // Subscription
            CreateMap<Subscription, SubscriptionReadDto>().ReverseMap();
            CreateMap<SubscriptionCreateDto, Subscription>();
            CreateMap<SubscriptionUpdateDto, Subscription>();
        }
    }
}
