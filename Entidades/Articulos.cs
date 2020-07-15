﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public virtual Categorias Categoria { get; set; } = new Categorias();
        public int Stock { get; set; }
        public double Precio { get; set; }
        public double Costo { get; set; }
        [ForeignKey("ArticuloId")]
        public virtual List<ComprasDetalle> ComprasDetalles { get; set; } = new List<ComprasDetalle>();

        [ForeignKey("ArticuloId")]
        public virtual List<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();

    }
}
