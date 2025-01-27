using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Sort
{
    public static class OrderQueryBuilder
    {
        //bu metot simdiik skill şlemleri için sadece skille ait yerlerde sıralama yapılmak içim kullanılmiştir. var doldugu yer SkillRepositoryExcentions burasıdır.

        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderPrams = orderByQueryString.Trim().Split(',');//querystring ifadesi alındı.

            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);//nesne üzerinden propery (örn suan skill de çalıştıgımıza göre skill içindeki id,title gibi bilgileri almak )

            var orderquerybuilder = new StringBuilder();


            foreach (var param in orderPrams)
            {
                if (string.IsNullOrWhiteSpace(param))
                {
                    continue;
                }
                var proportyFromQueryName = param.Split(" ")[0];
                var objectproperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(proportyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectproperty is null)
                {
                    continue;
                }

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                orderquerybuilder.Append($"{objectproperty.Name.ToString()} {direction},");
            }


            var orderquery = orderquerybuilder.ToString().TrimEnd(',', ' ');
            return orderquery;
        }
    }
}
