import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbCardModule, NbMenuModule, NbSearchModule, NbThemeModule, NbTimepickerModule } from '@nebular/theme';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { CommonValidatorModule } from './@core/common/component/validator/common-validator/common-validator.module';
import { CommonInterceptor } from './@core/interceptor/common.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { PagesModule } from './pages/pages.module';
@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    PagesModule,
    AuthModule,
    CommonValidatorModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot(),
    NbMenuModule.forRoot(),
    NbTimepickerModule.forRoot(),
    NgxPaginationModule,
    NbCardModule,
    HttpClientModule,
    NbSearchModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })
  ],
  providers:[{
    provide: HTTP_INTERCEPTORS,
    useClass: CommonInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
