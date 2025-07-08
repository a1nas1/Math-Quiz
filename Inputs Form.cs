// Form 1 User Inputs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza_Form
{


    public partial class MathGame : Form
    {
        public MathGame()
        {
            InitializeComponent();
        }
        public class clsSettings
        {
            public enum enLevel
            {
                Easy,
                Med,
                Hard
            }
            public enum enOprationType
            {
                Add,
                Sub,
                Multi,
                Div,
                Mix
            }
            public enLevel Selected_Level { get; set; }
            public enOprationType Selected_OprationType { get; set; }
            public byte Number_OfQ { get; set; }
            public byte Time_PerQ { get; set; }
            public int TotalTimeTook { get; set; } = 0;
            public byte True { get; set; } = 0;
            public byte False { get; set; } = 0;
            public string AllOpratins { get; set; }
        }
        private void Restart_Btn_Click(object sender, EventArgs e)
        {
            // Restart Level

            Easy.Checked = false;
            Meduim.Checked = false;
            Hard.Checked = false;

            // Restart Inputs
            NumberOfQ.Value = NumberOfQ.Minimum;
            TimePerQ.Value = TimePerQ.Minimum;

            // Restart Type 
            rbAddition.Checked = false;
            rbsubtraction.Checked = false;
            rbdivision.Checked = false;
            rbMix.Checked = false;
            rbMultiplication.Checked = false;
        }
        public void Start_btn_Click(object sender, EventArgs e)
        {
            clsSettings Settings = new clsSettings();
            // Level Check
            if (!(Easy.Checked || Meduim.Checked || Hard.Checked))
            {
                MessageBox.Show("Please Enter Level ");
                return;
            }

            Settings.Selected_Level = Easy.Checked ? clsSettings.enLevel.Easy :
                Meduim.Checked ? clsSettings.enLevel.Med : clsSettings.enLevel.Hard;

            // Op type
            if (!(rbAddition.Checked || rbsubtraction.Checked || rbMultiplication.Checked || rbdivision.Checked || rbMix.Checked))
            {
                MessageBox.Show("Please Enter Opration Type");
                return;
            }
            Settings.Selected_OprationType = rbAddition.Checked ? clsSettings.enOprationType.Add :
                rbsubtraction.Checked ? clsSettings.enOprationType.Sub :
                rbMultiplication.Checked ? clsSettings.enOprationType.Multi :
                rbdivision.Checked ? clsSettings.enOprationType.Div :
                clsSettings.enOprationType.Mix;
            Settings.Number_OfQ = Convert.ToByte(NumberOfQ.Value);
            Settings.Time_PerQ = Convert.ToByte(TimePerQ.Value);
            Form frm = new DoingTheMathGame(Settings);

            this.Hide();

            frm.ShowDialog();

            this.Close();

        }

        private void MathGame_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    }
