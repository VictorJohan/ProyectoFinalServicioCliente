using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class EventosDetalle
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; } = DateTime.Now;
        public double Subtotal { get; set; }
    }
}
