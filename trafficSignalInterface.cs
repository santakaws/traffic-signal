using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class trafficSignalInterface: Form {
    private Panel header = new Panel();
    private Graphicpanel lights = new Graphicpanel();
    private Panel buttons = new Panel();
    private Size minimumSize = new Size(450,690);
    private Size maximumSize = new Size(450,690);
    private Label title = new Label();
    private Label author = new Label();
    private Button start = new Button();
    private Button fast = new Button();
    private Button slow = new Button();
    private Button pause = new Button();
    private Button exit = new Button();
    private static System.Timers.Timer redClock = new System.Timers.Timer();
    private static System.Timers.Timer greenClock = new System.Timers.Timer();
    private static System.Timers.Timer yellowClock = new System.Timers.Timer();
    private enum Light {blank, red, green, yellow};
    private static Light current = Light.blank;
    bool boot = false;
    bool isPaused = false;

    public trafficSignalInterface() {
        MinimumSize = minimumSize;
        MaximumSize = maximumSize;

        Size = new Size(450,690);
        header.Size = new Size(450,60);
        lights.Size = new Size(450,530);
        buttons.Size = new Size(450,61);
        title.Size = new Size(200,30);
        author.Size = new Size(200,20);
        start.Size = new Size(70,40);
        fast.Size = new Size(70,40);
        slow.Size = new Size(70,40);
        pause.Size = new Size(70,40);
        exit.Size = new Size(70,40);

        header.BackColor = Color.Goldenrod;
        lights.BackColor = Color.Thistle;
        buttons.BackColor = Color.RoyalBlue;
        start.BackColor = Color.Maroon;
        fast.BackColor = Color.Maroon;
        slow.BackColor = Color.Maroon;
        pause.BackColor = Color.Maroon;
        exit.BackColor = Color.Maroon;

        header.Location = new Point(0,0);
        lights.Location = new Point(0,60);
        buttons.Location = new Point(0,590);
        title.Location = new Point(112,0);
        author.Location = new Point(112,35);
        start.Location = new Point(8,10);
        fast.Location = new Point(94,10);
        slow.Location = new Point(180,10);
        pause.Location = new Point(266,10);
        exit.Location = new Point(352,10);

        Text = "Traffic Signal";
        title.Text = "Traffic Signal";
        author.Text = "By Brennon Hahs";
        start.Text = "Start";
        fast.Text = "Fast";
        slow.Text = "Slow";
        pause.Text = "Pause";
        exit.Text = "Exit";

        title.Font = new Font("Times New Roman",20,FontStyle.Bold);
        author.Font = new Font("Times New Roman",10,FontStyle.Regular);
        start.Font = new Font("Arial",10,FontStyle.Bold);
        fast.Font = new Font("Arial",10,FontStyle.Bold);
        slow.Font = new Font("Arial",10,FontStyle.Bold);
        pause.Font = new Font("Arial",10,FontStyle.Bold);
        exit.Font = new Font("Arial",10,FontStyle.Bold);

        title.TextAlign = ContentAlignment.MiddleCenter;
        author.TextAlign = ContentAlignment.MiddleCenter;
        start.TextAlign = ContentAlignment.MiddleCenter;
        fast.TextAlign = ContentAlignment.MiddleCenter;
        slow.TextAlign = ContentAlignment.MiddleCenter;
        pause.TextAlign = ContentAlignment.MiddleCenter;
        exit.TextAlign = ContentAlignment.MiddleCenter;

        Controls.Add(header);
        header.Controls.Add(title);
        header.Controls.Add(author);
        Controls.Add(lights);
        Controls.Add(buttons);
        buttons.Controls.Add(start);
        buttons.Controls.Add(fast);
        buttons.Controls.Add(slow);
        buttons.Controls.Add(pause);
        buttons.Controls.Add(exit);
        
        start.Click += new EventHandler(startSignal);
        fast.Click += new EventHandler(setFast);
        slow.Click += new EventHandler(setSlow);
        pause.Click += new EventHandler(pauseResume);
        exit.Click += new EventHandler(shutdown);

        redClock.Elapsed += new ElapsedEventHandler(startSignal);
        yellowClock.Elapsed += new ElapsedEventHandler(startSignal);
        greenClock.Elapsed += new ElapsedEventHandler(startSignal);

        CenterToScreen();
    }

    void pauseResume(Object sender, EventArgs events) {
        isPaused = !isPaused;
        
        if (isPaused == true) {
            redClock.Enabled = false;
            yellowClock.Enabled = false;
            greenClock.Enabled = false;
            pause.Text = "Resume";
        }
        else {
            if (boot == true) {
                redClock.Enabled = true;
                yellowClock.Enabled = true;
                greenClock.Enabled = true;
            }
            pause.Text = "Pause";
        }
    }

    void setFast(Object sender, EventArgs events) {
        redClock.Interval = 4000;
        yellowClock.Interval = 1000;
        greenClock.Interval = 4000;
    }

    void setSlow(Object sender, EventArgs events) {
        redClock.Interval = 8000;
        yellowClock.Interval = 2000;
        greenClock.Interval = 8000;
    }

    void startSignal(Object sender, EventArgs events) {
        redClock.Enabled = false;
        yellowClock.Enabled = false;
        greenClock.Enabled = false;
        
        if (boot == false) {
            redClock.Interval = 8000;
            yellowClock.Interval = 2000;
            greenClock.Interval = 8000;
        }
        if (current == Light.red) {
            current = Light.green;
            lights.Invalidate();
            greenClock.Enabled = true;
        }
        else if (current == Light.green) {
            current = Light.yellow;
            lights.Invalidate();
            yellowClock.Enabled = true;
        }
        else {
            current = Light.red;
            lights.Invalidate();
            redClock.Enabled = true;
        }
        boot = true;
    }

    void shutdown(Object sender, EventArgs events) {
        Close();
    }

    public class Graphicpanel: Panel {
        private Brush greenBrush = new SolidBrush(Color.Green);
        private Brush yellowBrush = new SolidBrush(Color.Yellow);
        private Brush redBrush = new SolidBrush(Color.Red);
        private Brush bobRoss = new SolidBrush(Color.Gray);
        private Pen bicPen = new Pen(Color.Black, 3);

        public Graphicpanel() {
            Console.WriteLine("A graphic enabled panel was created");
        }
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.DrawEllipse(bicPen, 140, 13, 150, 150);
            g.DrawEllipse(bicPen, 140, 190, 150, 150);
            g.DrawEllipse(bicPen, 140, 365, 150, 150);
            
            if (current == Light.blank) {
                g.FillEllipse(bobRoss, 140, 13, 150, 150);
                g.FillEllipse(bobRoss, 140, 190, 150, 150);
                g.FillEllipse(bobRoss, 140, 365, 150, 150);
            }
            if (current == Light.red) {
                Console.WriteLine("Red");
                g.FillEllipse(redBrush, 140, 13, 150, 150);
                g.FillEllipse(bobRoss, 140, 190, 150, 150);
                g.FillEllipse(bobRoss, 140, 365, 150, 150);
            }
            if (current == Light.yellow) {
                Console.WriteLine("Yellow");
                g.FillEllipse(bobRoss, 140, 13, 150, 150);
                g.FillEllipse(yellowBrush, 140, 190, 150, 150);
                g.FillEllipse(bobRoss, 140, 365, 150, 150);
            }
            if (current == Light.green) {
                Console.WriteLine("Green");
                g.FillEllipse(bobRoss, 140, 13, 150, 150);
                g.FillEllipse(bobRoss, 140, 190, 150, 150);
                g.FillEllipse(greenBrush, 140, 365, 150, 150);
            }
            base.OnPaint(e);
        }
    }
}