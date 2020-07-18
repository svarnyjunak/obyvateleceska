using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SvarnyJunak.CeskeObce.Data.Entities;

namespace SvarnyJunak.CeskeObce.Data.Repositories
{
    public interface IPopulationFrameRepository : IRepository<PopulationFrame>
    {
    }

    public class PopulationFrameRepository : RepositoryBase<PopulationFrame>, IPopulationFrameRepository
    {
        public PopulationFrameRepository(CeskeObceDbContext dbContext) : base(dbContext)
        {
        }

        protected override DbSet<PopulationFrame> GetDbSet()
        {
            return DbContext.PopulationFrames;
        }
    }
}
