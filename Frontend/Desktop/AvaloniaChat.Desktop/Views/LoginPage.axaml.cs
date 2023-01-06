using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaChat.Desktop.Views
{
    public partial class LoginPage : UserControl
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(obj: this);
        }
    }
}
