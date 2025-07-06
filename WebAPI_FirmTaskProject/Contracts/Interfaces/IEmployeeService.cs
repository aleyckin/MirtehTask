using Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllAsync(EmployeeQueryParameters query);
        Task CreateAsync(EmployeeDto employeeDto);
        Task UpdateAsync(Guid Id, EmployeeDto employeeDto);
        Task DeleteAsync(Guid Id);
    }
}
