import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { LoginDto } from 'src/app/models/auth/loginDto';
import { RegistrationDto } from 'src/app/models/auth/registrationDto';

import { devEnv } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {

  public isAuthenticated$ = new BehaviorSubject<boolean>(false);
  public username?: string;

  constructor(private http: HttpClient) {}

  setAuthToken(token: string) {
    sessionStorage.setItem('token', token);
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

  logout() {
    this.isAuthenticated$.next(false);
    sessionStorage.removeItem('token');
  }
}
