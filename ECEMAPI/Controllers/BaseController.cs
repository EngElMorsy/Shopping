using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using  Abstraction =ECMDomain.Abstraction;

namespace ECEMAPI.Controllers;

    
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult CreateResult<TDto>(Abstraction.Result<TDto> result)
            where TDto : Abstraction.IResult => result.StatusCode == 204
            ? new ObjectResult(null) { StatusCode = 204 }
            : new ObjectResult(result) {StatusCode=result.StatusCode };
       
    }

