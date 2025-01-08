using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.LogDetailsfilter
{
    public class LogActionFilter
    {
        //Context üzerinden kullanılacagı için object olarak tanımlandı.
        public object? ModelName { get; set; }
        public object? Controller { get; set; }
        public object? Action { get; set; }
        public object? Id { get; set; }
        public object? CreatAdd { get; set; }
        public LogActionFilter()
        {
            CreatAdd= DateTime.UtcNow;
        }
        //json formatında olsun diye override işlemi yapıyoruz 
        public override string ToString()
        {
           return JsonSerializer.Serialize(this);
        }
    }
}
