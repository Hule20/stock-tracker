import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from './authentication.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const authGuard = () => {
  const authService = inject(AuthenticationService);
  const router = inject(Router);

  return authService.isAuthenticated$.pipe(
    map((authenticated) => {
      if (authenticated) {
        return true;
      }

      router.navigate(['/login']);
      return false;
    })
  );
};
