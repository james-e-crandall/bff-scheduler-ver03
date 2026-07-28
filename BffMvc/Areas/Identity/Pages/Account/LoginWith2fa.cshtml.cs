using System.ComponentModel.DataAnnotations;
using AuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BffMvc.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LoginWith2faModel : PageModel
{
    private readonly SignInManager<ApplicationUser> _SignInManager;

    public LoginWith2faModel(SignInManager<ApplicationUser> SignInManager)
    {
        _SignInManager = SignInManager;
    }

    [BindProperty]
    public InputModel Input { get; set; } = new();

    public IActionResult OnGet(string? rememberMe, string? returnUrl = null)
    {
        Input.RememberMe = rememberMe == "True";
        Input.ReturnUrl = returnUrl ?? string.Empty;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _SignInManager.GetTwoFactorAuthenticationUserAsync();
        if (user is null)
        {
            return RedirectToPage("./Login");
        }

        var result = await _SignInManager.TwoFactorAuthenticatorSignInAsync(Input.TwoFactorCode, Input.RememberMe, Input.RememberMachine);
        if (result.Succeeded)
        {
            return LocalRedirect(string.IsNullOrEmpty(Input.ReturnUrl) ? "/" : Input.ReturnUrl);
        }

        if (result.IsLockedOut)
        {
            return RedirectToPage("./Lockout");
        }

        ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
        return Page();
    }

    public class InputModel
    {
        [Required]
        [StringLength(7)]
        public string TwoFactorCode { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public bool RememberMachine { get; set; }

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
