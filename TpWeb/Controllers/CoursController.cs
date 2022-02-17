using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TpWeb.Controllers
{
    public class CoursController : Controller
    {

        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Cours.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Cours")]
        [Route("Cours/Index")]
        [HttpGet]

        public IActionResult Index([FromQuery] string nomCegep,[FromQuery] string nomDepartement)
        {
            //Préparation des données pour la vue...
            if (nomDepartement == null && nomCegep==null)
            {
                nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                
            }
            try
            {

                ViewBag.nomCegepChoix = nomCegep;
                ViewBag.nomDepartementChoix = nomDepartement;
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                ViewBag.ListDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
                ViewBag.ListeCourts = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Erreur lors de l'obtention d'un département par son nom et son cégep...")
                {

                    if (CegepControleur.Instance.ObtenirListeDepartement(nomCegep).Count > 0)
                    {
                        nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                        ViewBag.nomCegepChoix = nomCegep;
                        ViewBag.nomDepartementChoix = nomDepartement;
                        ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                        ViewBag.ListDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
                        ViewBag.ListeCourts = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();
                    }
                    else
                    {
                        nomDepartement = "";
                        ViewBag.nomDepartementChoix = nomDepartement;
                        ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                        ViewBag.ListDepartements = new List<DepartementDTO>().ToArray();
                        ViewBag.ListeCourts = new List<CoursDTO>().ToArray();
                    }
                    
                } else
                {
                    ViewBag.MessageErreur = ex.Message;
                }

                
            }
            //retourne la vue
            return View();
        }
        [Route("Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm]string nomCegep, [FromForm] string nomDepartement,
            [FromForm] CoursDTO unCoursDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, unCoursDTO);
            }
            catch (Exception e)
            {

                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}
