using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaChat.Desktop.Views
{
    public partial class RegistrationPage : UserControl
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(obj: this);
        }
    }
}
