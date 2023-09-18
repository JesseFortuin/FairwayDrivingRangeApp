import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthentificationService } from 'src/app/services/authentification.service';
import { ILoginResponse } from 'src/assets/ILoginResponse';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthentificationService) {}

  ngOnInit(): void {
  }

  loginObj: any = {
    adminName: '',
    password: ''
  }

  loginProcess() {
    this.authService.login(this.loginObj).subscribe((result : ILoginResponse) =>{
      localStorage.setItem('Token', result.errorMessage)
    })
  }
}
