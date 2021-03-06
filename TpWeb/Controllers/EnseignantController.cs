using GestionCegepWeb.Logics.Controleurs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TpWeb.Controllers
{
    public class EnseignantController : Controller
    {

        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///   -Afficher la liste des Enseignants.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Enseignant")]
        [Route("Enseignat/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {

            //Préparation des données pour la vue...
            if (nomCegep == null || nomDepartement==null)
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
                ViewBag.ListeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep,nomDepartement).ToArray();
            }
            catch (Exception e)
            {
                if(e.Message== "Erreur lors de l'obtention d'un département par son nom et son cégep...")
                {
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                    ViewBag.nomCegepChoix = nomCegep;
                    ViewBag.nomDepartementChoix = nomDepartement;
                    ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                    ViewBag.ListDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
                    ViewBag.ListeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement).ToArray();
                }
                else
                {
                    ViewBag.MessageErreur = e.Message;
                }
                
                //ViewBag.AfficherResulat = ViewBag.MessageErreur;
            }
            //retoune la vue
            return View();
        }
    }
}
