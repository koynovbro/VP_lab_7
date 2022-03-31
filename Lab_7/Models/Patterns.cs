using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace Lab_7.Models
{
    public class Patterns : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
			else return;
		}

        ISolidColorBrush[] color = new ISolidColorBrush[3];
        public ISolidColorBrush this[int i]
        {
            get
            {
                return color[i];
            }
            set
            {
                color[i] = value;
                NotifyPropertyChanged();
            }
        }


    }
}
