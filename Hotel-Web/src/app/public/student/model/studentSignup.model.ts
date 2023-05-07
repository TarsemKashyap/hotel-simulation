export interface StudentSignup {
    firstName : string;
    lastName : string;
    institute : string;
    classCode: string;
    email: string;
    password: string;
}

export interface StudentPaymentSignUp extends StudentSignup {
    reference: string;
}
