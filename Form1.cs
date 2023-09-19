using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypingSimulator
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;

        public Form1()
        {
            InitializeComponent();

            // Initialize the system tray icon and context menu
            trayIcon = new NotifyIcon();

            trayIcon.Icon = new Icon("tray_stop.ico");// Replace with your custom icon
            trayIcon.Visible = false;
            trayIcon.Click += ExitApplication;

            // Hide the main form
            //this.WindowState = FormWindowState.Minimized;
            //this.ShowInTaskbar = false;
        }

        private void SendTextToOtherApplication(object sender, EventArgs e)
        {
            // Get the text you want to send from a TextBox or any other source
            string textToSend = textBoxInput.Text; // Replace with your source
            //Hide the main form
            trayIcon.Visible = true;
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Thread.Sleep(5000);
            // Use SendKeys to send the text to another application
            foreach (char s in textToSend)
            {
                string ss = s.ToString();
                if (s.ToString().Equals("("))
                    ss = ss.Replace("(", "{(}");
                if (s.ToString().Equals(")"))
                    ss = ss.Replace(")", "{)}");
                if (s.ToString().Equals("{"))
                    ss = ss.Replace("{", "{{}");
                if (s.ToString().Equals("}"))
                    ss = ss.Replace("}", "{}}");
                if (s.ToString().Equals("\n"))
                    ss = ss.Replace("\n", "{ENTER}");
                Thread.Sleep(200);
                SendKeys.SendWait(ss);
            }
            trayIcon.Visible = false;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            // Clean up and exit the application
            trayIcon.Visible = false;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            //Application.Exit();
            SendKeys.Flush();
        }

        // Rest of your form code
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendTextToOtherApplication(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendTextToOtherApplication(sender, e);
        }
    }
}
