using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProyectoFinalServicioCliente.Entidades
{
    public class EventosDetalle
    {
        [Key]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; } = DateTime.Now;
        public double Precio { get; set; }
        public int FotografoId { get; set; }
        [ForeignKey("FotografoId")]
        public virtual Fotografos Fotografo { get; set; }
    }
}
