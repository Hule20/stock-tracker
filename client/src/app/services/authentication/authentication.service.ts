import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegistrationDto } from 'src/app/models/registrationDto';

import { devEnv } from 'src/environments/environment.dev';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private http: HttpClient) {}

  register(registrationDto: RegistrationDto): Observable<RegistrationDto> {
    return this.http.post<RegistrationDto>(
      `${devEnv.be_base_url}auth/register`,
      registrationDto,
    );
  }
}
