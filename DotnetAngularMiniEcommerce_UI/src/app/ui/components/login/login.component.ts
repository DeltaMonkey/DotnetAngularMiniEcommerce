import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent{

  constructor(
    spinner: NgxSpinnerService,
    private userAuthService: UserAuthService,
    private toasterService: CustomToastrService,
    private authService: AuthService,
    private activatedRoute: ActivatedRoute,
    private router: Router
    ) {
      super(spinner);
    }

  async login(userNameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.BallAtom);
    await this.userAuthService.login(userNameOrEmail, password, (tokenResponse) => {
      if(tokenResponse)
      {
        localStorage.setItem("accessToken", tokenResponse.tokenDto.accessToken);
        localStorage.setItem("refreshToken", tokenResponse.tokenDto.refreshToken);

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

      this.authService.identityCheck();

      this.activatedRoute.queryParams.subscribe(params => {
        const url: string = params["URL"];
        if(url)
          this.router.navigate([url])
      });

      this.hideSpinner(SpinnerType.BallAtom);
    });
  }

}
