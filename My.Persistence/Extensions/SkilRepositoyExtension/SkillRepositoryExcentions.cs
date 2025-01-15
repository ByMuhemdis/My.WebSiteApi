using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Entities.Sort;

namespace My.Application.Extensions.SkilRepositoyExtension
{
    public static class SkillRepositoryExcentions
    {
        //Arama da kullanılacak metot
        public static IQueryable<Skill> Search(this IQueryable<Skill> skills,string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return skills;
            var lowerCase = searchTerm.Trim().ToLower();
            return skills.Where(s=>s.Name.ToLower().Contains(searchTerm));
        }
        //sıralama için gerekli metod

        public static IQueryable<Skill> Sort(this IQueryable<Skill> skills,string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return skills.OrderBy(s=>s.Id);
            //buradaki ara işlemleri OrderQueryBuilder buraya tasıdık amac başka bir yerde de sıralama işlemi yapılırsa direk dinamik olarak sınıftan cekilsin.
            //ve aynı kodu defalarca yazman zorun daklmayalım 
            var orderquery = OrderQueryBuilder.CreateOrderQuery<Skill>(orderByQueryString);

            if (orderquery is null)
                return skills.OrderBy(s=>s.Id);
            return skills.OrderBy(orderquery);

        }
    }
}
