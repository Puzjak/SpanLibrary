export interface ConfirmDialogData {
  title: string;
  description: string;
  positiveText: string;
  negativeText: string;
  callback: () => void;
}
