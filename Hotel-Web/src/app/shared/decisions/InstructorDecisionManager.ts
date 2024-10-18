import { Injectable, Injector } from '@angular/core';
import { StudentDecision } from '../class/model/decision.model';
import { DecisionManager } from './DecisionManager';
import { StudentList } from '../class/model/studentList.model';
import { SessionStore } from 'src/app/store';

@Injectable()
export class InstructorDecisionManager extends DecisionManager {
  decision: StudentDecision;
  constructor(private sessionStore: SessionStore) {
    super();
  }
  public setGridRow(data: StudentList) {
    const stduentDecision = {
      classId: data.classId,
      groupSerial: data.groupSerial,
      studentId: data.id,
    };
    this.sessionStore.SetStudentDecision(stduentDecision);
  }
  public getClassDecision(): StudentDecision {
    return this.sessionStore.getStudentDecision();
  }
}
