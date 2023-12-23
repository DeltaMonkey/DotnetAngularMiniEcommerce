import { Component } from '@angular/core';
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'DotnetAngularMiniEcommerce_UI';
  
  constructor() { 

  }
}


//$.get("https://localhost:7240/api/Products").done((data) => {
//  console.log(data);
//})