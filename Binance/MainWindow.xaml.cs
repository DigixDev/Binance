using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
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

namespace Binance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientWebSocket _client;
        private CancellationToken _token;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
              {
                  this.Left = SystemParameters.WorkArea.Width - this.Width;
                  this.Top = SystemParameters.WorkArea.Height - this.Height;
                  txtOrderPercent.Text = "5";
              };
        }

        private void Sum()
        {
            var sum = Convert.ToDouble(txtStartAmount.Text);
            for(var i=0; i<Convert.ToInt32(txtDays.Text); i++)
            {
                sum += (sum * Convert.ToInt32(txtPercent.Text) / 100);
            }
            txtTotal.Text = $"{sum.ToString("N0")}$ - {(sum * 8.5).ToString("N0")}TL";
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTpSl();
        }

        private void CalculateTpSl()
        { 
            try
            {
                if (txtUpperPrice == null)
                    return;

                var price = Convert.ToDouble(txtCurrentPrice.Text);
                var percent = price / 100 * (Convert.ToDouble(txtOrderPercent.Text)/Convert.ToInt16(txtLeverage.Text));
                
                txtUpperPrice.Text = (price + percent).ToString("N4");
                txtLowerPrice.Text = (price - percent).ToString("N4");
            }
            catch (Exception)
            {
                txtLowerPrice.Text = txtUpperPrice.Text = "";
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Sum();   
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var txtBox=(TextBox)sender;
            if(string.IsNullOrEmpty(txtBox.Text)==false)
                Clipboard.SetText(txtBox.Text);
        }

        private void txtOrderPercent_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculateTpSl();
        }
    }
}
