import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { authGuard } from './components/login/authGuard/authGuard';
import { RegisterCustomerComponent } from './components/booking/register-customer/register-customer.component';
import { BookingTableComponent } from './components/booking/booking-table/booking-table.component';
import { CustomerLoginComponent } from './components/booking/customer-login/customer-login.component';
import { bookingGuard } from './components/login/authGuard/bookingGuard';
import { OrderConfirmationComponent } from './components/booking/order-confirmation/order-confirmation.component';

const routes: Routes = [
  {
    path: 'customer',
    component: RegisterCustomerComponent,
    canActivate: [bookingGuard]
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
    path: '',
    component: BookingTableComponent
  },
  {
    path: 'confirmation',
    component: OrderConfirmationComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
