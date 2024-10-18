import { Injectable, Injector } from '@angular/core';
import { StudentDecision } from '../class/model/decision.model';
import { DecisionManager } from './DecisionManager';
import { DecisionService } from './decision/decision.service';

@Injectable()
export class StudentDecisionManager extends DecisionManager {
  private decisionService: DecisionService;
  constructor(injector: Injector) {
    super();
    this.decisionService = injector.get(DecisionService);
  }
  public getClassDecision(): StudentDecision {
    console.log("Student decision");
    const defaultClass = this.decisionService.GetClass();
    return {
      classId: defaultClass.classId,
      groupSerial: defaultClass.groupSerial,
    } as StudentDecision;
  }
}
