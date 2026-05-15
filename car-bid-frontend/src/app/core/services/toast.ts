import {Injectable, signal} from '@angular/core';

export type ToastType = 'success' | 'error';

export interface Toast {
  id: number;
  message: string;
  type: ToastType;
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  toasts = signal<Toast[]>([]);

  showToast(message: string, type: ToastType): void {
    const id = Date.now();
    const newToast: Toast = { id, message, type };

    this.toasts.update(currentToasts => [...currentToasts, newToast]);
    setTimeout(() => {
      this.toasts.update(currentToasts => currentToasts.filter(toast => toast.id != id));
    }, 3000)
  }
}
