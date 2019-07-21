using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimedMathQuizProject
{
    public partial class Form1 : Form
    {
        // Create a Random object called randomizer 
        // to generate random numbers.
        Random randomizer = new Random();
        // These integer variables store the numbers 
        // for the addition problem. 
        int addend1;
        int addend2;

        // These integer variables store the numbers 
        // for the subtraction problem. 
        int minuend;
        int subtrahend;

        // These integer variables store the numbers 
        // for the multiplication problem. 
        int multiplicand;
        int multiplier;

        // These integer variables store the numbers 
        // for the division problem. 
        int dividend;
        int divisor;
        // This integer variable keeps track of the 
        // remaining time.
        int timeLeft;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timeLabel1.Text = DateTime.Now.ToLongDateString();

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }
       
        /// <summary>
        /// Start the quiz by filling in all of the problems
        /// and starting the timer.
        /// </summary>
        public void StartTheQuiz()
        {

            //set BackColor for numCounter
            sum.BackColor = Color.Red;
            difference.BackColor = Color.Red;
            product.BackColor = Color.Red;
            quotient.BackColor = Color.Red;
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //check if time is remaining
            if (timeLeft > 0)
            {
                // Display the new time left
                // by updating the Time Left label.
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show 
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
                restartTest_option();
            }

            //check if the anwsere are all completed 
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right with " + timeLeft + " seconds left, Congratulations!");
                startButton.Enabled = true;
                restartTest_option();


            }

            //display warning if time is below 10 seconds 
            if (timeLeft < 10 && timeLeft > 5)
            {
                timeLabel.BackColor = Color.Yellow;
               
            }

            else if (timeLeft <= 5)
            {
                if (timeLabel.BackColor == Color.Yellow)
                {
                    timeLabel.BackColor = Color.Red;
                    timeWarning.BackColor = Color.Yellow;
                    timeWarning.Text = "Hurry!";
                }

                else
                {
                    timeLabel.BackColor = Color.Yellow;
                    timeWarning.BackColor = Color.Red;
                    timeWarning.Text = "Time is almost out!";
                }

            }
           
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        /// <summary>
        /// Check the answer to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {                     

            if (addend1 + addend2 == sum.Value)
            {
                sum.BackColor = Color.LightGreen;
            }
            if (minuend - subtrahend == difference.Value)
            {
                difference.BackColor = Color.LightGreen;
            }
            if (multiplicand * multiplier == product.Value)
            {
                product.BackColor = Color.LightGreen;
            }
            if (dividend / divisor == quotient.Value)
            {
                quotient.BackColor = Color.LightGreen;
            }


            if ((addend1 + addend2 == sum.Value)
         && (minuend - subtrahend == difference.Value)
         && (multiplicand * multiplier == product.Value)
         && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;

        }

        private void answer_enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void restartTest_option()
        {
            DialogResult dialogResult = MessageBox.Show("Want to beat your time?", "Retake the Test", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                restartTest();
                StartTheQuiz();


            }
            else if (dialogResult == DialogResult.No)
            {
                Application.Exit();          

            }
            
        }
        private void restartTest()
        {
            //make sure all warnings are clear
            startButton.Enabled = true;
            timeWarning.BackColor = Color.White;
            timeWarning.Text = "";
            // reset text
            sum.Value = 0;
            difference.Value = 0;
            product.Value = 0;
            quotient.Value = 0;
         







        }



    }
}



