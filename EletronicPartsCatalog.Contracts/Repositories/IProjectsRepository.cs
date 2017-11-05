using EletronicPartsCatalog.Contracts.DataContracts;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.Repositories
{
    public interface IProjectsRepository
    {
       List<ProjectDto> GetAll();
       ProjectDto GetById(int id);
       void Add(AddProjectDto project);
       ProjectDto GetByName(string projectName);
       void Delete(int id);
    }
}
