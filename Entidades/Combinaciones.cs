using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Combinaciones
    {
        [Key]
        public int Id { get; set; }
        public string Combinacion { get; set; }

    }
}