using Microsoft.AspNetCore.Mvc;
using GestionCegepWeb.Logics.Controleurs;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TpWeb.Controllers
{
    public class DepartementController : Controller
    {
        
        [Route("Departement")]
        [Route("Departement/Index")]
        [HttpGet]
       
        public IActionResult Index([FromQuery] string nomCegep)
        {
            if(nomCegep == null)
            {
                nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
            }

            try
            {

                ViewBag.nomCegepChoix = nomCegep;
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                ViewBag.ListeDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();


            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
                //ViewBag.AfficherResulat = ViewBag.MessageErreur;
            }





            return View();
        }
        //[HttpGet]
        //public IActionResult Index([FromQuery] string nomCegep)
        //{


        //    try
        //    {


                
        //        ViewBag.ListeDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.MessageErreur = e.Message;
        //        //ViewBag.AfficherResulat = ViewBag.MessageErreur;
        //    }


        //    return View();
        //}

        //[Route("Departement/AfficherDepartement")]
        //[HttpGet]
        //public IActionResult AfficherDepartement()
        //{
        //    ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
        //    //ViewBag.ListeDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
        //    return View("Index");
        //}
    }
}
