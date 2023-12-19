import { Component } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
declare var $: any

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DotnetAngularMiniEcommerce_UI';

  constructor(private toastrService: CustomToastrService) {
    toastrService.message("merhaba", "engin", { 
      messageType: ToastrMessageType.Success, 
      position: ToastrPosition.TopCenter 
    });
    toastrService.message("merhaba", "engin", { 
      messageType: ToastrMessageType.Error, 
      position: ToastrPosition.TopCenter 
    });
    toastrService.message("merhaba", "engin", { 
      messageType: ToastrMessageType.Warning, 
      position: ToastrPosition.TopCenter 
    });
    toastrService.message("merhaba", "engin");
  }
}