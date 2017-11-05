using EletronicPartsCatalog.Contracts.Common;
using EletronicPartsCatalog.Contracts.DataContracts;
using System.Collections.Generic;

namespace EletronicPartsCatalog.Contracts.Services
{
    public interface IProjectsService
    {
        List<ProjectDto> GetAll();
        CommonResult<ProjectDto> GetById(int id);
        CommonResult Add(AddProjectDto project);
    }
}