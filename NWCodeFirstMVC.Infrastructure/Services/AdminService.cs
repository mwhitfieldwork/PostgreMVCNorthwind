using AutoMapper;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Domain.PocoModels;
using NWCodeFirstMVC.Infrastructure.PgModels;
using NWCodeFirstMVC.Domain;
using NWCodeFirstMVC.Domain.Contracts;
using NWCodeFirstMVC.Domain.PocoModels;
using NWCodeFirstMVC.Domain.Dto;
using NWCodeFirstMVC.Infrastructure.Repositories;

namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly NWCodeFirstMVC.Domain.Contracts.IGenericRepository<NWCodeFirstMVC.Infrastructure.PgModels.User> _repo;
        private readonly IMapper _mapper;

        public AdminService(NWCodeFirstMVC.Domain.Contracts.IGenericRepository<NWCodeFirstMVC.Infrastructure.PgModels.User> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<NWCodeFirstMVC.Domain.PocoModels.User>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            var users = _mapper.Map<List<NWCodeFirstMVC.Domain.PocoModels.User>>(entities);
            return users;
        }
    }
}
