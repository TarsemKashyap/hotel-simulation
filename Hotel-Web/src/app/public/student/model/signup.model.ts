export interface Signup extends Person {
  password: string;
}

export interface Person {
  email: string;
  firstName: string;
  lastName: string;
}
export interface LoginModel {
  userId: string;
  password: string;
}

export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
  roles: Array<AppRoles>;
}

export interface InstructorSignup extends Signup {
  institute: string;
}

export interface InstructorUpdate extends Person {
  institute: string;
}

export enum AppRoles {
  Student = 'Student',
  Admin = 'Admin',
  Instructor = 'Instructor',
}
