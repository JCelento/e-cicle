using System;
using System.Collections.Generic;
using System.Linq;
using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Repositories;
using EletronicPartsCatalog.DataAccess.Models;

namespace EletronicPartsCatalog.DataAccess.Repositories
{
    public class PartsRepository : IPartsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PartsRepository(ApplicationDbContext dbContext) {
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
                    PartObjects = x.PartObjects,
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
                PartObjects = part.PartObjects,
                IsDeleted = part.IsDeleted,
                CreatedBy = part.CreatedBy.UserName
            };
        }

        public void Add(AddPartDto part) {
            _dbContext.Parts.Add(new Part {
                Name = part.Name,
                Description = part.Description,
                IsDeleted = false,
                PartObjects = part.PartObjects,
                CreationDate = DateTime.Now
            });

            _dbContext.SaveChanges();
        }

        public PartDto GetByName(string partName) {
            var existingPart = _dbContext.Parts.FirstOrDefault(x => x.Name.ToLower() == partName.ToLower());

            if (existingPart != null) {
                return new PartDto() {
                    Id = existingPart.Id,
                    Name = existingPart.Name,
                    Description = existingPart.Description,
                    CreationDate = existingPart.CreationDate,
                    PartObjects = existingPart.PartObjects,
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

