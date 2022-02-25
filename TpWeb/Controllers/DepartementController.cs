using Microsoft.AspNetCore.Mvc;
using GestionCegepWeb.Logics.Controleurs;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GestionCegepWeb.Models;

namespace TpWeb.Controllers
{
    public class DepartementController : Controller
    {
        //CegepDTO lecegep;
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

        /// <summary>
        /// Méthode service qui permet d'ajoutyer un Département
        /// </summary>
        /// <param name="departementDTO"></param>
        /// <param name="nomCegep"></param>
        /// <returns></returns>
        [Route("Departement/AjouterDepartement")]
        [HttpPost]

        public IActionResult AjouterDepartement([FromForm] DepartementDTO departementDTO ,[FromForm] string nomCegep)
        {
            //Préparation des données pour la vue...
            if (departementDTO != null && nomCegep!=null)
            {
                try
                {

                    CegepControleur.Instance.AjouterDepartement(nomCegep, departementDTO);
                }
                catch (Exception e)
                {
                    ViewBag.MessageErreur = e.Message;

                }
            }
            
            return RedirectToAction("Index", "Departement", new {nomCegep=nomCegep});
        }
        /// <summary>
        /// Méthode service qui permet d'afficher les informations d' un département dans un formulaire
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <returns></returns>
        [Route("Departement/FormulaireModifierDepartement")]
        [HttpGet]
        public IActionResult FormulaireModifierDepartement([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            ViewBag.nomCegepChoix = nomCegep;
            DepartementDTO departementDTO;
            departementDTO = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);

            return View(departementDTO);
           
        }
        /// <summary>
        /// Méthode service qui permet de modifier un département
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="departementDTO"></param>
        /// <returns></returns>
        [Route("Departement/ModifierDepartement")]
        [HttpPost]
        public IActionResult ModifierDepartement([FromForm] string nomCegep, [FromForm] DepartementDTO departementDTO)
        {
            
            try
            {
                ViewBag.nomCegepChoix = nomCegep;
                CegepControleur.Instance.ModifierDepartement(nomCegep, departementDTO);
            }
            catch (Exception)
            {

                return RedirectToAction("FormulaireModifierDepartement", "Departement", new { nomCegep = nomCegep, nomDepartement = departementDTO });
            }
            

            return RedirectToAction("Index", "Departement",new {nomCegep=nomCegep});

        }


    }
}
