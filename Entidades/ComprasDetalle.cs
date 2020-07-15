using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class ComprasDetalle
    {
        [Key]
        public int CompraDetalleId { get; set; }
        public int CompraId { get; set; }
        public int ArticuloId { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual Articulos Articulo { get; set; }//Conecta con la entidad Articulos para evitar la redundancia
        public int CantidadArticulos { get; set; }
        public double Costo { get; set; }

        public ComprasDetalle(int compraId, int articuloId, int cantidadArticulos, double costo)
        {
            CompraDetalleId = 0;
            CompraId = compraId;
            ArticuloId = articuloId;
            Articulo = new Articulos();
            CantidadArticulos = cantidadArticulos;
            Costo = costo;
        }
    }
}
