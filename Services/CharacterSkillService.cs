using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.CharacterSkill;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterSkillService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccesor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccesor = httpContextAccesor;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.Characters
                        .Include(c => c.Weapon)
                        .Include(c => c.CharacterSkills)
                        .ThenInclude(cs => cs.Skill)
                        .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId
                                                && c.User.Id == int.Parse(_httpContextAccesor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                                            );

                if (character is null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }

                CharacterSkill characterSkill = new CharacterSkill
                {
                    Character = character,
                    Skill = skill
                };

                await _context.CharacterSkills.AddAsync(characterSkill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}