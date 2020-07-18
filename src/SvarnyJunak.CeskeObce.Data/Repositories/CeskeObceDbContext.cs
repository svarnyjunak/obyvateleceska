using System;
using System.Collections.Generic;
using System.Text;
using FileContextCore;
using FileContextCore.FileManager;
using FileContextCore.Serializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public class CeskeObceDbContext : DbContext
    {
        public CeskeObceDbContext(DbContextOptions<CeskeObceDbContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; } = null!;

        public DbSet<PopulationFrame> PopulationFrames { get; set; } = null!;
    }
}
