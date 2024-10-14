import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom, Observable, retry, RetryConfig } from 'rxjs';
import { AccountService, AppRoles } from 'src/app/public/account';
import { ClassService, DefaultClassSession } from 'src/app/shared/class';
import { SessionStore } from 'src/app/store';

@Injectable({
  providedIn: 'root',
})
export class DecisionService {
  constructor(
    private classService: ClassService,
    private accountService: AccountService,
    private store: SessionStore
  ) {}

  GetClass(): DefaultClassSession {
    if (this.accountService.userHasRole(AppRoles.Student)) {
      const defaultClss = this.store.GetDefaultClass();
      if (defaultClss) {
        return defaultClss;
      }
      firstValueFrom(this.classService.getStudentDefaultClass()).then();
      return this.store.GetDefaultClass()!;
    }

    throw new Error('role is not student');
  }

  async GetClassById(classId: string) {
    // instructor class

    const defaultClass = this.store.GetDefaultClass();
    if (!defaultClass) {
      await this.getclass(classId);
    }

    if (defaultClass && defaultClass.classId !== parseInt(classId)) {
      this.store.removeDefaultClass();
      await this.getclass(classId);
    }

    return this.store.GetDefaultClass()!;
  }

  private async getclass(classId: string) {
    const data = await firstValueFrom(
      this.classService.getClass(parseInt(classId))
    );
    this.store.SetDefaultClass(data as DefaultClassSession);
    return data as DefaultClassSession;
  }

  public get IsStudent(): boolean {
    return this.accountService.userHasRole(AppRoles.Student);
  }
}
