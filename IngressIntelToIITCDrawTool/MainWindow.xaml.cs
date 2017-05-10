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
        private string strIITCBeginSeq = "{\"type\":\"polyline\",\"latLngs\":[";
        private string strIITCEndSeq = "],\"color\":\"#a24ac3\"}";
        private string strIITClat = "\"lat\":";
        private string strIITClng = "\"lng\":";

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
            tbIntel.Focus();
        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            if(rbIntelToIITC.IsChecked == true)
            {
                tbIITC.Text = "[";
                IntelToIITC(tbIntel.Text, 0);
                tbIITC.Text += ']';
            }
            else if (rbIITCToIntel.IsChecked == true)
            {
                //TODO: IITCToIntel method
            }

        }

        private void IntelToIITC(String strTemp, int loopCounter)
        {            
            if ( loopCounter == 0)
            {
                int index;

                if (strTemp.Contains("pls="))
                    index = strTemp.IndexOf("pls=") + 4;
                else
                    index = 0;

                tbIITC.Text += strIITCBeginSeq;
                IntelToIITC(strTemp.Substring(index), loopCounter + 1);
            }
            else 
            {
                int indexComma;

                if (loopCounter == 1 || loopCounter == 3)
                {
                    indexComma = strTemp.IndexOf(',');
                    tbIITC.Text += '{' + strIITClat + strTemp.Substring(0, indexComma);
                    IntelToIITC(strTemp.Substring(indexComma + 1), loopCounter + 1);
                }

                else if (loopCounter == 2 )
                {

                    indexComma = strTemp.IndexOf(',');
                    tbIITC.Text += ',' + strIITClng + strTemp.Substring(0, indexComma) + "},";
                    IntelToIITC(strTemp.Substring(indexComma + 1), loopCounter + 1);
                }

                else if (loopCounter == 4)
                {
                    if (strTemp.Contains('_'))
                    {
                        int indexUnderscore = strTemp.IndexOf('_');
                        tbIITC.Text += ',' + strIITClng + strTemp.Substring(0, indexUnderscore) + '}' + strIITCEndSeq + ',';
                        IntelToIITC(
                            strTemp.Substring(strTemp.IndexOf('_') + 1)
                            , 0);
                    }
                    else
                    {
                        tbIITC.Text += ',' + strIITClng + strTemp + '}' + strIITCEndSeq;
                        return;
                    }

                }

            }

            return;
        }
    }
}
