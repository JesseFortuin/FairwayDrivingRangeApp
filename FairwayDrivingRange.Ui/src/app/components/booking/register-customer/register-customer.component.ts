import { HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IApiResponse } from 'src/app/shared/interfaces/IApiResponse';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.css']
})
export class RegisterCustomerComponent {
  constructor(private infoService: CustomerInformationService,
              public router : Router) {}

  customerObj: any = {
    name: '',
    email: '',
    phone: 0
  }

  registerProcess() {
    this.infoService.login().subscribe((result: IApiResponse) =>{
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
