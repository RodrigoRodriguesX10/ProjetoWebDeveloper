﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeiraAPI.Services
{
    public class CrudService<T> where T: class
    {
        public string ConnectionString { get; set; }
        public List<Erro> Mensagens { get; set; }
        public CrudService(string connString)
        {
            ConnectionString = connString;
            Mensagens = new List<Erro>();
        }
        public virtual List<T> List()
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var cursos = banco.GetCollection<T>().FindAll().ToList();
                return cursos;
            }
        }
        public virtual T Criar(T model)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                try
                {
                    banco.GetCollection<T>().Insert(model);
                    return model;
                }
                catch (Exception ex)
                {
                    Mensagens.Add(new Erro
                    {
                        Comando = "Insert",
                        Mensagem = ex.Message
                    });
                    return null;
                }
            }
        }
        public virtual T Editar(T model)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                try
                {
                    banco.GetCollection<T>().Update(model);
                    return model;
                }
                catch (Exception ex)
                {
                    Mensagens.Add(new Erro
                    {
                        Comando = "Update",
                        Mensagem = ex.Message
                    });
                    return null;
                }
            }
        }

        public virtual T Excluir(BsonValue id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var registro = banco.GetCollection<T>().FindById(id);
                banco.GetCollection<T>().Delete(id);
                return registro;
            }
        }

        public virtual T Retorna(BsonValue id)
        {
            using (var banco = new LiteDatabase(ConnectionString))
            {
                var registro = banco.GetCollection<T>().FindById(id);
                return registro;
            }
        }
    }
    public class Erro
    {
        public string Comando { get; set; }
        public string Mensagem { get; set; }
    }
}
