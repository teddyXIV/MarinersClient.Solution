using MarinersClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarinersClient.Controllers;

public class AccountsController : Controller
{
    [HttpGet]
    public IActionResult SignIn()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> SignIn(User user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await Models.User.SignIn(user);
                return RedirectToAction("Index", "Players");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Sign in failure");
                return View(user);
            }
        }
        return View(user);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(User model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            Models.User.Register(model);
            return RedirectToAction("SignIn");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "An error occurred during registration. Please try again later");
            return View(model);
        }
    }
}