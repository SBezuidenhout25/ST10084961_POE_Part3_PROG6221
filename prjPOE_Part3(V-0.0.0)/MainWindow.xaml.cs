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

namespace prjPOE_Part3_V_0._0._0_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List (Generic Collection) to store expenses
        List<double> lstExpenses = new List<double>();
        double income, price, deposit, interest, months;
        //Variables to get user input from textBoxes
        double carPrice, carDeposit, carInterest, insurance;
        string make, model;

        private void rbtSaveYes_CheckedChanged(object sender, RoutedEventArgs e)
        {
            lblReason.Visibility = Visibility.Visible;
            txtReason.Visibility = Visibility.Visible;
            lblAmount.Visibility = Visibility.Visible;
            txtAmount.Visibility = Visibility.Visible;
            lblTime.Visibility = Visibility.Visible;
            dtEndDate.Visibility = Visibility.Visible;
            txtSavingsInterest.Visibility = Visibility.Visible;
            lblSavingsInterest.Visibility = Visibility.Visible;
        }

        private void rbtSaveNo_CheckedChanged(object sender, RoutedEventArgs e)
        {
            lblReason.Visibility = Visibility.Hidden;
            txtReason.Visibility = Visibility.Hidden;
            lblAmount.Visibility = Visibility.Hidden;
            txtAmount.Visibility = Visibility.Hidden;
            lblTime.Visibility = Visibility.Hidden;
            dtEndDate.Visibility = Visibility.Hidden;
            txtSavingsInterest.Visibility = Visibility.Hidden;
            lblSavingsInterest.Visibility = Visibility.Hidden;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        //Radio Button method checks what radio button is selected and
        //makes the required changes to the form, (Only makes
        //relevant text boxes visible)
        private void rbtBuy_CheckedChanged(object sender, EventArgs e)
        {
            lblPriceRent.Content = "Purchase Price:";
            lblPriceRent.Visibility = Visibility.Visible;
            txtPriceRent.Visibility = Visibility.Visible;

            lblDeposit.Visibility = Visibility.Visible;
            txtDeposit.Visibility = Visibility.Visible;
            lblInterest.Visibility = Visibility.Visible;
            txtInterest.Visibility = Visibility.Visible;
            lblMonths.Visibility = Visibility.Visible;
            txtMonths.Visibility = Visibility.Visible;
        }

        private void rbtYes_CheckedChanged(object sender, EventArgs e)
        {
            lblMake.Visibility = Visibility.Visible;
            txtMake.Visibility = Visibility.Visible;
            lblModel.Visibility = Visibility.Visible;
            txtModel.Visibility = Visibility.Visible;
            lblCarPrice.Visibility = Visibility.Visible;
            txtCarPrice.Visibility = Visibility.Visible;
            lblCarDeposit.Visibility = Visibility.Visible;
            txtCarDeposit.Visibility = Visibility.Visible;
            lblCarInterest.Visibility = Visibility.Visible;
            txtCarInterest.Visibility = Visibility.Visible;
            lblCarInsurance.Visibility = Visibility.Visible;
            txtCarInsurance.Visibility = Visibility.Visible;
        }

        private void rbtNo_CheckedChanged(object sender, EventArgs e)
        {
            lblMake.Visibility = Visibility.Hidden;
            txtMake.Visibility = Visibility.Hidden;
            lblModel.Visibility = Visibility.Hidden;
            txtModel.Visibility = Visibility.Hidden;
            lblCarPrice.Visibility = Visibility.Hidden;
            txtCarPrice.Visibility = Visibility.Hidden;
            lblCarDeposit.Visibility = Visibility.Hidden;
            txtCarDeposit.Visibility = Visibility.Hidden;
            lblCarInterest.Visibility = Visibility.Hidden;
            txtCarInterest.Visibility = Visibility.Hidden;
            lblCarInsurance.Visibility = Visibility.Hidden;
            txtCarInsurance.Visibility = Visibility.Hidden;
        }

        private void rbtRent_CheckedChanged(object sender, EventArgs e)
        {
            this.lblPriceRent.Content = "Monthly Rent:";
            lblPriceRent.Visibility = Visibility.Visible;
            txtPriceRent.Visibility = Visibility.Visible;


            lblDeposit.Visibility = Visibility.Hidden;
            txtDeposit.Visibility = Visibility.Hidden;
            lblInterest.Visibility = Visibility.Hidden;
            txtInterest.Visibility = Visibility.Hidden;
            lblMonths.Visibility = Visibility.Hidden;
            txtMonths.Visibility = Visibility.Hidden;
        }
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var error = addValues();

            //If checks which radio button option is selected
            //and sets values to the applied class
            if (rbtBuy.IsChecked == true && error == false)
            {
                Expense buy = new HomeLoan();
                buy.income = income;
                buy.LstExpenses = lstExpenses;
                buy.deposit = deposit;
                buy.interest = interest;
                buy.months = months;

                //Checks if user wants to buy a car
                if (rbtYes.IsChecked == true)
                {
                    buy.LstExpenses = Car(buy.LstExpenses);
                    buy.LstExpensesNames.Add("Monthly car payments:");
                }
                buy.calculate(price);

                //Displays the output
                MessageBox.Show(buy.ToString() + checkSavings());
            }
            else if (rbtRent.IsChecked == true && error == false)
            {
                Expense rent = new Rent();
                rent.income = income;
                rent.LstExpenses = lstExpenses;

                //Checks if user wants to buy a car
                if (rbtYes.IsChecked == true)
                {
                    rent.LstExpenses = Car(rent.LstExpenses);
                    rent.LstExpensesNames.Add("Monthly Car payments:");
                }
                rent.calculate(price);

                //Display the output
                MessageBox.Show(rent.ToString() + checkSavings());
            }
            //Only runs if the user has not selected to buy or rent
            else if (rbtBuy.IsChecked == false && rbtRent.IsChecked == false)
            {
                MessageBox.Show("Please select if you are renting or buying a property.");
            }

            //Clears the list after calculation so that new expenses
            //can be saved anew instead of being added to existing expenses
            lstExpenses.Clear();
        }

        //Method adds values to the BuyCar class
        //and alters the expenses list accordingly
        private List<double> Car(List<double> expenses)
        {
            BuyCar car = new BuyCar();
            car.LstExpenses = expenses;
            car.deposit = carDeposit;
            car.interest = carInterest;
            car.months = 60;
            car.Insurance = insurance;
            car.Make = make;
            car.Model = model;
            car.calculate(carPrice);
            expenses = car.LstExpenses;
            return expenses;
        }

        private bool addValues()
        {
            var error = checkInput();

            //Try Catch to store input and throw exceptions
            //depending on error type
            try
            {
                if (error == true)
                {
                    throw new ArgumentNullException();
                }
                price = Convert.ToDouble(txtPriceRent.Text);
                income = Convert.ToDouble(txtIncome.Text);
                lstExpenses.Add(Convert.ToDouble(txtGroceries.Text));
                lstExpenses.Add(Convert.ToDouble(txtWaterLights.Text));
                lstExpenses.Add(Convert.ToDouble(txtTravel.Text));
                lstExpenses.Add(Convert.ToDouble(txtPhone.Text));
                lstExpenses.Add(Convert.ToDouble(txtOther.Text));


                //If statement stores buy values only if buy 
                //option is selected
                if (rbtBuy.IsChecked == true)
                {
                    deposit = Convert.ToDouble(txtDeposit.Text);
                    interest = Convert.ToDouble(txtInterest.Text) / 100;
                    months = Convert.ToDouble(txtMonths.Text);
                }

                //If statement stores car values only if yes 
                //option is selected
                if (rbtYes.IsChecked == true)
                {
                    make = txtMake.Text;
                    model = txtModel.Text;
                    carPrice = Convert.ToDouble(txtCarPrice.Text);
                    carDeposit = Convert.ToDouble(txtCarDeposit.Text);
                    carInterest = Convert.ToDouble(txtCarInterest.Text) / 100;
                    insurance = Convert.ToDouble(txtCarInsurance.Text);
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please enter all fields");
                error = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Please only enter numeric values");
                error = true;
            }
            return error;
        }

        //Method checks user input for errors and changes
        //color of text boxes with missing input
        private bool checkInput()
        {
            var error = false;
            foreach (TextBox t in mainGrid.Children.OfType<TextBox>() )
            {
                if (t.Text .Equals("") && t.IsVisible == true)
                {
                    error = true;
                    t.BorderBrush = Brushes.Red;
                }
                else
                {
                    t.BorderBrush = Brushes.Black;
                }
            }
            return error;
        }

        //Method to calculate the monthly amount that
        //neds to be saved each month to reach goal by set date
        private string checkSavings()
        {
            string strDisplay = "";
            if (rbtSaveYes.IsChecked == true)
            {
                string strReason = txtAmount.Text;
                double dblGoal = Convert.ToDouble(txtAmount.Text);
                DateTime startDate = DateTime.Today;
                DateTime endDate = Convert.ToDateTime(dtEndDate.DisplayDate);
                double dblMonths = Math.Abs((startDate.Month - endDate.Month) + 12 * (startDate.Year - endDate.Year));
                double dblInterest = Convert.ToDouble(txtSavingsInterest.Text) / 100;
                double dblCurrentAmount = 0;

                dblGoal = dblGoal * dblInterest;
                dblCurrentAmount = Math.Pow(1 + dblInterest, dblMonths);
                dblCurrentAmount = dblCurrentAmount - 1;
                dblCurrentAmount = dblGoal / dblCurrentAmount;


                strDisplay += "\n\n\nSAVINGS:\n---------------------------------------\n" +
                    "Monthly Amount to save to reach goal = R"
                    + Math.Round(dblCurrentAmount);
            }
            

            return strDisplay;
        }
    }
}
