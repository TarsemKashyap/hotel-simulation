import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {
  RefreshTokennterceptor,
} from './interceptors/http.interceptor';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { JwtInterceptor, JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { SessionStore } from './store/session.store';
import { AdminModule } from './admin';
import { publicModule } from './public/public.module';
import { AuthGuard } from './shared';
import { SharedModule } from './shared/shared.module';
import { APIInterceptor } from './interceptors/api.Interceptor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    publicModule,
    MatSnackBarModule,
    AdminModule,
    SharedModule,
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
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function jwtOptionsFactory(sessionStore: SessionStore) {
  return {
    tokenGetter: () => {
      console.log('jwtOptionsFactory');
      return sessionStore.GetAccessToken();
    },
    allowedDomains: ['localhost:4200'],
  };
}
