using ApiLibros.Areas.Identity.Data;
using ApiLibros.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace ApiLibros.Controllers.Identity
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly ApiLibrosContext _context;
        private readonly UserManager<ApiLibrosUser> _userManager;
        private readonly IUserStore<ApiLibrosUser> _userStore;
        private readonly IUserEmailStore<ApiLibrosUser> _emailStore;
        private readonly SignInManager<ApiLibrosUser> _signInManager;

        public AspNetUsersController(ApiLibrosContext context,
            UserManager<ApiLibrosUser> userManager,
            IUserStore<ApiLibrosUser> userStore,
            SignInManager<ApiLibrosUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
        }

        
        [HttpPost]
        public async Task<ActionResult<ApiLibrosUser>> PostAspNetUser([FromBody] ApiLibrosUser aspNetUser)
        {
            var user = CreateUser();

            await _userStore.SetUserNameAsync(user, aspNetUser.UserName, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, aspNetUser.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, aspNetUser.PasswordHash);

            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var result1 = await _userManager.ConfirmEmailAsync(user, code);
                if (result1.Succeeded)
                {
                    {
                        return Ok(user);
                    }
                }
            }
            return Conflict(result.Errors.First().Code + " --- " + result.Errors.First().Description);

        }

        [HttpGet]
        public async Task<ActionResult> GetLogin(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent:false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult> GetLogOut()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        
        private ApiLibrosUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApiLibrosUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(ApiLibrosUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApiLibrosUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApiLibrosUser>)_userStore;
        }
    }
}
