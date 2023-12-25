import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { List_Product } from 'src/app/contracts/list_product';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent {

  displayedColumns: string[] = ['Name', 'Stock', 'Price', 'CreatedDate', 'UpdatedDate'];
  dataSource: MatTableDataSource<List_Product> = null;

  constructor() {
    
  }

}
