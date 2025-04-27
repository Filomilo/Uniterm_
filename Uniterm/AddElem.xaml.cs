using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Uniterm.Models;

namespace Uniterm
{
    /// <summary>
    /// Interaction logic for AddElem.xaml
    /// </summary>
    public partial class AddElem : Window
    {
        public AddElem(string tile)
        {
            InitializeComponent();
            this.Title = tile;
        }

        private void validateInput()
        {
            if (this.tbA.Text.Length == 0)
            {
                throw new Exception("Nie można dodać operacji bez podania A");
            }

            if (this.tbB.Text.Length == 0)
            {
                throw new Exception("Nie można dodać operacji bez podania B");
            }
            if (this.tbC.Text.Length == 0)
            {
                throw new Exception("Nie można dodać operacji bez podania C");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validateInput();
                this.DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static AbstractOperation GetParrarelOpration(string dodajOperacjeZrónoleglania)
        {
            AddElem addElem = new AddElem(dodajOperacjeZrónoleglania);
            var res = addElem.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                AbstractOperation operation = OperationFactory.CreateOperation(
                    OperationType.Parallel,
                    addElem.tbA.Text,
                    addElem.tbB.Text,
                    addElem.tbC.Text,
                    DirectionEnum.Horizontal
                );
                return operation;
            }

            return null;
        }

        public static AbstractOperation GetSequencingOperation(string dodajOperacjeSekwencjonowania)
        {
            AddElem addElem = new AddElem(dodajOperacjeSekwencjonowania);
            var res = addElem.ShowDialog();
            if (res.HasValue && res.Value == true)
            {
                AbstractOperation operation = OperationFactory.CreateOperation(
                    OperationType.Sequencing,
                    addElem.tbA.Text,
                    addElem.tbB.Text,
                    addElem.tbC.Text,
                    DirectionEnum.Vertical
                );
                return operation;
            }
            return null;
        }
    }
}
