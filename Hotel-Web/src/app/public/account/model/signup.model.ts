export interface Signup {
  email: string;
  firstName: string;
  lastName: string;
  password: string;
}

export interface LoginModel {
  userId: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  roles: Array<string>;
}

export interface InstructorSignup extends Signup {
  institute: string;
}
