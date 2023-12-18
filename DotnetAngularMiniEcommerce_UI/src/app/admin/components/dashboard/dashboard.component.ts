import { Component } from '@angular/core';
import { AleritfyService, MessageType, Position } from 'src/app/services/admin/aleritfy.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {

  constructor(private alertify: AleritfyService) { }

  m() {
    this.alertify.message("merhaba", { 
      messageType: MessageType.Success,
      delay: 5,
      position: Position.TopRight
    });
  }

  d() {
    this.alertify.dismissAll();
  }
}
