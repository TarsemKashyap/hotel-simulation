export interface StudentSignup {
    firstName : string;
    lastName : string;
    institute : string;
    classCode: string;
    email: string;
    password: string;
    totalAmount: string;
}

export interface StudentPaymentSignUp extends StudentSignup {
    reference: string;
   
}
