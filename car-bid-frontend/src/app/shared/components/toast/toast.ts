import {Component, inject} from '@angular/core';
import {ToastService} from '../../../core/services/toast';

@Component({
  selector: 'app-toast',
  imports: [],
  templateUrl: './toast.html',
  styleUrl: './toast.css',
})
export class Toast {
  public toastService = inject(ToastService);

}
