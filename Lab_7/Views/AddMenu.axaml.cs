using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Lab_7.ViewModels;
using Lab_7.Models;

namespace Lab_7.Views
{
    public partial class AddMenu : Window
    {
        public AddMenu()
        {
            InitializeComponent();

            this.FindControl<Button>("Ok").Click += async delegate
            {
                Students st;
                bool error = false;
                st = new Students("", new int[3] { 0, 0, 0 });
                var context = this.DataContext as AddMenuViewModel;
                st = context.NewStudent;
                if (st == null || st.Fio == "") error = true;
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (st.Rating[i] > 2 || st.Rating[i] < 0) error = true;
                    }
                }
                
                if (!error) Close(st);
            };
            this.FindControl<Button>("Cancel").Click += async delegate
            {
                Close(null);
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.DataContext = new AddMenuViewModel();
        }
    }
}
