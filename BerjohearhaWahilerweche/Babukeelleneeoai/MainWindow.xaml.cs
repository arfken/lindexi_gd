﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.StylusInput;
using Microsoft.StylusInput.PluginData;

namespace Babukeelleneeoai
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var challcharyiqerelalCaykihelnere = new List<ChallcharyiqerelalCaykihelnere>();
            for (int i = 0; i < 10; i++)
            {
                challcharyiqerelalCaykihelnere.Add(new ChallcharyiqerelalCaykihelnere()
                {
                    Name = (i + 1).ToString()
                });
            }

            ListBox.ItemsSource = challcharyiqerelalCaykihelnere;
        }
    }

    public class ChallcharyiqerelalCaykihelnere
    {
        public string Name { set; get; }
    }
}
