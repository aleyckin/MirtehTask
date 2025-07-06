using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Dtos
{
    public record EmployeeDto(Guid Id, string Department, string FullName, DateTime BirthDate, DateTime EmploymentDate, decimal Salary);
    public record EmployeeCreateOrUpdateDto(string Department, string FullName, DateTime BirthDate, DateTime EmploymentDate, decimal Salary);
    public record EmployeeQueryParameters(
        string? Department,
        string? FullName,
        DateTime? BirthDateFrom,
        DateTime? BirthDateTo,
        DateTime? EmploymentDateFrom,
        DateTime? EmploymentDateTo,
        decimal? SalaryMin,
        decimal? SalaryMax,

        string? SortBy,
        bool SortDescending,

        int PageNumber,
        int PageSize);
}
