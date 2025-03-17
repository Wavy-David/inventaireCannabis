using Canabis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            lbIdentification.Visibility = Visibility.Hidden;
        }

        private bool ValidateForm()
        {
            // Check if all textboxes are filled
            if (string.IsNullOrWhiteSpace(tbPrenom.Text)
                || string.IsNullOrWhiteSpace(tbnom.Text)
                || string.IsNullOrWhiteSpace(tbEmail.Text)
                || string.IsNullOrWhiteSpace(PasswordBox.Password)
                || string.IsNullOrWhiteSpace(PasswordBox2.Password))
            {
                MessageBox.Show("Aucun champ ne doit être vide");
                return false;
            }

            // Check if the email format is valid using regular expression
            string email = tbEmail.Text;
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Email incorrect");
                return false;
            }

            // Check if both password boxes match
            if (PasswordBox.Password != PasswordBox2.Password)
            {
                MessageBox.Show("Les mots de passe sont différents");
                return false;
            }

            // Check if the password meets the minimum length requirement (3 characters)
            if (PasswordBox.Password.Length < 3)
            {
                MessageBox.Show("Le mot de passe doit contenir au moins 3 caractères");
                return false;
            }

            // If all checks pass, return true
            return true;
        }


        private void btEnregistrerUtilisateur_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    using (CompteUtilisateurContext PC = new CompteUtilisateurContext())
                    {
                        CompteUtilisateur newPlanteArchive = new CompteUtilisateur();

                        newPlanteArchive.IdUtilisateur = "U" + tbnom.Text + (plantuleControler.countAllUser() + 1);
                        newPlanteArchive.Nom = tbnom.Text;
                        newPlanteArchive.Prenom = tbPrenom.Text;
                        //newPlante.DateAjout = calendrier.SelectedDate.Value.ToShortDateString();
                        newPlanteArchive.Email = tbEmail.Text;
                        newPlanteArchive.motdepass = PasswordBox.Password;

                        //save dans la base de donnee
                        PC.CompteUtilisateur.Add(newPlanteArchive);
                        PC.SaveChanges();


                        //listInformation.Clear();
                        MessageBox.Show("Utilisateur Enregistré\nVotre nom d'utilisateur est: " + newPlanteArchive.IdUtilisateur);

                        lbIdentification.Content = newPlanteArchive.IdUtilisateur;
                        lbIdentification.Visibility = Visibility.Visible;

                        tbnom.Clear();
                        tbPrenom.Clear();
                        tbEmail.Clear();
                        PasswordBox.Clear();
                        PasswordBox2.Clear();
                    }

                }
                catch (Exception ex)
                {
                    //statusMessage.Text = ex.Message;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageConnexion;
        }
    }
}
