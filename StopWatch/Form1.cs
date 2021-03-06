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
        private KeyHandler ghk;
        public Form1()
        {
            ghk = new KeyHandler(Keys.Multiply, this);
            ghk.Register();

            InitializeComponent();
            //winform on bottom-right
            // https://stackoverflow.com/questions/1385674/place-winform-on-bottom-right
            this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            //middle top center    
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 0);
            this.TopMost = true;

            //round-shape-button
            //https://stackoverflow.com/questions/3708113/round-shaped-buttons
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(1, 1, button1.Width - 4, button1.Height - 4);
            button1.Region = new Region(p);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //encaspulate methad call in a button click
        //https://social.msdn.microsoft.com/Forums/windows/en-US/26546603-748f-482c-8f4d-ad454663bb9e/how-to-call-button1clickobject-sender-eventargs-e-from-other-method?forum=winforms
        void StartTimer()
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

        private void button1_Click(object sender, EventArgs e)
        {
            StartTimer();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = sw.Elapsed.Minutes.ToString("00") + ":" + sw.Elapsed.Seconds.ToString("00");
            Application.DoEvents();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

        }
        //detect key form not focused
        //https://stackoverflow.com/questions/18291448/how-do-i-detect-keypress-while-not-focused
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.VK_MULTIPLY)
                StartTimer();
            base.WndProc(ref m);
        }
    }
}