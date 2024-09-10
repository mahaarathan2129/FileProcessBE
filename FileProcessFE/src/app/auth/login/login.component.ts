import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { RecaptchaComponent } from 'ng-recaptcha';
import { ToastrService } from 'ngx-toastr';
import { commonValidator } from 'src/app/@core/common/component/validator/validator';
import { GlobalVariablesService } from 'src/app/services/global-variables.service';
import { GlobalService } from 'src/app/services/global.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: any;
  password: string | undefined;
  showPassword = false;

  public showError: boolean = false;
  constructor(
    private formBuilder: FormBuilder,
    private route: Router,
    private global: GlobalService,
    private toast: ToastrService,
    private login:GlobalVariablesService,
    private http:HttpClient
  ) { }
  ngOnInit(): void {
    RecaptchaComponent.prototype.ngOnDestroy = function () { };

    this.loginForm = this.formBuilder.group({
      userName: ['', commonValidator('User Name')],
      password: ['', commonValidator('Password')],
    });
  }
  get loginForms() {
    return this.loginForm.controls;
  }
  submitForm() {
    this.showError = true;
    if (this.loginForm.invalid) {
      this.toast.error('Form is Invalid!', 'Error');
      return;
    } else {
      var data:any ={
        email : this.loginForms['userName'].value,
        password : this.loginForms['password'].value
      }
      this.http.post(this.login.LOGIN_URL,data).subscribe((res:any)=>{
        console.log("res",res)
        if(res.statusCode==200 && res.data){
          this.global.setLocalStorage('Token',res.data.accessToken)
          this.toast.success('Logged in successfully!');
          this.route.navigate(['/file']);  
        }
        else{
          this.toast.warning('Invalid Login Credentials!')
        }
      }) 
    }
  }
}
