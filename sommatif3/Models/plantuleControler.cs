using Microsoft.EntityFrameworkCore;
using sommatif3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Canabis.Models
{
    public static class plantuleControler
    {
        public static List<string> trouverPlantuleInfo(string id)
        {
            List<string> listPlantInfo = new List<string>();

            using (PlanteContext PC = new PlanteContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesPlante = PC.plante.Where(p => p.IdPlante == id).Select(p => new
                    {
                        p.EtatSante,
                        p.DateAjout,
                        p.Provenance,
                        p.Description,
                        p.Stade,
                        p.Entreposage,
                        p.Active_Inactive,
                        p.ItemRetireInventaire,
                        p.Note,
                        p.Responsable
                    }).ToList();

                    //listPlantInfo.Add(MesPlante[0].EtatSante.ToString());
                    foreach (var plant in MesPlante)
                    {
                        listPlantInfo.Add(plant.EtatSante.ToString());
                        listPlantInfo.Add(plant.DateAjout.ToString());
                        listPlantInfo.Add(plant.Provenance);
                        listPlantInfo.Add(plant.Description);
                        listPlantInfo.Add(plant.Stade);
                        listPlantInfo.Add(plant.Entreposage);
                        listPlantInfo.Add(plant.Active_Inactive.ToString());
                        listPlantInfo.Add(plant.ItemRetireInventaire.ToString());
                        listPlantInfo.Add(plant.Responsable);
                        listPlantInfo.Add(plant.Note);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            return listPlantInfo;
        }

        public static int countActifPlantule()
        {
            using (PlanteContext PC = new PlanteContext())
            {
                int count = PC.plante.Count(p => p.Active_Inactive == 1);

                return count;
            }
        }

        public static int countInactifPlantule()
        {
            using (PlanteContext PC = new PlanteContext())
            {
                int count = PC.plante.Count(p => p.Active_Inactive == 0);

                return count;
            }
        }

        public static int countAllPlantule()
        {
            using (PlanteContext PC = new PlanteContext())
            {
                int count = PC.plante.Count(p => p.IdPlante != "");

                return count;
            }
        }

        public static int countAllUser()
        {
            using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
            {
                int count = PC.CompteUtilisateur.Count(p => p.IdUtilisateur != "");

                return count;
            }
        }

        //return the list of names depending on the parameter. it return a list of firstname if the 
        //parameter passed is "nom", it return a list of prenom if the parameter passed is "surname"
        public static List<string> getUtilisateurName(string arg)
        {
            List<string> nameList = new List<string>();
            nameList.Clear();

            if (arg == "nom")
            {
                using (CompteUtilisateurContext EC = new CompteUtilisateurContext())
                    try
                    {
                        //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                        var MesUtilisateur = EC.CompteUtilisateur.Select(p => new
                        {
                            p.Nom
                        }).ToList();

                        foreach (var m in MesUtilisateur)
                        {
                            nameList.Add(m.Nom.ToString());
                        }
                        return nameList;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
            }
            else if (arg == "prenom")
            {
                using (CompteUtilisateurContext EC = new CompteUtilisateurContext())
                    try
                    {
                        //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                        var MesUtilisateur = EC.CompteUtilisateur.Select(p => new
                        {
                            p.Prenom
                        }).ToList();

                        foreach (var m in MesUtilisateur)
                        {
                            nameList.Add(m.Prenom.ToString());
                        }
                        return nameList;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("error");
                    }
            }
            return nameList;
        }

        public static void setSanteColorUi(string color1, string color2, System.Windows.Controls.Border uiBorder)
        {
            System.Windows.Media.LinearGradientBrush gradientBrush = new System.Windows.Media.LinearGradientBrush();
            gradientBrush.StartPoint = new System.Windows.Point(0, 0);
            gradientBrush.EndPoint = new System.Windows.Point(1, 1);

            // Define the two colors of the gradient
            gradientBrush.GradientStops.Add(new System.Windows.Media.GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color1), 0));
            gradientBrush.GradientStops.Add(new System.Windows.Media.GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color2), 1));

            // Apply the gradient brush to the Border's background
            uiBorder.Background = gradientBrush;
        }

        public static void trouvePlantETChargerSurDataGrid(string id, DataGrid grille)
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesPlante = PC.plante.Where(p => p.IdPlante == id)
                        .Select(p => new
                    {
                        p.IdPlante,
                        p.EtatSante,
                        p.DateAjout,
                        p.Provenance,
                        p.Description,
                        p.Stade,
                        p.Entreposage,
                        p.Active_Inactive,
                        p.ItemRetireInventaire,
                        p.Note,
                        p.Responsable
                        }).ToList();
                    grille.ItemsSource = MesPlante;
                    //statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    // statusMessage.Text = ex.Message;
                    Console.WriteLine(ex.ToString());
                }
        }
        public static string getPlantIdFromDb(string id)
        {
            string plantId = string.Empty;
            using (PlanteContext PC = new PlanteContext())
            {
                try
                {
                    var mesPlante = PC.plante
                        .Where(p => p.IdPlante == id)
                        .Select(p => p.IdPlante)
                        .FirstOrDefault(); // Get the actual first result

                    plantId = mesPlante ?? string.Empty; // Ensure it's not null
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return plantId;
        }

        public static string getUserIdFromDb(string id)
        {
            string userId = string.Empty;
            using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
            {
                try
                {
                    var myUser = PC.CompteUtilisateur
                        .Where(p => p.IdUtilisateur == id)
                        .Select(p => p.IdUtilisateur)
                        .FirstOrDefault(); // Get the actual first result

                    userId = myUser ?? string.Empty; // Ensure it's not null
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return userId;
        }

        public static string getPasswordFromDb(string password)
        {
            string userId = string.Empty;
            using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
            {
                try
                {
                    var myUser = PC.CompteUtilisateur
                        .Where(p => p.motdepass == password)
                        .Select(p => p.motdepass)
                        .FirstOrDefault(); // Get the actual first result

                    userId = myUser ?? string.Empty; // Ensure it's not null
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return userId;
        }

        public static void chargerListePlantules(DataGrid maGrille)
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesPlante = PC.plante.Select(p => new
                    {
                        p.IdPlante,
                        p.EtatSante,
                        p.DateAjout,
                        p.Provenance,
                        p.Description,
                        p.Stade,
                        p.Entreposage,
                        p.Active_Inactive,
                        p.ItemRetireInventaire,
                        p.Note,
                        p.Responsable,
                    }).ToList();
                    maGrille.ItemsSource = MesPlante;
                    //statusMes.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    // statusMessage.Text = ex.Message;
                    MessageBox.Show(ex.Message);
                }
        }

        /*public static plante trouverPlantuleInfo(string id)
        {
            List<string> listPlantInfo = new List<string>();

            using (PlanteContext PC = new PlanteContext())
            {
                var MesPlante = PC.plante
                        .Include(p => p.Entreposage)
                        .Include(p => p.DateAjout).SingleOrDefault(p => p.IdPlante == id);
                return MesPlante;
            }  
        }*/

        /*public void ChargerListePlantules()
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesPlante = PC.plante.Select(p => new
                    {
                        p.IdPlante,
                        p.EtatSante,
                        p.DateAjout,
                        p.Provenance,
                        p.Description,
                        p.Stade,
                        p.Entreposage,
                        p.Quantite,
                        p.ItemRetireInventaire,
                        p.Responsable,
                        p.Note
                    }).ToList();
                    grillePlante.ItemsSource = MesPlante;
                    statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
        }

        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                var plantes = context.Plantes
                    .Where(p => p.IdPlante == "P1")
                    .Select(p => new { p.IdPlante, p.EtatSante, p.DateAjout })
                    .ToList();
                PlanteDataGrid.ItemsSource = plantes;
            }
        }*/
    }
}
