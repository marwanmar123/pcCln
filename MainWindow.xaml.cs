using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.IO;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Net;

namespace pcC
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string date;
        private string time;


        public string Date
        {
            get => date;
            set => date = value;
        }

        public string Time
        {
            get => time;
            set => time = value;
        }
        public string info;

        public MainWindow()
        {
            InitializeComponent();
            this.date = DateTime.Now.ToString("dd-MM-yyyy");
            this.time = DateTime.Now.ToString("HH-mm");

            Loadprogressbar();
        }
        private void Loadprogressbar()
        {
            Duration dur = new Duration(TimeSpan.FromSeconds(8));
            DoubleAnimation dbAni = new DoubleAnimation(100, dur);
            Progress_Bar.BeginAnimation(ProgressBar.ValueProperty, dbAni);

        }

        public void Btn_Analiser_Click(object sender, RoutedEventArgs e)
        {
            var tmpPath = System.IO.Path.GetTempPath();

            var files = Directory.GetFiles(tmpPath, "*.*", SearchOption.AllDirectories);
            var Listbox = new List<string>();
            Listbox.AddRange(files);
            for (int i = 0; i < files.Length; i++)
            {
                RichBox.Visibility = Visibility.Visible;
                Exit_Rich.Visibility = Visibility.Visible;
                RichBox.Document.Blocks.Add(new Paragraph(new Run(files[i])));
            }

        }

        private void Exit_Rich_Click(object sender, RoutedEventArgs e)
        {
            RichBox.Visibility = Visibility.Hidden;
            Exit_Rich.Visibility = Visibility.Hidden;
        }

        private void Btn_Site_web_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.google.com/");
        }

        private void Btn_Nettoyer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to Clean files?", "Confirmation", MessageBoxButton.YesNo) ==
                MessageBoxResult.Yes)
            {
                Delete.ClearTempData();
                CreateHistr();

            }

        }
        private void CreateHistr()
        {
            Task.Run(() =>
            {
                var d = Directory.CreateDirectory(@".\histr\" + this.date);
                //var Nme = "you are delete an item" + Environment.NewLine;
                var file = File.Create($"{d.FullName}/{this.time}.txt");
                var directory = System.IO.Path.GetFullPath(file.Name);
                try
                {

                    Console.WriteLine(directory);

                    try
                    {

                    }
                    catch (Exception exp)
                    {
                        Console.Write(exp.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }

        private void V_Ensemble_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {
            Directory.CreateDirectory(@".\histr");
            Process.Start(@".\histr\");
        }

        private void Btn_MetAjr_Click(object sender, RoutedEventArgs e)
        {

            Dispatcher.Invoke(() =>
            {
                using (var webClient = new WebClient())
                {
                    Thread.Sleep(100);

                    try
                    {
                        if (!webClient.DownloadString("https://pastebin.com/dl/FXi6TAuS").Contains("1.0.0"))
                        {
                            if (MessageBox.Show("do you want to do an update?", "pcC",
                                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                Process.Start("./Updater.exe");
                                Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }
    }

}
