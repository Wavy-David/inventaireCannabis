using Canabis.Models;
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
    /// Interaction logic for PageInscription.xaml
    /// </summary>
    public partial class PageInscription : Page
    {
        public PageInscription()
        {
            InitializeComponent();
        }

        private void btEnregistrerUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
                {
                    CompteUtilisateur newPlanteArchive = new CompteUtilisateur();

                    newPlanteArchive.IdUtilisateur = "U"+ tbnom.Text+ (plantuleControler.countAllUser() + 1);
                    newPlanteArchive.Nom = tbnom.Text;
                    newPlanteArchive.Prenom = tbPrenom.Text;
                    //newPlante.DateAjout = calendrier.SelectedDate.Value.ToShortDateString();
                    newPlanteArchive.Email = tbEmail.Text;
                    newPlanteArchive.MotDePasse = PasswordBox.Password;

                    //save dans la base de donnee
                    PC.CompteUtilisateur.Add(newPlanteArchive);
                    PC.SaveChanges();


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
    }
}
