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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel;

namespace BCSH2_Semestralka.View
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : Window, IScrollable
    {

        bool isDataDirty = false;

        public AppView() 
        {
            InitializeComponent();
            this.DataContext = new BCSH2_Semestralka.ViewModel.AppViewModel(this);
        }

        public void ScrollToEnd()
        {
            Debug.WriteLine("SCROLL");
            this.ScrollableOutput.ScrollToEnd();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            /*
            MessageBox.Show("Closing called");
            
            
            // If data is dirty, notify user and ask for a response
            if (false)
            {
                string msg = "Data is dirty. Close without saving?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Data App",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    // If user doesn't want to close, cancel closure
                    e.Cancel = true;
                }
            }*/
        }
    }
}
