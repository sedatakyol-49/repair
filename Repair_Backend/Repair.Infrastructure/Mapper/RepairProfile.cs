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
        }
    }
}
