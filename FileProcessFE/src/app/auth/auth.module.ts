import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NbIconModule, NbLayoutModule } from '@nebular/theme';
import { RecaptchaFormsModule, RecaptchaModule } from 'ng-recaptcha';
import { ToastrModule } from 'ngx-toastr';
import { CommonValidatorModule } from '../@core/common/component/validator/common-validator/common-validator.module';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';


const routes: Routes = [
  {
    path: '', component: AuthComponent, children: [
      { path: 'login', component: LoginComponent },
      { path : '' ,  redirectTo : 'login' , pathMatch : 'full'}
    ]
  },
];



@NgModule({
  declarations: [
    LoginComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    CommonValidatorModule,
    FormsModule,
    NbLayoutModule,
    NbIconModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    ToastrModule,
    RecaptchaModule,
    RecaptchaFormsModule,
  ]
})
export class AuthModule { }
