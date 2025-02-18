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
    /// Interaction logic for PageAcceuil.xaml
    /// </summary>
    public partial class PageAcceuil : Page
    {
        public PageAcceuil()
        {
            InitializeComponent();
        }

        private void affichePlantule_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageAffichePlantule;
        }

        private void jouterPlantule_click(object sender, RoutedEventArgs e)
        {
            //contro
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageAjouterPlantule;
        }

        private void ConsulterPlante_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageConsultation;
        }

        private void modifierPlantule_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageModifierPlantule;
        }

        private void btImport_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageImportDonnee;
        }

        private void Archiver_Plant_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageArchive;
        }

        private void btHistorique_click(object sender, RoutedEventArgs e)
        {
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageHistorique;
        }
    }
}
