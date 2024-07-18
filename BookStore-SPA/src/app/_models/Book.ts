export class Book {
  id: number;
  name: string;
  author: string;
  description: string;
  value: number | null;
  publishDate: any;
  categoryId: number | null;

  constructor(
    id: number,
    name: string,
    author: string,
    description: string,
    value: number | null,
    publishDate: any,
    categoryId: number | null
  ) {
    this.id = id;
    this.name = name;
    this.author = author;
    this.description = description;
    this.value = value;
    this.publishDate = publishDate;
    this.categoryId = categoryId;
  }
}
