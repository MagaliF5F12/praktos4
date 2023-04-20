using System.Windows;

namespace pration4
{
    /// <summary>
    /// Логика взаимодействия для dialog.xaml
    /// </summary>
    public partial class dialog : Window
    {
        public dialog()
        {
            InitializeComponent();
            tb.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text == "")
            {
                MessageBox.Show("Поле не заполнено");
                return;
            }
            if (Use.naz.Contains(tb.Text.ToString()))
            {
                MessageBox.Show("Такой тип уже есть");
                return;
            }
            Use.naz.Add(tb.Text);
            Jsonconvert.Ser("title.json", Use.naz);
            Hide();
            new MainWindow().Show();
        }
    }
}
