using IsconGathiya.Domain.DataModels;
using IsconGathiya.Service.Account;
using IsconGathiya.Service.Authentication;
using Microsoft.AspNetCore.Mvc;
using IsconGathiya.ViewModel;

namespace IsconGathiya.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        public AccountController(IAccountRepository accountRepository, IJwtTokenRepository jwtTokenRepository)
        {
            _accountRepository = accountRepository;
            _jwtTokenRepository = jwtTokenRepository;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["AuthValidator"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (Request.Cookies["AuthValidator"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            var admin = _accountRepository.GetUserDetailsByEmail(model.Email);
            if (admin == null || admin.Email != model.Email)
            {
                AddSweetAlertWarningPopup("Invalid Email or password");
                return View("Index");
            }

            var aspUser = _accountRepository.GetAspNetUserById(admin.AspNetUserId);
            if (aspUser == null || aspUser.PasswordHash != model.Password)
            {
                AddSweetAlertWarningPopup("Invalid Password");
                return View("Index", model);
            }

            var jwtModel = new SecAdmin
            {
                UserName = aspUser.UserName,
                AspNetUserId = aspUser.AspNetUserId,
                AdminId = admin.AdminId,
                RollType = admin.RollType,
                BranchId = admin.BranchId,
            };

            var token = _jwtTokenRepository.GenerateJWTAuthetication(jwtModel);
            _accountRepository.SaveUserToken(aspUser.AspNetUserId, token);
            Response.Cookies.Append("AuthValidator", token);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("AuthValidator");
            return View("Index");
        }
    }
}
