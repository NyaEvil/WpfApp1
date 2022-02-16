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
    public class Toad
    {
        public byte hp;
        public byte mana;
        public byte str;
        public string skill;
        public string clas;

        public Toad(string cls)
        {
            if (cls == "Воин")
            {

                this.hp = 150;
                this.mana = 50;
                this.str = 15;
                this.skill = "Клич!";
                this.clas = cls;
            }
            else if (cls == "Маг")
            {
                this.hp = 90;
                this.mana = 120;
                this.str = 10;
                this.skill = "Фаерболл!";
            }
            else if (cls == "Некромант")
            {
                this.hp = 100;
                this.mana = 100;
                this.str = 7;
                this.skill = "Умерщвление!";
            }

        }
    }

    public static class Ctr
    {
        public static int cn;
    }
}
