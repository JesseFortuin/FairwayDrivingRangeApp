import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerInformationComponent } from './register-customer.component';

describe('CustomerInformationComponent', () => {
  let component: CustomerInformationComponent;
  let fixture: ComponentFixture<CustomerInformationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerInformationComponent]
    });
    fixture = TestBed.createComponent(CustomerInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
