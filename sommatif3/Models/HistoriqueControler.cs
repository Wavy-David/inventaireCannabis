using Microsoft.EntityFrameworkCore;
using sommatif3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis.Models
{
    public static class HistoriqueControler
    {
        public static List<string> listAncienneValeur = new List<string>();
        public static List<string> listNouvelleValeur = new List<string>();
        public static string champ = "";
        public static string ancienneValeur = "";
        public static string nouvelleValeur = "";
        public static string action = "";
        public static int indexDucahmpModifie = -1;

        //if return -1 there is zero modifications
        public static void trouveToutChampModifier(List<string> listAncienneVal, List<string> listNouvelleVal)
        {
            /*for (int i = 0; i < listAncienneVal.Count; i++)
            {
                //return index of the field that was modified
                if (listAncienneVal[i] != listNouvelleVal[i])
                {
                    return i;
                }
            }
            return -1;*/

            for (int i = 0; i < listAncienneVal.Count; i++)
            {
                //return index of the field that was modified
                if (listAncienneVal[i] != listNouvelleVal[i])
                {
                    switch (i)
                    {
                        case 0:
                            HistoriqueControler.champ += "etatSante__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[0] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[0] + "__, ";
                            break;
                        case 1:
                            HistoriqueControler.champ += "dateAjout__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[1] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[1] + "__, ";
                            break;
                        case 2:
                            HistoriqueControler.champ += "provenance__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[2] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[2] + "__, ";
                            break;
                        case 3:
                            HistoriqueControler.champ += "description__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[3] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[3] + "__, ";
                            break;
                        case 4:
                            HistoriqueControler.champ += "stade__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[4] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[4] + "__, ";
                            break;
                        case 5:
                            HistoriqueControler.champ += "entreposage__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[5] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[5] + "__, ";
                            break;
                        case 6:
                            HistoriqueControler.champ += "active_Inactive__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[6] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[6] + "__, ";
                            break;
                        case 7:
                            HistoriqueControler.champ += "itemRetireInventaire__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[7] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[7] + "__, ";
                            break;
                        case 8:
                            HistoriqueControler.champ += "Responsable__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[8] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[8] + "__, ";
                            break;
                        case 9:
                            HistoriqueControler.champ += "note__, ";
                            HistoriqueControler.ancienneValeur += HistoriqueControler.listAncienneValeur[9] + "__, ";
                            HistoriqueControler.nouvelleValeur += HistoriqueControler.listNouvelleValeur[9] + "__, ";
                            break;
                        default:
                            Console.WriteLine("Invalid day value");
                            break;
                    }
                }
            }

            /*public static List<string> trouverPlantuleInfo(string id)
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


                        //listPlantInfo.Add(MesPlante.E);

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
            }*/
        }

        public static int countAllHistoriqueRecord()
        {
            using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
            {
                int count = PC.HistoriquePlante.Count(p => p.IdPlante != "");

                return count;
            }
        }
    }
}
