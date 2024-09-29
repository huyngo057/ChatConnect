using ChatConnectBE.Requests;
using ChatConnectBE.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatConnectBE.Controllers;

[ApiController]
[Route("account")]
public class AccountController : Controller
{
	private readonly TokenService _tokenService;
	private readonly UserManager<IdentityUser> _userManager;

	public AccountController(UserManager<IdentityUser> userManager, TokenService tokenService)
	{
		_userManager = userManager;
		_tokenService = tokenService;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
	{
		var user = new IdentityUser { UserName = registerRequest.UserName, Email = registerRequest.Email };
		var result = await _userManager.CreateAsync(user, registerRequest.Password);
		if (result.Succeeded)
		{
			var token = _tokenService.GenerateJwtToken(user);
			SetTokenCookie(token);
			return Ok(token);
		}

		return BadRequest(result.Errors);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
	{
		var user = await _userManager.FindByNameAsync(loginRequest.UserName);
		if (user == null)
		{
			return BadRequest("Invalid username or password");
		}

		var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
		if (result)
		{
			var token = _tokenService.GenerateJwtToken(user);
			SetTokenCookie(token);
			return Ok(token);
		}

		return BadRequest("Invalid username or password");
	}

	[HttpPost("logout")]
	public IActionResult Logout()
	{
		// Clear the JWT token cookie
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Expires = DateTime.Now.AddDays(-1),
			Secure = true
		};
		HttpContext.Response.Cookies.Append("jwt", "", cookieOptions);
		return Ok("Logged out successfully.");
	}

	private void SetTokenCookie(string token)
	{
		var cookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Expires = DateTime.Now.AddMinutes(20),
			Secure = true,
			SameSite = SameSiteMode.Strict
		};
		HttpContext.Response.Cookies.Append("jwt", token, cookieOptions);
	}
}