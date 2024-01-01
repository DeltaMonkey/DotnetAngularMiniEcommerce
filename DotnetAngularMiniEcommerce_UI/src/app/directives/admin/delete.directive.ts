import { Directive, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, Renderer2, ViewContainerRef } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { AleritfyService, MessageType } from 'src/app/services/admin/aleritfy.service';
import { ProductService } from 'src/app/services/common/models/product.service';
declare var $: any;

@Directive({
  selector: '[appDelete]'
})                            
export class DeleteDirective implements OnInit {

  @Input() id: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  constructor(
    private element: ElementRef,
    private _renderer: Renderer2,
    private productService: ProductService,
    private viewContainerRef: ViewContainerRef,
    private alertify: AleritfyService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    const componentRef = this.viewContainerRef.createComponent(MatIcon);
    componentRef.instance.fontIcon = "delete";

    componentRef.location.nativeElement.setAttribute("style", "cursor: pointer;");

    this._renderer.appendChild(this.element.nativeElement, componentRef.location.nativeElement);
  }

  @HostListener("click")
  async onClick() {
    this.spinner.show(SpinnerType.BallClimbingDot);
    const td: HTMLTableElement = this.element.nativeElement;
    this.productService.delete(this.id).then(
      () => {
        $(td).parent().fadeOut(2000, () => {
          this.callback.emit();
        });
      },
    ).catch((reason: any) => {
        this.alertify.message(`An error occured: ${reason}` , {
          messageType: MessageType.Error
        });
        this.spinner.hide(SpinnerType.BallClimbingDot);
      }
    );
  }
}