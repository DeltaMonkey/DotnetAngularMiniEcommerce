import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, _MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { List_Product } from 'src/app/contracts/list_product';
import { AleritfyService, MessageType } from 'src/app/services/admin/aleritfy.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  displayedColumns: string[] = ['Name', 'Stock', 'Price', 'CreatedDate', 'UpdatedDate', 'Edit', 'Delete'];
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    spinner: NgxSpinnerService,
    private productService: ProductService,
    private alertify: AleritfyService) {
    super(spinner);
  }

  async ngOnInit(): Promise<void> {
    await this.getProducts();
  }

  async getProducts(): Promise<void> {
    this.showSpinner(SpinnerType.BallClimbingDot);
    const all_produts: { totalCount: number; products: List_Product[]; } = await this.productService.read(
      this.paginator ? this.paginator.pageIndex : 0,
      this.paginator ? this.paginator.pageSize : 5,
      () => {
        this.hideSpinner(SpinnerType.BallClimbingDot);
      },
      (errorMessage: string) => {
        this.alertify.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error
        })
      }
    );

    this.paginator.length = all_produts.totalCount;
    this.dataSource = new MatTableDataSource<List_Product>(all_produts.products);
  }

  async pageChanged(): Promise<void> {
    await this.getProducts();
  }

  //delete(id, event: MouseEvent) {
  //  const elem = event.target;
  //  $(elem).parent().parent().fadeOut(2000);
  //}
}
