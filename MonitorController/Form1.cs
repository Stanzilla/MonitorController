using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonitorController
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "MonitorController";
            AutoSize = true;
        }

        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Monitors.DisplayInfo monitor = cb.Tag as Monitors.DisplayInfo;

            // Send a power command
            bool res = Monitors.SetVCPFeature(monitor.PhysicalMonitor, 0xD6, cb.Checked ? Monitors.POWER_ON : Monitors.POWER_STANDBY);

            //MessageBox.Show(res.ToString());

            // Samsung
            //Monitors.SetVCPFeature(monitor.PhysicalMonitor, 0xE1, cb.Checked ? (uint)1 : 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var monitors = Monitors.GetDisplays();
            //var monitor = monitors.First();

            int i = 0;

            foreach (var monitor in monitors)
            {
                CheckBox cb = new CheckBox()
                {
                    AutoSize = true,
                    Location = new Point(12, 20 * i),
                    Name = "checkBox" + i,
                    Size = new Size(80, 17),
                    TabIndex = 1 + i,
                    Text = monitor.Description + " " + i,
                    UseVisualStyleBackColor = true,
                    Tag = monitor,
                    Padding = new Padding(4),
                    Checked = true
                };
                cb.CheckedChanged += Cb_CheckedChanged;
                this.Controls.Add(cb);

                i++;
            }
        }        
    }
}
