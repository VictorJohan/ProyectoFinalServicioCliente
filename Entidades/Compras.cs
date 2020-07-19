using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public double Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        [ForeignKey("CompraId")]
        public virtual List<ComprasDetalle> ComprasDetalles { get; set; } = new List<ComprasDetalle>();
    }
}
