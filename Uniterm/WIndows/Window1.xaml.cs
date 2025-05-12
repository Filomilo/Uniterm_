using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ModernWpf;
using Uniterm.Canvas;
using Uniterm.Database;
using Uniterm.Models;
using Uniterm.Properties;

namespace Uniterm.Windows
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IUnitermCanvas _unitermCanvas;
        private IDrawingCanvas _drawingCanvas;
        private IUnitermDataBase _db;

        public Window1()
        {
            try
            {
                InitializeComponent();
                _drawingCanvas = cDrawing;
                UnitermCanvas uniterm = new UnitermCanvas();
                _unitermCanvas = uniterm;
                _drawingCanvas.AddDrawable(uniterm);
                _db = new JsonUnitermDataBase();
                _db.OnDbChangeEvent += RefreshDbList;
                _db.LoadUnitermCollection();
                _unitermCanvas.UnitermCanvasChangedEvent += _drawingCanvas.Refresh;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void RefreshDbList()
        {
            lbUniterms.Items.Clear();
            var collection = _db.GetUnitermCollectionEntries();
            if (collection == null)
                return;
            foreach (var uniterm in collection)
            {
                lbUniterms.Items.Add(uniterm);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ApplicationTheme == null)
            {
                ThemeManager.Current.ApplicationTheme = ThemeManager.Current.ActualApplicationTheme;
            }
            foreach (FontFamily f in System.Windows.Media.Fonts.SystemFontFamilies)
            {
                cbFonts.Items.Add(f);
            }
            if (cbFonts.Items.Count > 0)
                cbFonts.SelectedIndex = 0;

            for (int i = 8; i <= 40; i++)
            {
                cbfSize.Items.Add(i);
            }
            cbfSize.SelectedIndex = 9;
        }

        private void ehCBFontsChanged(object sender, SelectionChangedEventArgs e)
        {
            _drawingCanvas.SetFontFamily(new FontFamily(e.AddedItems[0].ToString()));
        }

        private void ehcbfSizeChanged(object sender, SelectionChangedEventArgs e)
        {
            _drawingCanvas.SetFontSize((int)e.AddedItems[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AbstractOperation op = AddElem.GetSequencingOperation(
                "Dodaj operacje sekwencjonowania"
            );
            if (op == null)
                return;
            this._unitermCanvas.AddVerticalOperation(op);
        }

        private void btnAddEl_Click(object sender, RoutedEventArgs e)
        {
            AbstractOperation op = AddElem.GetParrarelOpration("Dodaj operacje zrónoleglania");
            if (op == null)
                return;
            this._unitermCanvas.AddHorizontalOperation(op);
        }

        private void btnRedraw_Click(object sender, RoutedEventArgs e)
        {
            ChangeWIndow.Change(_unitermCanvas);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveWIndow.SaveToDatabase(_db, _unitermCanvas.GetUnitermCollection());
        }

        private void ehlbUNitermsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            if (!this._unitermCanvas.IsEmpty())
            {
                MessageBoxResult result = MessageBox.Show(
                    "Wybranie unitermu wymaże aktualne zmiany czy aby na pewno chesz kontynuwać?",
                    "Confirmation",
                    MessageBoxButton.YesNo
                );

                if (result != MessageBoxResult.Yes)
                {
                    lbUniterms.SelectedItem = null;
                    return;
                }
            }

            UnitermCollectinEntry uniterm = (UnitermCollectinEntry)lbUniterms.SelectedItem;
            _unitermCanvas.loadCollection(uniterm.Collection.Clone());
        }

        private void ehNowyClick(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            _unitermCanvas.Clear();
            lbUniterms.SelectedItem = null;
        }
    }
}
