using EstadisticaApp.Models;
using EstadisticaApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EstadisticaApp.DataAcces
{
    public class DBContext : DbContext
    {
        public DbSet<UnidadesIngresos> UnidadesIngreso { set; get; }
        public DbSet<UnidadesPresupuesto>  UnidadesPresupuesto { set; get; }
        

        //Parte de la contiguración de Entity
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexion = $"Filename={ConexionDBDevice.Ruta("SIEB.db")}";
            optionsBuilder.UseSqlite(conexion);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<UnidadesIngresos>(entity =>
            {
                //Le decimos que es primaryKey
                entity.HasKey(col => col.IdUnidades);
                entity.Property(col => col.IdUnidades).IsRequired().ValueGeneratedOnAdd();

            });
            modelBuilder.Entity<UnidadesPresupuesto>( entity =>
            {
                entity.HasKey( col => col.Id);
                entity.Property( col => col.Id).IsRequired().ValueGeneratedOnAdd();  
            });

        }

    }
}
