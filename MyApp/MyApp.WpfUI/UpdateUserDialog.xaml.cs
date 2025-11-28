using System.Windows;

namespace MyApp.WpfUI
{
    public partial class UpdateUserDialog : Window
    {
        public string UpdatedName { get; private set; } = "";
        public int UpdatedAge { get; private set; }

        public UpdateUserDialog(string currentName, int currentAge)
        {
            InitializeComponent();
            NameBox.Text = currentName;
            AgeBox.Text = currentAge.ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            UpdatedName = NameBox.Text;
            UpdatedAge = int.TryParse(AgeBox.Text, out var age) ? age : 0;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
