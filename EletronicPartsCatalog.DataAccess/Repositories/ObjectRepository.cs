using System.Collections.Generic;
using System.Linq;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.DataAccess.Repositories
{
    public class ObjectRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ObjectRepository(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public List<ObjectDto> GetAll() {
            return _dbContext.Objects
                .Where(x => x.IsDeleted == false)
                .Select(x => new ObjectDto() {
                    Id = x.Id,
                    Name = x.Name,
                    Parts = x.Parts,
                    Description = x.Description,
                    IsDeleted = x.IsDeleted,
                    CreationDate = x.CreationDate,
                    CreatedBy = x.CreatedBy.UserName
                })
                .ToList();
        }

        public ObjectDto GetById(int id) {
            var obj = _dbContext.Objects.First(x => x.Id == id);

            return new ObjectDto {
                Id = obj.Id,
                Name = obj.Name,
                Parts = obj.Parts,
                Description = obj.Description,
                CreationDate = obj.CreationDate,
                IsDeleted = obj.IsDeleted,
                CreatedBy = obj.CreatedBy.UserName
            };
        }

        //Create method for adding an object

        public ObjectDto GetByName(string objectName) {
            var existingObject = _dbContext.Objects.FirstOrDefault(x => x.Name.ToLower() == objectName.ToLower());

            if (existingObject != null) {
                return new ObjectDto {
                    Id = existingObject.Id,
                    Name = existingObject.Name,
                    Parts = existingObject.Parts,
                    Description = existingObject.Description,
                    CreationDate = existingObject.CreationDate,
                    IsDeleted = existingObject.IsDeleted,
                    CreatedBy = existingObject.CreatedBy.UserName
                };
            }

            return null;
        }

        public void Delete(int id) {
            var obj = _dbContext.Objects.First(x => x.Id == id);
            obj.IsDeleted = true;

            _dbContext.SaveChanges();
        }
    }

}


