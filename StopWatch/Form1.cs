using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;


namespace StopWatch
{
    public partial class Form1 : Form
    {
        //seven-segment-styoe font
        //https://stackoverflow.com/questions/47391827/how-to-show-seven-segment-display-in-a-textbox

        //on-top
        //https://stackoverflow.com/questions/683330/how-to-make-a-window-always-stay-on-top-in-net
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        //timer updaeing label
        //https://stackoverflow.com/questions/19960438/timer-updating-label/19962989
        System.Windows.Forms.Timer timer;
        Stopwatch sw;
        public Form1()
        {
            InitializeComponent();
            //winform on bottom-right
            // https://stackoverflow.com/questions/1385674/place-winform-on-bottom-right
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            this.TopMost = true;

            //round-shape-button
            //https://stackoverflow.com/questions/3708113/round-shaped-buttons
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

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

        }
    }
}