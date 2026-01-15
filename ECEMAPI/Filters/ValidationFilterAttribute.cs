

using ECMDomain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECEMAPI.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        #region SEARCHLearn 
        #endregion
        
        
        
        #region BaseLearn
        if (!context.ModelState.IsValid)
        {
            // Optional: Customize the error response format
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();
            if (errors.Any(x => x.Contains("request")))
                throw new PayloadFormatException(errors);
            else
                throw new RequestValidationException(errors);
        }
        #endregion
    }
}
//public class ValidationFilterAttribute : IAsyncActionFilter
//{
//    public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        #region BaseLearn
//        if (!context.ModelState.IsValid)
//        {
//            // Optional: Customize the error response format
//            var errors = context.ModelState.Values
//                .SelectMany(x => x.Errors)
//                .Select(x => x.ErrorMessage)
//                .ToList();

//            if (errors.Any(x => x.Contains("request")))

//            throw new PayloadFormatException(errors);

//            else
//                throw new RequestValidationException(errors);


//        }
//        #endregion
//    }
//}


