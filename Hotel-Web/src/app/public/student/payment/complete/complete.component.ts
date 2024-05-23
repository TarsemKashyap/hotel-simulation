import { Component } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { PaymentTransaction } from '../../model/PaymentTransaction.model';
import { StudentsignupService } from '../../signup/studentsignup.service';

@Component({
  selector: 'app-complete',
  templateUrl: './complete.component.html',
  styleUrls: ['./complete.component.css']
})
export class CompleteComponent {
  queryParams: any;
  paymentTrasa :   PaymentTransaction | undefined;
  isPaymentDone : boolean = false;
  responseMessage : string = "";
  constructor(
    private route: ActivatedRoute,
    private studentsignupService: StudentsignupService,
    private router : Router,
  ) {
  }

  ngOnInit() {
    this.paymentTrasa = new PaymentTransaction;
    this.route.queryParams.subscribe(params => {
        if(this.paymentTrasa)
        {
          this.paymentTrasa.PayerID = params['PayerID'];
          this.paymentTrasa.address_city = params['address_city'];
          this.paymentTrasa.address_country_code = params['address_country_code'];
          this.paymentTrasa.address_name = params['address_name'];
          this.paymentTrasa.address_state = params['address_state'];
          this.paymentTrasa.address_state = params['address_street'];
          this.paymentTrasa.address_zip = params['address_zip'];
         // this.paymentTrasa.amount = parseInt(params.amt);
          this.paymentTrasa.cc = params['cc'];
          this.paymentTrasa.first_name = params['first_name'];
          this.paymentTrasa.handling_amount = params['handling_amount'];
          this.paymentTrasa.last_name = params['last_name'];
          this.paymentTrasa.mc_currency = params['mc_currency'];
          this.paymentTrasa.mc_fee = params['mc_fee'];
          this.paymentTrasa.mc_gross = params['mc_gross'];
          this.paymentTrasa.notify_version = params['notify_version'];
          this.paymentTrasa.payer_email = params['payer_email'];
          this.paymentTrasa.payer_id = params['payer_id'];
          this.paymentTrasa.payer_status = params['payer_status'];
          this.paymentTrasa.payment_date = params['payment_date'];
          this.paymentTrasa.payment_fee = params['payment_fee'];
          this.paymentTrasa.PayerID = params['payment_gross'];
          this.paymentTrasa.payment_status = params['payment_status'];
          this.paymentTrasa.payment_type = params['payment_type'];
          this.paymentTrasa.protection_eligibility = params['protection_eligibility'];
          this.paymentTrasa.quantity = params['quantity'];
          this.paymentTrasa.receiver_id = params['receiver_id'];
          this.paymentTrasa.residence_country = params['residence_country'];
          this.paymentTrasa.shipping = params['shipping'];
          this.paymentTrasa.st = params['st'];
          this.paymentTrasa.tx = params['tx'];
          this.paymentTrasa.txn_id = params['txn_id'];
          this.paymentTrasa.txn_type = params['txn_type'];
          this.paymentTrasa.verify_sign = params['verify_sign'];
          this.paymentTrasa.custom = params['custom'];
        }
        this.paymentTransaction();
      });
  }
  
  openSignUp() {
   
    this.router.navigate(['signup'], {queryParams : {id: this.paymentTrasa?.custom}});
  }

  paymentTransaction() {
   
    this.studentsignupService.PaymentTransaction(this.paymentTrasa! ).subscribe((result) => {
      this.responseMessage = result.message;
      this.isPaymentDone = result.data.paymnentStatus; 
    });
  }
 
}
