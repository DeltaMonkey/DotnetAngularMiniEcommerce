import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(private toasterService: CustomToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      debugger;
      switch(error.status) {
        case HttpStatusCode.Unauthorized:
          this.toasterService.message("You don't have been authorized to this request.", "Unauthorized!", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
        break;
        case HttpStatusCode.InternalServerError:
          this.toasterService.message("Server has a business error with his request.", "Server Error!", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
        break;
        case HttpStatusCode.BadRequest:
          this.toasterService.message("Request parameters was wrong!", "Bad Request!", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
        break;
        case HttpStatusCode.NotFound:
          this.toasterService.message("Requested page not found.", "Not Found!", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
        break;
        default:
          this.toasterService.message(error.message, "Error!", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.BottomFullWidth
          });
        break;
      }

      return of(error);
    }));
  }
}
