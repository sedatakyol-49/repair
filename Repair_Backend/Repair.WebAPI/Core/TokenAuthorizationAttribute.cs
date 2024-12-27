using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FirstCashSolution.CESOP.WebAPI.Core;

//public class TokenAuthorizationAttribute : TypeFilterAttribute
//{
//    //public TokenAuthorizationAttribute() : base(typeof(TokenAuthorizationFilter))
//    //{
//    //}
//}

//public class TokenAuthorizationFilter : IAuthorizationFilter
//{
//    private readonly IAccountBL _accountBL;

//    public TokenAuthorizationFilter(IAccountBL accountBL)
//    {
//        _accountBL = accountBL;
//    }

//    public void OnAuthorization(AuthorizationFilterContext context)
//    {
//        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//        if (token == null || !_accountBL.ValidateToken(token))
//        {
//            context.Result = new UnauthorizedResult();
//        }
//    }
//}

