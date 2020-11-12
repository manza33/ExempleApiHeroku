using Catalog.Domain.Interfaces;
using Catalog.Domain.Items;
using Catalog.Infrastructure.Dtos;
using Dapper;
using Npgsql;
using System.Collections.Generic;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private NpgsqlConnection _db;

        public CatalogRepository(string connectionString)
        {
            _db = new NpgsqlConnection(connectionString);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var result = _db.Query<CategoryDto>(
                "SELECT \"IdCategory\", \"Name\", \"Parent_Category\" AS ParentCategory FROM \"Category\"");
            return Category.Map(result);
        }
    }
}
