import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDto } from 'src/app/models/loginDto';
import { RegistrationDto } from 'src/app/models/registrationDto';

import { devEnv } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  tokenKey: string = '';

  constructor(private http: HttpClient) {}

  setAuthToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getAuthToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  register(registrationDto: RegistrationDto): Observable<RegistrationDto> {
    return this.http.post<RegistrationDto>(
      `${devEnv.be_base_url}auth/register`,
      registrationDto
    );
  }

  login(loginDto: LoginDto): Observable<HttpResponse<LoginDto>> {
    return this.http.post<LoginDto>(
      `${devEnv.be_base_url}auth/login`,
      loginDto,
      { observe: 'response' }
    );
  }
}
