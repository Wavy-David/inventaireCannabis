using Canabis.Models;
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
        public PageAjouterPlantule()
        {
            InitializeComponent();

            //charger datagrid
            //ChargerListeMedecin();
            //charger datagrid
            ChargerListePlantules();

            //viderToutLesComboBox();
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
            cbStade.Items.Add("Hydroponie");

            /*cbEntreposage.Items.Add("B3200");
            cbEntreposage.Items.Add("B3080.01");
            cbEntreposage.Items.Add("B3070");
            cbEntreposage.Items.Add("F1260.01");
            cbEntreposage.Items.Add("F1260.04");
            cbEntreposage.Items.Add("B3320");*/

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

        /*public void ChargerListeMedecin()
        {
            using (MedecinContext MC = new MedecinContext())
                try
                {
                    
                    var MesMedecins = MC.medecin.ToList();
                    grille.ItemsSource = MesMedecins;
                    statusMessage.Text = "Liste des Medecins chargé  e";

                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
        }*/

        /*//charger data grid
        public void ChargerListePlantules()
        {
            using (PlanteContext PC = new PlanteContext())
                try
                {
                    var MesPlante = PC.plante.ToList();
                    grillePlante.ItemsSource = MesPlante;
                    statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
        }*/

        //charger data grid
        public void ChargerListePlantules()
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

        private void ajouter_plantule(object sender, RoutedEventArgs e)
        {
            
            // Récupérer la date sélectionnée
            //DateTime? dateSelectionnee = calendrier.SelectedDate;

            //******* Ajouter Plantule
            try
            {
                //utilise le context
                using (PlanteContext PC = new PlanteContext())
                {
                    plante newPlante = new plante();

                    //newPlante.IdPlante = tbIdentification.Text;
                    newPlante.IdPlante = "SLH" + (plantuleControler.countAllPlantule() + 1);
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

                    //add new car to the context/ Table SC: stand for --> specialite context
                    PC.plante.Add(newPlante);
                    //save dans la base de donnee
                    PC.SaveChanges();

                    enregistreHistorique();

                    GeneratePrintQRcode();

                    ChargerListePlantules();

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

            //****

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
            /*QRCoder.QRCodeGenerator QRgen = new QRCoder.QRCodeGenerator();
            var QRdata = QRgen.CreateQrCode(tbIdentification.Text, QRCoder.QRCodeGenerator.ECCLevel.H);
            var QRcode = new QRCoder.QRCode(QRdata);

            qrCodeImageBox.DataContext = QRcode;*/

            // Generate the QR code
            QRCodeGenerator QRgen = new QRCodeGenerator();

            string stringDuQrcode = $"" +
                $"SLH{plantuleControler.countAllPlantule().ToString()}, " +
                $"{cbEtatDeSante.SelectedItem.ToString()}, " +
                $"{(DateTime)calendrier.SelectedDate}, " +
                $"{tbProvenance.Text}, " +
                $"{tbDescription.Text}, " +
                $"{cbStade.SelectedItem.ToString()}, " +
                $"{cbEntreposage.SelectedItem.ToString()} " +
                $"{active_inactive}, " +
                $"{cbItemRetireDeLInventaire.SelectedItem.ToString()}, " +
                $"{tbNote.Text}, " +
                $"{tbResponsableDecontamination.Text}, ";

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
            //MessageBox.Show(entreposName);

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

        public void enregistreHistorique()
        {
            //historiqueModification();
            string myId = "SLH" + (plantuleControler.countAllPlantule() + 1);

            try
            {
                using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
                {
                    HistoriquePlante newPlanteArchive = new HistoriquePlante();

                    newPlanteArchive.Id = HistoriqueControler.countAllHistoriqueRecord() + 1;
                    newPlanteArchive.IdPlante = myId.ToUpper();
                    newPlanteArchive.Action = "ajout";
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

        /*        private void modifierPlant(object sender, RoutedEventArgs e)
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

                                    //save dans la base de donnee
                                    PC.SaveChanges();

                                    ChargerListePlantules();

                                    //viderToutLesComboBox();
                                    //chargerComboBox(cbMedecinSpecialite);
                                    //chargerComboBox(cbSpecialiteConsultation);
                                    statusMessage.Text = "Plantule modifiée";
                                }
                                else
                                {
                                    statusMessage.Text = "ID invalide";
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            statusMessage.Text = ex.Message;
                        }
                    }
                    else
                    {
                        statusMessage.Text = "aucun champ ne doit etre vide";
                    }

                }

                private void suprimerPlantule(object sender, RoutedEventArgs e)
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
                                    PC.plante.Remove(newPlante);
                                    PC.SaveChanges();

                                    ChargerListePlantules();

                                    //viderToutLesComboBox();
                                    //chargerComboBox(cbMedecinSpecialite);
                                    //chargerComboBox(cbSpecialiteConsultation);
                                    statusMessage.Text = "Plantule suprimée";
                                }
                                else
                                {
                                    statusMessage.Text = "ID invalide";
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            statusMessage.Text = ex.Message;
                        }
                    }
                    else
                    {
                        statusMessage.Text = "aucun champ ne doit etre vide";
                    }
                }*/


        /*private void Ajouter_Medecin_Click(object sender, RoutedEventArgs e)
        {
            if (tbMedecinId.Text != "" && tbMedecinNom.Text != "" && tbMedecinPrenom.Text != "" && tbMedecinTelephone.Text != "" && tbMedecinSalaire.Text != "" && cbMedecinSpecialite.SelectedItem != null)
            {
                try
                {
                    // Utilisation du context pour se connecter a la BD
                    using (MedecinContext medecinContext = new MedecinContext())
                    {
                        medecin nouveauMedecin = new medecin();
                        nouveauMedecin.MedecinId = int.Parse(tbMedecinId.Text);
                        nouveauMedecin.MedecinNom = tbMedecinNom.Text;
                        nouveauMedecin.MedecinPrenom = tbMedecinPrenom.Text;
                        nouveauMedecin.MedecinTelephone = long.Parse(tbMedecinTelephone.Text);
                        nouveauMedecin.SpecialiteId = trouveIdSpecialite(cbMedecinSpecialite.SelectedItem.ToString());
                        nouveauMedecin.SpecialiteNom = cbMedecinSpecialite.SelectedItem.ToString();
                        nouveauMedecin.MedecinSalaire = decimal.Parse(tbMedecinSalaire.Text);

                        medecinContext.medecin.Add(nouveauMedecin);
                        medecinContext.SaveChanges();
                        ChargerListeMedecin();
                        statusMessage.Text = "Nouveau medecin ajoutee!";

                    }

                }
                catch (Exception ex)
                {
                    statusMessage.Text = ex.Message;
                }
            }
            else
            {
                statusMessage.Text = "aucun champ ne doit etre vide";
            }
            
        }
*/
        /*private void Modifier_Medecin_Click(object sender, RoutedEventArgs e)
        {
            if (tbMedecinId.Text != "" && tbMedecinNom.Text != "" && tbMedecinPrenom.Text != "" && tbMedecinTelephone.Text != "" && tbMedecinSalaire.Text != "" && cbMedecinSpecialite.SelectedItem != null)
            {
                try
                {
                    using (MedecinContext medecinContext = new MedecinContext())
                    {
                        medecin medecinAModifier = medecinContext.medecin.FirstOrDefault(med => med.MedecinId.Equals(int.Parse(tbMedecinId.Text)));
                        if (medecinAModifier != null)
                        {
                            medecinAModifier.MedecinNom = tbMedecinNom.Text;
                            medecinAModifier.MedecinPrenom = tbMedecinPrenom.Text;
                            medecinAModifier.MedecinTelephone = long.Parse(tbMedecinTelephone.Text);
                            medecinAModifier.MedecinSalaire = decimal.Parse(tbMedecinSalaire.Text);
                            medecinAModifier.SpecialiteId = trouveIdSpecialite(cbMedecinSpecialite.SelectedItem.ToString());
                            medecinAModifier.SpecialiteNom = cbMedecinSpecialite.SelectedItem.ToString();

                            medecinContext.SaveChanges();

                            ChargerListeMedecin();
                            statusMessage.Text = "Medecin modifie!";
                        }
                        else
                        {
                            statusMessage.Text = "Aucun medecin avec cet ID";
                        }

                    }
                }
                catch (Exception exp)
                {
                    statusMessage.Text = exp.Message;
                }
            }
            else
            {
                statusMessage.Text = "aucun champ ne doit etre vide";
            }
        }*/

        /*private void Spprimer_Medecin_Click(object sender, RoutedEventArgs e)
        {
            if(tbMedecinId.Text != "")
            {
                try
                {
                    using (MedecinContext medecinContext = new MedecinContext())
                    {
                        medecin medecinASupprimer = medecinContext.medecin.FirstOrDefault(med => med.MedecinId.Equals(int.Parse(tbMedecinId.Text)));
                        if (medecinASupprimer != null)
                        {
                            medecinContext.medecin.Remove(medecinASupprimer);
                            medecinContext.SaveChanges();
                            ChargerListeMedecin();
                            statusMessage.Text = "Medecin Supprimee!";
                        }
                        else
                        {
                            statusMessage.Text = "Aucun medecin avec cet ID";
                        }
                    }
                }
                catch (Exception exp)
                {
                    statusMessage.Text = exp.Message;
                }
            }
            else
            {
                statusMessage.Text = "le champ identifiant medecin ne doit etre vide";
            } 
        }*/

        /*private void rechercheConsultation(object sender, RoutedEventArgs e)
        {
            if(tbSalaireTrie.Text != "" && cbSpecialiteConsultation.SelectedItem != null)
            {
                int IdspecialiteSelectione = trouveIdSpecialite(cbSpecialiteConsultation.SelectedItem.ToString());
                using (MedecinContext MC = new MedecinContext())
                    try
                    {
                        if (operateurLogicWhere == "==")
                        {
                            var MesMedecins = MC.medecin.Where(m => m.SpecialiteId == IdspecialiteSelectione && m.MedecinSalaire == Decimal.Parse(tbSalaireTrie.Text)).ToList();
                            grilleConsultation.ItemsSource = MesMedecins;
                            statusMessage.Text = "Liste des Medecins chargée";
                        }
                        else if (operateurLogicWhere == ">")
                        {
                            var MesMedecins = MC.medecin.Where(m => m.SpecialiteId == IdspecialiteSelectione && m.MedecinSalaire > Decimal.Parse(tbSalaireTrie.Text)).ToList();
                            grilleConsultation.ItemsSource = MesMedecins;
                            statusMessage.Text = "Liste des Medecins chargée";
                        }
                        else
                        {
                            var MesMedecins = MC.medecin.Where(m => m.SpecialiteId == IdspecialiteSelectione && m.MedecinSalaire < Decimal.Parse(tbSalaireTrie.Text)).ToList();
                            grilleConsultation.ItemsSource = MesMedecins;
                            statusMessage.Text = "Liste des Medecins chargée";
                        }


                    }
                    catch (Exception ex)
                    {
                        statusMessage.Text = ex.Message;
                    }
            }
            else
            {
                statusMessage.Text = "aucun champ ne doit etre vide";
            }
        }*/

        /*private void rbSuperieur_Checked(object sender, RoutedEventArgs e)
        {
            operateurLogicWhere = ">";
        }

        private void rbEgal_Checked(object sender, RoutedEventArgs e)
        {
            operateurLogicWhere = "==";
        }

        private void rbInferieur_Checked(object sender, RoutedEventArgs e)
        {
            operateurLogicWhere = "<";
        }

        public void viderToutLesComboBox()
        {
            cbSpecialiteConsultation.Items.Clear();
            cbMedecinSpecialite.Items.Clear();
        }*/
    }
}
