﻿using LiteDB;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Models;
using System.Linq;

namespace PrimeiraAPI.Controllers
{
    public class AlunoController : Controller
    {
        private string ConnectionString;
        public AlunoController(string connString)
        {
            ConnectionString = connString;
        }
        
        public JsonResult Listar()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var cursos = banco.GetCollection<Aluno>().FindAll().ToList();
                return new JsonResult(cursos);
            }
        }

        public JsonResult Criar([FromBody] Aluno c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
                {
                    banco.GetCollection<Aluno>().Insert(c);
                    return new JsonResult(c);
                }
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }
        
        public JsonResult Editar([FromBody] Aluno c)
        {
            if (ModelState.IsValid)
            {
                using (var banco = new LiteDatabase(ConnectionString))
                {
                    banco.GetCollection<Aluno>().Update(c);
                    return new JsonResult(c);
                }
            }
            else
            {
                Response.StatusCode = 422;
                return new JsonResult(ModelState);
            }
        }
        
        public JsonResult Deletar(string id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var curso = banco.GetCollection<Aluno>().FindById(id);
                banco.GetCollection<Aluno>().Delete(id);
                return new JsonResult(curso);
            }
        }

        [HttpGet("api/[controller]/{id:regex(\\d{{6}})}")]
        public JsonResult GetById(string id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var curso = banco.GetCollection<Aluno>().FindById(id);
                return new JsonResult(curso);
            }
        }
        
        public JsonResult Relatorio()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var alunos = banco.GetCollection<Aluno>().FindAll();

                var relatorio = from aluno in alunos
                                where aluno.Idade >= 20
                                orderby aluno.DataNascimento ascending
                                group aluno by aluno.TipoGraduacao.ToString() into alunosGraduacao
                                select new
                                {
                                    TipoGraduacao = alunosGraduacao.Key,
                                    Alunos = alunosGraduacao.ToList()
                                };
                var relatorioMetodo = alunos.Where(aluno => aluno.Idade >= 20)
                                        .OrderBy(aluno => aluno.DataNascimento)
                                        .GroupBy(aluno => aluno.TipoGraduacao.ToString())
                                        .Select(alunosGraduacao => new
                                        {
                                            TipoGraduacao = alunosGraduacao.Key,
                                            Alunos = alunosGraduacao.ToList()
                                        });

                return new JsonResult(relatorio);
            }
        }
    }
}
