using AutoMapper;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            //Aluno
            CreateMap<Aluno, AlunoDTO>()
                .ForMember(dest => dest.Nome,
                           opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"))
                .ForMember(dest => dest.Idade,
                           opt => opt.MapFrom(src => src.DataNasc.IdadeAtual()));
            CreateMap<AlunoDTO, Aluno>();
            CreateMap<AlunoRegistrarDTO, Aluno>().ReverseMap();

            //Professor
            CreateMap<Professor, ProfessorDTO>()
                .ForMember(dest => dest.Nome,
                           opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}"));
            CreateMap<ProfessorDTO, Professor>();
            CreateMap<ProfessorRegistrarDTO, Professor>().ReverseMap();
        }
    }
}
