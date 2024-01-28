import { Component } from '@angular/core';
import { AuthService } from './services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { Router } from '@angular/router';
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DotnetAngularMiniEcommerce_UI';
  
  constructor(
    public authService: AuthService,
    private toastr: CustomToastrService,
    private router: Router
  ) { 
    this.authService.identityCheck();
  }

  logOff() {
    localStorage.removeItem("accessToken");
    this.authService.identityCheck();
    this.router.navigate([""]);
    this.toastr.message("Your session has been terminated successfully.", "Success", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
  }
}


//$.get("https://localhost:7240/api/Products").done((data) => {
//  console.log(data);
//})