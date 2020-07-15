using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Articulos
    {
        [Key]
        public int ArticuloId { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public double Costo { get; set; }

    }
}
