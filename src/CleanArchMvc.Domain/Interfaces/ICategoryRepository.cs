using CleanArchMvc.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetById(int? id);
        Task<CategoryDto> Create(CategoryDto category);
        Task<CategoryDto> Update(CategoryDto category);
        Task<CategoryDto> Remove(CategoryDto category);

    }
}
