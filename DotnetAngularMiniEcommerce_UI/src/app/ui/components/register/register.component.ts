import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { Create_User } from 'src/app/contracts/user/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent implements OnInit {

  constructor(
    spinner: NgxSpinnerService,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private customToastrService: CustomToastrService
    ) { 
      super(spinner);
    }
  
  
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
  async onSubmit(user: User) {
    this.submitted = true;
    if (this.frm.invalid)
      return;

    const result: Create_User = await this.userService.create(user);
    
    if(result.succeeded)
    {
      this.customToastrService.message(result.message, "Başarılı", {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
    }
    else
    {
      this.customToastrService.message(result.message, "Hata", {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    }
  }

}
