﻿using System;
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
            if(strTemp.Contains("pls=") && loopCounter == 0)
            {
                int index = strTemp.IndexOf("pls=") + 4;
                tbIITC.Text += strIITCBeginSeq;
                IntelToIITC(strTemp.Substring(index), loopCounter + 1);
            }
            else 
            {
                int indexComma = -1;

                if (loopCounter == 1 || loopCounter == 3)
                {
                    indexComma = strTemp.IndexOf(',');
                    tbIITC.Text += '{' + strIITClat + strTemp.Substring(0, indexComma - 1);
                    IntelToIITC(strTemp.Substring(indexComma + 1), loopCounter + 1);
                }

                else if (loopCounter == 2 || loopCounter == 4)
                {
                    indexComma = strTemp.IndexOf(',');
                    tbIITC.Text += ',' + strIITClng + strTemp.Substring(0, indexComma - 1) + '}';

                    if (loopCounter == 2)
                    { 
                        tbIITC.Text += ',';
                        IntelToIITC(strTemp.Substring(indexComma + 1), loopCounter + 1);
                    }
                    else if (loopCounter == 4)
                    {
                        tbIITC.Text += strIITCEndSeq;

                        if (strTemp.Contains('_'))
                            IntelToIITC(
                                strTemp.Substring(strTemp.IndexOf('_') + 1)
                                , 1);
                        else
                            return;
                    }

                }

            }

            return;
        }
    }
}
