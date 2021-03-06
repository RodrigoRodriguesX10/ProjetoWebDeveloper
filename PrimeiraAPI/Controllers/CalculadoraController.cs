﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PrimeiraAPI.Controllers
{
    [Route("api/[controller]")]
    public class CalculadoraController : Controller
    {
        [HttpPost("somar")]
        public double Somar([FromBody] Dictionary<string, string> valores)
        {
            var numero1 = double.Parse(valores["numero1"]);
            var numero2 = double.Parse(valores["numero2"]);
            return numero1 + numero2;
        }

        [HttpGet("subtrair")]
        public double Subtrair(double numero1, double numero2)
        {
            return numero1 - numero2;
        }

        [HttpPost("multiplicar")]
        public object Multiplicar([FromBody] Dictionary<string, string> valores)
        {
            var numero1 = double.Parse(valores["numero1"]);
            var numero2 = double.Parse(valores["numero2"]);
            return new
            {
                Multiplicando = numero1,
                Multiplicador = numero2,
                Produto = numero1 * numero2
            };
        }
    }
}
