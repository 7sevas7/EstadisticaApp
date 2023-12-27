using EstadisticaApp.Models;
using EstadisticaApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EstadisticaApp.DataAcces
{
    public class DBContext : DbContext
    {
        public DbSet<UnidadesIngresos> UnidadesIngreso { set; get; }
        public DbSet<UnidadesPresupuesto> UnidadesPresupuesto { set; get; }

        //Prototipi
        private  static DBContext contextt;

        public static DBContext Instancia()
        {
            
                if (contextt == null) {
                    contextt = new DBContext();

                } 
            

            return contextt;
        }
        //Parte de la contiguración de Entity
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            string conexion = $"Filename={ConexionDBDevice.Ruta("SIEB.db")}";
            optionsBuilder.EnableSensitiveDataLogging();
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
