import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfirmDialogData } from './confirm-dialog.model';

@Component({
  templateUrl: 'confirm-dialog.component.html',
})
export class ConfirmDialogComponent implements OnInit {
  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData
  ) {}

  ngOnInit() {}

  onNegativeClick() {
    this.dialogRef.close();
  }

  onPositiveClick() {
    this.data.callback();
    this.dialogRef.close();
  }
}
