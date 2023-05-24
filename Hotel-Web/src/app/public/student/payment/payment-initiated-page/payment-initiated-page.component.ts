import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { StudentsignupService } from '../../signup/studentsignup.service';
import { StudentPaymentSignUp, StudentSignup } from '../../model/studentSignup.model';
import {
  IPayPalConfig,
  ICreateOrderRequest 
} from 'ngx-paypal';
import { PaymentStatus, PaymentTransaction } from '../../model/PaymentTransaction.model';

@Component({
  selector: 'app-payment-initiated-page',
  templateUrl: './payment-initiated-page.component.html',
  styleUrls: ['./payment-initiated-page.component.css'],

})
export class PaymentInitiatedPageComponent {
  studentId: number | undefined;
  studentPaymentDtls: StudentPaymentSignUp| undefined;
  public payPalConfig ? : IPayPalConfig;
  paypalTransaction :   PaymentTransaction | undefined;
  isPaymentDone : boolean = false;
  isPaymentApprove : boolean = false;
  responseMessage : string = "";
  constructor(
    private _snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router : Router,
    private studentsignupService: StudentsignupService

  ) {
  }

  ngOnInit(): void {
    this.paypalTransaction = new PaymentTransaction;
    this.studentId = this.route.snapshot.params['id'];
    this.loadStudentData();
   
  }

  openSignUp() {
    this.router.navigate(['signup'], {queryParams : {id:  this.studentPaymentDtls?.reference}});
  }

  private initConfig(): void {
    console.log(this.studentPaymentDtls?.totalAmount,"d")
    this.payPalConfig = {
        currency: 'USD',
        clientId: 'ASJKwWf8dvQt2b5iSMB8mSldtpopiH6KHZ9RLYaEwRos9u4vpp1ox0P--bnexBfHc1NOxopRV123BJ2t',
        advanced:{commit:'true'},
       // advanced: {extraQueryParams:[{name:'enable-funding',value:'venmo,paypal'}],commit:'true'},
        //fundingSource: "VENMO",
        createOrderOnClient: (data) => < ICreateOrderRequest > {
            intent: 'CAPTURE',
            purchase_units: [{
                amount: {
                    currency_code: 'USD',
                    value: this.studentPaymentDtls?.totalAmount,
                },
                items: []
            }]
        },
       
        style: {
            label: 'paypal',
            layout: 'vertical',
            fundingicons:true
        },
        onApprove: (data, actions) => {
            console.log('onApprove - transaction was approved, but not authorized', data, actions.order.get());
            // actions.order.get().then(details => {
            //     console.log('onApprove - you can get full order details inside onApprove: ', details);
            // });

        },
        onClientAuthorization: (data) => {
            console.log('onClientAuthorization - you should probably inform your server about completed transaction at this point', data);
            var _amount = data.purchase_units[0].amount.value.toString();
            this.paypalTransaction = new PaymentTransaction;
            this.paypalTransaction.first_name = data.payer.name?.given_name;
            this.paypalTransaction.payer_email = data.payer.email_address;
            this.paypalTransaction.tx = data.id;
            this.paypalTransaction.custom =   this.studentPaymentDtls?.reference;
            if(data.status == "COMPLETED") {
              this.paypalTransaction.payment_status =   PaymentStatus.COMPLETED;
            } else {
              this.paypalTransaction.payment_status =   PaymentStatus.Failed;
            }
            this.paypalTransaction.RawTransactionResponse = JSON.stringify(data);
            this.paypalTransaction.amount =   _amount;
            this.studentsignupService.PaymentTransaction(this.paypalTransaction! ).subscribe((result) => {
              if (result.data.paymnentStatus == true) {
                this.isPaymentDone = true;
                this.isPaymentApprove = true;
                this.responseMessage = "Thank you for your payment. The transaction has been completed successfully. Please use the transaction ID below to register a new account. The transaction ID will be emailed to you as well for your records";
              }
            });
            
        },
        onCancel: (data, actions) => {
            console.log('OnCancel', data, actions);
        },
        onError: err => {
            console.log('OnError', err);
            this.isPaymentApprove = false;
            this.responseMessage = "Your Payment Failed!";
        },
        onClick: (data, actions) => {
            console.log('onClick', data, actions);
            
        }
    };
}

  private loadStudentData() {
    this.studentsignupService.getStudentData(this.studentId!).subscribe({
      next: (data: StudentPaymentSignUp) => {

        this.studentPaymentDtls = data;
        console.log(this.studentPaymentDtls)
        this.initConfig();
      },
      error: (err) => {
        this._snackBar.open(err.error);
      },
    });
  }

  onSubmit(){

  }
}
