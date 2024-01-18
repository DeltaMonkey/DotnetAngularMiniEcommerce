import { ComponentType } from '@angular/cdk/portal';
import { Injectable } from '@angular/core';
import { DialogPosition, MatDialog } from '@angular/material/dialog';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private dialog:MatDialog) { }

  openDialog(dialogParameters: Partial<DialogParameters<any>>): void  {
    const dialogRef = this.dialog.open(dialogParameters.component, {
      data: dialogParameters.data,
      width: dialogParameters.options?.widht,
      height: dialogParameters.options?.height,
      position: dialogParameters.options?.position,
      maxWidth: dialogParameters.options?.maxWidth,
      maxHeight: dialogParameters.options?.maxHeight
    });

    dialogRef.afterClosed().subscribe(async result => {
      if(result == dialogParameters.data) {
        await dialogParameters.afterClosed();
      }
    });
  }
}

export class DialogParameters<DialogComponentType> {
  component: ComponentType<DialogComponentType>;
  afterClosed: () => Promise<void>;
  data: any;
  options: Partial<DialogOptions>;
}

export class DialogOptions {
  widht?: string;
  height?: string;
  maxWidth?: string;
  maxHeight?: string;
  position?: DialogPosition;
}