import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { NbDateFnsDateModule } from '@nebular/date-fns';
import { NbActionsModule, NbButtonModule, NbCardModule, NbCheckboxModule, NbDatepickerModule, NbIconModule, NbInputModule, NbLayoutModule, NbMenuModule, NbSelectModule, NbSidebarModule, NbSpinnerModule, NbTimepickerModule } from '@nebular/theme';
import { AgGridModule } from 'ag-grid-angular';
import { RecaptchaFormsModule, RecaptchaModule } from 'ng-recaptcha';
import { NgxEchartsModule } from 'ngx-echarts';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { CommonComponentModule } from '../@core/common/component/common-component.module';
import { CommonValidatorModule } from '../@core/common/component/validator/common-validator/common-validator.module';
import { FileProcessComponent } from './file-process/file-process.component';
import { PagesComponent } from './pages.component';


const routes: Routes = [
  {
    path: '', component: PagesComponent, children: [
      { path: 'file', component: FileProcessComponent },
      { path: '', redirectTo: '/auth/login', pathMatch: 'full' },
    ]
  }
]

const nebularModules = [
  NbSidebarModule,
  NbMenuModule,
  NbLayoutModule,
  NbButtonModule,
  NbCardModule,
  NbActionsModule,
  NbInputModule, NbSelectModule,
  NbDatepickerModule.forRoot(),
  NbTimepickerModule,
  NbDateFnsDateModule, 
  NbIconModule, 
  NbCheckboxModule
];

const coreModules = 
[
  
  CommonModule,
  ReactiveFormsModule,
  FormsModule, 
  RouterModule.forChild(routes)
];

const packageModules = [
  CommonComponentModule,
  CommonValidatorModule,
  NgxPaginationModule,
  NbSpinnerModule,
  ToastrModule,
  NgxEchartsModule,
  RecaptchaModule,
  RecaptchaFormsModule
]

const agGridModules = [AgGridModule]


@NgModule({
  declarations: [PagesComponent, FileProcessComponent],
  imports: [...coreModules, ...agGridModules, ...nebularModules, ...packageModules],
  exports: [RouterModule],
})
export class PagesModule { }
