// Game Form
using Pizza_Form.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza_Form
{
    public partial class DoingTheMathGame : Form
    {
        readonly MathGame.clsSettings Settings;
        public DoingTheMathGame(MathGame.clsSettings settings)
        {
            InitializeComponent();
            Settings = settings;
            lbLevel.Text = settings.Selected_Level.ToString();
            lbQuizType.Text = settings.Selected_OprationType.ToString();
            lbTotalRounds.Text = settings.Number_OfQ.ToString();
            lbTimePerQuis.Text = settings.Time_PerQ.ToString();
            TimeLeft.Tag = settings.Time_PerQ;
            TimeLeft.Text = TimeLeft.Tag.ToString();
            PerformApp();
        }
        int Right_Answer = 0;
        int Time_Left;
        int Total_Time_Took;
        int Round_Number = 0;
        Random random = new Random();
        Random random2 = new Random();

        public void Solve_Q(int n1 , int n2 , char Symbol)
        {
            switch (Symbol) {
                case '+':
                    Right_Answer = n1+n2;
                    break;
                case '-':
                    Right_Answer = n1-n2;
                    break;
                case '/':
                    Right_Answer = n1 / n2;
                    break;
                case '*':
                    Right_Answer = n1 * n2;
                    break;

            }
        }
        public char Get_Random_op()
        {
            Random random = new Random();
            int random_int= random.Next(1, 4);
            switch (random_int) {
                case 1:
                    return '+';
                    break;
                case 2:
                    return '-';
                    break; 
                case 3:
                    return '/';
                    break;
                case 4:
                    return '*';
                    break;
            }
            return 'n';
        }
        public void Q()
        {
            int From = 0 , To = 0;
            char Symbol = 'n';
            // Check Level
            switch (Settings.Selected_OprationType)
            {
                case MathGame.clsSettings.enOprationType.Add:
                    Symbol = '+';
                    break;
                case MathGame.clsSettings.enOprationType.Sub:
                    Symbol = '-';
                    break;
                case MathGame.clsSettings.enOprationType.Multi:
                    Symbol = '*';
                    break;
                case MathGame.clsSettings.enOprationType.Div:
                    Symbol = '/';
                    break;
                case MathGame.clsSettings.enOprationType.Mix:
                    Symbol = Get_Random_op();
                    break;
            }

            switch (Settings.Selected_Level)
            {
                case MathGame.clsSettings.enLevel.Easy:
                    From = 1;
                    To = 50;

                    break;
                case MathGame.clsSettings.enLevel.Med:
                    From = 50;
                    To = 100;
                    break;
                case MathGame.clsSettings.enLevel.Hard:
                    From = 100;
                    To = 300;
                    break;
            }
            Number1.Text = random.Next(From, To).ToString();
            if (Symbol == '/' || Symbol == '*')
            {
                Number2.Text = random2.Next(2, 10).ToString();
            }
            if (Symbol == '-')
            {
                Number2.Text = random2.Next(2, Convert.ToInt32(Number2.Text)).ToString();
            }
            if (Symbol == '+')
            { 
                Number2.Text = random2.Next(From,(int)(To * 0.5)).ToString();

            }
            TypeOFoprationSign.Text = Convert.ToString(Symbol);
            Solve_Q(Convert.ToInt32(Number1.Text), Convert.ToInt32(Number2.Text), Symbol);
        }
        public void GenerateQuis()
        {
            Round_Number++;
            lbRoundNumber.Text = Round_Number.ToString();
            if (Round_Number <= Settings.Number_OfQ)
            {
                Answer.Value = 0;
                Time_Left = Settings.Time_PerQ;
                TimeLeft.Text = Time_Left.ToString();
                timer1.Start();
                Optimize_Hint();
                Q();
            }
            else
            {

                lbGameOver.Visible = true;
                gbGameOver.Visible = true;
                Form frm = new Results(Settings);
                timer1.Stop();
                frm.ShowDialog();
                this.Close();
            }
        }
        public void Optimize_Hint()
        {
            Random Random2  = new Random();
            int GreaterThan = Right_Answer - Random2.Next(3,15);
            int LessThan    = Right_Answer + Random2.Next(3, 15);
            TheNumberGreaterThan.Text = GreaterThan.ToString();
            TheNumberLessThan.Text = LessThan.ToString();

        }
        private void ShowHint_Click(object sender, EventArgs e)
        {
            lbNumberIsHint.Visible = !lbNumberIsHint.Visible;
            lbLessThan.Visible = !lbLessThan.Visible;
            lbGreaterThan.Visible = !lbGreaterThan.Visible;
            TheNumberGreaterThan.Visible = !TheNumberGreaterThan.Visible;
            TheNumberLessThan.Visible = !TheNumberLessThan.Visible;
            if (lbNumberIsHint.Visible) {
                ShowHint.Text = "Hide Hint";
                Optimize_Hint();
            }
            else
            {
                ShowHint.Text = "Show Hint";
            }
            
        }
        public void PerformApp()
        {
            GenerateQuis();
        }

        private void Confirm_btn_Click(object sender, EventArgs e)
        {
            if (Answer.Value == Right_Answer) {
                Settings.True++;
            }
            else
            {
                Settings.False++;
            }
            lbTrue.Text = Settings.True.ToString();
            lbFalse.Text = Settings.False.ToString();
            GenerateQuis();
        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time_Left--;
            Settings.TotalTimeTook++;
            TimeLeft.Text = Time_Left.ToString();
            if (Time_Left == 0)
            {
                Settings.False++;
                Answer.Value = 0;
                GenerateQuis();
            }
            lbFalse.Text = Settings.False.ToString();

        }
    }
}
