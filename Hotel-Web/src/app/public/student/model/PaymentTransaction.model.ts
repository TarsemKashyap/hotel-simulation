export class PaymentTransaction {
    PayerID : string | undefined;
    st : string | undefined;
    tx : string | undefined;
    cc: string | undefined;
    amount : string | undefined;
    cm: string | undefined;
    payer_email :string | undefined;
    payer_id : string | undefined;
    payer_status: string | undefined;
    first_name: string | undefined;
    last_name: string | undefined;
    address_name: string | undefined;
    address_street: string | undefined;
    address_city: string | undefined;
    address_state: string | undefined;
    address_country_code: string | undefined;
    residence_country : string | undefined;
    txn_id: string | undefined; 
    custom: string | undefined;
    handling_amount : string | undefined;
    mc_currency :string | undefined;
    mc_fee :string | undefined; 
    mc_gross : string | undefined;
    notify_version : string | undefined;
    payment_date : string| undefined;
    payment_fee : number | undefined;
    payment_gross : string | undefined;
    payment_status : PaymentStatus | undefined;
    payment_type : string | undefined;  
    protection_eligibility : string | undefined;
    quantity : string | undefined;
    receiver_id : string | undefined;
    shipping : string | undefined;
    txn_type : string | undefined;
    verify_sign : string | undefined;
    address_zip : string | undefined;
    RawTransactionResponse : string | undefined;
}

export enum PaymentStatus {
    NotStarted = 0,
    COMPLETED = 1,
    Failed = 2
  }