export class Client{
    id:number;
    firstName: string;
    lastName:string;
    email: string;
    phoneNr:string;
    address:string;

    constructor( id:number,
        firstName: string,
        lastName:string,
        email: string,
        phoneNr:string,
        address:string)
    {
        this.id=id;
        this.firstName=firstName;
        this.lastName=lastName;
        this.email=email;
        this.phoneNr=phoneNr;
        this.address=address;
    }
}