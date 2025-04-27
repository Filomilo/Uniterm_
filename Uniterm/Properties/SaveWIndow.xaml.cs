using System;
using System.Collections.Generic;
using System.Data.Services.Client;
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
using System.Windows.Shapes;
using Uniterm.Interfaces;
using Uniterm.Models;

namespace Uniterm.Properties
{
    /// <summary>
    /// Interaction logic for SaveWIndow.xaml
    /// </summary>
    public partial class SaveWIndow : Window
    {
        IUnitermDataBase _unitermDataBase;
        UnitermCollection _unitermCollection;

        public SaveWIndow(IUnitermDataBase dataBase, UnitermCollection uniterm)
        {
            InitializeComponent();
            _unitermDataBase = dataBase;
            _unitermCollection = uniterm;
        }

        private void SaveButon_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TItleBOx.Text) || string.IsNullOrEmpty(DescritpionBox.Text))
            {
                MessageBox.Show("Nie można zapisać jednostki bez tytułu i opisu");
                return;
            }
            string title = TItleBOx.Text;
            string description = DescritpionBox.Text;
            if (this._unitermDataBase.GetUnitermOfName(title) != null)
            {
                if (!ShoudOVerwrtie())
                    return;
            }
            UnitermCollectinEntry entry = new UnitermCollectinEntry(
                title,
                description,
                _unitermCollection
            );
            this._unitermDataBase.SaveNewUnitermCollectionEntry(entry);
            this.Close();
        }

        private static bool ShoudOVerwrtie()
        {
            MessageBoxResult result = MessageBox.Show(
                "Uniterm o tej nazwie już insteije czy chesz nadpisać",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SaveToDatabase(IUnitermDataBase unitermData, UnitermCollection uniterm)
        {
            SaveWIndow saveWIndow = new SaveWIndow(unitermData, uniterm);
            saveWIndow.ShowDialog();
        }
    }
}
