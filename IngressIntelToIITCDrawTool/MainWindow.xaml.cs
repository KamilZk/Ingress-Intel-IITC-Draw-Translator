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

namespace IngressIntelToIITCDrawTool
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string strIntel;
        //private string strIITC;
        //private string strTemp;

        public MainWindow()
        {
            InitializeComponent();
            tbIntel.Focus();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbIntel.Text = "";
            tbIITC.Text = "";
            rbIntelToIITC.IsChecked = true;
            //strIntel = null;
            //strIITC = null;
            //strTemp = null;

        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            if(rbIntelToIITC.IsChecked == true)
            {
                string strIntel, strTemp, strIITC;
                int indexP1latStart, indexP1latEnd, indexP1lngStart, indexP1lntEng;
                int indexP2latStart, indexP2latEnd, indexP2lngStart, indexP2lntEng;

                strIntel = tbIntel.Text;
                indexP1latStart = strIntel.IndexOf("pls=") + 4;
                strTemp = strIntel.Substring(indexP1latStart);

                strIITC = "[{ \"type\":\"polyline\",\"latLngs\" [";
                //string strTemp = strIntel + " strTemp";

                //string strIITC = strTemp + " strIITC";

                tbIITC.Text = strIITC + strTemp + "],\"color\":\"#a24ac3\"}]";
            }
            else if (rbIITCToIntel.IsChecked == true)
            {
                string strIITC = tbIITC.Text;

                string strTemp = strIITC + " strTemp";

                string strIntel = strTemp + " strIntel";

                tbIntel.Text = strIntel;
            }

        }


    }
}
