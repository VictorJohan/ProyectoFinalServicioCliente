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
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        
        public double Total { get; set; }

        [ForeignKey("ClienteId")]
        public virtual List<EventosDetalle> EventosDetalles { get; set; } = new List<EventosDetalle>();
        
    }
}
