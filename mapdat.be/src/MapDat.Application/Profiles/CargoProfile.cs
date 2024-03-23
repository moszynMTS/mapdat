using AutoMapper;
using MapDat.Application.Models.Cargoes;
using MapDat.Domain.Entities;
using MapDat.Application.Features.Cargoes.Commands.CreateCargo;
using MapDat.Application.Features.Cargoes.Commands.UpdateCargo;
using MapDat.Domain.Enums;
using MapDat.Application.Features.Common;

namespace MapDat.Application.Profiles
{
    public class CargoProfile : Profile
    {
        public CargoProfile()
        {
            CreateMap<CargoEntity, CargoEntity>();

            CreateMap<CargoEntity, SelectlistResult>();

            CreateMap<CreateCargoCommand, CargoEntity>();

            CreateMap<UpdateCargoCommand, CargoEntity>();

            CreateMap<CargoEntity, CargoViewModel>();
        }
    }
}
