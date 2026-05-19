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
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        public StellarContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Camara>().ToTable("Camaras");
            modelBuilder.Entity<Telescopio>().ToTable("Telescopios");
            modelBuilder.Entity<Montura>().ToTable("Monturas");
            modelBuilder.Entity<Ocular>().ToTable("Oculares");
            //Sujeto a cambios
            modelBuilder.Entity<Prestamo>().HasOne(p => p.Telescopio)
            .WithMany()
            .HasForeignKey(p => p.TelescopioId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Montura)
            .WithMany()
            .HasForeignKey(p => p.MonturaId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Camara)
            .WithMany()
            .HasForeignKey(p => p.CamaraId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Ocular)
            .WithMany()
            .HasForeignKey(p => p.OcularId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Socio)
            .WithMany()
            .HasForeignKey(p => p.SocioId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Coordinador)
            .WithMany()
            .HasForeignKey(p => p.CoordinadorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>().OwnsOne(u => u.Email);
            modelBuilder.Entity<Usuario>().OwnsOne(u => u.Contrasenia);


            base.OnModelCreating(modelBuilder);
        }
    }
}
