using System;
using System.Collections.Generic;
using System.Text;
using EletronicPartsCatalog.Contracts.Common;
using EletronicPartsCatalog.Contracts.DataContracts;

namespace EletronicPartsCatalog.Contracts.Services
{
    public interface IObjectsService
    {
        List<ObjectDto> GetAll();
        CommonResult<ObjectDto> GetById(int id);
        CommonResult Add(AddObjectDto project);
        void Delete(int id);
    }
}
