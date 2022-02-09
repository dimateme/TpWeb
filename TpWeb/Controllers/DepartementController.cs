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

        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Départements.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Departement")]
        [Route("Departement/Index")]
        [HttpGet]
       
        public IActionResult Index([FromQuery] string nomCegep)
        {
            //Préparation des données pour la vue...
            if (nomCegep == null)
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




            //retoune la vue
            return View();
        }
        
    }
}
