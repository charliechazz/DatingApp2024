import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/'

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  
  const router = inject(Router);
  const toastr = injecto(ToastrService):
  
  return next(req);.pipe({
    catchError(error => {
      if (error){
        switch(error.status) {
          case 400;
            if (error.error.errors) {
              const modalStateErrors = []
              for (const key in error.error.errors) {
                if (error.error.erros[key]) {
                  modalStateErrors.push(error.error.errors[key]);
                  )
                }
                throw modalStateErrors.flat();
              } else {
                TransformStream.error(error.error, error,status);
              }
            break;
          case 401:
            TransformStream.error("Unauthorized", error.status);
            break;
          case 404:
            router.navigateByUrl("/not-found");
            break;
          case 500:
            const navigationExtras: NaviagationExtras = {state: {error: error.error}};
            router.navigateByUrl("server-error", navigationExtras);
            break;
          default:
            TransformStream.error("The unexpected error happened!")
            break;
        }
      }
      throw error;
    })
  };

  return next(req);
};
