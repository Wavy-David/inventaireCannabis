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
    /// Interaction logic for PageConsultationPlantule.xaml
    /// </summary>
    public partial class PageConsultationPlantule : Page
    {
        List<string> listInformation = new List<string>();
        public PageConsultationPlantule()
        {
            InitializeComponent();
        }

        private void btRecherche_Click(object sender, RoutedEventArgs e)
        {
            listInformation = plantuleControler.trouverPlantuleInfo(tbId.Text);

            lbEtatSante.Content = listInformation[0];
            lbDate.Content = listInformation[1];
            lbProvenance.Content = listInformation[2];
            lbDescription.Content = listInformation[3];
            lbStade.Content = listInformation[4];
            lbEntreposage.Content = listInformation[5];
            lbQuantiteActif_inActif.Content = listInformation[6];
            lbItemRetireInventaire.Content = listInformation[7];
            lbResponsable.Content = listInformation[8];
            tbNote.Text = listInformation[9]; 

            enregistreHistorique();

            listInformation.Clear();
            plantuleControler.trouverPlantuleInfo(tbId.Text).Clear();
        }

        public void enregistreHistorique()
        {
            //historiqueModification();
            string myId = tbId.Text;

            try
            {
                using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
                {
                    HistoriquePlante newPlanteArchive = new HistoriquePlante();

                    newPlanteArchive.Id = HistoriqueControler.countAllHistoriqueRecord() + 1;
                    newPlanteArchive.IdPlante = myId.ToUpper();
                    newPlanteArchive.Action = "Consulté";
                    //newPlante.DateAjout = calendrier.SelectedDate.Value.ToShortDateString();
                    newPlanteArchive.Date = DateTime.Today;
                    newPlanteArchive.Champ = "--";
                    newPlanteArchive.AncienneValeur = "--";
                    newPlanteArchive.NouvelleValeur = "__";

                    //save dans la base de donnee
                    PC.HistoriquePlante.Add(newPlanteArchive);
                    PC.SaveChanges();

                    HistoriqueControler.listAncienneValeur.Clear();
                    HistoriqueControler.listAncienneValeur.Clear();
                    plantuleControler.trouverPlantuleInfo(myId).Clear();

                    //listInformation.Clear();
                    //MessageBox.Show("Plantule archivée");
                }
            }
            catch (Exception ex)
            {
                //statusMessage.Text = ex.Message;
                //MessageBox.Show(ex.Message);
            }
        }

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
            tbId.Clear();
            tbNote.Text = string.Empty;
            lbEntreposage.Content = string.Empty;
            lbDescription.Content = string.Empty;
            lbDate.Content = string.Empty;
            lbEtatSante.Content = string.Empty;
            lbItemRetireInventaire.Content = string.Empty;
            lbProvenance.Content = string.Empty;
            lbQuantiteActif_inActif.Content = string.Empty;
            lbResponsable.Content = string.Empty;
            lbStade.Content = string.Empty;
        }
    }
}
