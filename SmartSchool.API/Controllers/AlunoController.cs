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
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //api/Aluno
        [HttpGet]
        public IActionResult Get()
        {
            var alunos = _repo.GetAllAlunos(true);
            var alunosRetorno = _mapper.Map<IEnumerable<AlunoDTO>>(alunos);
            return Ok(alunosRetorno);
        }

        //api/Aluno/1
        [HttpGet("{Id}")]
        public IActionResult GetById(int Id)
        {
            var aluno = _repo.GetAlunoById(Id, true);
            if (aluno == null) return BadRequest("Aluno não encontrado.");
            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);
            return Ok(alunoDTO);
        }

        //api/Aluno
        [HttpPost()]
        public IActionResult Post(AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _mapper.Map<Aluno>(alunoDTO);
            _repo.Add(aluno);
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            return BadRequest("Não foi possível cadastrar o aluno.");
        }

        //api/Aluno/4
        [HttpPut("{Id}")]
        public IActionResult Put(int id, AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(alunoDTO, aluno);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
                return Created($"/api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            return BadRequest("Aluno não atualizado");
        }

        //api/Aluno/4
        [HttpPatch("{Id}")]
        public IActionResult Patch(int id, AlunoRegistrarDTO alunoDTO)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _mapper.Map(aluno, alunoDTO);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
                return Created($"api/aluno/{alunoDTO.Id}", _mapper.Map<AlunoDTO>(aluno));
            return BadRequest("Não foi possível atualizar o aluno.");
        }

        //api/Aluno/ByName
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = _repo.GetAlunoById(id);
            if (aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if (_repo.SaveChanges())
                return Ok("Aluno deletado.");
            return BadRequest("Não foi possível deletar o aluno.");
        }
    }
}
