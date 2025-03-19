using Canabis;
using Canabis.Models;
using OfficeOpenXml.Style;
using QRCoder;
using sommatif3.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

namespace sommatif3
{
    /// <summary>
    /// Interaction logic for AffichePlantule.xaml
    /// </summary>
    public partial class PageAjouterPlantule : Page
    {
        string nomEntrepo = "";
        string planteSante = String.Empty;

        List<string> responsableNom = new List<string>();
        List<string> responsablePrenom = new List<string>();
        List<string> responsableFullName = new List<string>();
        public PageAjouterPlantule()
        {
            InitializeComponent();
            //ChargerListePlantules();
            AjouterElementsAComboBox();
        }

        private void AjouterElementsAComboBox()
        {
            //cbEtatDeSante.Items.Add("rouge");
            //cbEtatDeSante.Items.Add("orange");
            //cbEtatDeSante.Items.Add("jaune");
            //cbEtatDeSante.Items.Add("vert");

            cbStade.Items.Add("initiation");
            cbStade.Items.Add("micro dissection");
            cbStade.Items.Add("magenta");
            cbStade.Items.Add("double magenta");
            cbStade.Items.Add("Hydroponie");

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

        public void chargerEntrepo()
        {
            #region add entrepo from db
            using (EntreposageContext EC = new EntreposageContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesEntrepo = EC.Entreposage.Select(p => new
                    {
                        p.idEntreposage
                    }).ToList();

                    foreach (var m in MesEntrepo)
                    {
                        cbEntreposage.Items.Add(m.idEntreposage.ToString());
                    }

                    statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
            #endregion
        }

        public void chargerResponsableDansComboBox()
        {
            //get nom
            responsableNom = plantuleControler.getUtilisateurName("nom");
            //MessageBox.Show(responsableNom.Count.ToString());

            //get prenom
            responsablePrenom = plantuleControler.getUtilisateurName("prenom");

            //get full name
            for (int i = 0; i < responsableNom.Count; i++)
            {
                responsableFullName.Add(responsablePrenom[i] + " " + responsableNom[i]);
            }

            //fill comboBox
            for (int i = 0; i < responsableNom.Count; i++)
            {
                cbResponsableDecontamination.Items.Add(responsableFullName[i]);
            }

        }

        //trouve Id d'une specialité selectionée dans un combobox
        public int trouveIdSpecialite(string specialite)
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
                    var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    return rechercheSpecialite.Active_Inactive;
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                    return -1;
                }
        }

        //charger data grid
        public void ChargerListePlantules()
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
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
                    grillePlante.ItemsSource = MesPlante;
                    statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
        }

        private void chargerComboBox(ComboBox cb)
        {
            using (PlanteContext SC = new PlanteContext())
            {
                try
                {
                    var MesSpecialite = SC.plante.ToList();

                    if (MesSpecialite.Count > 0)
                    {
                        foreach (plante specialite in MesSpecialite)
                        {
                            cb.Items.Add(specialite.DateAjout.ToString());
                        }
                    }
                    else
                    {
                        statusMessage.Text = "La liste des spécialités est vide.";
                    }
                }
                catch (Exception ex)
                {
                    statusMessage.Text = "Une erreur s'est produite : " + ex.Message;
                }
            }

        }

        private bool ValidateForm()
        {
            // Validate TextBoxes (except tbNote)
            if (string.IsNullOrWhiteSpace(tbProvenance.Text) ||
                string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate ComboBoxes
            if (cbStade.SelectedIndex == -1 || cbEntreposage.SelectedIndex == -1 ||
                cbItemRetireDeLInventaire.SelectedIndex == -1 || cbResponsableDecontamination.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner une option pour tous les menus déroulants.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validate DatePicker
            if (!calendrier.SelectedDate.HasValue)
            {
                MessageBox.Show("Veuillez sélectionner une date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (planteSante == string.Empty || planteSante == null || planteSante == "" || planteSante == " ")
            {
                MessageBox.Show("Veuillez sélectionner l'etat de santé.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


        private void ajouter_plantule(object sender, RoutedEventArgs e)
        {
            //******* Ajouter Plantule
            if (ValidateForm())
            {
                try
                {
                    //utilise le context
                    using (PlanteContext PC = new PlanteContext())
                    {
                        plante newPlante = new plante();
                        String tempId = "SLH" + (plantuleControler.countAllPlantule() + 1);
                        int idDigit = 1;

                        while (plantuleControler.getPlantIdFromDb(tempId) == tempId)
                        {
                            idDigit++;
                            tempId = "SLH" + (plantuleControler.countAllPlantule() + idDigit);
                        }

                        newPlante.IdPlante = tempId;
                        //newPlante.EtatSante = cbEtatDeSante.SelectedItem.ToString()
                        newPlante.EtatSante = planteSante;
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
                        newPlante.Responsable = cbResponsableDecontamination.Text;

                        //add new car to the context/ Table SC: stand for --> specialite context
                        PC.plante.Add(newPlante);

                        //save dans la base de donnee
                        PC.SaveChanges();

                        enregistreHistorique(newPlante.IdPlante);

                        GeneratePrintQRcode();

                        ChargerListePlantules();

                        statusMessage.Text = "Plantule ajoutée";

                    }
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void GeneratePrintQRcode()
        {
            int active_inactive = 0;

            if (rbActif.IsChecked == true)
            {
                active_inactive = 1;
            }
            else if (rbInactif.IsChecked == true)
            {
                active_inactive = 0;
            }
            #region generate qr code
            // Generate the QR code
            QRCodeGenerator QRgen = new QRCodeGenerator();

            //string stringDuQrcode = $"" +
            //    $"SLH{plantuleControler.countAllPlantule().ToString()}, " +
            //    $"{cbEtatDeSante.SelectedItem.ToString()}, " +
            //    $"{(DateTime)calendrier.SelectedDate}, " +
            //    $"{tbProvenance.Text}, " +
            //    $"{tbDescription.Text}, " +
            //    $"{cbStade.SelectedItem.ToString()}, " +
            //    $"{cbEntreposage.SelectedItem.ToString()} " +
            //    $"{active_inactive}, " +
            //    $"{cbItemRetireDeLInventaire.SelectedItem.ToString()}, " +
            //    $"{tbNote.Text}, " +
            //    $"{tbResponsableDecontamination.Text}, ";

            string stringDuQrcode = $"" +
                $"SLH{plantuleControler.countAllPlantule().ToString()}, " +
                $"{planteSante}, " +
                $"{(DateTime)calendrier.SelectedDate}, " +
                $"{tbProvenance.Text}, " +
                $"{tbDescription.Text}, " +
                $"{cbStade.SelectedItem.ToString()}, " +
                $"{cbEntreposage.SelectedItem.ToString()} " +
                $"{active_inactive}, " +
                $"{cbItemRetireDeLInventaire.SelectedItem.ToString()}, " +
                $"{tbNote.Text}, " +
                $"{cbResponsableDecontamination.Text}, ";

            QRCodeData QRdata = QRgen.CreateQrCode(stringDuQrcode, QRCodeGenerator.ECCLevel.H);

            QRCode QRcode = new QRCode(QRdata);

            // Convert the QR code to a bitmap
            Bitmap qrCodeImage = QRcode.GetGraphic(20);

            // Convert the bitmap to a BitmapSource that can be used in WPF
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                qrCodeImage.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions()
            );

            // Dispose of the bitmap to avoid memory leaks
            qrCodeImage.Dispose();

            // Set the Image control's source to the BitmapSource
            qrCodeImageBox.Source = bitmapSource;

            #endregion
        }

        private BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }

        private void ajouter_entrepo_click(object sender, RoutedEventArgs e)
        {
            string promptMessage = "Entrer le code de l'entrepos:";
            string title = "Ajouter un entrepo";
            string entrepoCode = Microsoft.VisualBasic.Interaction.InputBox(promptMessage, title, "");
            string nomEntrepo = Microsoft.VisualBasic.Interaction.InputBox("Entrer le nom de l'entrepos:", title, "");

            // Check if the input is not empty
            if (!string.IsNullOrEmpty(entrepoCode))
            {
                cbEntreposage.Items.Add(entrepoCode);
                MessageBox.Show("Entrepos Ajouté", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                #region add entrepo to db
                try
                {
                    //utilise le context
                    using (EntreposageContext EC = new EntreposageContext())
                    {
                        Entreposage newEntrepo = new Entreposage();

                        //newPlante.IdPlante = tbIdentification.Text;
                        newEntrepo.idEntreposage = entrepoCode;
                        newEntrepo.nom = nomEntrepo;

                        //add new car to the context/ Table SC: stand for --> specialite context
                        EC.Entreposage.Add(newEntrepo);
                        //save dans la base de donnee
                        EC.SaveChanges();

                        //GeneratePrintQRcode();

                        //ChargerListePlantules();

                        //viderToutLesComboBox();
                        //chargerComboBox(cbMedecinSpecialite);
                        //chargerComboBox(cbSpecialiteConsultation);
                        statusMessage.Text = "Plantule ajoutée";

                    }
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                    // MessageBox.Show("s'assurer de mettre l'Id de l'utilisateur sur le champ Responsable");
                    MessageBox.Show(ex.Message.ToString());
                }
                #endregion
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

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
            grillePlante.ItemsSource = null;
            tbProvenance.Clear();
            tbDescription.Clear();
            tbNote.Clear();
            qrCodeImageBox.Source = null;

            responsableNom.Clear();
            responsablePrenom.Clear();
            responsableFullName.Clear();
            cbResponsableDecontamination.Items.Clear();
            plantuleControler.setSanteColorUi("#6C0D92", "#A85ED0", borderSanteIndicator);
        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            responsableNom.Clear();
            responsablePrenom.Clear();
            responsableFullName.Clear();
            cbResponsableDecontamination.Items.Clear();
            cbEntreposage.Items.Clear();
            plantuleControler.setSanteColorUi("#6C0D92", "#A85ED0", borderSanteIndicator);

            ChargerListePlantules();
            chargerEntrepo();
            chargerResponsableDansComboBox();
        }

        private void sante_vert_click(object sender, RoutedEventArgs e)
        {
            planteSante = "vert";

            // Define the gradient brush for visual feedback
            plantuleControler.setSanteColorUi("#11d171", "#3ea882", borderSanteIndicator);
        }

        private void sante_jaune_click(object sender, RoutedEventArgs e)
        {
            planteSante = "jaune";

            // Define the gradient brush
            plantuleControler.setSanteColorUi("#D6DE16", "#E1E489", borderSanteIndicator);
        }

        private void sante_orange_click(object sender, RoutedEventArgs e)
        {
            planteSante = "orange";
            // Define the gradient brush
            plantuleControler.setSanteColorUi("#E29A3B", "#ECC48F", borderSanteIndicator);
        }

        private void sante_rouge_click(object sender, RoutedEventArgs e)
        {
            planteSante = "rouge";
            // Define the gradient brush
            plantuleControler.setSanteColorUi("#DE3333", "#E87777", borderSanteIndicator);
        }
    }
}
