using PluginImplementation.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginImplementation.WindowsForms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            loadMenuToolStripMenuItem_Click(null, null);
        }

        private void loadMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exeName = Application.ExecutablePath;
            string folder = Path.Combine(Path.GetDirectoryName(exeName), "Plugins");

            List<IPlugin> list = Utility.GetPlugins<IPlugin>(folder);

            menuStrip.Items.Clear();
            foreach (var item in list)
            {
                var menuStripItem = new ToolStripMenuItem { Text = item.MenuTitle };

                foreach (var ddItem in item.Menu)
                    menuStripItem.DropDownItems.Add(ddItem);

                menuStrip.Items.Add(menuStripItem);
            }

            lblMessage.Text = String.Format("{0} plugin(s) loaded.", list.Count);

            // Load Menu Item
            var loadMenu = new ToolStripMenuItem { Text = "Load Menu" };
            loadMenu.Click += new EventHandler(loadMenuToolStripMenuItem_Click);
            menuStrip.Items.Add(loadMenu);

            // Exit Menu Item
            var exitMenu = new ToolStripMenuItem { Text = "Exit" };
            exitMenu.Click += new EventHandler(exitToolStripMenuItem_Click);
            menuStrip.Items.Add(exitMenu);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK ==
                    MessageBox.Show(
                        "Are you sure you want to exit?",
                        "Confirmation",
                        MessageBoxButtons.OKCancel
                    )
                )
                Environment.Exit(0);
        }
    }
}
