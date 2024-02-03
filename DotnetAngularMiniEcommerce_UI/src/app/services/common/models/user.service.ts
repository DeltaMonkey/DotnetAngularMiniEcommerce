import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from 'src/app/entities/user';
import { Create_User } from 'src/app/contracts/user/create_user';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClientService: HttpClientService) { }

  async create(user: User) :  Promise<Create_User> {
    const observable$ : Observable<Create_User | User> = this.httpClientService.post<Create_User | User>({
      controller: "users",
    }, user);

    const promiseData: Promise<Create_User | User> = firstValueFrom(observable$);
    return await promiseData as Create_User;
  }
}
