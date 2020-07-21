using System;
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
        public Usuarios Usuario { get; set; } = new Usuarios();
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; } = DateTime.Now;
        public decimal Precio { get; set; }
        public virtual List<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
        public bool Disponible { get; internal set; }
    }
}
