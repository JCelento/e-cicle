using System;
using System.Collections.Generic;
using System.Text;

namespace EletronicPartsCatalog.DataAccess.Repositories
{
    public class ObjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ObjectRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

    }
}

