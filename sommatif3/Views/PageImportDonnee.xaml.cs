using System;
using System.Collections.Generic;
using System.Data;
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
using OfficeOpenXml;
using Microsoft.Win32;
using System.IO;
using sommatif3.Models;
using Canabis.Models;
using System.ComponentModel.DataAnnotations;

namespace Canabis.Views
{
    /// <summary>
    /// Interaction logic for PageImportDonnee.xaml
    /// </summary>
    public partial class PageImportDonnee : Page
    {
        public List<plante> listMesPlantules  = new List<plante>();
        public PageImportDonnee()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                //list plante from excel
                listMesPlantules = ReadExcelFile(filePath);

                //import from excel to db
                importDonneeDansDb(listMesPlantules);

                //Display db
                plantuleControler.chargerListePlantules(grilleImport);

                //DataTable dataTable = ReadExcelFile(filePath);
                //DataGrid.ItemsSource = dataTable.DefaultView;
                /*MessageBox.Show(ReadExcelFile(filePath)[0].IdPlante);
                MessageBox.Show(ReadExcelFile(filePath)[0].EtatSante);
                MessageBox.Show(ReadExcelFile(filePath)[1].IdPlante);
                MessageBox.Show(ReadExcelFile(filePath)[1].EtatSante);*/
            }
        }

        public void enregistreHistorique(string plantId)
        {
            //historiqueModification();
            //string myId = "SLH" + (plantuleControler.countAllPlantule() + 1);

            try
            {
                using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
                {
                    HistoriquePlante newPlanteArchive = new HistoriquePlante();

                    newPlanteArchive.Id = HistoriqueControler.countAllHistoriqueRecord() + 1;
                    newPlanteArchive.IdPlante = plantId.ToUpper();
                    newPlanteArchive.Action = "ajout";
                    newPlanteArchive.Date = DateTime.Today;
                    newPlanteArchive.Champ = "--";
                    newPlanteArchive.AncienneValeur = "--";
                    newPlanteArchive.NouvelleValeur = "__";

                    //save dans la base de donnee
                    PC.HistoriquePlante.Add(newPlanteArchive);
                    PC.SaveChanges();

                    HistoriqueControler.listAncienneValeur.Clear();
                    HistoriqueControler.listAncienneValeur.Clear();
                    plantuleControler.trouverPlantuleInfo(plantId).Clear();
                }
            }
            catch (Exception ex)
            {
                //statusMessage.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }
        }

        private List<plante> ReadExcelFile(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

            var records = new List<plante>();

            FileInfo fileInfo = new FileInfo(filePath);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Start reading from the second row to skip headers
                for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var row = new plante{
                        EtatSante = worksheet.Cells[rowNum, 1].Text,
                        DateAjout = DateTime.Parse(worksheet.Cells[rowNum, 2].Text),
                        IdPlante = worksheet.Cells[rowNum, 3].Text,
                        Provenance = worksheet.Cells[rowNum, 4].Text,
                        Description = worksheet.Cells[rowNum, 5].Text,
                        Stade = worksheet.Cells[rowNum, 6].Text,
                        Entreposage = worksheet.Cells[rowNum, 7].Text,
                        //QteAjoutee = worksheet.Cells[rowNum, 8].Text,
                        //QteRetiree = worksheet.Cells[rowNum, 9].Text,
                        //ItemRetireDeLInventaire1 = worksheet.Cells[rowNum, 10].Text,
                        //ItemRetireDeLInventaire2 = worksheet.Cells[rowNum, 11].Text,
                        //ResponsableDecontamination = worksheet.Cells[rowNum, 12].Text,
                        //Note = worksheet.Cells[rowNum, 13].Text
                        Active_Inactive = int.Parse(worksheet.Cells[rowNum, 8].Text),
                        ItemRetireInventaire = worksheet.Cells[rowNum, 9].Text,
                        Note = worksheet.Cells[rowNum, 10].Text,
                        Responsable = worksheet.Cells[rowNum, 11].Text
                    };

                    records.Add(row);
                }
            }

            return records;
        }

        private void importDonneeDansDb(List<plante> mesPlantes)
        {
            //Add data to database
            foreach (plante maPlante in mesPlantes)
            {
                try
                {
                    //utilise le context
                    using (PlanteContext PC = new PlanteContext())
                    {
                        plante newPlante = new plante();

                        newPlante.IdPlante = maPlante.IdPlante;
                        newPlante.EtatSante = maPlante.EtatSante;
                        newPlante.DateAjout = maPlante.DateAjout;
                        newPlante.Provenance = maPlante.Provenance;
                        newPlante.Description = maPlante.Description;
                        newPlante.Stade = maPlante.Stade;
                        newPlante.Entreposage = maPlante.Entreposage;
                        newPlante.Active_Inactive = maPlante.Active_Inactive;
                        newPlante.ItemRetireInventaire = maPlante.ItemRetireInventaire;
                        newPlante.Note = maPlante.Note;
                        newPlante.Responsable = maPlante.Responsable;

                        //add new car to the context/ Table SC: stand for --> specialite context
                        PC.plante.Add(newPlante);
                        //save dans la base de donnee
                        PC.SaveChanges();

                        plantuleControler.chargerListePlantules(grilleImport);
                        enregistreHistorique(newPlante.IdPlante);

                        //viderToutLesComboBox();
                        //chargerComboBox(cbMedecinSpecialite);
                        //chargerComboBox(cbSpecialiteConsultation);
                        //statusMessage.Text = "Plantule ajoutée";

                    }
                }
                catch (Exception ex)
                {
                    //statusMessage.Text = ex.Message;
                    MessageBox.Show("s'assurer de mettre l'Id de l'utilisateur sur le champ Responsable");
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
        }
    }
}
