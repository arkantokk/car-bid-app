import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '../services/toast';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toastService = inject(ToastService);

  return next(req).pipe(
    catchError((error: HttpErrorResponse) => {
      let errorMessage = 'An unexpected error occurred. Please try again.';
      if (error.status === 401) {
        errorMessage = 'You are not authorized. Please log in.';
      } else if (error.error) {
        if (error.error.detail) {
          errorMessage = error.error.detail;
        }
        else if (error.error.errors) {
          const firstKey = Object.keys(error.error.errors)[0];
          errorMessage = error.error.errors[firstKey][0];
        }
        else if (typeof error.error === 'string') {
          errorMessage = error.error;
        }
      }
      toastService.showToast(errorMessage, 'error');
      return throwError(() => error);
    })
  );
};
