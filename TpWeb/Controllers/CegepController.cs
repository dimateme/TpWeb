using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace TpWeb.Controllers
{
    public class CegepController : Controller
    {


        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Cégeps.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("")]
        [Route("Cegep")]
        [Route("Cegep/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }
        /// <summary>
        /// Méthode service qui permet d'ajouter un Cégep
        /// </summary>
        /// <param name="cegepDTO"></param>
        /// <returns></returns>
        [Route("Cegep/AjouterCegep")]
        [HttpPost]
        public IActionResult AjouterCegep([FromForm] CegepDTO cegepDTO)
        {
            
            if (cegepDTO != null)
            {
                try
                {
                    
                     CegepControleur.Instance.AjouterCegep(cegepDTO);
                }
                catch (Exception e)
                {
                    ViewBag.MessageErreur = e.Message;
                }

            }
            //Retour de la vue...
            return RedirectToAction("Index","Cegep", cegepDTO);
        }
        /// <summary>
        /// Méthode service qui permet d'afficher les information d' un Cégep dans un formulaire
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <returns></returns>
        [Route("Cegep/FormulaireModifierCegep")]
        [HttpGet]
        public IActionResult FormulaireModifierCegep([FromQuery]string  nomCegep)
        {
            
            CegepDTO unCegepDTO;
            if(nomCegep!= null)
            {
                unCegepDTO = CegepControleur.Instance.ObtenirCegep(nomCegep);
                return View(unCegepDTO);
            }else
            {
                unCegepDTO = null;
                return View(unCegepDTO);
            }
            
            //return View();
        }
        /// <summary>
        /// Méthode service qui permet de modifier un Cégep
        /// </summary>
        /// <param name="unCegepDTO"></param>
        /// <returns></returns>
        [Route("Cegep/ModifierCegep")]
        [HttpPost]
        public IActionResult ModifierCegep([FromForm] CegepDTO unCegepDTO)
        {

            try
            {
                CegepControleur.Instance.ModifierCegep(unCegepDTO);
            }
            catch (Exception)
            {

                return RedirectToAction("FormulaireModifierCegep","Cegep");
            }
             

            return RedirectToAction("Index", "Cegep", unCegepDTO);
        }



    }
}
