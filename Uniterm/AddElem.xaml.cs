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

        private void btnAdd_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }

        public static AbstractOperation GetParrarelOpration(string dodajOperacjeZrónoleglania)
        {
            AddElem addElem = new AddElem(dodajOperacjeZrónoleglania);
            addElem.ShowDialog();

            AbstractOperation operation = OperationFactory.CreateOperation(OperationType.Parallel, addElem.tbA.Text , addElem.tbB.Text, addElem.tbC.Text);
            return operation;
        }
    }
}
