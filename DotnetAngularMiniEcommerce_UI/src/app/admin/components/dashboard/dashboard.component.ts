import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { AleritfyService, MessageType, Position } from 'src/app/services/admin/aleritfy.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(
    spinner: NgxSpinnerService,
    private alertify: AleritfyService
    ) { 
    super(spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallSpinClockwiseFadeRotating);
  }

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
