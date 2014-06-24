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

namespace _01.Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stack<String> operations = new Stack<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMouseEnter(object sender, MouseEventArgs e) // To be made better!
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {

                tb.Background = Brushes.AliceBlue;
            }
        }

        private void OnDigitButtonClick(object sender, RoutedEventArgs e)
        {

            Object digit = new Object();
            Button btn = sender as Button;

            if (btn != null)
            {
                digit = btn.Content;

                if ((this.display.Text.StartsWith("0") && display.Text.Length == 1)|| ContainsOperator(this.display.Text) == true 
                    || this.display.Text.StartsWith("A"))
                {
                    this.display.Text = digit.ToString();
                   
                }
                else
                {
                    this.display.Text += digit.ToString();
                }


            }
        }

        private void OnActionButtonClick(object sender, RoutedEventArgs e)
        {

            if (this.operations.Count == 0)
            {
                StringBuilder operation = new StringBuilder();
                operation.Append(this.display.Text);
                Object content = new Object();
                Button btn = sender as Button;
                if (btn != null)
                {
                    content = btn.Content;
                    //operation.Append(content);
                    //operations.Push(operation.ToString());

                    if (content.ToString() != "=")
                    {
                        content = btn.Content;
                        operation.Append(content);
                        operations.Push(operation.ToString());
                        this.display.Text += " ";
                        this.display.Text += content;
                        this.display.Text += " ";
                    }
                }
               
            }
            else
            {
                String operation = operations.Pop();
                StringBuilder number = new StringBuilder();
                int indexOperation = -1;
                foreach (char symbol in operation)
                {
                    indexOperation++;
                    if ((symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/' || symbol == '=' ) && indexOperation != 0)
                    {

                        break;
                    }
                    else
                    {
                        number.Append(symbol);
                    }

                }
                decimal firstArgument = decimal.Parse(number.ToString());
                decimal result = 0;
                try
                {

                    switch (operation[indexOperation])
                    {
                        case '+':
                            result = Add(firstArgument, decimal.Parse(this.display.Text.ToString()));
                            break;
                        case '-':
                            result = Substract(firstArgument, decimal.Parse(this.display.Text.ToString()));
                            break;
                        case '*':
                            result = Multiply(firstArgument, decimal.Parse(this.display.Text.ToString()));
                            break;
                        case '/':
                            result = Divide(firstArgument, decimal.Parse(this.display.Text.ToString()));
                            break;
                        case '=':
                            result = firstArgument;
                            break;
                    }

                    Button btn = sender as Button;
                    if (btn.Content.ToString() != "=")
                    {
                        this.display.Text = result.ToString() + " " + btn.Content + " ";
                    }
                    else
                    {
                        this.display.Text = result.ToString();
                    }
                    
                    StringBuilder newOperation = new StringBuilder();
                    newOperation.Append(result.ToString());
                    if (btn != null)
                    {
                        newOperation.Append(btn.Content);
                    }
                    
                    this.operations.Push(newOperation.ToString());
                }
                catch (DivideByZeroException exc)
                {
                    this.display.Text = exc.Message;
                }
            }
        }


        private decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        private decimal Substract(decimal a, decimal b)
        {
            return a - b;
        }

        private decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }

        private decimal Divide(decimal a, decimal b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            else
            {
                return a / b;
            }
        }

        private bool ContainsOperator(string text)
        {
            return text.Contains('+') ||
                   text.Contains('-') ||
                   text.Contains('*') ||
                   text.Contains('/');
        }

        private void OnRationalButtonClick(object sender, RoutedEventArgs e)
        {
            decimal number = decimal.Parse(this.display.Text);
            number = 1 / number;
            this.display.Text = number.ToString();
        }

        private void OnSqrtButtonClick(object sender, RoutedEventArgs e)
        {
            decimal number = decimal.Parse(this.display.Text);
            number = decimal.Parse(Math.Sqrt(double.Parse(number.ToString())).ToString());
            this.display.Text = number.ToString();
        }

        private void OnCButtonClick(object sender, RoutedEventArgs e)
        {
            this.display.Text = "0";
            this.operations = new Stack<string>();
        }

        private void OnComplementButtonClick(object sender, RoutedEventArgs e)
        {
            decimal number = decimal.Parse(this.display.Text);
            number *= -1;
            this.display.Text = number.ToString();
        }

        private void OnCEButtonClick(object sender, RoutedEventArgs e)
        {
            this.display.Text = "0";
        }

        private void OnCommaButtonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            
            if (btn != null)
            {
                this.display.Text += btn.Content;
            }
        }

    }
}
