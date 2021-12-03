using System.Linq;
using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // source -> target
            // we're mapping Character to GetCharacterDto, etc.
            CreateMap<Character, GetCharacterDto>()
                .ForMember(dto => dto.Skills,
                            c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
            CreateMap<AddCharacterDto, Character>();

            // since the GetCharacterDto has a GetWeaponDto property,
            // you need to map Weapon to GetWeaponDto as well
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}