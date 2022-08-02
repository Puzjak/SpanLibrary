// ----------------------------------------------
//  Interface and model
// ----------------------------------------------
export interface IDropdownDto {
  id: number;
  name: string;
}

export class DropdownDto implements IDropdownDto {
  public id: number;
  public name: string;

  constructor(id: number, value: string) {
    this.id = id;
    this.name = value;
  }
}
