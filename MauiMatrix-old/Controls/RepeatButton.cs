using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMatrix.Controls
{
    public class RepeatButton : Button
    {
        System.Timers.Timer timer;

        public RepeatButton() : base()
        {
            this.Pressed += RepeatButton_Pressed;
            this.Released += RepeatButton_Released;
            timer = new System.Timers.Timer(400);
            timer.Enabled = false;
            timer.Elapsed += Timer_Elapsed; ;
        }

        private void RepeatButton_Pressed(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendClicked();
        }

        private void RepeatButton_Released(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        
    }
}
