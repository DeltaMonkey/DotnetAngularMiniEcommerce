import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { TokenResponse } from 'src/app/contracts/token/tokenResponse';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private httpClientService: HttpClientService) { }

  async login(userNameOrEmail: string, password: string, callBackFunction: (token: TokenResponse) => void ) : Promise<void> {
    const observable$ : Observable<any | TokenResponse> = this.httpClientService.post<any | TokenResponse>({
      controller: 'auth',
      action: 'login'
    }, {
      UserNameOrEmail: userNameOrEmail,
      Password: password
    });

    try
    {
    const promiseData: Promise<TokenResponse> = await firstValueFrom(observable$);
    const tokenResponse: TokenResponse = await promiseData as TokenResponse;
    callBackFunction(tokenResponse);
    }
    catch {
      callBackFunction(null);
    }

  }
}
