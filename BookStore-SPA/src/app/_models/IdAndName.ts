export class IdAndName{
    Id : number;
    FilteredValue: string;
    constructor(
        Id : number,
        FilteredValue: string
    )
    {
        this.FilteredValue = FilteredValue;
        this.Id = Id;
    }
}