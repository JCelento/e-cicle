using System.Collections.Generic;
using EletronicPartsCatalog.Contracts.Common;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.Contracts.Services
{
    public interface IPartsService
    {
        List<PartDto> GetAll();
        CommonResult<PartDto> GetById(int id);
        CommonResult Add(AddPartDto project);
        CommonResult<PartDto> GetByName(string name);
        void Delete(int id);
    }
}
