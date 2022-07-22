// ----------------------------------------------
//  Interface and model
// ----------------------------------------------
export interface IDropdownDto {
  id: number;
  value: string;
}

export class DropdownDto implements IDropdownDto {
  public id: number;
  public value: string;

  constructor(id: number, value: string) {
    this.id = id;
    this.value = value;
  }
}
