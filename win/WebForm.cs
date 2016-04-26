using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class WebForm : Form
    {
        private string url = "";
        public WebForm()
        {
            int BrowserVer, RegVal;

            // get the installed IE version
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            // set the appropriate IE version
            if (BrowserVer >= 11)
                RegVal = 11001;
            else if (BrowserVer == 10)
                RegVal = 10001;
            else if (BrowserVer == 9)
                RegVal = 9999;
            else if (BrowserVer == 8)
                RegVal = 8888;
            else
                RegVal = 7000;

            // set the actual key
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
            Key.Close();

            InitializeComponent();
            this.TransparencyKey = Color.FromArgb(255, 255, 0, 255);
            webBrowser1.Navigate(url);

        }
        protected override void OnLoad(EventArgs e)
        {
            var newPosition = new Point();
            var screen = Screen.FromControl(this);
            newPosition.X = screen.WorkingArea.Right - this.Width - 20;
            newPosition.Y = screen.WorkingArea.Bottom - this.Height - 20;
            this.Location = newPosition;
            base.OnLoad(e);
        }
    }
}
