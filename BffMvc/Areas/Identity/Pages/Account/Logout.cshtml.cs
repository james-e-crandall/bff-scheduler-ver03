using AuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BffMvc.Areas.Identity.Pages.Account;

[Authorize]
public class LogoutModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _SignInManager;

    public LogoutModel(SignInManager<ApplicationUser> SignInManager)
    {
        _SignInManager = SignInManager;
    }

    public IActionResult OnGet() => Page();

    public async Task<IActionResult> OnPostAsync()
    {
        await _SignInManager.SignOutAsync();
        return LocalRedirect("/");
    }
}
