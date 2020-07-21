using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Ventas
    {
        [Key]
        public int VentaId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Clientes Cliente { get; set; } = new Clientes();
        public int FotografoId { get; set; }
        [ForeignKey("FotografoId")]
        public Fotografos Fotografo { get; set; } = new Fotografos();
        public DateTime Fecha { get; set; } = DateTime.Now;
        public double Total { get; set; }
        [ForeignKey("VentaId")]
        public virtual List<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();

    }
}
