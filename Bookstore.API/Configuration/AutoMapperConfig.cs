using AutoMapper;
using BookStore.API.Dtos.Book;
using BookStore.API.Dtos.Category;
using BookStore.API.Dtos.Client;
using BookStore.API.Dtos.Order;
using BookStore.Domain.Models;

namespace BookStore.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryEditDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>().ReverseMap();

            CreateMap<Book, BookAddDto>().ReverseMap();
            CreateMap<Book, BookEditDto>().ReverseMap();
            CreateMap<Book, BookResultDto>().ReverseMap();

            CreateMap<Client, ClientAddDto>().ReverseMap();
            CreateMap<Client, ClientEditDto>().ReverseMap();
            CreateMap<Client, ClientResultDto>().ReverseMap();

            CreateMap<Orders, OrderAddDto>().ReverseMap();
            CreateMap<Orders, OrderEditDto>().ReverseMap();
            CreateMap<Orders, OrderResultDto>().ReverseMap();
        }
    }
}