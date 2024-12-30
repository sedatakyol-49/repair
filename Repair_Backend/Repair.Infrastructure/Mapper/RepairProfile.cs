using AutoMapper;
using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.Mapper
{
    public class RepairProfile:Profile
    {
        public RepairProfile()
        {
            // Entity -> DTO
            CreateMap<RepairModel, RepairDTO>();

            CreateMap<RepairDTO, RepairModel>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());

            CreateMap<RepairDTO, RepairModel>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) 
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                        .ForMember(dest => dest.StatusHistory, opt => opt.MapFrom(src => src.StatusHistory ?? new List<RepairStatus>())); 

        }
    }
}
