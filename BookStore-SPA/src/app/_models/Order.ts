export class Order {
    id :number;
    clientId : number;
    bookId: number;
    totalPrice: number;
    quantity: number;
    orderNr: number;

    constructor(id :number,
        clientId : number,
        bookId: number,
        totalPrice: number,
        quantity: number,
        orderNr: number)
    {
        this.bookId = bookId;
        this.clientId = clientId;
        this.id = id;
        this.totalPrice = totalPrice;
        this.quantity = quantity;
        this.orderNr = orderNr;
    }
}