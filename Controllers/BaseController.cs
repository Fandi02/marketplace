using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using marketplace.Interfaces;
using marketplace.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace marketplace.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(HttpContext.User == null || HttpContext.User.Identity == null){
            ViewBag.IsLogged = false;
        } else {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.IsCustomer = HttpContext.User.IsInRole(AppConstant.CUSTOMER_ROLE);
        }

        base.OnActionExecuted(context);
    }
}