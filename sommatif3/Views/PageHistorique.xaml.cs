using Canabis.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for PageHistorique.xaml
    /// </summary>
    public partial class PageHistorique : Page
    {
        public PageHistorique()
        {
            InitializeComponent();
        }
        public List<string> listAncienneValeur = new List<string>();
        public List<string> listNouvelleValeur = new List<string>();
        public string champ = "";
        public string ancienneValeur = "";
        public string nouvelleValeur = "";

        public void modification()
        {
            listAncienneValeur = plantuleControler.trouverPlantuleInfo(tbIdentification.Text);
        }

        private void btRecherche_Click(object sender, RoutedEventArgs e)
        {
            using (HistoriquePlanteContext PC = new HistoriquePlanteContext())
                try
                {
                    //var rechercheSpecialite = PC.plante.FirstOrDefault(s => s.IdPlante == specialite);
                    var MesPlante = PC.HistoriquePlante.Where(p => p.IdPlante == tbIdentification.Text)
                        .Select(p => new
                        {
                            p.Id,
                            p.IdPlante,
                            p.Action,
                            p.Date,
                            p.Champ,
                            p.AncienneValeur,
                            p.NouvelleValeur
                        }).ToList();
                    grillePlante.ItemsSource = MesPlante;
                    //statusMessage.Text = "Liste des Specialités chargée";
                }
                catch (Exception ex)
                {
                    // statusMessage.Text = ex.Message;
                    Console.WriteLine(ex.ToString());
                }
        }
    }
}
