using Canabis.Models;
using sommatif3;
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
    /// Interaction logic for PageConnexion.xaml
    /// </summary>
    public partial class PageConnexion : Page
    {
        public PageConnexion()
        {
            InitializeComponent();
        }

        private void btInscription_Click(object sender, RoutedEventArgs e)
        {
            // Navigation vers la page d'inscription
            NavigationService?.Navigate(new PageInscription());
            tbIdentification.Clear();
            PasswordBox.Clear();
        }
    

        private void btConnexion_Click(object sender, RoutedEventArgs e)
        {
            //bool isUsername = false;
            //bool isPassword = false;

            try
            {
                if (tbIdentification.Text.Equals(plantuleControler.getUserIdFromDb(tbIdentification.Text), StringComparison.OrdinalIgnoreCase)  && PasswordBox.Password == plantuleControler.getPasswordFromDb(PasswordBox.Password))
                {
                    tbIdentification.Clear();
                    PasswordBox.Clear();
                    ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
                }
                else
                {
                    MessageBox.Show("Mot de passe, nom d'utilisateur incorrect");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            
            /*=====================================================================================
            try
            {
                //utilise le context
                using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
                {
                    CompteUtilisateur newPlante = PC.CompteUtilisateur.FirstOrDefault(p => p.IdUtilisateur.Equals(tbIdentification.Text));

                    if (newPlante != null)
                    {
                       isUsername = true;
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
            =====================================================================================*/


            /*===================================================================================
            try
            {
                //utilise le context
                using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
                {
                    CompteUtilisateur newPlante = PC.CompteUtilisateur.FirstOrDefault(p => p.motdepass.Equals(PasswordBox.Password));

                    if (newPlante != null)
                    {
                        isUsername = true;
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

            if (isPassword && isPassword)
            {
                //ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
            }
            else
            {
               // MessageBox.Show("invalide info");
            }
            ======================================================================================*/

            //ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.PageAcceuil;
        }

    }
}
