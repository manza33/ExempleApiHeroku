using Catalog.Domain.Interfaces.IDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Infrastructure.Dtos
{
    public class CategoryDto : ICategoryDto
    {
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
    }
}
