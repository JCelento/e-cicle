using EletronicPartsCatalog.Contracts.Services;
using System.Collections.Generic;
using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Repositories;
using EletronicPartsCatalog.Contracts.Common;

namespace EletronicPartsCatalog.Services.Services
{
    public class ObjectsService : IObjectsService
    {
        private readonly IObjectsRepository _objectsRepository;

        public ObjectsService(IObjectsRepository objectsRepository) {
            _objectsRepository = objectsRepository;
        }

        public List<ObjectDto> GetAll() {
            return _objectsRepository.GetAll();
        }

        public CommonResult<ObjectDto> GetById(int id) {
            var obj = _objectsRepository.GetById(id);

            if (obj == null || obj.IsDeleted) {
                return CommonResult<ObjectDto>.Failure<ObjectDto>("Problem occured during fetching object with given id.");
            } else {
                return CommonResult<ObjectDto>.Success<ObjectDto>(obj);
            }
        }

        public CommonResult Add(AddObjectDto obj) {
            if (string.IsNullOrEmpty(obj.Name)) {
                return CommonResult.Failure("Cannot create project without name provided.");
            }

            if (string.IsNullOrEmpty(obj.Description)) {
                return CommonResult.Failure("Cannot create project without description provided.");
            }

            var existingObject = _objectsRepository.GetByName(obj.Name);

            if (existingObject != null && !existingObject.IsDeleted && existingObject.Name == obj.Name) {
                return CommonResult.Failure("Object name already exists.");
            }

            _objectsRepository.Add(obj);

            return CommonResult.Success();
        }
        public void Delete(int id) {
            _objectsRepository.Delete(id);
        }
    }
}
