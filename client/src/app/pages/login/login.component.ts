import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { LoginDto } from 'src/app/models/loginDto';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginDto = new LoginDto();

  constructor(private authService: AuthenticationService) {}

  login(loginDto: LoginDto) {
    this.authService.login(loginDto).subscribe({
      next: (response) => {
        const token = response.headers.get('Authorization')?.slice(7);

        if (token) {
          this.authService.setAuthToken(token);
        }

        console.log(token);
      },
      error: (err) => console.log('error logging in', err),
    });
  }
}
