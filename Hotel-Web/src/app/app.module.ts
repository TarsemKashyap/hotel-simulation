import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APIInterceptor } from './interceptors/http.interceptor';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { JwtModule, JWT_OPTIONS } from "@auth0/angular-jwt";
import { SessionStore } from './store/session.store';
import { AdminModule } from './admin';
import { publicModule } from './public/public.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    publicModule,
    MatSnackBarModule,
    AdminModule,
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: (sessionStore: SessionStore) => {
          tokenGetter: sessionStore.GetAccessToken()
          allowedDomains: ["localhost.*"]
        },
        deps: [SessionStore]
      }
    })
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: APIInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent],
})
export class AppModule { }
