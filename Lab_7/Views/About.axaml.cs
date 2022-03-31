using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Lab_7.Views
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
