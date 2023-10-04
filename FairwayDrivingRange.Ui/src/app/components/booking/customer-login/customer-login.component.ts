import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IResponse } from 'src/assets/IResponse';

@Component({
  selector: 'app-customer-login',
  templateUrl: './customer-login.component.html',
  styleUrls: ['./customer-login.component.css']
})
export class CustomerLoginComponent {
  customerObj: any = {
    name: '',
    email: ''
  }

  constructor(private infoService: CustomerInformationService,
    public router : Router) {}

  loginProcess() {
    this.infoService.login(this.customerObj.email).subscribe((result: IResponse) => {
      if (result.isSuccess){
        sessionStorage.setItem('customer',this.customerObj),
        this.router.navigate(['booking']);
      }
      if (!result.isSuccess){
        alert(result.errorMessage)
      }
    })
  }
}
