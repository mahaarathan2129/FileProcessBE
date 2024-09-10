import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-common-validator',
  templateUrl: './common-validator.component.html',
  styleUrls: ['./common-validator.component.scss']
})
export class CommonValidatorComponent {
  @Input() error: any;
  @Input() showError: any;
}
