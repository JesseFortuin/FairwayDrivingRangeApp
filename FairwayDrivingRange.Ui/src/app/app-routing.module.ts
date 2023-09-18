import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { AuthGuardServiceService } from './services/auth-guard-service.service';

const routes: Routes = [
  {path: '', component: LoginComponent},
  {
    path:'admin',
    component: AdminComponent,
    canActivate: [AuthGuardServiceService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
