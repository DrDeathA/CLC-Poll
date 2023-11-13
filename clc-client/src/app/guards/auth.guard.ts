import { CanActivateFn, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthenticationService);
  const toastr = inject(ToastrService);
  const router = inject(Router);

  if (authService.user) {
    return true;
  }

  toastr.error("Please Login To Gain Access!");
  router.navigateByUrl('login');
  return false;
};
