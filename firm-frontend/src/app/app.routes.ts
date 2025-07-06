import { AboutComponent } from './components/about/about.component';
import { EmployeesComponent } from './components/employees/employees.component';

export const routes = [
  { path: '', component: AboutComponent },
  { path: 'employees', component: EmployeesComponent },
];