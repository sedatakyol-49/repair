using AutoMapper;
using Repair.Infrastructure.DTOs;
using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.Mapper
{
    public class RepairProfile:Profile
    {
        public RepairProfile()
        {
            //GetRepair
            CreateMap<RepairModel, RepairDto>();

            //Update Repair
            CreateMap<UpdateRepairDto, RepairModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());

            //Create repair
            CreateMap<CreateRepairDto, RepairModel>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) 
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                        .ForMember(dest => dest.StatusHistory, opt => opt.MapFrom(src =>new List<RepairStatus>())); 

        }
    }
}
