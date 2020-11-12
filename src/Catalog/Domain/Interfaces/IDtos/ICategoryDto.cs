using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Interfaces.IDtos
{
    public interface ICategoryDto
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
