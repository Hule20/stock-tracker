import { HttpResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginDto } from 'src/app/models/loginDto';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginDto = new LoginDto();

  constructor(private authService: AuthenticationService, private router: Router) {}

  login(loginDto: LoginDto) {
    this.authService.login(loginDto).subscribe({
      next: (response) => {
        const token = response.headers.get('Authorization')?.slice(7);

        if (token) {
          this.authService.setAuthToken(token);
          this.authService.isAuthenticated.next(true);
        }

        this.router.navigate(['/home']);
      },
      error: (err) => console.log('error logging in', err),
    });
  }
}
