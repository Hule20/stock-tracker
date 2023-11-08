import { Component } from '@angular/core';
import { RegistrationDto } from 'src/app/models/registrationDto';
import { AuthenticationService } from 'src/app/services/authentication/authentication.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css'],
})
export class RegistrationComponent {
  registrationDto = new RegistrationDto();

  constructor(private authService: AuthenticationService) {}

  public register(registrationDto: RegistrationDto) {
    this.authService.register(registrationDto).subscribe({
      next: (val) => console.log(val.username + ' succesfully registered'),
      error: (err) => console.log('HTTP error: ' + err.error),
    });
  }
}
