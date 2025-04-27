using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Uniterm.Models;

namespace Uniterm
{
    /// <summary>
    /// Interaction logic for ChangeWIndow.xaml
    /// </summary>
    public partial class ChangeWIndow : Window
    {
        private IUnitermCanvas _unitermCanvas;

        public ChangeWIndow(IUnitermCanvas unitermCanvas)
        {
            InitializeComponent();
            _unitermCanvas = (IUnitermCanvas)unitermCanvas;
            foreach (var horizontalOperation in unitermCanvas.GetHorizontalOperations())
            {
                this.ListBox_Horziontal.Items.Add(horizontalOperation);
            }
            foreach (var verticalOperation in unitermCanvas.GetVerticalOperations())
            {
                this.ListBox_Vertical.Items.Add(verticalOperation);
            }

            if (this.ListBox_Horziontal.Items.Count > 0)
            {
                this.ListBox_Horziontal.SelectedIndex = 0;
            }
            if (this.ListBox_Vertical.Items.Count > 0)
            {
                this.ListBox_Vertical.SelectedIndex = 0;
            }
        }

        internal static void Change(IUnitermCanvas unitermCanvas)
        {
            ChangeWIndow wIndow = new ChangeWIndow(unitermCanvas);
            wIndow.ShowDialog();
        }

        private void RadioButton_Vertical_B_Checked(object sender, RoutedEventArgs e) { }

        private void RadioButton_Horizontal_A_Checked(object sender, RoutedEventArgs e) { }

        private void RadioButton_Vertical_A_Checked(object sender, RoutedEventArgs e) { }

        private void ListBox_Vertical_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AbstractOperation ParamB = (AbstractOperation)e.AddedItems[0];
            if (ParamB != null)
            {
                this.RadioButton_Vertical_A.Content = ParamB.ExpressionA;
                this.RadioButton_Vertical_B.Content = ParamB.ExpressionB;
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnitermCollection unitermCollection = _unitermCanvas.GetUnitermCollection();
                if (
                    this.ListBox_Horziontal.SelectedItem == null
                    || this.ListBox_Vertical.SelectedItem == null
                )
                {
                    MessageBox.Show("Please select a value");
                    return;
                }

                int indexHorziontal = this.ListBox_Horziontal.SelectedIndex;
                int indexVertical = this.ListBox_Vertical.SelectedIndex;
                AbstractOperation ParamA = unitermCollection.VerticalOperations.ElementAt(
                    indexVertical
                );
                AbstractOperation ParamB = unitermCollection.HorizontalOperations.ElementAt(
                    indexHorziontal
                );
                if (ParamA != null && ParamB != null)
                {
                    if (this.RadioButton_Vertical_A.IsChecked == true)
                    {
                        ParamA.ExpressionA = ParamB;
                    }
                    else if (this.RadioButton_Vertical_B.IsChecked == true)
                    {
                        ParamA.ExpressionB = ParamB;
                    }
                    else
                    {
                        MessageBox.Show("Please select a value");
                        return;
                    }

                    unitermCollection.HorizontalOperations.RemoveAt(indexHorziontal);
                    this._unitermCanvas.loadCollection(unitermCollection);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Wystąpł błąd: {ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
