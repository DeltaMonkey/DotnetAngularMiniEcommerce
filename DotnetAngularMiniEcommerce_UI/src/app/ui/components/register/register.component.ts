import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }
  
  
  frm: FormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      nameSurname: ["", [
        Validators.required, 
        Validators.minLength(3),
        Validators.maxLength(50) 
      ]],
      userName: ["", [
        Validators.required, 
        Validators.minLength(3),
        Validators.maxLength(50) 
      ]],
      email: ["", [
        Validators.required, 
        Validators.email,
        Validators.maxLength(250)
      ]],
      password: ["", [
        Validators.required
      ]],
      passwordRepeat: ["", [
        Validators.required
      ]]
    },
    {
      validators: (group: AbstractControl): ValidationErrors | null  => {
        let pass = group.get("password").value;
        let passRepeat = group.get("passwordRepeat").value;
        return pass === passRepeat ? null : { notsame: true };
      }
    }
    );
  }

  get component() {
    return this.frm.controls;
  }

  submitted: boolean = false;
  onSubmit(data: any) {
    this.submitted = true;
    if (this.frm.invalid)
      return;
  }

}
