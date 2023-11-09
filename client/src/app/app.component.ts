import { ChangeDetectorRef, Component } from '@angular/core';
import { AuthenticationService } from './services/authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'client';

  constructor(
    public authService: AuthenticationService,
    private router: Router,
  ) {}

  logout() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
