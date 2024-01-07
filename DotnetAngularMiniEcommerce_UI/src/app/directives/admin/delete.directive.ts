import { HttpErrorResponse } from '@angular/common/http';
import { Directive, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, Renderer2, ViewContainerRef } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DeleteDialogComponent, DeleteState } from 'src/app/dialogs/delete-dialog/delete-dialog.component';
import { AleritfyService, MessageType } from 'src/app/services/admin/aleritfy.service';
import { DialogService } from 'src/app/services/common/dialog.service';
import { HttpClientService } from 'src/app/services/common/http-client.service';
declare var $: any;

@Directive({
  selector: '[appDelete]'
})                            
export class DeleteDirective implements OnInit {

  @Input() id: string;
  @Input() controller: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  constructor(
    private element: ElementRef,
    private _renderer: Renderer2,
    private httpClientService: HttpClientService,
    private viewContainerRef: ViewContainerRef,
    private alertify: AleritfyService,
    private spinner: NgxSpinnerService,
    private dialogService: DialogService
  ) { }

  ngOnInit(): void {
    const componentRef = this.viewContainerRef.createComponent(MatIcon);
    componentRef.instance.fontIcon = "delete";

    componentRef.location.nativeElement.setAttribute("style", "cursor: pointer;");

    this._renderer.appendChild(this.element.nativeElement, componentRef.location.nativeElement);
  }

  @HostListener("click")
  async onClick() {
    this.dialogService.openDialog(
      {afterClosed: 
        async () => {
        this.spinner.show(SpinnerType.BallClimbingDot);
        const td: HTMLTableElement = this.element.nativeElement;
        this.httpClientService.delete({
          controller: this.controller
        }, this.id).subscribe({
            next: (result) => {
              $(td).parent().animate({
                opacity: 0,
                left: "+=50",
                height: "toogle"
              }, 700, () => {
                this.callback.emit();
                this.alertify.message("Item deleted sucessfully." , {
                  messageType: MessageType.Success
                });
              })
            },
            error: (errorResponse: HttpErrorResponse) => {
              this.alertify.message(`An error occured: ${errorResponse.message}` , {
                messageType: MessageType.Error
              });
              this.spinner.hide(SpinnerType.BallClimbingDot);
            }
        });
      },
      component: DeleteDialogComponent,
      data: DeleteState.Yes,
    })
  }
}
