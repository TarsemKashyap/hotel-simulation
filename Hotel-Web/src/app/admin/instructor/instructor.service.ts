import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { InstructorDto } from './instructor.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InstructorService {
  constructor(private httpClient: HttpClient) {}

  instructorList(): Observable<InstructorDto[]> {
    return this.httpClient.get<InstructorDto[]>('account/instructor/list');
  }
}
