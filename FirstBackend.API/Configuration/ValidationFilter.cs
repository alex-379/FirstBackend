//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;

//namespace FirstBackend.API.Configuration
//{
//    public class ValidationFilter : IActionFilter
//    {
//        public void OnActionExecuting(ActionExecutingContext context)
//        {
//            var a = context.ModelState;
//            if (!context.ModelState.IsValid)
//            {
//                var messages = context.ModelState
//                    .SelectMany(message => message.Value.Errors)
//                    .Select(error => error.ErrorMessage)
//                    .ToList();

//                context.Result = new UnprocessableEntityObjectResult(messages);
//            }
//        }

//        public void OnActionExecuted(ActionExecutedContext context) { }
//    }
//}
