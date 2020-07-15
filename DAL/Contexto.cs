using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.DAL
{
    public class Contexto : DbContext
    {

        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Combinaciones> Combinaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= DATA\TeacherControl.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuarios>().HasData(new Usuarios
            {
                UsuarioId = 1,
                Nombres = "Elvis J.",
                Apellidos = "Duarte V.",
                NombreUsuario = "admin",
                Contrasena = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"
            });
        }
    }
}