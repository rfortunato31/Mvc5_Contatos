using Antlr.Runtime.Misc;
using Mvc5_Contatos.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc5_Contatos.Controllers
{
    public class FundoController : Controller
    {
        // GET: Fundo
        public ActionResult Index(int? pagina)
        {
            IEnumerable<FundoViewModel> fundos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");
                //HTTP GET
                var responseTask = client.GetAsync("fundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<FundoViewModel>>();
                    readTask.Wait();

                    fundos = readTask.Result;
                }
                else
                {
                    fundos = Enumerable.Empty<FundoViewModel>();
                    ModelState.AddModelError(string.Empty, "Falha no Envio.");
                }
                
            }            
            return View(fundos);

        }
        [HttpGet]
        public ActionResult create()
        {
            FundoViewModel fundo = null;

            CadFundoViewModel cadFundo = new CadFundoViewModel();

            IEnumerable<TipoFundoViewModel> tipoFundos = null;

            cadFundo.Fundo = fundo;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync("tipoFundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoFundoViewModel>>();
                    readTask.Wait();
                    tipoFundos = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca dos Tipos de Fundo.");
                }
            }
            cadFundo.TipoFundoList = tipoFundos;
            return View(cadFundo);
        }

        [HttpPost]
        public ActionResult create(CadFundoViewModel cadFundo)
        {
            if (cadFundo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<FundoViewModel>("fundo", cadFundo.Fundo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Inclusão.");
                }
            }

            IEnumerable<TipoFundoViewModel> tipoFundos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync("tipoFundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoFundoViewModel>>();
                    readTask.Wait();

                    tipoFundos = readTask.Result;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca dos Tipos de Fundo.");
                }
            }
            cadFundo.TipoFundoList = tipoFundos;
            return View(cadFundo);           

        }

        public ActionResult Details(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FundoViewModel fundo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync(codigo);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FundoViewModel>();
                    readTask.Wait();

                    fundo = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca do Fundo.");
                }
            }
            return View(fundo);
        }        

        public ActionResult Delete(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FundoViewModel fundo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync(codigo);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Exclusão.");
                }
            }

            return View(fundo);
        }

        public ActionResult Edit(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FundoViewModel fundo = null;

            CadFundoViewModel cadFundo = new CadFundoViewModel();

            IEnumerable<TipoFundoViewModel> tipoFundos = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync(codigo);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FundoViewModel>();
                    readTask.Wait();

                    fundo = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca dos Tipos de Fundo.");
                }
            }            

            cadFundo.Fundo = fundo;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync("tipoFundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoFundoViewModel>>();
                    readTask.Wait();

                    tipoFundos = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca do Fundo.");
                }
                ViewBag.TipoFundo = View(tipoFundos);
            }
            cadFundo.TipoFundoList = tipoFundos;
            return View(cadFundo);

        }

        [HttpPost]
        public ActionResult Edit(string codigo, CadFundoViewModel cadFundo)
        {
            if (cadFundo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            IEnumerable<TipoFundoViewModel> tipoFundos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync("tipoFundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoFundoViewModel>>();
                    readTask.Wait();

                    tipoFundos = readTask.Result;
                }
                else
                {
                    tipoFundos = Enumerable.Empty<TipoFundoViewModel>();
                    ModelState.AddModelError(string.Empty, "Falha na Busca dos Tipos de Fundo.");
                }
            }
            cadFundo.TipoFundoList = tipoFundos;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<FundoViewModel>("fundo/"+ codigo, cadFundo.Fundo);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Alteração.");
                }
            }

            
            return View(cadFundo);
        }

        public ActionResult MovFund(string codigo)
        {
            if (codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FundoViewModel fundo = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync(codigo);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<FundoViewModel>();
                    readTask.Wait();

                    fundo = readTask.Result;
                    fundo.Patrimonio = 0;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Busca do Fundo.");
                }
            }

            return View(fundo);
        }

        [HttpPost]
        public ActionResult MovFund(string codigo, FundoViewModel fundo)
        {
            if (fundo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<FundoViewModel>(codigo+ "/patrimonio", fundo);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha na Movimentação.");
                }
            }
            return View(fundo);
        }

        // GET: TipoFundo
        public ActionResult TipoFundo()
        {
            IEnumerable<TipoFundoViewModel> tipoFundos = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/api/fundo/");
                //HTTP GET
                var responseTask = client.GetAsync("tipoFundo");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TipoFundoViewModel>>();
                    readTask.Wait();

                    tipoFundos = readTask.Result;
                }
                else
                {
                    tipoFundos = Enumerable.Empty<TipoFundoViewModel>();
                    ModelState.AddModelError(string.Empty, "Falha na Busca dos Tipos de Fundo.");
                }
                return View(tipoFundos);
            }

        }

    }
}