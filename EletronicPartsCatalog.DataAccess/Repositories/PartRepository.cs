using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.DataAccess.Repositories
{
    public class PartRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PartRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public List<PartDto> GetAll() {
            return _dbContext.Parts
                .Where(x => x.IsDeleted == false)
                .Select(x => new PartDto() {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    IsDeleted = x.IsDeleted,
                    CreationDate = x.CreationDate,
                    CreatedBy = x.CreatedBy.UserName
                })
                .ToList();
        }

        public PartDto GetById(int id) {
            var part = _dbContext.Parts.First(x => x.Id == id);

            return new PartDto {
                Id = part.Id,
                Name = part.Name,
                Description = part.Description,
                CreationDate = part.CreationDate,
                IsDeleted = part.IsDeleted,
                CreatedBy = part.CreatedBy.UserName
            };
        }

        //Create method for adding an object

        public PartDto GetByName(string partName) {
            var existingPart = _dbContext.Parts.FirstOrDefault(x => x.Name.ToLower() == partName.ToLower());

            if (existingPart != null) {
                return new PartDto() {
                    Id = existingPart.Id,
                    Name = existingPart.Name,
                    Description = existingPart.Description,
                    CreationDate = existingPart.CreationDate,
                    IsDeleted = existingPart.IsDeleted,
                    CreatedBy = existingPart.CreatedBy.UserName
                };
            }

            return null;
        }

        public void Delete(int id) {
            var part = _dbContext.Parts.First(x => x.Id == id);
            part.IsDeleted = true;

            _dbContext.SaveChanges();
        }
    }
}

