import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if(accountService.currentUser()) {
    return true;
  } else {
    toastr.error("You don't have access right now")
    return false;
  }
};
