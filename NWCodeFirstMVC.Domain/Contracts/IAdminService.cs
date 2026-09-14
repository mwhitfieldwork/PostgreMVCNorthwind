using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NWCodeFirstMVC.Domain.PocoModels;

namespace NWCodeFirstMVC.Domain.Contracts
{
    public interface IAdminService
    {
        Task<List<User>> GetAllAsync();
    }
}
