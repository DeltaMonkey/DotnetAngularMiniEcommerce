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

  async refreshTokenLogin(refreshToken: string, callBackFunction?: () => void ): Promise<any> {
    const observable$: Observable<any | TokenResponse> = this.httpClientService.post({
      controller: 'auth',
      action: 'RefreshTokenLogin'
    }, {
      RefreshToken: refreshToken
    });

    const tokenResponse: TokenResponse = await firstValueFrom(observable$) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.tokenDto.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.tokenDto.refreshToken);
    }

    if(callBackFunction) callBackFunction();
  }
}
