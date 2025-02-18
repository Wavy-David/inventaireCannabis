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
    /// Interaction logic for PageAffichePlantule.xaml
    /// </summary>
    public partial class PageAffichePlantule : Page
    {
        public PageAffichePlantule()
        {
            InitializeComponent();

            lbPlantActif.Content = plantuleControler.countActifPlantule();

            lbPlantInactif.Content = plantuleControler.countInactifPlantule();

            lbTotalPlant.Content = (plantuleControler.countActifPlantule() + plantuleControler.countInactifPlantule()).ToString();

            ChargerListePlantules();
        }

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
