import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../../services/employee.service';
import { EmployeeDto, EmployeeQueryParameters, EmployeeCreateOrUpdateDto } from '../../models/employee.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeModalComponent } from '../employee-modal/employee-modal.component';
import { ConfirmDeleteModalComponent } from '../confirm-delete-modal/confirm-delete-modal.component';

@Component({
  selector: 'app-employees',
  imports: [CommonModule, FormsModule, EmployeeModalComponent, ConfirmDeleteModalComponent],
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

  modalVisible = false;
  modalMode: 'create' | 'edit' = 'create';
  selectedEmployee?: EmployeeDto;
  deleteModalVisible = false;

  pageNumber = 1;
  pageSize = 10;
  hasMore = true;

  constructor(private employeeService: EmployeeService) {}

  ngOnInit() {
    this.loadEmployees();
  }

  loadEmployees() {
    const query: EmployeeQueryParameters = {
      ...this.filters,
      SortBy: this.sortBy,
      SortDescending: this.sortDesc,
      PageNumber: this.pageNumber,
      PageSize: this.pageSize,
    };
    this.employeeService.getAll(query).subscribe(data => {
      this.employees = data;
      this.hasMore = data.length === this.pageSize;
    });
  }

  nextPage() {
    if (this.hasMore) {
      this.pageNumber++;
      this.loadEmployees();
    }
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadEmployees();
    }
  }

  filter(){
    this.pageNumber = 1;
    this.loadEmployees();
  }

  sort(column: string) {
    if (this.sortBy === column) {
      this.sortDesc = !this.sortDesc;
    } else {
      this.sortBy = column;
      this.sortDesc = false;
    }
    this.pageNumber = 1;
    this.loadEmployees();
  }

  openCreateModal() {
    this.modalMode = 'create';
    this.selectedEmployee = undefined;
    this.modalVisible = true;
  }

  openEditModal(employee: EmployeeDto) {
    this.modalMode = 'edit';
    this.selectedEmployee = employee;
    this.modalVisible = true;
  }

  openDeleteModal(employee: EmployeeDto) {
    this.selectedEmployee = employee;
    this.deleteModalVisible = true;
  }

  onModalSave(employee: EmployeeDto) {
     const payload: EmployeeCreateOrUpdateDto = {
      department: employee.department,
      fullName: employee.fullName,
      birthDate: employee.birthDate,
      employmentDate: employee.employmentDate,
      salary: employee.salary,
    };
    if (this.modalMode === 'create') {
      this.employeeService.create(payload).subscribe(() => {
        this.loadEmployees();
        this.modalVisible = false;
      });
    } else {
      if (!this.selectedEmployee) return;
      this.employeeService.update(this.selectedEmployee.id, payload).subscribe(() => {
        this.loadEmployees();
        this.modalVisible = false;
      });
    }
  }

  onModalClose() {
    this.modalVisible = false;
  }

  onDeleteConfirm() {
    if (!this.selectedEmployee) return;
    this.employeeService.delete(this.selectedEmployee.id).subscribe(() => {
      this.loadEmployees();
      this.deleteModalVisible = false;
    });
  }

  onDeleteCancel() {
    this.deleteModalVisible = false;
  }

  resetFilters() {
    this.filters = {
      department: '',
      fullName: '',
      birthDateFrom: null,
      birthDateTo: null,
      employmentDateFrom: null,
      employmentDateTo: null,
      salaryMin: null,
      salaryMax: null,
    };
    this.loadEmployees();
  }

  resetSorting() {
    this.sortBy = 'fullName';
    this.sortDesc = false;
    this.loadEmployees();
  }

  resetAll() {
    this.resetFilters();
    this.resetSorting();
  }
}