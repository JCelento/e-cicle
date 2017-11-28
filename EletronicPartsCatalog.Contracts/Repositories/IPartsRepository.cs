using EletronicPartsCatalog.Contracts.DataContracts;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.Repositories
{
    public interface IPartsRepository
    {
       List<PartDto> GetAll();
       PartDto GetById(int id);
       void Add(AddPartDto project);
       PartDto GetByName(string projectName);
       void Delete(int id);
    }
}
