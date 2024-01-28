import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { AuthService, _isAuthenticated } from 'src/app/services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

export const AuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  
  const router = inject(Router);
  const spinner = inject(NgxSpinnerService);
  const toaster = inject(CustomToastrService);

  spinner.show(SpinnerType.BallClimbingDot);

  if (!_isAuthenticated) {
    router.navigate(["login"], { queryParams: { URL: state.url } });  
    toaster.message("Unauthorized access!", "Warning", {
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopRight
    })
  } 

  spinner.hide(SpinnerType.BallClimbingDot);

  return true;
};
