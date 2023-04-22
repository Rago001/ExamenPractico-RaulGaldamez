using ExamenPractico_RaulGaldamez.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenPractico_RaulGaldamez {

    public class AppDbContext: DbContext {

        public AppDbContext(DbContextOptions options) : base(options) { 
        
        }

        public DbSet<Survey> Survey { get; set; }
        public DbSet<Field> Field { get; set; }

    }
}
