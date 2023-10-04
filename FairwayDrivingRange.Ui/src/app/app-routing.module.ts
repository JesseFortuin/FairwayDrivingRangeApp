import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { authGuard } from './components/login/authGuard/authGuard';
import { CustomerInformationComponent } from './components/booking/register-customer/register-customer.component';
import { BookingTableComponent } from './components/booking/booking-table/booking-table.component';
import { CustomerLoginComponent } from './components/booking/customer-login/customer-login.component';

const routes: Routes = [
  {
    path: '',
    component: CustomerInformationComponent
  },
  {
    path: 'registered',
    component: CustomerLoginComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path:'admin',
    component: AdminComponent,
    canActivate: [authGuard]
  },
  {
    path: 'booking',
    component: BookingTableComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
