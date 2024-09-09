using InMemory.SpendSmart.Models;
using Microsoft.EntityFrameworkCore;

namespace InMemory.SpendSmart.Data
{
    public class SpendSmartDbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : base(options)
        {
                
        }
    }
}
