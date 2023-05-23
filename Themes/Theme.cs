using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestApp.Themes
{
    public class Theme
    {
        public int id { get; set; }
        public string Name { get; set; }
        public Color Background { get; set; }
        public Color Borders { get; set; }
        public Color BackgroundTwo { get; set; }
        public Color Main { get; set; }

    }
}
