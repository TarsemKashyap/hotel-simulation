import { Injectable, InjectionToken } from '@angular/core';
import { StudentDecision } from '../class/model/decision.model';

@Injectable()
export abstract class DecisionManager {
  public abstract getClassDecision(): StudentDecision;
}
