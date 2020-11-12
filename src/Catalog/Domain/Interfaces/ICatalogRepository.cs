using Catalog.Domain.Interfaces.IDtos;
using Catalog.Domain.Items;
using System.Collections.Generic;

namespace Catalog.Domain.Interfaces
{
    public interface ICatalogRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllCategories();
    }
}
