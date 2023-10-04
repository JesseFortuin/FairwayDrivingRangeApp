import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CustomerInformationService } from 'src/app/services/customer-information.service';
import { IResponse } from 'src/assets/IResponse';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.css']
})
export class CustomerInformationComponent {
  constructor(private infoService: CustomerInformationService,
              public router : Router) {}

  customerObj: any = {
    name: '',
    email: '',
    phone: 0
  }

  registerProcess() {
    this.infoService.register(this.customerObj).subscribe((result: IResponse) =>{
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
