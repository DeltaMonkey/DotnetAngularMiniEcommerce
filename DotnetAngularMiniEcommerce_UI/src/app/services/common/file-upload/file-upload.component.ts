import { Component, Input } from '@angular/core';
import { NgxFileDropEntry } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AleritfyService, MessageType, Position } from '../../admin/aleritfy.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { DialogService } from '../dialog.service';
import { FileUploadDialogComponent, FileUploadDialogState } from 'src/app/dialogs/file-upload-dialog/file-upload-dialog.component';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss']
})
export class FileUploadComponent extends BaseComponent {

  public files: NgxFileDropEntry[] = [];

  @Input() options: Partial<FileUploadOptions>;

  constructor(
    spinner: NgxSpinnerService,
    private httpClientService: HttpClientService,
    private alertify: AleritfyService,
    private toastr: CustomToastrService,
    private dialogService: DialogService
  ) {
    super(spinner)
  }

  public filesSelected(files: NgxFileDropEntry[]) {
    this.files = files;
    const fileData: FormData = new FormData();
    for (const droppedFile of files) {
      const fileEntry = droppedFile.fileEntry as FileSystemFileEntry;
      fileEntry.file((file: File) => {
        fileData.append(file.name, file, droppedFile.relativePath);
      });
    }
    
    this.dialogService.openDialog({ 
      afterClosed: async () => {
        this.showSpinner(SpinnerType.BallSpinClockwiseFadeRotating)
        this.httpClientService.post({
          controller: this.options.controller,
          action: this.options.action,
          queryString: this.options.queryString,
          headers: new HttpHeaders({ "responseType": "blob" }),
        }, fileData).subscribe({
          next: (data) => { 
            const message: string = "Dosyalar başarıyla yüklenmiştir";

            if(this.options.isAdminPage) { 
              this.alertify.message(message, {
                dismissOthers: true,
                messageType: MessageType.Success,
                position: Position.TopRight
              });
            }  
            else { 
              this.toastr.message(message, "Başarılı", {
                messageType: ToastrMessageType.Success,
                position: ToastrPosition.TopRight
              });
            }
          },
          error: (errorResponse: HttpErrorResponse) => { 
            const message: string = "Dosyalar beklenmedik bir hata ile karşlılaşıldı.";

            if(this.options.isAdminPage) { 
              this.alertify.message(message, {
                dismissOthers: true,
                messageType: MessageType.Error,
                position: Position.TopRight
              });
            }  
            else { 
              this.toastr.message(message, "Başarısız", {
                messageType: ToastrMessageType.Error,
                position: ToastrPosition.TopRight
              });
            }
          },
          complete: () => {
            this.hideSpinner(SpinnerType.BallSpinClockwiseFadeRotating)
          }
        })
      },
      component: FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      options: {
        widht: "300px"
      }
    });
  }
}

export class FileUploadOptions {
    controller?: string;
    action?: string;
    queryString?: string;
    explanation?: string;
    accept?: string;
    isAdminPage?: boolean = false;
}
