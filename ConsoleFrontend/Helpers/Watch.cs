using bCurses.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrontend.Helpers
{
    public class Watch : BaseModel
    {
        public string Time => DateTime.Now.ToString("HH:mm:ss");

        public Watch()
        {
            UpdateTimeTask().ConfigureAwait(false);
        }

        private async Task UpdateTimeTask()
        {
            while(true)
            {
                await Task.Delay(200);
                NotifyPropertyChanged(nameof(Time));
            }
        }
    }
}
