import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FileUploadComponent } from 'src/app/services/common/file-upload/file-upload.component';
import { NgxFileDropModule } from 'ngx-file-drop';


@NgModule({
  declarations: [
    FileUploadComponent
  ],
  imports: [
    CommonModule,
    NgxFileDropModule
  ],
  exports: [
    FileUploadComponent
  ]
})
export class FileUploadModule { }
