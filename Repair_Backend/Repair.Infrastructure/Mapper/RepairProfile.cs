using AutoMapper;
using Repair.Infrastructure.DTOs.Repair;
using Repair.Infrastructure.DTOs.User;
using Repair.Infrastructure.Models;

namespace Repair.Infrastructure.Mapper
{
    public class RepairProfile : Profile
    {
        public RepairProfile()
        {
            //Get Repair
            CreateMap<RepairModel, RepairDto>();

            //Update Repair
            CreateMap<UpdateRepairDto, RepairModel>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.StatusHistory, opt => opt.Ignore());

            //Create repair
            CreateMap<CreateRepairDto, RepairModel>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                        .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                        .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                        .ForMember(dest => dest.StatusHistory, opt => opt.MapFrom(src => new List<RepairStatus>()));

            //Add User
            CreateMap<UserDto, User>()
                        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                        .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            //Get Users
            CreateMap<User, UserDto>();


        }
    }
}
