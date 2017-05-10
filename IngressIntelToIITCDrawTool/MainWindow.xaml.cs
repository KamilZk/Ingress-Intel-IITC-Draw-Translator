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
                string strIntel, strTemp, strIITC;
                int indexP1latStart, indexP1latEnd, indexP1lngStart, indexP1lntEng;
                int indexP2latStart, indexP2latEnd, indexP2lngStart, indexP2lntEng;

                strIntel = tbIntel.Text;
                IntelToIITC(strIntel);

                //tbIITC.Text = strIITC;
                
                //strTemp = strIntel.Substring(indexP1latStart);

            }
            else if (rbIITCToIntel.IsChecked == true)
            {
                string strIITC = tbIITC.Text;

                string strTemp = strIITC + " strTemp";

                string strIntel = strTemp + " strIntel";

                tbIntel.Text = strIntel;
            }

        }

        private void IntelToIITC(String strTemp)
        {            
            if(strTemp.Contains("pls="))
            {
                int index = strTemp.IndexOf("pls=") + 4;
                tbIITC.Text += strIITCBeginSeq;
                IntelToIITC(strTemp.Substring(index));
            }
            else 
            {
                int indexPrzecinek = -1, indexPodreslnik = -1;

                if (strTemp.Contains(','))
                {
                    indexPrzecinek = strTemp.IndexOf(',');
                    //tbIITC.Text += " zawiera przecinek " + indexPrzecinek;
                }
                if (strTemp.Contains('_'))
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
