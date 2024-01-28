import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent{

  constructor(
    spinner: NgxSpinnerService,
    private userService: UserService,
    private toasterService: CustomToastrService
    ) {
      super(spinner);
    }

  async login(userNameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.BallAtom);
    await this.userService.login(userNameOrEmail, password, (tokenResponse) => {
      if(tokenResponse)
      {
        debugger;
        localStorage.setItem("accessToken", tokenResponse.tokenDto.accessToken);

        this.toasterService.message("Logged in successfully", "Welcome", {
          messageType: ToastrMessageType.Success,
            position: ToastrPosition.TopRight
        });
      }else {
        this.toasterService.message("Something is wrong!", "Sorry!", {
          messageType: ToastrMessageType.Error,
            position: ToastrPosition.TopRight
        });
      }

      this.hideSpinner(SpinnerType.BallAtom);
    });
  }

}
