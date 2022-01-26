using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string t;
        int i = 0;
        int ct = 0;
        int c_c = 0;
        List<string> chats = new List<string>();
        string conSt = "server=nyaeviu7.beget.tech;user id=nyaeviu7_is31;persistsecurityinfo=True;database=nyaeviu7_is31;password=Testis31;allowuservariables=True";
        MySqlConnection con;

        object obj = null;
        public MainWindow()
        {
            con = new MySqlConnection(conSt);
            con.Open();
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.SelectedItem = create;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tb1.SelectedItem = join;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            con.Close();
            this.Close();
        }


        private void BC3(object sender, RoutedEventArgs e)
        {
            tb1.SelectedItem = main;
        }

        private void crt(object sender, RoutedEventArgs e)
        {
            string nm = name.Text.ToString();
            string pasw = pass.Text;
            int pls = plrs.SelectedIndex;
            string crt;
            MySqlCommand com;
            try
            {
                crt = $"INSERT INTO crt (g_name, g_pass) VALUES ('{nm}', '{pasw}')";
                com = new MySqlCommand(crt, con);
                com.ExecuteNonQuery();
                crt = $"CREATE TABLE {nm} (id INTEGER AUTO_INCREMENT, name VARCHAR(255) NOT NULL UNIQUE, hp INTEGER, mana INTEGER, str INTEGER, skill TEXT, PRIMARY KEY(id))";
                com = new MySqlCommand(crt, con);
                com.ExecuteNonQuery();
                crt = $"CREATE TABLE {nm}_chat (id INTEGER AUTO_INCREMENT, mess VARCHAR(255), PRIMARY KEY(id))";
                com = new MySqlCommand(crt, con);
                com.ExecuteNonQuery();
                tb1.SelectedItem = w_game;
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                Window1 er = new Window1();
                er.Show();
            }
            t = nm;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = System.IO.Path.GetFullPath(location);
            var path2 = System.IO.Path.Combine(path, "..\\..\\..\\.");
            string parentDirectory = System.IO.Path.GetDirectoryName(path2);
             string[] alf = Directory.GetFiles($"{parentDirectory}/Toads");
            try
            {
                i++;
                toad_im.Source = new BitmapImage(new Uri($"{alf[i]}", UriKind.RelativeOrAbsolute));

            }
            catch (System.IndexOutOfRangeException)
            {
                i = 0;
                toad_im.Source = new BitmapImage(new Uri($"{alf[i]}", UriKind.RelativeOrAbsolute));
                i++;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var path = System.IO.Path.GetDirectoryName(location);
            string[] alf = Directory.GetFiles($"{path}/Toads");
            try
            {
                i--;
                toad_im.Source = new BitmapImage(new Uri($"{alf[i]}", UriKind.RelativeOrAbsolute));
            }
            catch (System.IndexOutOfRangeException)
            {
                i = alf.Length;
                i--;
                toad_im.Source = new BitmapImage(new Uri($"{alf[i]}", UriKind.RelativeOrAbsolute));
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            string nm = t_name1.Text;
            string cls = t_class1.SelectionBoxItem.ToString();
            Toad you = new Toad(cls);

            string cm = $"INSERT INTO {t} (name, hp, mana, str, skill) VALUES ('{nm}', {you.hp}, {you.mana}, {you.str}, '{you.skill}')";
            MySqlCommand com = new MySqlCommand(cm, con);
            com.ExecuteNonQuery();
            PREV.Visibility = Visibility.Collapsed;
            NEXT.Visibility = Visibility.Collapsed;
            t_name.Visibility = Visibility.Collapsed;
            t_name1.Visibility = Visibility.Collapsed;
            t_class.Visibility = Visibility.Collapsed;
            t_class1.Visibility = Visibility.Collapsed;
            start.Visibility = Visibility.Collapsed;
            chat.Visibility = Visibility.Visible;
            plars.Visibility = Visibility.Visible;
            name_sh.Content = nm;
            class_sh.Content = cls;
            name_sh.Visibility = Visibility.Visible;
            class_sh.Visibility = Visibility.Visible;
            Thread th = new Thread(() => chk());
            th.Start();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string nm = join_name.Text;
            string ps = join_pass.Text;
            t = nm;

            tb1.SelectedItem = w_game;
            string crt = $"SELECT g_name FROM crt WHERE g_name='{nm}' AND g_pass='{ps}'";
            MySqlCommand com = new MySqlCommand(crt, con);
            com.ExecuteScalar().ToString();
        }

        private async void chk()
        {
            Timer tmr = new Timer(check, null, 200, 200);
            Timer tmr1 = new Timer(g_chat, null, 200, 200);
        }


        public void check(object obj)
        { 
            MySqlConnection sql = new MySqlConnection(conSt);
            sql.Open();
            byte ii = 0;
            string crt = $"SELECT COUNT(*) FROM {t}";
            MySqlCommand com = new MySqlCommand(crt, sql);
            var c = Convert.ToInt32(com.ExecuteScalar());
            c_c = c;
            crt = $"SELECT name, hp, mana, str FROM {t}";
            com = new MySqlCommand(crt, sql);
            var Reader = com.ExecuteReader();
            if (ct != c)
            {
                List<string> names = new List<string>();
                List<string> hps = new List<string>();
                List<string> manas = new List<string>();
                List<string> strs = new List<string>();
                for (byte i = 0; i < c; i++)
                {
                    if (c != ii)
                    {
                        ii++;
                        Reader.Read();
                        names.Add(Reader.GetString("name"));
                        hps.Add(Reader.GetString("hp"));
                        manas.Add(Reader.GetString("mana"));
                        strs.Add(Reader.GetString("str"));
                    }
                    else
                    {
                        continue;
                    }
                }
                ct = c;
                for (byte id =0; id<names.Count; id++)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        plars.Items.Add(names[id]+" hp: "+hps[id]+" mana: "+manas[id]+" strength: "+strs[id]);
                    }));
                }
                sql.Close();
                check(obj);
            }
            else
            {
                sql.Close();
                check(obj);
            }
        }

        private void t_chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = t_chat.Text;
                t_chat.Text = "";
                this.Dispatcher.Invoke((Action)(() =>
                {
                    chat.Items.Add(text);
                }));
                MySqlConnection cons = new MySqlConnection(conSt);
                cons.Open();
                string crt = $"INSERT INTO {t}_chat (mess) VALUES ({text})";
                MySqlCommand com = new MySqlCommand(crt, cons);
                com.ExecuteNonQuery();
                crt = $"SELECT id FROM {t}_chat WHERE id=(SELECT * FROM `table_name` WHERE id=(SELECT MAX(id) FROM 'table_name'))";
                com = new MySqlCommand(crt, cons);
                com.ExecuteNonQuery();
                string txt = Convert.ToString(com.ExecuteScalar());
                this.Dispatcher.Invoke((Action)(() =>
                {
                    label.Content = txt;
                    label_Copy.Content = c_c;
                }));
            }
        }

        public void g_chat(object obj)
        {
            MySqlConnection sql3 = new MySqlConnection(conSt);
            sql3.Open();
            MySqlConnection sql2 = new MySqlConnection(conSt);
            sql2.Open();
            string crt = $"SELECT mess FROM {t}";
            MySqlCommand com = new MySqlCommand(crt, sql3);
            var read = com.ExecuteReader();
            crt = $"SELECT COUNT(*) FROM {t}";
            com = new MySqlCommand(crt, sql2);
            var c = Convert.ToInt32(com.ExecuteScalar());
            List<string> mes = new List<string>();
            for (byte i =0; i<c; i++)
            {
                read.Read();
                mes.Add(read.GetString("mess"));
            }
            if (mes!=chats)
            {
                chats.Clear();
                string[] mess= new string[20];
                mes.CopyTo(mess);
                foreach (string i in mess)
                {
                    chats.Add(i);
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        chat.Items.Add(i);
                        if (chat.Items.Count > 14)
                        {
                            chat.Items.RemoveAt(0);
                        }
                    }));
                }
            }
            sql2.Close();
            sql3.Close();
            g_chat(obj);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
                con.Close();
            }
        }
    }
}
