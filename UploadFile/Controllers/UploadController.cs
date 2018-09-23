using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadFile.Models;

namespace UploadFile.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload  
        public ActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {

            try
            {
                using (MeuContexto contexto = new MeuContexto())
                {
                    if (file.ContentLength > 0)
                    {
                        UploadFileResult uploadFileResult = new UploadFileResult();
                        uploadFileResult.Nome = Path.GetFileName(file.FileName);
                        uploadFileResult.Caminho = Path.Combine(Server.MapPath("~/UploadedFiles"), uploadFileResult.Nome);
                        file.SaveAs(uploadFileResult.Caminho);
                        uploadFileResult.CalculaHash(uploadFileResult.Caminho);
                        ViewBag.Message = uploadFileResult.Hash;

                        contexto.uploadFileResults.Add(uploadFileResult);
                        contexto.SaveChanges();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        public ActionResult Listar()
        {
            List<UploadFileResult> results = new List<UploadFileResult>();

            using (MeuContexto contexto = new MeuContexto())
            {
                foreach(UploadFileResult item in contexto.uploadFileResults.ToList())
                {
                    results.Add(item);
                }
            }

            return View(results);
        }
    }
}