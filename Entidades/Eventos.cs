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
        [ForeignKey("ClienteId")]
        public virtual Clientes Cliente { get; set; } = new Clientes();
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuarios Usuario { get; set; } = new Usuarios();
        
        public int FotografoId { get; set; }
        [ForeignKey("FotografoId")]
        public virtual Fotografos Fotografo { get; set; } = new Fotografos();
        public double Total { get; set; }

        [ForeignKey("ClienteId")]
        public virtual List<EventosDetalle> EventosDetalles { get; set; } = new List<EventosDetalle>();
        
    }
}
