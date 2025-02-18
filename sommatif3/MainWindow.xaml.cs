
using Canabis;
using Canabis.Views;
using sommatif3.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sommatif3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //permet a identifier l'operateur logic pour les requette afin de mieu trier la list de consultation selon le besoin
        public string operateurLogicWhere;
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = ControlerPage.mainFrameControl;
            ControlerPage.mainFrameControl.MainFrame.Content = ControlerPage.pageConnexion;
           // Main.Content = new PageAffichePlantule();
        }

    }
}