using ASP_NET_MVC_ADONET.Data;
using ASP_NET_MVC_ADONET.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_MVC_ADONET.Controllers
{
    public class HomeController : Controller
    {


        private readonly DataAccess _dataAccess;

        public HomeController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }


        public IActionResult Index()
        {

            try
            {
                var usuarios = _dataAccess.ListarUsuarios();
                return View(usuarios);
            }
            catch(Exception ex)
            {
                TempData["MensagemErro"] = "Erro ao buscar registros de usuários cadastrados!";
                return View();
            }
            
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var usuario = _dataAccess.BuscarUsuarioPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {

            if(ModelState.IsValid)
            {

                var result = _dataAccess.Cadastrar(usuario);

                if(!result)
                {
                    TempData["MensagemErro"] = "Ocorreu um erro ao cadastrar o usuário!";
                    return View(usuario);
                }

                TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Index");

            }

            return View();
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                var result = _dataAccess.Editar(usuario);
                if (!result)
                {
                    TempData["MensagemErro"] = "Ocorreu um erro na edição do usuário!";
                    return View(usuario);
                }

                TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                return RedirectToAction("Index");
                
            }

            return View();

        }

        public IActionResult Detalhes(int id)
        {
            var usuario = _dataAccess.BuscarUsuarioPorId(id);
            return View(usuario);
        }

        public IActionResult Remover(int id)
        {
            var result = _dataAccess.Remover(id);
            if (!result)
            {
                TempData["MensagemErro"] = "Ocorreu um erro ao remover o usuário!";
                return View();
            }

            TempData["MensagemSucesso"] = "Usuário removido com sucesso!";
            return RedirectToAction("Index");
        }

    }
}
