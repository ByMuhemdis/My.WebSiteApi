using Entities.LogDetailsfilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using My.Services.Iservices;

namespace WebSiteApi.ActionFilter
{
    public class LogFilterAttrubute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttrubute(ILoggerService logger)
        {
            _logger = logger;
        }

       
        public override void OnActionExecuting(ActionExecutingContext context)//işlem başlamadan önce log alınacak
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
           
        }

        private string Log(string modelName, RouteData routeData)
        {
            var logdetails = new LogActionFilter()
            {
                ModelName = modelName,
                Action = routeData.Values["action"],
                Controller = routeData.Values["controller"],
                
            };
            if(routeData.Values.Count >= 3)
            {
                logdetails.Id=routeData.Values["id"];
            }
            return logdetails.ToString();
        }
    }
}
