using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        //set up inital variables
        string firstnum = "";
        string secondnum = "";
        string mathsymbol;
        bool secondnumber = false;
        string memorynum = "";
        private double num1; //number input 1 variable 
        private double num2 = 0; // number input 2 variable
        int facTotal;



        //equation code
        private void calcutation()
        {
            double total = 0;  //total variable
            num1 = double.Parse(firstnum); //converts firstnum to double
            num2 = double.Parse(secondnum); //converts secondnum to double



            switch (mathsymbol) //does equation based in which opertator is entered
            {

                case "+": //addition
                    total = num1 + num2;
                    break;
                case "x": //multiplecation
                    total = num1 * num2;
                    break;
                case "/": //division
                    total = num1 / num2;
                    break;
                case "^": // exponent/power of
                    total = Math.Pow(num1, num2);
                    break;
                default: //debug if an operator isn't used
                    if (!txtinputs.Text.Contains(firstnum) && !txtinputs.Text.Contains(secondnum))
                    {

                    }
                    break;
            }
            //subtraction
            if (txtinputs.Text.Contains(num1 + "-" + num2))
            {
                total = num1 - num2;
            }

            txtinputs.Text = total.ToString();//the text box now shows the total
            lboxInput.Items.Add(total); //the total is added to the list box
            num1 = total; //num1 in now equal to the total so the user can continue off the equation they just did
            secondnum = ""; //secondnum is set back to nothing 
            secondnumber = false; //secondnumber bool is switched back to false 
            firstnum = total.ToString(); //firstnum is now equal to the total
            if (txtinputs.Text.Contains("-"))
            {
                txtinputs.ForeColor = Color.Red;
                txtinputs.Refresh();
            }
            else
            {
                txtinputs.ForeColor = Color.Black;
                txtinputs.Refresh();
            }

            foreach (object lboxob in lboxInput.Items)
            {
                if (lboxob.ToString().Contains("-"))
                {
                    lboxInput.ForeColor = Color.Red;
                }
                else
                {
                    lboxInput.ForeColor = Color.Black;
                }
            }
            lblfocus.Focus();//Puts focus on invisble label
        }

        //Exit button
        private void btnexit_Click(object sender, EventArgs e)
        {
            Application.Exit(); //pressing exit closes app
        }


        //Clear text box button
        private void btnclear_Click(object sender, EventArgs e)
        {
            txtinputs.Clear(); //clears text box
            firstnum = ""; //clears firstnum variable
            secondnum = ""; //clears secondnum varible
            mathsymbol = ""; //clears mathsymbol variable
            secondnumber = false; //no second number so secondnumber boolean needs to be false
            lblfocus.Focus();//Puts focus on invisble label
        }



        //number input code
        private void btn1_Click(object sender, EventArgs e)
        {
            Button number = sender as Button; //creates variable for all number buttons

            //creates the two numbers for the equation
            if (secondnumber == false)//if secondnumber bool is false then any digits entered belong to first number
            {
                
                if (!txtinputs.Text.Contains("-")) //lets the user start with a negative number if entered
                {   
                    firstnum += number.Text; 
                }
                else
                {
                    firstnum = (txtinputs.Text + number.Text);
                }

            }
            else
            {
                //if secondnumber bool is true then any digit entered belongs to second number
                secondnum += number.Text;
            }
            txtinputs.Text += number.Text;//inputs the digits the user is entering to the right of the current
            lblfocus.Focus();//Puts focus on invisble label
        }



        //operator code
        private void btndivide_Click(object sender, EventArgs e)
        {
            Button msymbol = sender as Button; //creates variable for math symbol buttons



            if (txtinputs.Text.Length == 0)//Debug: if user enters a math symbol before a number
            {
                
            }
            else if (txtinputs.Text.EndsWith("-") || txtinputs.Text.EndsWith("+") || txtinputs.Text.EndsWith("x") || txtinputs.Text.EndsWith("/") || txtinputs.Text.EndsWith("^"))//Debug: If the user tries to enter a second negative sign at the begining 
            {
               
            }            
            else if (txtinputs.Text.Contains("+") || txtinputs.Text.Contains("x") || txtinputs.Text.Contains("/") || txtinputs.Text.Contains("^")) //lets the user continue equations easier
            {
                btnequal.PerformClick();
                txtinputs.Text += msymbol.Text;
                mathsymbol = msymbol.Text;
                secondnumber = true;
            }
            else if (secondnumber == false)
            {
                mathsymbol = msymbol.Text; //The math symbol enter with take the place of mathsymbol variable
                secondnumber = true; //second number is switched to true so computer knows anything enter after this is the second number in equation
                txtinputs.Text += msymbol.Text; //adds the mathsymbol to the text box
            }
            lblfocus.Focus();//Puts focus on invisble label
        }



        //adds decimal
        private void btndecimal_Click(object sender, EventArgs e)
        {
            Button decimalplace = sender as Button; //creates a variable for the decimal button (probably unneeded)
            
            if (txtinputs.Text.EndsWith("."))
            {

            }
            else if (secondnumber == false) //if secondnumber boolean is false then the computer knows that the decimal goes with the first number in the equation
            {
                if (txtinputs.Text.EndsWith("-")) 
                {
                    txtinputs.Text = "-0" + decimalplace.Text; //puts the negative number with the 0 in the text box
                    firstnum = txtinputs.Text;
                }
                else if (txtinputs.Text.Length == 0) 
                {
                    txtinputs.Text = "0" + decimalplace.Text; //puts the positive number with the 0 in the text box
                    firstnum = txtinputs.Text;
                }
                else if (txtinputs.Text.Length > 0 && !txtinputs.Text.Contains("."))
                {
                    txtinputs.Text += decimalplace.Text;
                    firstnum = txtinputs.Text;
                }
                
            }
            else if (secondnumber == true && secondnum != "") //adds a 0 in front of a decimal if the user forgot to put one for number 2
            { 
                txtinputs.Text += decimalplace.Text; //puts the second nunmber with the 0 in the text box
                secondnum += decimalplace.Text;
            }
            else if (secondnumber == true && secondnum == "")
            {
                txtinputs.Text += "0.";
                secondnum = "0.";
            }
            lblfocus.Focus();//Puts focus on invisble label
        }



        
        private void btnequal_Click(object sender, EventArgs e)
        {
             calcutation(); //calls calcutions method when equals button is clicked
        }




        //turns textbox number to percent 
        private void btnpercent_Click(object sender, EventArgs e)
        {
            double percentage = 0; //creates percentage varible
            double numper; //creates numper variable

            if (txtinputs.Text.Length == 0)//Debug: checks to see if there is a number entered
            {
                //Debug: checks to see if there is a number entered
            }
            else
            {
                //Percent equation 
                if (txtinputs.Text.Contains("+") || txtinputs.Text.Contains("-") || txtinputs.Text.Contains("x") || txtinputs.Text.Contains("/"))//checks if theres an operator already entered 
                {
                   //Debug: if there is this error box shows 
                }
                else //if there's no operator already it goes ahead with percent  equation
                {
                    numper = double.Parse(txtinputs.Text); //converts textbox to double and makes it numper
                    percentage = numper * 100; //does percentage eqaution
                    txtinputs.Text = percentage.ToString(); //converts percentage variable to string and put it in text box
                }
            }
            lblfocus.Focus();//Puts focus on invisble label

        }

        //turns textbox code to its square root
        private void btnsroot_Click(object sender, EventArgs e)
        {
            double squareroot = 0; //creates squareroot variable
            double numsroot; //creates numsroot variable

            if (txtinputs.Text.Length == 0)//Debug: checks to see if there is a number entered
            {
                
            }
            else
            {
                //Square root equation
                if (txtinputs.Text.Contains("+") || txtinputs.Text.Contains("-") || txtinputs.Text.Contains("x") || txtinputs.Text.Contains("/"))//checks if theres an operator already entered 
                {
                    //Debug: if there is this error box shows 
                }
                else //if there's not operator already it goes ahead with the square root equation
                {
                    numsroot = double.Parse(txtinputs.Text); //converts whats in text box to double and is now numsroot
                    squareroot = Math.Sqrt(numsroot); //does square root equation
                    txtinputs.Text = squareroot.ToString(); //converts sqaureroot to string then puts it in text box
                }
            }
            lblfocus.Focus();//Puts focus on invisble label

        }

        //puts whats in text box into memory
        private void btnmemadd_Click(object sender, EventArgs e)
        {
            memorynum = txtinputs.Text; //creates memory variable and saves it as what's in text box
            lblfocus.Focus();//Puts focus on invisble label
        }

        //recalls memory
        private void btnmemrecall_Click(object sender, EventArgs e)
        {
            txtinputs.Text += memorynum; //puts what was saved as memory and adds it to text box
            lblfocus.Focus();//Puts focus on invisble label
        }

        //clears memory
        private void btnmemclear_Click(object sender, EventArgs e)
        {
            memorynum = ""; //resets memory back to nothing
            lblfocus.Focus();//Puts focus on invisble label
        }

        //minus/negative button
        private void btnminus_Click(object sender, EventArgs e)
        {
            if (txtinputs.Text.Length == 0) //lets the user start with a negative number
            {
                txtinputs.Text += "-";
            }
            else if (txtinputs.Text.EndsWith("-")) //Debug: if the user tries to enter two minus sign back to back 
            {
                
            }
            else if (secondnumber == false && txtinputs.Text.Length > 0 && !txtinputs.Text.Contains("-")) //sets minus as the math symbol
            {
                txtinputs.Text += "-";
                secondnumber = true;
            }
            else if (secondnumber == false && txtinputs.Text.Length >= 2) //
            {
                txtinputs.Text += "-";
                secondnumber = true;
            }
            else if (txtinputs.Text.EndsWith("+") || txtinputs.Text.EndsWith("x") || txtinputs.Text.EndsWith("/") || txtinputs.Text.EndsWith("^") || txtinputs.Text.EndsWith("-"))
            {

            }
            else if (txtinputs.Text.Contains("+") || txtinputs.Text.Contains("x") || txtinputs.Text.Contains("/") || txtinputs.Text.Contains("^")) //lets the user continue equations easier
            {
                btnequal.PerformClick();
                txtinputs.Text += "-";
                secondnumber = true;
            }
            else if (secondnumber == true)
            {
                if (secondnum != "" && secondnum != "0.")
                {
                    btnequal.PerformClick();
                    txtinputs.Text += "-";
                    secondnumber = true;
                }
            }
            lblfocus.Focus(); //Puts focus on invisble label
        }


        
        //Code to use keyboard keys instead of app ui
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.NumPad1 || e.KeyCode == Keys.D1) //lets the user trigger button with number 2 by pressing 1 on keyboard
            {
                btn1.PerformClick();
            }
            else if(e.KeyCode == Keys.NumPad2 || e.KeyCode == Keys.D2) //lets the user trigger button with number 2 by pressing 2 on keyboard
            {
                btn2.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad3 || e.KeyCode == Keys.D3) //lets the user trigger button with number 3 by pressing 3 on keyboard
            {
                btn3.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad4 || e.KeyCode == Keys.D4) //lets the user trigger button with number 4 by pressing 4 on keyboard
            {
                btn4.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.D5) //lets the user trigger button with number 5 by pressing 5 on keyboard
            {
                btn5.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad6 || e.KeyCode == Keys.D6) //lets the user trigger button with number 6 by pressing 6 on keyboard
            {
                btn6.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad7 || e.KeyCode == Keys.D7) //lets the user trigger button with number 7 by pressing 7 on keyboard
            {
                btn7.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad8 || e.KeyCode == Keys.D8) //lets the user trigger button with number 8 by pressing 8 on keyboard
            {
                btn8.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad9 || e.KeyCode == Keys.D9) //lets the user trigger button with number 9 by pressing 9 on keyboard
            {
                btn9.PerformClick();
            }
            else if (e.KeyCode == Keys.NumPad0 || e.KeyCode == Keys.D0) //lets the user trigger button with number 0 by pressing 0 on keyboard
            {
                btn0.PerformClick();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) //lets the user trigger button with plus by pressing + on keyboard
            {
                btnplus.PerformClick();
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) //lets the user trigger button with minus by pressing - on keyboard
            {
                btnminus.PerformClick();
            } 
            else if (e.KeyCode == Keys.Multiply || e.KeyCode == Keys.X) //lets the user trigger button with multiply by pressing x or * on keyboard
            {
                btnmultiply.PerformClick();
            }
            else if (e.KeyCode == Keys.Divide || e.KeyCode == Keys.OemQuestion) //lets the user trigger button with divide by pressing / on keyboard
            {
                btndivide.PerformClick();
            }
            else if (e.KeyCode == Keys.Enter) //lets the user trigger button with equals sign by pressing = on keyboard
            {
                btnequal.PerformClick();
            }
            else if (e.KeyCode == Keys.Decimal || e.KeyCode == Keys.OemPeriod) //lets the user trigger button with decimal by pressing . on keyboard
            {
                btndecimal.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape) //lets the user trigger button with Exit by pressing escape key on keyboard
            {
                btnexit.PerformClick();
            }
        }

        //Binary converstion button
        private void btnbinary_Click(object sender, EventArgs e)
        {
            if (secondnumber == false)//Button will only activate if theres one number and no math symbol
            {
                if (!firstnum.Contains("-") || !firstnum.Contains(".")) //button will only activate if the number entered is positive and whole
                {
                    int value = Convert.ToInt32(txtinputs.Text); //converts textbox inputs to integar and in set as value variable
                    string binary = Convert.ToString(value, 2); //
                    txtinputs.Text = binary; //set what is in the text box as binary variable
                    num1 = Convert.ToDouble(binary); //converts binary string variable to num1 double variable
                    firstnum = txtinputs.Text; //sets the converted number in text box to firstnum
                    lboxInput.Items.Add(binary); //and puts it in teh list box as well
                }
            }
        }

        //factorial equation
        private void btnFactorial_Click(object sender, EventArgs e)
        {
            int facStart = int.Parse(txtinputs.Text); //the number in text box is the number you want to find the facortial of
            int total = 1; //sets up total variable as 1 instead of 0 beacause we don't want to multiple by 0 when doing factorials
            for (int i = facStart; i >= 1; i--) //sets variable i as facStart, loop will run until i is less than 1, and subtracts 1 from i everytime its executed
            {
                total = total * i; 
            }
            txtinputs.Text = total.ToString(); //puts the product in txt box
        }
    }
}