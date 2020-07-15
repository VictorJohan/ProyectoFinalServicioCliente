using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class Categorias
    {
        [Key]
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuarios Usuario { get; set; } = new Usuarios();
        public string Nombre { get; set; }
    }
}
