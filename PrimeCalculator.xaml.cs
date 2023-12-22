using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Prime_Number_Calculator
{
    public partial class PrimeCalculator : Window
    {
        public PrimeCalculator()
        {
            InitializeComponent();
            Error.FontSize = 16;
            Error.Foreground = Brushes.DarkGray;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Error.Text = "";
            
        }

        private void Calculate_Button_Click(object sender, RoutedEventArgs e)
        {
            var inputValue = NumberTextBox.Text;

            if (inputValue.Length > 10)
            {
                Error.Text = "Make sure that what you typed is an integer between 1 and 1,000,000,000 and try again.";
                Error.Foreground = Brushes.Red;
                return;
            }

            bool success = int.TryParse(inputValue, out var result);

            if (!success)
            {
                Error.Text = "Make sure that what you typed is an integer and try again.";
                Error.Foreground = Brushes.Red;
                return;
            }

            if (result <= 0)
            {
                Error.Text = "Make sure that what you typed is an integer between 1 and 1,000,000,000 and try again.";
                Error.Foreground = Brushes.Red;
                return;
            }

            var (isPrime, factors) = CalculatePrimeFactors(result);

            Error.Foreground = Brushes.Black;
            Error.Text = result.ToString();

            Labels.Text = "Prime Number:\nPrime Factors:";

            Results.Text = $"{isPrime}\n{string.Join(", ", factors)}";



        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            NumberTextBox.Text = "";
            Results.Text = "";
            Labels.Text = "";

            Error.Foreground = Brushes.DarkGray;
            Error.Text = "Result will appear here.";
            
        }

        private (bool isPrime, int[] factors) CalculatePrimeFactors(int number)
        {
           

            List<int> primeFactors = [1];

            while (number%2 == 0) {
                primeFactors.Add(2);
                number /= 2;
            }

            for (int i = 3; i <= Math.Sqrt(number); i++)
            {
                while(number%i == 0)
                {
                    primeFactors.Add(i);
                    number /= i;
                }
            }

            if (number > 2 ) 
            {
                primeFactors.Add(number);
            }

            var result = (isPrime: primeFactors.Count <= 2, factors: primeFactors.ToArray());

            return result;
        }
    }
}