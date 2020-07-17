﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Eventos
    {
        [Key]
        public int EnventoId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = DateTime.Now;
        public double Precio { get; set; }
        public virtual List<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
    }
}