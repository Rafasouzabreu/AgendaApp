using AgendaApp.MVC.Models.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AgendaApp.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            try
            {
                //serializar os dados da model em JSON
                var request = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                //enviando a requisição de autenticação de usuário para a API
                var response = await _httpClient.PostAsync("api/usuarios/autenticar", request);
                var content = await response.Content.ReadAsStringAsync();

                //verificando se foi retornado resposta de sucesso
                if (response.IsSuccessStatusCode)
                {
                    //criando uma identificação com os dados do usuário autenticado
                    var claimsIdentity = new ClaimsIdentity(new[]
                        { new Claim(ClaimTypes.Name, content) }, CookieAuthenticationDefaults.AuthenticationScheme);

                    //gravar esta identificação em um arquivo de Cookie no navegador
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    //redirecionar o usuário para a página de consulta de tarefas
                    return RedirectToAction("Consulta", "Tarefas");
                }
                else
                    TempData["MensagemErro"] = $"Erro: {content}";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        //GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        //POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {
            try
            {
                //serializar os dados da model em JSON
                var request = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                //enviando a requisição de cadastro de usuário para a API
                var response = await _httpClient.PostAsync("api/usuarios/criar", request);
                var content = await response.Content.ReadAsStringAsync();

                //verificando se foi retornado resposta de sucesso
                if (response.IsSuccessStatusCode)
                    TempData["MensagemSucesso"] = $"Sucesso: {content}";
                else
                    TempData["MensagemErro"] = $"Erro: {content}";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View();
        }

        //GET: /Account/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionar o usuário para a página de autenticação
            return RedirectToAction("Login");
        }
    }
}



