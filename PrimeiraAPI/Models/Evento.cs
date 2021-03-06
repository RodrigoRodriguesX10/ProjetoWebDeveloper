﻿using LiteDB;
using PrimeiraAPI.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeiraAPI.Models
{
    public class Evento
    {
        public Evento()
        {
            Participantes = new List<Aluno>();
        }
        [BsonId]
        public int Codigo { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        [BsonRef]
        [ListSize(1)]
        public IList<Aluno> Participantes { get; set; }
    }
}
