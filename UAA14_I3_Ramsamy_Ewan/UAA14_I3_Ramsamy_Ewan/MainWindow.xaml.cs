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

namespace UAA14_I3_Ramsamy_Ewan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Nbr1.PreviewTextInput += new TextCompositionEventHandler(VerifTextInput);
            Nbr2.PreviewTextInput += new TextCompositionEventHandler(VerifTextInput);
            BtnCalculer.Click += new RoutedEventHandler(Calculer_Click);
            BtnReset.Click += new RoutedEventHandler(Reset_Click);
        }
        private void VerifTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "1" && e.Text != "0")
            {
                e.Handled = true;
            }
            else if (e.Text == "1" || e.Text == "0")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private void Calculer_Click(object sender, RoutedEventArgs e)
        {
            if(Soustraction == null)
            {
                Additionne(t1,t2, out ushort[] tRes, out bool ok);
            }
            else if(Addition == null)
            {
                Soustrait(t1, t2, out ushort[] tRes);
            } 
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
        }
        public ushort[] RemplirTableau(string nombreBinaire)
        {
            ushort[] TabBin = new ushort[8];
            for (int i = 0; i < TabBin.Length; i++)
            {
                TabBin[i] = 0;
            }
            for (int i = 0; i < nombreBinaire.Length; i++)
            {
                TabBin[7 - i] = ushort.Parse(nombreBinaire[nombreBinaire.Length - 1 - i].ToString());
            }
            return TabBin;
        }
        public void Additionne(ushort[] t1, ushort[] t2, out ushort[] tRes, out bool ok)
        {
            ok = true;
            ushort report = 0;
            ushort res;
            tRes = new ushort[8];
            for (int i = 7; i >= 0; i--)
            {
                res = (ushort)(t1[i] + t2[i] + report);
                if ((int)(res / 2) == 0)
                {
                    report = 0;
                }
                else
                {
                    report = 1;
                }
                if (res == 1)
                {
                    tRes[i] = 1;
                }
                else
                {
                    if (res % 2 == 1)
                    {
                        tRes[i] = 1;
                    }
                    else
                    {
                        tRes[i] = 0;
                    }
                }

            }
            if (report == 1)
            {
                ok = false;
            }
        }
        public bool Soustrait(ushort[] t1, ushort[] t2, out ushort[] tRes)
        {
            int[] tTemp = new int[8];
            tRes = new ushort[8];
            bool ok = true;

            for (int i = 0; i <= 7; i++)
            {
                tTemp[i] = t1[i] - t2[i];
            }
            for (int i = 7; i >= 1; i--)
            {
                if (tTemp[i] == -1)
                {
                    t2[i - 1] = (ushort)(t2[i - 1] + 1);
                    t1[i] = (ushort)(t1[i] + 2);
                }
                if (t1[i] < t2[i])
                {
                    t2[i - 1] = (ushort)(t2[i - 1] + 1);
                    t1[i] = (ushort)(t1[i] + 2);
                }
                tRes[i] = (ushort)(t1[i] - t2[i]);
            }
            if (t1[0] >= t2[0])
            {
                tRes[0] = (ushort)(t1[0] - t2[0]);
            }
            else
            {
                ok = false;
            }
            return ok;
        }
        public string Concatene(ushort[] t)
        {
            string chaine = "";
            for (int i = 0; i < t.Length; i++)
            {
                chaine += t[i];
            }
            return chaine;
        }
    }
}
