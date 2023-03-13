export interface Signup {
    Email: string;
    FirstName: string;
    LastName: string;
    Password: string;
}

export interface LoginModel {
    userId: string;
    password: string;
}

export interface LoginResponse {
    accessToken: string
    refreshToken: string
    roles: Array<string>
}