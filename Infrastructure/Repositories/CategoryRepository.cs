using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.AppRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(TaskDbContext context) : base(context)
        {
        }
    }
}
