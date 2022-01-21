using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace StopWatch
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        Stopwatch sw;
        public Form1()
        {
            InitializeComponent();

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(1, 1, button1.Width - 4, button1.Height - 4);
            button1.Region = new Region(p);
        }
        public class RoundButton : Button
        {
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                GraphicsPath grPath = new GraphicsPath();
                grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
                this.Region = new System.Drawing.Region(grPath);
                base.OnPaint(e);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "START")
            {
                timer = new System.Windows.Forms.Timer();
                timer.Interval = (1000);
                timer.Tick += new EventHandler(timer1_Tick);
                sw = new Stopwatch();
                timer1.Start();
                sw.Start();
                button1.Text = "STOP";
                button1.BackColor = Color.Red;
            }
            else
            {
                timer1.Stop();
                sw.Stop();
                button1.Text = "START";
                button1.BackColor = Color.Green;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = sw.Elapsed.Hours.ToString("00") + ":" + sw.Elapsed.Minutes.ToString("00") +":"+ sw.Elapsed.Seconds.ToString("00");
            Application.DoEvents();
        }
    }
}