using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketplace.Models;
using marketplace.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using marketplace.Interfaces;

namespace marketplace.Controllers;

public class AccountCustomerController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    public AccountCustomerController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login(){

        return View(new AccountLoginViewModel());
    }

    public async Task<IActionResult> Logout(){
        await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountLoginViewModel request)
    {
        //Cocokan username dan password ke database
        var result = await _accountService.LoginPembeli(request.Username, request.Password);

        if (result == null)
        {
            return View(new AccountLoginViewModel { });
        }

        try
        {
            #region set session login
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, result.IdPembeli.ToString()),
            new Claim(ClaimTypes.Name, result.Email ?? result.Nama),
            new Claim("FullName", result.Nama),
            new Claim(ClaimTypes.Role, AppConstant.CUSTOMER_ROLE),
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {

            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            #endregion

            // return RedirectToActionPermanent("Index", "Produk");
            //Agar tidak dapat kembali
            return RedirectPermanentPreserveMethod("https://localhost::7077");
        }
        catch (System.Exception)
        {
            return View(request);
        }

    }
        public IActionResult Register() {
            return View(new RegisterViewModel());
        }

        public IActionResult Registered() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel request) {
            if(!ModelState.IsValid) {
                return View(request);
            }

            try{
                await _accountService.Register(request);

                return RedirectToAction("Registered", "AccountCustomer");
            } catch(InvalidOperationException ex) {
                ViewBag.ErrorMessage = ex.Message;
            } catch (System.Exception) {
                throw;
            }

            return View(request);
        }
}