import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthentificationService } from 'src/app/services/authentification.service';
import { IResponse } from 'src/assets/IResponse';
import { authGuard } from './authGuard/authGuard';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthentificationService,
              public router: Router) {}

  ngOnInit(): void {
  }

  loginObj: any = {
    adminName: '',
    password: ''
  }

  loginProcess() {
    this.authService.login(this.loginObj).subscribe((result : IResponse) =>{
      if (!result.isSuccess){
        sessionStorage.clear();

        alert(result.errorMessage);
      }

      if (result.isSuccess){
        sessionStorage.setItem('Token', result.value);

        authGuard();

        this.router.navigate(['admin'])
      };
    })
  }
}
