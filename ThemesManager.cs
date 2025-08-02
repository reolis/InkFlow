using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InkFlow
{
    public enum Theme
    {
        Light,
        Dark
    }

    internal class ThemesManager
    {
        public Theme CurrentTheme { get; set; }

        public void ApplyTheme(Form form, Theme theme)
        {
            CurrentTheme = theme;

            Color backColor;
            Color foreColor;

            if (theme == Theme.Dark)
            {
                backColor = Color.FromArgb(30, 30, 30);
                foreColor = Color.White;
            }
            else
            {
                backColor = Color.White;
                foreColor = Color.Black;
            }

            ApplyToControls(form.Controls, backColor, foreColor);
            form.BackColor = backColor;
            form.ForeColor = foreColor;
        }

        private void ApplyToControls(Control.ControlCollection controls, Color backColor, Color foreColor)
        {
            foreach (Control control in controls)
            {
                control.BackColor = backColor;
                control.ForeColor = foreColor;

                if (control.HasChildren)
                {
                    ApplyToControls(control.Controls, backColor, foreColor);
                }
            }
        }
    }
}
