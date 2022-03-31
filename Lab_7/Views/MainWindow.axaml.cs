using Avalonia.Controls;
using Avalonia.Interactivity;
using Lab_7.Models;
using Lab_7.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_7.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            this.FindControl<MenuItem>("Save").Click += ClickEventSave;
            this.FindControl<MenuItem>("Load").Click += ClickEventLoad;
            this.FindControl<MenuItem>("Exit").Click += ClickEventExit;
            this.FindControl<MenuItem>("About").Click += ClickEventAbout;
            this.FindControl<Button>("Add").Click += ClickEventAdd;
            this.FindControl<Button>("Delete").Click += ClickEventDelete;
        }

        private async void ClickEventSave(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new SaveFileDialog()
            {
                Title = "Save file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Текстовый файл (.txt)", Extensions = { "txt" } });

            path = await taskPath.ShowAsync((Window)this.VisualRoot);
            if (path is not null)
            {
                var items = (this.DataContext as MainWindowViewModel).Items;
                using (StreamWriter wr = File.CreateText(path))
                {
                    wr.WriteLine(items.Count.ToString());
                    foreach (var item in items)
                    {
                        wr.WriteLine(item.Fio);
                        for (int i = 0; i < 3; i++)
                            wr.WriteLine(item[i]);
                    }
                }
            }
        }
        private async void ClickEventLoad(object? sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open file",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "TXT files", Extensions = { "txt" } });

            string[]? pathArray = await taskPath.ShowAsync((Window)this.VisualRoot);
            path = pathArray is null ? null : string.Join(@"\", pathArray);

            if (path != null)
            {
                var items = (this.DataContext as MainWindowViewModel).Items;
                items.Clear();
                using (StreamReader sr = File.OpenText(path))
                {
                    //bool error = false;
                    //int rt = 0;
                    int count;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return; // В первой строке должно быть количество студентов

                    for (int i = 0; i < count; i++)
                    {
                        items.Add(new Students(sr.ReadLine(), new int[3] { Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine()), Int32.Parse(sr.ReadLine()) }));
                    }

                    foreach (var item in items)
                    {
                        item.PropertyChanged += (this.DataContext as MainWindowViewModel).ContentCollectionChanged;
                    }
                }

                (this.DataContext as MainWindowViewModel).update();
            }
        }
        private async void ClickEventAdd(object? sender, RoutedEventArgs e)
        {
            var context = (this.DataContext as MainWindowViewModel);
            Students st = await new AddMenu().ShowDialog<Students>((Window)this.VisualRoot);
            if (st != null)
            {
                st.PropertyChanged += context.ContentCollectionChanged;
                context.Items.Add(st);
                context.update();
            }
        }
        private async void ClickEventDelete(object? sender, RoutedEventArgs e)
        {
            var context = (this.DataContext as MainWindowViewModel);
            var items = context.Items;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Check) { items.Remove(items[i]); i--; };
            }
            context.update();
        }
        private async void ClickEventExit(object? sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ClickEventAbout(object? sender, RoutedEventArgs e)
        {

            await new About().ShowDialog((Window)this.VisualRoot);
        }
    }
}
