using System.Windows;

namespace ApplicationUnderTest
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            this.Close();
            mainWindow.Show();
        }
    }
}
