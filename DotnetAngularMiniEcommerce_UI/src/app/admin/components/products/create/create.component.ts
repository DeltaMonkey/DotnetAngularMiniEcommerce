import { Component } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import { AleritfyService, MessageType, Position } from 'src/app/services/admin/aleritfy.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent{
  
  constructor(
    ngxSpinnerService: NgxSpinnerService,
    private productService: ProductService,
    private alertifyService: AleritfyService
    ) 
  { 
      super(ngxSpinnerService);
  }

  create(name: HTMLInputElement, stock: HTMLInputElement, price: HTMLInputElement) {
    this.showSpinner(SpinnerType.BallClimbingDot);
    const create_product: Create_Product = new Create_Product();
    
    create_product.name = name.value;
    create_product.stock = parseInt(stock.value);
    create_product.price = parseFloat(price.value);

    if(!name.value) {
      this.alertifyService.message("Lütfen ürün adını giriniz!", {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
      return;
    }

    if(parseInt(stock.value) < 0) {
      this.alertifyService.message("Lütfen stok bilginisi doğru giriniz!", {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
      return;
    }

    this.productService.create(
      create_product, 
      () => { 
        this.hideSpinner(SpinnerType.BallClimbingDot);
        this.alertifyService.message("Ürün başarıyla eklenmiştir.", {
          dismissOthers: true,
          messageType: MessageType.Success,
          position: Position.TopRight
        });
      },
      (errorMessage) => {
        this.alertifyService.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.TopRight
        });
      }
      );
  }
}
