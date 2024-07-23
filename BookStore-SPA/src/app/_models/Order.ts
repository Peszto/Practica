export class Order {
    id :number;
    clientId : number | null;
    bookId: number | null;
    quantity: number | null;
    orderNr: number | null;
    totalPrice : number;

    constructor(id :number,
        clientId : number | null,
        bookId: number | null,
        quantity: number | null,
        orderNr: number | null,
        totalPrice: number
    )
    {
        this.bookId = bookId;
        this.clientId = clientId;
        this.id = id;
        this.quantity = quantity;
        this.orderNr = orderNr;
        this.totalPrice = totalPrice;
    }
}