import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommonValidatorComponent } from './common-validator.component';


@NgModule({
  declarations: [CommonValidatorComponent],
  imports: [
    CommonModule
  ],
  exports:[CommonValidatorComponent]
})
export class CommonValidatorModule { }
