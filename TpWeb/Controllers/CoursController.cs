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
           
            return View();
        }
        /// <summary>
        /// Méthode service qui permet d'ajouter un Cours
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <param name="unCoursDTO"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Méthode service qui permet d'afficher les informations d' un Cours dans un formulaire
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <param name="nomCours"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Cours/FormulaireModifierCours")]
        public IActionResult FormulaireModifierCours([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] string nomCours)
        {
            CoursDTO coursDTO;
            ViewBag.nomCegepChoix = nomCegep;
            ViewBag.nomDepartementChoix = nomDepartement;
            if (nomCegep !=null && nomDepartement !=null && nomCours!=null)
            {
                
                coursDTO = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
                return View(coursDTO);
            }else
            {
                coursDTO = null;
                return View(coursDTO);
            }
        }
        /// <summary>
        /// Méthode service qui permet de modifier un Cours
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <param name="coursDTO"></param>
        /// <returns></returns>
        [Route("Cours/ModifierCours")]
        [HttpPost]
        public IActionResult ModifierCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                ViewBag.nomCegepChoix = nomCegep;
                ViewBag.nomDepartementChoix = nomDepartement;
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception)
            {

                return RedirectToAction("FormulaireModifierCours", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement, nomCours = coursDTO.Nom });
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}
