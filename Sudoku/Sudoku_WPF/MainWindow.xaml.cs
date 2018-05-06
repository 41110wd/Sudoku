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
using Sudoku;

namespace Sudoku_WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rnd;
        Button[] butarr;
        int[] ar;
        int anzahl;

        public MainWindow()
        {
            InitializeComponent();

            butarr = new Button[] { a1, a2, a3, b4, b5, b6, c7, c8, c9 };
            rnd = new Random();

            Disable_All();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = ((Button)sender);
                if ((string)button.Content == "")
                    button.Content = 0;
                if (Convert.ToInt32(button.Content) >= 9)
                    button.Content = 0;

                int wert = Convert.ToInt32(button.Content);
                wert++;
                button.Content = wert.ToString();

                foreach(Button b in butarr)
                {
                    b.Background = Brushes.Gainsboro;
                }

                for (int i = 0; i < butarr.Length; i++)
                {
                    for (int j = 0; j < butarr.Length; j++)
                    {
                        if (i != j && (string)butarr[i].Content != "" && (string)butarr[j].Content != "")
                        {
                            if (Convert.ToInt32(butarr[i].Content) == Convert.ToInt32(butarr[j].Content))
                            {
                                butarr[i].Background = Brushes.Red;
                            }
                        }
                    }
                }
                Winner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                anzahl = 0;

                foreach (Button b in butarr)
                {
                    b.Content = "";
                    b.IsEnabled = true;
                    b.Background = Brushes.Gainsboro;
                }

                ar = new int[butarr.Length];

                if (Convert.ToInt32(TextBoxStart.Text) > 2 && Convert.ToInt32(TextBoxStart.Text) < 8)
                {
                    anzahl = Convert.ToInt32(TextBoxStart.Text);
                    MessageBox.Show("Good Luck");
                    TextBoxStart.Text = "";
                }
                else
                {
                    throw new Exception("Only Numbers between 3 and 7 are allowed");
                }

                bool prueffuellung = true;
                while (prueffuellung)
                {
                    prueffuellung = false;
                    for (int i = 0; i < anzahl; i++)
                    {
                        ar[i] = rnd.Next(1, 10);

                        for (int j = 0; j < anzahl; j++)
                        {
                            for (int k = 0; k < anzahl; k++)
                            {
                                if (j != k)
                                {
                                    if (ar[j] == ar[k])
                                    {
                                        ar[i] = rnd.Next(1, 10);
                                        prueffuellung = true;
                                    }
                                }
                            }
                        }
                    }
                }

                bool pruefauswahl = true;
                while (pruefauswahl)
                {
                    pruefauswahl = false;
                    for (int i = 0; i < anzahl; i++)
                    {
                        int wert = rnd.Next(0, 9);
                        if ((string)butarr[wert].Content == "")
                        {
                            butarr[wert].Content = ar[i].ToString();
                            butarr[wert].IsEnabled = false;
                        }
                        else
                            i--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Disable_All()
        {
            foreach(Button b in butarr)
            {
                b.IsEnabled = false;
            }
        }

        private void Winner()
        {
            int winner = 0;
            for (int i = 0; i < butarr.Length; i++)
            {
                for (int j = 0; j < butarr.Length; j++)
                {
                    if (i != j && (string)butarr[i].Content != "" && (string)butarr[j].Content != "")
                    {
                        if (Convert.ToInt32(butarr[i].Content) != Convert.ToInt32(butarr[j].Content))
                        {
                            winner++;
                        }
                    }
                }
            }

            if (winner == 72)
            {
                MessageBox.Show("You have won");

                foreach (Button b in butarr)
                {
                    b.IsEnabled = false;
                }
            }
        }
    }
}
