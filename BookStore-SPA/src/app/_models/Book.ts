export class Book {
  id: number;
  name: string;
  author: string;
  description: string;
  price: number | null;
  publishDate: any;
  categoryId: number | null;
  pieces: number | null;

  constructor(
    id: number,
    name: string,
    author: string,
    description: string,
    price: number | null,
    publishDate: any,
    categoryId: number | null,
    pieces: number | null
  ) {
    this.id = id;
    this.name = name;
    this.author = author;
    this.description = description;
    this.price = price;
    this.publishDate = publishDate;
    this.categoryId = categoryId;
    this.pieces = pieces;
  }
}
