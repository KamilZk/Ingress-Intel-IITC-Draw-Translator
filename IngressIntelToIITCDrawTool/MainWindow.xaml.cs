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
        private string strIITCBeginSeq = "[{ \"type\":\"polyline\",\"latLngs\" [";
        private string strIITCEndSeq = "],\"color\":\"#a24ac3\"}]";
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
            //strIntel = null;
            //strIITC = null;
            //strTemp = null;

        }

        private void btnTranslate_Click(object sender, RoutedEventArgs e)
        {
            if(rbIntelToIITC.IsChecked == true)
            {
                IntelToIITC(tbIntel.Text, 0);
            }
            else if (rbIITCToIntel.IsChecked == true)
            {
                string strIITC = tbIITC.Text;

                string strTemp = strIITC + " strTemp";

                string strIntel = strTemp + " strIntel";

                tbIntel.Text = strIntel;
            }

        }

        private void IntelToIITC(String strTemp, int loopCounter)
        {            
            if(strTemp.Contains("pls=") && loopCounter == 0)
            {
                int index = strTemp.IndexOf("pls=") + 3;
                tbIITC.Text += strIITCBeginSeq;
                IntelToIITC(strTemp.Substring(index), loopCounter + 1);
            }
            else 
            {
                int indexPrzecinek = -1, indexPodreslnik = -1;

                if (strTemp[0] == '=') //first parameter (always in second recurency)
                {
                    indexPrzecinek = strTemp.IndexOf(',');
                    tbIITC.Text += "{" + strIITClat + strTemp.Substring(1, indexPrzecinek);
                    IntelToIITC(strTemp.Substring(indexPrzecinek), loopCounter + 1);
                }

                else if (strTemp[0] == ',')
                {
                    if(strTemp.Contains('_'))
                        indexPodreslnik = strTemp.IndexOf('_');

                    if (strTemp.Substring(1).Contains(','))
                        indexPrzecinek = strTemp.Substring(1).IndexOf(',');

                    if (strTemp.Substring(1).IndexOf(',') > indexPodreslnik)
                    {
                        indexPrzecinek = strTemp.Substring(1).IndexOf(',') + 1;
                        tbIITC.Text += "," + strIITClng + strTemp.Substring(1, indexPrzecinek) + "},";
                    }

                   

                }

                else if (strTemp[0] == '_')
                { 
                    indexPodreslnik = strTemp.IndexOf('_');
                    //tbIITC.Text += " zawiera podreslnik " + indexPodreslnik;
                }
                
                //TODO: sprawdzic czy indexPrzecinek>indexPodreslnik -> wartosc pierwsza
                if (indexPrzecinek > indexPodreslnik)
                {

                }

                //TODO: sprawdzic czy indexPodreslnik>indexPrzecinek -> zakończenie sekwencji i nowy zestaw
                else if (indexPodreslnik > indexPrzecinek)
                {
                    //TODO: dopisanie zakończenia
                    IntelToIITC(strTemp.Substring(indexPodreslnik+1));
                }
                else if (indexPrzecinek == -1 && indexPodreslnik == -1) //nie znaleziono przecinka i podkreslnika => koniec
                {
                    return;
                }

                //TODO: jeżeli oba indexy są puste to dodać koniec return

                //TODO: dodawanie sekwencji
                //tbIITC.Text += strTemp + strIITCEndSeq; //test
            }

            return;
        }
    }
}
