using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace pration4
{
    class My
    {
        public string Name { get; set; }
        public string Type { get; set; }
        private int Money;
        public int money
        {
            get { return Money; }
            set
            {
                Money = Math.Abs(value);
            }
        }
        public bool are_income { get; set; }
        public DateTime? data;

        public My(DateTime? selectedDate, string text1, string v, int text2, bool isin)
        {
            data = selectedDate;
            Name = text1;
            Type = v;
            money = text2;
            are_income = isin;
        }
    }
    class Use
    {
        public static List<string> naz = new List<string>();
    }
    public partial class MainWindow : Window
    {
        static int count = 0;
        public static string text = "";
        private static List<My> us = new List<My>();
        private static List<My> uses = new List<My>();

        public MainWindow()
        {
            InitializeComponent();
            us = Jsonconvert.Des<List<My>>("Mys.json");
            if (us == null)
                us = new List<My>();
            count = Jsonconvert.Read();
            summ.Content = count.ToString();
            Use.naz = Jsonconvert.Des<List<string>>("title.json") ?? new List<string>();
            combobox.ItemsSource = Use.naz;
            if (us != null)
            {
                if (us.Count != 0)
                {
                    select((DateTime)date.SelectedDate);
                    dg.ItemsSource = uses;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textbox_2.Text) > 0)
                {
                    us.Add(new My(date.SelectedDate, textbox_1.Text.ToString(), combobox.SelectedItem.ToString(), Convert.ToInt32(textbox_2.Text), true));
                    uses.Add(new My(date.SelectedDate, textbox_1.Text.ToString(), combobox.SelectedItem.ToString(), Convert.ToInt32(textbox_2.Text), true));
                }
                else
                {
                    us.Add(new My(date.SelectedDate, textbox_1.Text.ToString(), combobox.SelectedItem.ToString(), Convert.ToInt32(textbox_2.Text), false));
                    uses.Add(new My(date.SelectedDate, textbox_1.Text.ToString(), combobox.SelectedItem.ToString(), Convert.ToInt32(textbox_2.Text), false));
                }
                count += Convert.ToInt32(textbox_2.Text);
                dg.ItemsSource = null;
                dg.ItemsSource = uses;
                summ.Content = count.ToString();
                Jsonconvert.Ser("Mys.json", us);
                Jsonconvert.Write(count);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка ввода");
            }
        }

        void select(DateTime time)
        {
            uses.Clear();
            foreach (My My in us)
                if (time == My.data) uses.Add(My);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new dialog().Show();
            Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((dg.SelectedItem) != null)
            {
                textbox_1.Text = ((My)dg.SelectedItem).Name.ToString();
                combobox.SelectedItem = ((My)dg.SelectedItem).Type;
                textbox_2.Text = ((My)dg.SelectedItem).money.ToString();
            }
        }

        private void date_CalendarClosed(object sender, RoutedEventArgs e)
        {
            dg.ItemsSource = null;
            textbox_1.Text = null;
            combobox.SelectedIndex = -1;
            textbox_2.Text = null;
            select((DateTime)date.SelectedDate);
            dg.ItemsSource = uses;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            count -= Convert.ToInt32(((My)dg.SelectedItem).money);
            uses.Remove((My)dg.SelectedItem);
            us.Remove((My)dg.SelectedItem);
            Jsonconvert.Ser("Mys.json", us);
            Jsonconvert.Write(count);
            dg.ItemsSource = null;
            dg.ItemsSource = uses;
            summ.Content = count.ToString();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int j = 0;
            foreach (My s in us)
            {
                if (s == dg.SelectedItem)
                {
                    j = Convert.ToInt32(((My)dg.SelectedItem).money);
                    foreach (My w in uses)
                    {
                        if (w == s)
                        {
                            w.Name = textbox_1.Text.ToString();
                            w.Type = combobox.SelectedItem.ToString();
                            w.money = Convert.ToInt32(textbox_2.Text);
                            break;
                        }
                    }
                    s.Name = textbox_1.Text.ToString();
                    s.Type = combobox.SelectedItem.ToString();
                    s.money = Convert.ToInt32(textbox_2.Text);
                    break;
                }
            }
            count -= (j - Convert.ToInt32(textbox_2.Text));
            Jsonconvert.Ser("Mys.json", us);
            Jsonconvert.Write(count);
            dg.ItemsSource = null;
            dg.ItemsSource = uses;
            summ.Content = count.ToString();
        }

    }
    class Jsonconvert
    {
        public static void Ser<T>(string path, T ato)
        {
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(ato));
        }
        public static T Des<T>(string path)
        {
            if (!System.IO.File.Exists(path))
                System.IO.File.WriteAllText(path, "");
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(path));
        }
        public static void Write(int a)
        {
            System.IO.File.WriteAllText("summ.json", $"let a = {a}");
        }
        public static int Read()
        {
            string s = "";
            if (!System.IO.File.Exists("summ.json"))
            {
                System.IO.File.Create("summ.json");
                return 0;
            }
            else
                s = System.IO.File.ReadAllText("summ.json");
            if (s == null || s == "")
                return 0;
            string h = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '=' && s[i + 1] == ' ')
                {
                    for (int j = i + 2; j < s.Length; j++)
                    {
                        h += s[j];
                    }
                    break;
                }
            }
            return Convert.ToInt32(h);
        }
    }
}
