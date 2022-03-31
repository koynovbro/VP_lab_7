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
	public class Students : INotifyPropertyChanged
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

		string fio;
		public string Fio
        {
			get { return fio; }
			set { fio = value; NotifyPropertyChanged(); }
        }

		int[] rating = new int[3];
		public int[] Rating { 
			get { 
				return rating; 
			} 
			set { 
				rating = value;
				NotifyPropertyChanged();
			} 
		}
		string srRate;
		public string SrRate
        {
            get { return srRate; }
			set { srRate = value; NotifyPropertyChanged(); }
        }

		public void UpdateSrRate()
        {
			float sum = 0;
			float answ = 0;
			for (int i = 0; i < 3; i++)
            {
				sum += rating[i];
            }
			answ = (sum / 3);
			SrRate = answ.ToString();

			if (answ >= 1.5) Color = Brushes.SpringGreen;
			else if (answ >= 1 && answ < 1.5) Color = Brushes.Gold;
			else if (answ < 1) Color = Brushes.DarkRed;
			else Color = Brushes.White;
			NotifyPropertyChanged();


		}

        public int this[int i] 
		{
			get
			{
				return rating[i];
			}
			set
			{
				if (value > -1 && value < 3) { rating[i] = value; UpdateSrRate(); }
				else { rating[i] = 404; SrRate = "#ERROR"; Color = Brushes.White; }
				NotifyPropertyChanged();
			}
		}
		bool check;
		public bool Check
		{
			get 
			{ 
				return check; 
			}
			set
			{
				check = value;
				NotifyPropertyChanged();
			}
		}

		ISolidColorBrush color;
		public ISolidColorBrush Color
		{
			get 
			{ 
				return color;
			}
			set
			{
				color = value;
				NotifyPropertyChanged();
			}
		}

		public Students(string FIO, int[] rt)
		{
			fio = FIO;
			rating = rt;
			UpdateSrRate();
		}
	}
}
