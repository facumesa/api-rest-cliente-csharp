using Microsoft.EntityFrameworkCore;
using Negocio.Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccesoDatos.EF
{
    public class StellarContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Coordinador> Coordinadores { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Camara> Camaras { get; set; }
        public DbSet<Telescopio> Telescopios { get; set; }
        public DbSet<Montura> Monturas { get; set; }
        public DbSet<Ocular> Oculares { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=ObligatorioDW_2026; Trusted_connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Camara>().ToTable("Camaras");
            modelBuilder.Entity<Telescopio>().ToTable("Telescopios");
            modelBuilder.Entity<Montura>().ToTable("Monturas");
            modelBuilder.Entity<Ocular>().ToTable("Oculares");

            modelBuilder.Entity<Usuario>().OwnsOne(u => u.Email);
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.Contrasenia);


            base.OnModelCreating(modelBuilder);
        }
    }
}
