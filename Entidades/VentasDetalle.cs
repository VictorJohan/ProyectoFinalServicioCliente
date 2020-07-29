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
        public int Id { get; set; }
        public int VentaId { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
        public int ArticuloId { get; set; }

        [ForeignKey("ArticuloId")]
        public Articulos Articulo { get; set; }
    }
}
