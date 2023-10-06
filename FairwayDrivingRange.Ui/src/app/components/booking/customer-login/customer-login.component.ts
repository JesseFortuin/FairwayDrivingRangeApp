import { HttpParams } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IApiResponse } from 'src/app/shared/interfaces/IApiResponse';

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

    this.infoService.login().subscribe((result: IApiResponse) => {
      if (result.isSuccess){
        sessionStorage.setItem('customer',this.customerObj);

        this.router.navigate(['confirmation'])
      }
      if (!result.isSuccess){
        alert(result.errorMessage)
      }
    })
  }
}
