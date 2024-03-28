using AgendaApp.MVC.Models.Tarefas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AgendaApp.MVC.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly HttpClient _httpClient;

        public TarefasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //GET: /Tarefas/Consulta
        public IActionResult Consulta()
        {
            return View();
        }

        //POST: /Tarefas/Consulta
        [HttpPost]
        public async Task<IActionResult> Consulta(TarefasConsultaViewModel model)
        {
            try
            {
                var dataInicio = model.DataInicio.Value.ToString("yyyy-MM-dd");
                var dataFim = model.DataFim.Value.ToString("yyyy-MM-dd");

                //resgatar os dados contidos no Cookie de autenticação
                var identity = User.Identity.Name;
                var userAuth = JsonConvert.DeserializeObject<UserAuth>(identity);

                //enviando o cabeçalho da requisição com o TOKEN JWT
                _httpClient.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Bearer", userAuth.AccessToken);

                var response = await _httpClient.GetAsync($"api/tarefas/{dataInicio}/{dataFim}");
                var content = await response.Content.ReadAsStringAsync();

                TempData["ConsultaTarefas"] = content;
            }
            catch (Exception e)
            {
                TempData["ConsultaTarefas"] = e.Message;
            }

            return View();
        }
    }

    public class UserAuth
    {
        public string? AccessToken { get; set; }
    }
}



