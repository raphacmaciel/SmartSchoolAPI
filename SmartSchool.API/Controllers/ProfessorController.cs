using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ProfessorController(IRepository repo, IMapper mapper) 
        {
            _repo = repo;
            _mapper = mapper;
        }

        //api/professor
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repo.GetAllProfessores();
            var listaProfessores = _mapper.Map<IEnumerable<ProfessorDTO>>(professores);
            return Ok(listaProfessores);
        }

        //api/professor/3
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            var professorDTO = _mapper.Map<ProfessorDTO>(professor);
            return Ok(professorDTO);
        }

        //api/professor
        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDTO professorDTO)
        {
            var professor = _mapper.Map<Professor>(professorDTO);
            _repo.Add(professor);
            if (_repo.SaveChanges())
                return Created($"api/professor/{professorDTO.Id}", _mapper.Map<ProfessorDTO>(professor));
            return BadRequest("Não foi possível cadastrar o aluno.");
        }

        //api/professor/3
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDTO professorDTO)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(professorDTO, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Created($"api/professor/{professorDTO.Id}", _mapper.Map<ProfessorDTO>(professor));
            return BadRequest("Não foi possível alterar professor.");
        }

        //api/professor/3
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDTO professorDTO)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(professorDTO, professor);
            _repo.Update(professor);
            if (_repo.SaveChanges())
                return Created($"api/professor/{professorDTO.Id}", _mapper.Map<ProfessorDTO>(professor));
            return BadRequest("Não foi possível alterar professor.");
        }

        //api/professor/3
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");
            _repo.Delete(professor);
            if (_repo.SaveChanges())
                return Ok("Deletado com sucesso!");
            return BadRequest("Não foi possível excluir o professor");
        }
    }
}
