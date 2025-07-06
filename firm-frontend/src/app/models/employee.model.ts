export interface EmployeeDto {
  department: string;
  fullName: string;
  birthDate: string;
  employmentDate: string;
  salary: number;
}

export interface EmployeeQueryParameters {
  department?: string;
  fullName?: string;
  birthDateFrom?: string;
  birthDateTo?: string;
  employmentDateFrom?: string;
  employmentDateTo?: string;
  salaryMin?: number;
  salaryMax?: number;

  SortBy?: string;
  SortDescending?: boolean;

  PageNumber: number;
  PageSize: number;
}