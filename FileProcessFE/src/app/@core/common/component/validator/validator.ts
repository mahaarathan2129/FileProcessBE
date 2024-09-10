import { AbstractControl, FormGroup, ValidationErrors } from '@angular/forms';

export function nameValidator(
    controlName: string,
    minlength: number,
    maxlength: number
) {
    return (control: AbstractControl): ValidationErrors | null => {
        let pattern = '^[a-zA-Z]{' + minlength + ',' + maxlength + '}$';
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(new RegExp(pattern))
                ? {
                    error: true,
                    message: `${controlName} must contain only alphabets. Min no is ${minlength} and the Max is ${maxlength}`,
                }
                : null;
    };
}

export function emailValidator(controlName: string) {
    return (control: AbstractControl): ValidationErrors | null => {        
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)
                ? { error: true, message: `${controlName} is invalid` }
                : null;
    };
}

export function passwordValidator(controlName: string) {
    return (control: AbstractControl): ValidationErrors | null => {
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(/^(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{8,20}$/) && controlName != 'Confirm Password'
                ? { error: true, message: `Your ${controlName} must be between 8 - 20 characters and contain an uppercase letter, a lowercase letter, a number, a special character` }
                : null;
    };
}

export function phoneValidator(controlName: string, expectedLength: number) {
    let pattern = '^\\d{' + expectedLength + '}$';
    /* Add an extra backslash before \d if regex is constructed as a string. There is no need for // at the beginning and end of string if using RegExp  */
    return (control: AbstractControl): ValidationErrors | null => {
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(new RegExp(pattern))
                ? {
                    error: true,
                    message: `${controlName} must be numeric with ${expectedLength} digits`,
                }
                : null;
    };
}

export function commonValidator(controlName: string) {    
    return (control: AbstractControl): ValidationErrors | null => {
        let today = new Date();
        let selectedDate = new Date(control.value);
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : null
    };
}

export function numberValidator(
    controlName: string,
    minlength: number,
    maxlength: number
) {
    return (control: AbstractControl): ValidationErrors | null => {
        let pattern = '^[0-9]{' + minlength + ',' + maxlength + '}$';
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(new RegExp(pattern))
                ? {
                    error: true,
                    message: `${controlName} must contain only numbers. Min no is ${minlength} and the Max is ${maxlength}`,
                }
                : null;
    };
}
export function idValidator(
    controlName: string,

) {
    return (control: AbstractControl): ValidationErrors | null => {
        let pattern = '^[0-9]$';
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(new RegExp(pattern))
                ? {
                    error: true,
                    message: `${controlName} must contain only numbers`,
                }
                : null;
    };
}
export function alphaNumericValidator(
    controlName: string,
    minlength: number,
    maxlength: number
) {
    return (control: AbstractControl): ValidationErrors | null => {
        let pattern = '^[a-zA-Z0-9]{' + minlength + ',' + maxlength + '}$';
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(new RegExp(pattern))
                ? {
                    error: true,
                    message: `${controlName} must contain only alphabets and numbers`,
                }
                : null;
    };
}

export function timePickerValidator(controlName: string) {
    return (control: AbstractControl): ValidationErrors | null => {
        return !control.value
            ? { error: true, message: `${controlName} is required` }
            : null
    };
}

// custom validator to check that two fields match
export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];
    if (matchingControl.errors && !matchingControl.errors['mustMatch']) {
      // return if another validator has already found an error on the matchingControl
      return;
    }

    // set error on matchingControl if validation fails
    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ mustMatch: true });
    } else {
      matchingControl.setErrors(null);
    }
  };
}
export function PANValidator(controlName: string) {
    return (control: AbstractControl): ValidationErrors | null => {        
        return !control.value.length
            ? { error: true, message: `${controlName} is required` }
            : !control.value.match(/^[A-Z]{5}[0-9]{4}[A-Z]{1}/)
                ? { error: true, message: `${controlName} format is invalid` }
                : null;
    };
}

export function aadharValidator(controlName: string) {
      let pattern = '^[0-9]{4}[0-9]{4}[0-9]{4}$';
      return (control: AbstractControl): ValidationErrors | null => {   
        var controlValue = (control.value)
        return !controlValue.length
          ? { error: true, message: `${controlName} is required` }
          : !controlValue.match(new RegExp(pattern))
            ? { error: true, message: `${controlName} format is invalid` }
            : null;
      };
    }
