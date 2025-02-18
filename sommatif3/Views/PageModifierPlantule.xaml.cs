using Canabis.Models;
using sommatif3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Canabis.Views
{
    /// <summary>
    /// Interaction logic for PageModifierPlantule.xaml
    /// </summary>
    public partial class PageModifierPlantule : Page
    {
        List<string> listInformation = new List<string>();
        public PageModifierPlantule()
        {
            InitializeComponent();
            AjouterElementsAComboBox();
        }

        private void AjouterElementsAComboBox()
        {
            cbEtatDeSante.Items.Add("rouge");
            cbEtatDeSante.Items.Add("orange");
            cbEtatDeSante.Items.Add("jaune");
            cbEtatDeSante.Items.Add("vert");

            cbStade.Items.Add("initiation");
            cbStade.Items.Add("micro dissection");
            cbStade.Items.Add("magenta");
            cbStade.Items.Add("double magenta");
            cbStade.Items.Add("hydroponie");

            cbEntreposage.Items.Add("B3200");
            cbEntreposage.Items.Add("B3080.01");
            cbEntreposage.Items.Add("B3070");
            cbEntreposage.Items.Add("F1260.01");
            cbEntreposage.Items.Add("F1260.04");
            cbEntreposage.Items.Add("B3320");

            cbItemRetireDeLInventaire.Items.Add("DESTRUCTION PAR AUTOCLAVE");
            cbItemRetireDeLInventaire.Items.Add("TRANSFERT CLIENT");
            cbItemRetireDeLInventaire.Items.Add("TRANSFERT AUTRE CENTRE");
            cbItemRetireDeLInventaire.Items.Add("TRANSFERT POUR ANALYSE");
            cbItemRetireDeLInventaire.Items.Add("ANALYSE");
            cbItemRetireDeLInventaire.Items.Add("CONTAMINATION");
            cbItemRetireDeLInventaire.Items.Add("LIMITATION DE LA LICENCE");
            cbItemRetireDeLInventaire.Items.Add("PERTE DE L'ÉCHANTILLION");
            cbItemRetireDeLInventaire.Items.Add("PLANTULES N'ONT PAS SURVÉCU À L'EXPÉRIENCE");
            cbItemRetireDeLInventaire.Items.Add("AUTRE (INDIQUER LA RAISON DANS NOTE)");
        }

        private void btRecherche_Click(object sender, RoutedEventArgs e)
        {
            listInformation = plantuleControler.trouverPlantuleInfo(tbIdentification.Text);
            //MessageBox.Show("size: "+listInformation.Count);

            /*lbEtatSante.Content = listInformation[0];
            lbDate.Content = listInformation[1];
            lbProvenance.Content = listInformation[2];
            lbDescription.Content = listInformation[3];
            lbStade.Content = listInformation[4];
            lbEntreposage.Content = listInformation[5];
            lbQuantiteActif_inActif.Content = listInformation[6];
            lbItemRetireInventaire.Content = listInformation[7];
            lbResponsable.Content = listInformation[8];
            tbNote.Text = listInformation[9];*/

            cbEtatDeSante.SelectedItem = listInformation[0].ToLower();
            calendrier.SelectedDate = DateTime.Parse(listInformation[1]);
            tbProvenance.Text = listInformation[2];
            tbDescription.Text = listInformation[3];
            cbStade.SelectedItem = listInformation[4].ToLower();
            cbEntreposage.SelectedItem = listInformation[5];
            if (listInformation[6] == "1")
            {
                rbActif.IsChecked = true;
                rbInactif.IsChecked = false;
            }
            else if (listInformation[6] == "0")
            {
                rbInactif.IsChecked = true;
                rbActif.IsChecked = false;
            }
            cbItemRetireDeLInventaire.SelectedItem = listInformation[7];
            tbNote.Text = listInformation[9];
            tbResponsableDecontamination.Text = listInformation[8];

            //grillePlante.ItemsSource = listInformation;
            plantuleControler.trouvePlantETChargerSurDataGrid(tbIdentification.Text, grillePlante);

            listInformation.Clear();
            plantuleControler.trouverPlantuleInfo(tbIdentification.Text).Clear();
        }

        private void modifierPlant_Click(object sender, RoutedEventArgs e)
        {
            if (tbIdentification.Text != "")
            {
                try
                {
                    //utilise le context
                    using (PlanteContext PC = new PlanteContext())
                    {
                        plante newPlante = PC.plante.FirstOrDefault(p => p.IdPlante.Equals(tbIdentification.Text));

                        if (newPlante != null)
                        {
                            //save old data pour l'historique
                            HistoriqueControler.listAncienneValeur = plantuleControler.trouverPlantuleInfo(tbIdentification.Text);
                            //identifier l'action pour l'historique
                            HistoriqueControler.action = "modification";


                            newPlante.IdPlante = tbIdentification.Text;
                            newPlante.EtatSante = cbEtatDeSante.SelectedItem.ToString();
                            //newPlante.DateAjout = calendrier.SelectedDate.Value.ToShortDateString();
                            newPlante.DateAjout = (DateTime)calendrier.SelectedDate;
                            newPlante.Provenance = tbProvenance.Text;
                            newPlante.Description = tbDescription.Text;
                            newPlante.Stade = cbStade.SelectedItem.ToString();
                            newPlante.Entreposage = cbEntreposage.SelectedItem.ToString();
                            if (rbActif.IsChecked == true)
                            {
                                newPlante.Active_Inactive = 1;
                            }
                            else if (rbInactif.IsChecked == true)
                            {
                                newPlante.Active_Inactive = 0;
                            }
                            newPlante.ItemRetireInventaire = cbItemRetireDeLInventaire.SelectedItem.ToString();
                            newPlante.Note = tbNote.Text;
                            newPlante.Responsable = tbResponsableDecontamination.Text;

                            //save dans la base de donnee
                            PC.SaveChanges();

                            plantuleControler.trouvePlantETChargerSurDataGrid(tbIdentification.Text, grillePlante);

                            //save new value for l'historique
                            HistoriqueControler.listNouvelleValeur = plantuleControler.trouverPlantuleInfo(tbIdentification.Text);

                            HistoriqueControler.trouveToutChampModifier(HistoriqueControler.listAncienneValeur, HistoriqueControler.listNouvelleValeur);
                            enregistreHistorique();

                            //viderToutLesComboBox();
                            //chargerComboBox(cbMedecinSpecialite);
                            //chargerComboBox(cbSpecialiteConsultation);
                            //statusMessage.Text = "Plantule modifiée";
                        }
                        else
                        {
                            //statusMessage.Text = "ID invalide";
                            MessageBox.Show("ID invalide");
                        }

                    }
                }
                catch (Exception ex)
                {
                    //statusMessage.Text = ex.Message;
                    //MessageBox.Show(ex.Message);
                }
            }
            else
            {
                //statusMessage.Text = "aucun champ ne doit etre vide";
                MessageBox.Show("aucun champ ne doit etre vide");
            }
        }

        public void historiqueModification()
        {
            /*HistoriqueControler.indexDucahmpModifie = HistoriqueControler.retourneIndexDuChampModifier(HistoriqueControler.listAncienneValeur, HistoriqueControler.listNouvelleValeur);

            switch (HistoriqueControler.indexDucahmpModifie)
            {
                case 0:
                    HistoriqueControler.champ = "etatSante";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[0];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[0];
                    break;
                case 1:
                    HistoriqueControler.champ = "dateAjout";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[1];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[1];
                    break;
                case 2:
                    HistoriqueControler.champ = "provenance";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[2];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[2];
                    break;
                case 3:
                    HistoriqueControler.champ = "description";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[3];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[3];
                    break;
                case 4:
                    HistoriqueControler.champ = "stade";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[4];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[4];
                    break;
                case 5:
                    HistoriqueControler.champ = "entreposage";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[5];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[5];
                    break;
                case 6:
                    HistoriqueControler.champ = "active_Inactive";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[6];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[6];
                    break;
                case 7:
                    HistoriqueControler.champ = "itemRetireInventaire";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[7];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[7];
                    break;
                case 8:
                    HistoriqueControler.champ = "note";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[8];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[8];
                    break;
                case 9:
                    HistoriqueControler.champ = "Responsable";
                    HistoriqueControler.ancienneValeur = HistoriqueControler.listAncienneValeur[9];
                    HistoriqueControler.nouvelleValeur = HistoriqueControler.listAncienneValeur[9];
                    break;
                default:
                    Console.WriteLine("Invalid day value");
                    break;
            }*/
            
        }

        public void enregistreHistorique()
        {
            //historiqueModification();

            try
            {
                using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
                {
                    HistoriquePlante newPlanteArchive = new HistoriquePlante();

                    newPlanteArchive.Id = HistoriqueControler.countAllHistoriqueRecord() + 1;
                    newPlanteArchive.IdPlante = tbIdentification.Text.ToUpper();
                    newPlanteArchive.Action = HistoriqueControler.action;
                    //newPlante.DateAjout = calendrier.SelectedDate.Value.ToShortDateString();
                    newPlanteArchive.Date = DateTime.Today;
                    newPlanteArchive.Champ = HistoriqueControler.champ;
                    newPlanteArchive.AncienneValeur = HistoriqueControler.ancienneValeur;
                    newPlanteArchive.NouvelleValeur = HistoriqueControler.nouvelleValeur;

                    //save dans la base de donnee
                    PC.HistoriquePlante.Add(newPlanteArchive);
                    PC.SaveChanges();

                    HistoriqueControler.listAncienneValeur.Clear();
                    HistoriqueControler.listAncienneValeur.Clear();
                    plantuleControler.trouverPlantuleInfo(tbIdentification.Text).Clear();

                    listInformation.Clear();
                   // MessageBox.Show("Plantule archivée");
                }
            }
            catch (Exception ex)
            {
                //statusMessage.Text = ex.Message;
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
