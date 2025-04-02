using Canabis.Views;
using sommatif3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canabis
{
    public static class ControlerPage
    {
        public static MainSwitch mainFrameControl = new MainSwitch();

        public static PageAffichePlantule pageAffichePlantule = new PageAffichePlantule();

        public static PageAjouterPlantule pageAjouterPlantule = new PageAjouterPlantule();

        public static PageConnexion pageConnexion = new PageConnexion();

        public static PageInscription pageInscription = new PageInscription();

        public static PageAcceuil PageAcceuil = new PageAcceuil();

        public static PageConsultationPlantule pageConsultation = new PageConsultationPlantule();

        public static PageModifierPlantule pageModifierPlantule = new PageModifierPlantule();

        public static PageImportDonnee pageImportDonnee = new PageImportDonnee();

        public static PageArchive pageArchive = new PageArchive();

        public static PageHistorique pageHistorique = new PageHistorique();
    }
}
