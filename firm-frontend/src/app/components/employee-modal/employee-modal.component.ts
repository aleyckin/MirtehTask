import { Component, EventEmitter, Input, Output } from '@angular/core';
import { EmployeeDto } from '../../models/employee.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-employee-modal',
  imports: [FormsModule],
  templateUrl: './employee-modal.component.html',
  styleUrl: './employee-modal.component.scss'
})

export class EmployeeModalComponent {
  @Input() mode: 'create' | 'edit' = 'create';

   @Input() employee?: EmployeeDto | null;

  @Output() saved = new EventEmitter<EmployeeDto>();
  @Output() closed = new EventEmitter<void>();

  tempEmployee: EmployeeDto = {
    id: '',
    department: '',
    fullName: '',
    birthDate: '',
    employmentDate: '',
    salary: 0,
  };

  ngOnChanges() {
     if (this.employee) {
      this.tempEmployee = { ...this.employee };

      if (this.isDate(this.tempEmployee.birthDate)) {
        this.tempEmployee.birthDate = this.formatDate(this.tempEmployee.birthDate);
      }
      if (this.isDate(this.tempEmployee.employmentDate)) {
        this.tempEmployee.employmentDate = this.formatDate(this.tempEmployee.employmentDate);
      }
    } else {
      this.tempEmployee = {
        id: '',
        department: '',
        fullName: '',
        birthDate: this.formatDate(new Date()),
        employmentDate: this.formatDate(new Date()),
        salary: 0,
      };
    }
  }

  private formatDate(date: Date): string {
    const d = new Date(date);
    const month = ('0' + (d.getMonth() + 1)).slice(-2);
    const day = ('0' + d.getDate()).slice(-2);
    const year = d.getFullYear();
    return `${year}-${month}-${day}`;
  }

  private isDate(value: any): value is Date {
    return value instanceof Date && !isNaN(value.getTime());
  }


  save() {
    this.saved.emit(this.tempEmployee);
  }

  close() {
    this.closed.emit();
  }
}
