using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public int Id { get; set; }
        public int CompraId { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
        public int ArticuloId { get; set; }

        [ForeignKey("ArticuloId")]
        public virtual Articulos Articulo { get; set; } = new Articulos();


    }
}
