import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../components/confirm-dialog/confirm-dialog.component';
import { ConfirmDialogData } from '../components/confirm-dialog/confirm-dialog.model';

@Injectable({ providedIn: 'root' })
export class ConfirmDialogService {
  constructor(private dialog: MatDialog) {}

  open(
    title: string,
    description: string,
    callback: () => void,
    positiveText = 'Yes',
    negativeText = 'Cancel'
  ) {
    const data: ConfirmDialogData = {
      callback,
      title,
      description,
      positiveText,
      negativeText,
    };

    this.dialog.open(ConfirmDialogComponent, {
      width: '500px',
      data,
    });
  }
}
