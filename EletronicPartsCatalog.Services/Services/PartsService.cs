using EletronicPartsCatalog.Contracts.Services;
using System.Collections.Generic;
using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Repositories;
using EletronicPartsCatalog.Contracts.Common;

namespace EletronicPartsCatalog.Services.Services
{
    public class PartsService : IPartsService
    {
        private readonly IPartsRepository _partsRepository;

        public PartsService(IPartsRepository partsRepository) {
            _partsRepository = partsRepository;
        }

        public List<PartDto> GetAll() {
            return _partsRepository.GetAll();
        }

        public CommonResult<PartDto> GetById(int id) {
            var part = _partsRepository.GetById(id);

            if (part == null || part.IsDeleted) {
                return CommonResult<PartDto>.Failure<PartDto>("Problem occured during fetching part with given id.");
            } else {
                return CommonResult<PartDto>.Success<PartDto>(part);
            }
        }

        public CommonResult Add(AddPartDto part) {
            if (string.IsNullOrEmpty(part.Name)) {
                return CommonResult.Failure("Cannot create part without name provided.");
            }

            if (string.IsNullOrEmpty(part.Description)) {
                return CommonResult.Failure("Cannot create part without description provided.");
            }

            var existingProject = _partsRepository.GetByName(part.Name);

            if (existingProject != null && !existingProject.IsDeleted && existingProject.Name == part.Name) {
                return CommonResult.Failure("Part name already exists.");
            }

            _partsRepository.Add(part);

            return CommonResult.Success();
        }
        public void Delete(int id) {
            _partsRepository.Delete(id);
        }
    }
}
