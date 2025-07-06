import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeDto, EmployeeQueryParameters } from '../../models/employee.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, FormsModule],
  templateUrl: './employees.component.html',
  styleUrl: './employees.component.scss'
})

export class EmployeesComponent implements OnInit {
  employees: EmployeeDto[] = [];
  filters: any = {
    department: '',
    fullName: '',
    birthDateFrom: null,
    birthDateTo: null,
    employmentDateFrom: null,
    employmentDateTo: null,
    salaryMin: null,
    salaryMax: null,
  };
  sortBy = 'fullName';
  sortDesc = false;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    const query: EmployeeQueryParameters = {
      ...this.filters,
      SortBy: this.sortBy,
      SortDescending: this.sortDesc,
      PageNumber: 1,
      PageSize: 10,
    };
    this.employeeService.getAll(query).subscribe(data => {
      this.employees = data;
    });
  }

  sort(column: string) {
    if (this.sortBy === column) {
      this.sortDesc = !this.sortDesc;
    } else {
      this.sortBy = column;
      this.sortDesc = false;
    }
    this.loadEmployees();
  }

  openCreateModal() {
    // TODO: открыть окно создания
  }

  openEditModal(employee: EmployeeDto) {
    // TODO: открыть окно редактирования с данными employee
  }

  openDeleteModal(employee: EmployeeDto) {
    // TODO: окно подтверждения удаления
  }
}
