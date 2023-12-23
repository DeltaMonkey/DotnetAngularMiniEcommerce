import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(
    spinner: NgxSpinnerService,
    private httpClientService: HttpClientService
    )
  { 
    super(spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallSpinClockwiseFadeRotating);

    //this.httpClientService.get<Create_Product[]>({ 
    //  controller: "products"
    //}).subscribe(data => console.log(data[0].name));

    //this.httpClientService.post({ 
    //  controller: "products"
    //}, {
    //  name: "Kalem",
    //  stock: 100,
    //  price: 15
    //}).subscribe();

    //this.httpClientService.put({ 
    //  controller: "products"
    //}, {
    //  ID: "ba7ba707-e78a-494f-80de-f29fa74b3e1a",
    //  name: "Silgi",
    //  stock: 100,
    //  price: 15
    //}).subscribe();

    //this.httpClientService.delete({ 
    //  controller: "products"
    //}, "ba7ba707-e78a-494f-80de-f29fa74b3e1a")
    //.subscribe(data => console.log(data));
  }
}
