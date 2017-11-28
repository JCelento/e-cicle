using EletronicPartsCatalog.Contracts.DataContracts;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.Repositories
{
    public interface IObjectsRepository
    {
       List<ObjectDto> GetAll();
       ObjectDto GetById(int id);
       void Add(AddObjectDto project);
       ObjectDto GetByName(string projectName);
       void Delete(int id);
    }
}
