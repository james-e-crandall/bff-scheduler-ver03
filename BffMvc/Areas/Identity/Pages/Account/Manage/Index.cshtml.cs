using AuthLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BffMvc.Areas.Identity.Pages.Account.Manage;

[Authorize]
public class IndexModel : PageModel
{
    private readonly UserManager<ApplicationUser> _UserManager;

    public IndexModel(UserManager<ApplicationUser> UserManager)
    {
        _UserManager = UserManager;
    }

    public ApplicationUser? UserProfile { get; private set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _UserManager.GetUserAsync(User);
        UserProfile = user;
        return Page();
    }
}
