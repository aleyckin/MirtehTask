using AutoMapper;
using Contracts.Dtos;
using Contracts.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Exceptions.AppException;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public EmployeeService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task CreateAsync(EmployeeCreateOrUpdateDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            _dataContext.Employees.Add(employee);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Employee employee = await GetEmployeeOrThrowAsync(id);
            _dataContext.Employees.Remove(employee);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<EmployeeDto>> GetAllAsync(EmployeeQueryParameters query)
        {
            IQueryable<Employee> queryable = _dataContext.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Department))
                queryable = queryable.Where(e => e.Department.Contains(query.Department));

            if (!string.IsNullOrWhiteSpace(query.FullName))
                queryable = queryable.Where(e => e.FullName.Contains(query.FullName));

            if (query.BirthDateFrom.HasValue)
                queryable = queryable.Where(e => e.BirthDate >= query.BirthDateFrom.Value);

            if (query.BirthDateTo.HasValue)
                queryable = queryable.Where(e => e.BirthDate <= query.BirthDateTo.Value);

            if (query.EmploymentDateFrom.HasValue)
                queryable = queryable.Where(e => e.EmploymentDate >= query.EmploymentDateFrom.Value);

            if (query.EmploymentDateTo.HasValue)
                queryable = queryable.Where(e => e.EmploymentDate <= query.EmploymentDateTo.Value);

            if (query.SalaryMin.HasValue)
                queryable = queryable.Where(e => e.Salary >= query.SalaryMin.Value);

            if (query.SalaryMax.HasValue)
                queryable = queryable.Where(e => e.Salary <= query.SalaryMax.Value);

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                queryable = query.SortBy?.ToLower() switch
                {
                    "department" => query.SortDescending
                        ? queryable.OrderByDescending(e => e.Department)
                        : queryable.OrderBy(e => e.Department),
                    "fullname" => query.SortDescending
                        ? queryable.OrderByDescending(e => e.FullName)
                        : queryable.OrderBy(e => e.FullName),
                    "birthdate" => query.SortDescending
                        ? queryable.OrderByDescending(e => e.BirthDate)
                        : queryable.OrderBy(e => e.BirthDate),
                    "employmentdate" => query.SortDescending
                        ? queryable.OrderByDescending(e => e.EmploymentDate)
                        : queryable.OrderBy(e => e.EmploymentDate),
                    "salary" => query.SortDescending
                        ? queryable.OrderByDescending(e => e.Salary)
                        : queryable.OrderBy(e => e.Salary),
                    _ => queryable.OrderBy(e => e.FullName)
                };
            }
            else
            {
                queryable = queryable.OrderBy(e => e.FullName);
            }

            int skip = (query.PageNumber - 1) * query.PageSize;
            queryable = queryable.Skip(skip).Take(query.PageSize);

            return await _mapper.ProjectTo<EmployeeDto>(queryable).ToListAsync();
        }

        public async Task UpdateAsync(Guid id, EmployeeCreateOrUpdateDto employeeDto)
        {
            Employee employee = await GetEmployeeOrThrowAsync(id);
            _mapper.Map(employeeDto, employee);
            await _dataContext.SaveChangesAsync();
        }

        private async Task CheckForExistAsync(Guid id)
        {
            bool exist = await _dataContext
                .Employees
                .AnyAsync(x => x.Id == id);
            if (exist) { throw new EntityWithIdAlreadyExistException(id); }
        }

        private async Task<Employee> GetEmployeeOrThrowAsync(Guid id)
        {
            Employee employee = await _dataContext
                .Employees
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new EntityNotFoundException(id);
            return employee;
        }
    }
}
