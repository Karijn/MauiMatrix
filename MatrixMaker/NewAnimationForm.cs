using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixMaker
{
    public partial class NewAnimationForm : Form
    {
        public NewAnimationForm()
        {
            InitializeComponent();
        }

        public int ImageWidth
        {
            get { return (int)udWidth.Value; }
            set { udWidth.Value = value; }
        }
        public int ImageHeight
        {
            get { return (int)udHeight.Value; }
            set { udHeight.Value = value; }
        }
    }
}
