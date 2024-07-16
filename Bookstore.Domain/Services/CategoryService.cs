using System;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace BookStore.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookService _bookService;

        public CategoryService(ICategoryRepository categoryRepository, IBookService bookService)
        {
            _categoryRepository = categoryRepository;
            _bookService = bookService;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }
        public async Task<Category> GetById(int id)
        {
            return await _categoryRepository.GetById(id);
        }
        public async Task<Category> Add(Category category)
        {
            if(isCategoryExisting(category)) return null;
            
            await _categoryRepository.Add(category);
            return category;
        }
        public async Task<Category> Update(Category category)
        {
            if(_categoryRepository.Search(c=> category.Name == c.Name && category.Id != c.Id).Result.Any())
                return null;

            await _categoryRepository.Update(category);
            return category;
        }
        public async Task<bool> Remove(Category category)
        {
            if(!isCategoryExisting(category)) return false;

            var books = await _bookService.GetBooksByCategory(category.Id);
            if (books.Any()) return false;

            await _categoryRepository.Remove(category);
            return true;
        }
        public async Task<IEnumerable<Category>> Search(string categoryName)
        {
            return await _categoryRepository.Search(c => c.Name.Contains(categoryName));
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }

        private bool isCategoryExisting(Category category){
            return _categoryRepository.Search(c => category.Name == c.Name).Result.Any();
        }
    }
}