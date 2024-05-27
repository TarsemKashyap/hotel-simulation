import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RefreshTokennterceptor } from './interceptors/http.interceptor';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { SessionStore } from './store/session.store';
import { AdminModule } from './admin';
import { SharedModule } from './shared/shared.module';
import { APIInterceptor } from './interceptors/api.interceptor';
import { MaterialModule } from './material.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AgGridModule } from 'ag-grid-angular';
import { publicModule } from './public/public.module';
import { StudentModule } from './student';
import { InstructorModule } from './instructor';
import { OverlayInterceptor } from './interceptors/OverlayInterceptor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    publicModule,
    AdminModule,
    StudentModule,
    InstructorModule,
    SharedModule,
    MaterialModule,
    AgGridModule,
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtOptionsFactory,
        deps: [SessionStore],
      },
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: APIInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RefreshTokennterceptor,
      multi: true,
    },
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: OverlayInterceptor,
    //   multi: true,
    // },
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}

export function jwtOptionsFactory(sessionStore: SessionStore) {
  return {
    tokenGetter: () => {
      return sessionStore.GetAccessToken();
    },
    allowedDomains: ['localhost:4200'],
  };
}
