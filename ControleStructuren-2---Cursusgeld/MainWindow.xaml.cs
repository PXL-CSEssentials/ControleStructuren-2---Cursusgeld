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

namespace ControleStructuren_2___Cursusgeld
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Zet variabelen hier als je die nodig hebt over meerdere functies
        private int _year;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void numericButton_Click(object sender, RoutedEventArgs e)
        {
            // Resultaat wordt geschreven in jaar variabele.
            // TryParse geeft een bool terug (true als het de tekst kan omzetten naarint, 
            // false als dit niet gaat)
            bool isNumeric = int.TryParse(yearTextBox.Text, out _year);


            if (isNumeric)
            {
                numericLabel.Content = "Is numeriek";
                // Indien numeriek, dan kunnen we berekenen
                calculateButton.IsEnabled = true;
            }
            else
            {
                numericLabel.Content = "Geef een correct jaartal!";
                calculateButton.IsEnabled = false;
            }

            // Verleg de focus naar de BtnBerekenen button
            calculateButton.Focus();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            const float PricePerCredit = 1.5f;
            int numberOfClassHours = int.Parse(numberOfClassHoursTextBox.Text);

            // Opnieuw het jaar opvragen omdat het gewijzigd kan zijn.      
            bool isNumeric = int.TryParse(yearTextBox.Text, out _year);

            // Schrikkeljaar: jaar dat deelbaar is door 4 en geen vol eeuwjaar, 
            // of als het een eeuwjaar is dan moet het deelbaar zijn door 400.     
            // Dit betekent dat 1900 en 2100 geen schrikkeljaren zijn (volle eeuwjaren) en het jaar 2000 wel (deelbaar door 400). 

            if (isNumeric)
            {
                // Test op schrikkeljaar     
                // * Jaar deelbaar door 4 EN geen eeuwjaar (niet deelbaar door honderd)
                // * OF een jaar is deelbaar door 400
                if ((_year % 4 == 0 && _year % 100 != 0) || (_year % 400 == 0))
                {
                    leapYearLabel.Content = "Is een schrikkeljaar";
                    // Schrikkeljaar, dus 8 extra studiepunten
                    float subscriptionFee = ((numberOfClassHours + 1) * PricePerCredit);
                    subscriptionFeeTextBox.Text = subscriptionFee.ToString();
                }
                else
                {
                    leapYearLabel.Content = "Is geen schrikkeljaar";
                    // Geen schrikkeljaar, dus geen extra studiepunten
                    subscriptionFeeTextBox.Text = (numberOfClassHours * PricePerCredit).ToString();
                }
            }

            yearTextBox.Focus();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
