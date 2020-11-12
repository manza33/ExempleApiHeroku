using Catalog.Domain.Interfaces.IDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalog.Domain.Items
{
    /// <summary>
    /// 
    /// </summary>
    public class Category
    {
        public Category(int? idCategory, string name, int parentCategory)
        {
            IdCategory = idCategory;
            Name = name;
            ParentCategory = parentCategory;
        }



        /// <summary>
        /// Identifier of Category
        /// </summary>
        public int? IdCategory { get; set; }
        /// <summary>
        /// Name of Category
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Parent of Category
        /// </summary>
        public int ParentCategory { get; set; }

        public Category WithIdCategory(int id)
        {
            IdCategory = id;
            return this;
        }

        public Category WithName(string name)
        {
            Name = name;
            return this;
        }

        public Category WithParentCategory(int parentCategory)
        {
            ParentCategory = parentCategory;
            return this;
        }

        public static Category Create(ICategoryDto dto)
        {
            if (dto == null)
            {
                return null;
            }
            return new Category(
                dto.IdCategory,
                dto.Name,
                dto.ParentCategory
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public static IEnumerable<Category> Map(
            IEnumerable<ICategoryDto> dtos
            )
        {
            if (dtos == null)
            {
                return new List<Category>();
            }

            var dtosList = new List<Category>();
            foreach (var dto in dtos)
            {
                dtosList.Add(Create(dto));
            }

            return dtosList.Any() ? dtosList : null;
        }


    }
}
