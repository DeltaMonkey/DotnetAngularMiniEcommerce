import { Component, Inject, OnInit, Output } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FileUploadOptions } from 'src/app/services/common/file-upload/file-upload.component';
import { ProductService } from 'src/app/services/common/models/product.service';
import { List_Product_Image } from 'src/app/contracts/list_product_image';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { DeleteDialogComponent, DeleteState } from '../delete-dialog/delete-dialog.component';
declare var $:any;

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {

  @Output() fileUploadOptions: Partial<FileUploadOptions> = {
    accept: ".png, .jpg, .jpeg, .gif",
    action: "upload",
    controller: "products",
    explanation: "Ürün resmini seçin veya buraya sürükleyin...",
    isAdminPage: true,
    queryString: `id=${this.data}`,
  }

  images: List_Product_Image[];

  constructor(
    dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageDialogState | string,
    private productService: ProductService,
    private spinner: NgxSpinnerService,
    private dialogService: DialogService
  ) {
    super(dialogRef);
  }
 
  async ngOnInit(): Promise<void> {
    this.spinner.show(SpinnerType.BallAtom);
    this.images = await this.productService.readImages(this.data as string, () => this.spinner.hide(SpinnerType.BallAtom));
  }

  async deleteImage(imageId: string, event: any): Promise<void> {
    this.dialogService.openDialog({
      component: DeleteDialogComponent,
      data: DeleteState.Yes,
      afterClosed: async () => {
        this.spinner.show(SpinnerType.BallAtom);
        await this.productService.deleteImages(this.data as string,imageId, () => this.spinner.hide(SpinnerType.BallAtom));
        let card = $(event.srcElement).parent().parent().parent();
        $(card).fadeOut(500);
      }
    })

  }
}

export enum SelectProductImageDialogState {
  Close
}