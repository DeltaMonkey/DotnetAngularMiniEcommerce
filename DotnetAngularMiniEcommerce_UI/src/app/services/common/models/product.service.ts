import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from 'src/app/contracts/create_product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product } from 'src/app/contracts/list_product';
import { Observable, lastValueFrom } from 'rxjs';
import { List_Product_Image } from 'src/app/contracts/list_product_image';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void) {
    this.httpClientService.post<Create_Product>({
      controller: "products"
    }, product)
    .subscribe({
      next: (result) =>  {
        if(successCallBack) successCallBack();
      },
      error: (errorResponse: HttpErrorResponse) => {
        const _error: Array<{key: string, value: Array<string>}> = errorResponse.error;
        let message = "";
        _error.forEach((v, index) => {
          v.value.forEach((_v, _index) => {
            message += `${_v}<br>`;
          });
        });
        if(errorCallBack) errorCallBack(message);
      }
    });
  }

  async read(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{ totalCount: number; products: List_Product[]; }> {
    const observable$ : Observable<{ totalCount: number; products: List_Product[]; }> = this.httpClientService.get<{ totalCount: number; products: List_Product[]; }>({ 
      controller: "products",
      queryString: `page=${page}&size=${size}`
    });
    const promiseData = lastValueFrom(observable$);

    promiseData
    .then(d =>  {
      if(successCallBack) successCallBack();
    })
    .catch((errorResponse: HttpErrorResponse) => {
      if(errorCallBack) errorCallBack(errorResponse.message);
    });

    return await promiseData;
  }

  async delete(id: string) : Promise<void> {
    const observable$: Observable<void> = this.httpClientService.delete<void>({
      controller: "products"
    }, id);

    const promiseData = lastValueFrom(observable$);

    return await promiseData;
  }

  async readImages(id: string, successCallBack?: () => void): Promise<List_Product_Image[]> {
    const observable$: Observable<List_Product_Image[]> = this.httpClientService.get<List_Product_Image[]>({
      action: 'getproductimages',
      controller: 'products',
    }, id)

    const promiseData = lastValueFrom(observable$);
    const images: List_Product_Image[] = await promiseData;

    if(successCallBack) successCallBack();

    return images
  }

  async deleteImages(id: string, imageId: string, successCallBack?: () => void): Promise<void> {
    const observable$: Observable<void> = this.httpClientService.delete({
      action: "deleteproductimage",
      controller: "products",
      queryString: `imageId=${imageId}`
    }, id);

    const promiseData = lastValueFrom(observable$);
    await promiseData;

    if(successCallBack) successCallBack();
  }
}
