using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WS02._1.Models;

namespace WS02._1.Data
{
    public class WS02_1Context : DbContext
    {
        public WS02_1Context (DbContextOptions<WS02_1Context> options)
            : base(options)
        {
        }

        public DbSet<WS02._1.Models.Paciente> Paciente { get; set; } = default!;
    }
}
