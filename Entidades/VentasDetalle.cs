using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class VentasDetalle
    {
        [Key]
        public int VentaDetalleId { get; set; }
        public int VentaId { get; set; }
        public int ArticuloId { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulos Articulo { get; set; } = new Articulos();
        public int CantidadArticulos { get; set; }
        public int EventoId { get; set; }
        [ForeignKey("EventoId")]
        public virtual Eventos Evento { get; set; } = new Eventos();

        public double Monto { get; set; }
    }
}
