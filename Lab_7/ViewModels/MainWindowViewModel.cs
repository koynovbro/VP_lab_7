using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ReactiveUI;
using Avalonia.Media;
using Lab_7.Models;
using System.IO;
using Avalonia.Controls;

namespace Lab_7.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            List<Students> Students = NewStudents();
            foreach (var item in Students)
                item.PropertyChanged += ContentCollectionChanged;
            Items = new ObservableCollection<Students>(Students);
            update(); 
        }
        ObservableCollection<Students> items;
        public ObservableCollection<Students> Items
        {
            get
            {
                return items;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref items, value);
                update();
            }
        }

        public void ContentCollectionChanged(object? sender, PropertyChangedEventArgs e)
        {
                update(); 
        }

        double[] AllRate = new double[] { 0, 0, 0 };
        public double this[int i]
        { 
            get 
            { 
                return AllRate[i]; 
            } 
            set 
            {
                this.RaiseAndSetIfChanged(ref AllRate[i], value);
                
            } 
        }
        Patterns color = new Patterns();
        Patterns Color { get { return color; } set { this.RaiseAndSetIfChanged(ref color, value); } }

        public void update()
        {
            int count = 0;
            foreach (var item in items) count++;
            for (int j = 0; j < 3; j++)
            {
                this[j] = 0;
                foreach (var item in items) { if (item.Rating[j] != 404) this[j] += item.Rating[j]; }
                this[j] /= count;

                if (this[j] >= 1.5) Color[j] = Brushes.SpringGreen;
                else if (this[j] >= 1 && this[j] < 1.5) Color[j] = Brushes.Gold;
                else if (this[j] < 1) Color[j] = Brushes.DarkRed;
                else Color[j] = Brushes.White;
          
            }
        }

        

        private List<Students> NewStudents()
        {
            return new List<Students>
            {
                new Students("Иванов Иван Иванович", new int[3] {1,1,2}),
                new Students("Петров Петр Петрович", new int[3] {0,0,1}),
                new Students("Сидоров Сидор Сидорович", new int[3] {1,0,1}),
                new Students("Зайцев Заяц Зайцович", new int[3] {0,2,1}),
                new Students("Сибгутов Сибгут Сибгутович", new int[3] {2,2,1}),
            };
        }
    }
}
