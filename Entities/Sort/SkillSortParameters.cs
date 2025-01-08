using Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Sort
{
    public class SkillSortParameters//Sıralama isteklerin için yapıılan sınıf
    {
        public string? OrderBy { get; set; }
        public SkillSortParameters()
        {
            OrderBy = "id";//id e göre defoult oldugunda sıralanacak
        }
    }
}
