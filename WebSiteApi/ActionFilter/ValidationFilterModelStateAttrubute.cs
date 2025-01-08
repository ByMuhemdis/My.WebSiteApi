using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebSiteApi.ActionFilter
{
    public class ValidationFilterModelStateAttrubute : ActionFilterAttribute
    {
        /// <summary>
        /// Verdiğiniz kod, ASP.NET Core'da bir ActionFilterAttribute sınıfıdır ve amaç, denetleyicilerin eylem yöntemleri çalışmadan 
        /// önce bazı doğrulama işlemleri yapmaktır. Ancak, bu kod hata loglarını tutmak için değil, istek öncesinde model durumunu kontrol etmek ve eğer bir sorun varsa 
        /// uygun bir sonuç döndürmek için tasarlanmıştır. İşte kodun detaylı açıklaması:
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)//işlem başlamadan önce kontrol saglanacak
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            //parametre bilgisi alma 

            var param = context.ActionArguments
                .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (controller == null)
            {
                context.Result = new BadRequestObjectResult($"object is null. " +
                    $"controller : {controller} " +
                    $"action : {action}");
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
