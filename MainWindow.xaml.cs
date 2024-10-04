using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BukkMaraton2019GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tavvalaszto.ItemsSource=tavok.Keys;
        }

        Dictionary<string,int>tavok=new Dictionary<string, int>() 
        {
            {"Mini - 16km",16 },
            {"Rövid - 38km",38 },
            {"Pedelec - 54km",54 },
            {"Közép - 57km", 57 },
            {"Hosszú - 94km",94 }
        };

        private int Getseconds(string time)
        {
            if (time == "Nem teljesítő") { return 0; }
            else
            {
                string[] parts = time.Split(':');
                foreach (string part in parts)
                {
                    foreach (char s in part)
                    {
                        if (part.Length > 2)
                        {
                           MessageBox.Show("A következő formátumban adja meg a számot: hh:mm:ss!");
                           return 0;
                        }
                        if (s != 0) break;
                        else part.Remove(s);

                    }
                }
                if (parts.Length < 3)
                {
                    MessageBox.Show("A következő formátumban adja meg a számot: hh:mm:ss!");
                    return 0;
                }
                else
                {
                    bool a, b, c;
                    int hours, minutes, seconds, totalSeconds;

                    a = int.TryParse(parts[0], out hours);
                    b = int.TryParse(parts[1], out minutes);
                    c = int.TryParse(parts[2], out seconds);

                    if (a && b && c)
                    {
                        totalSeconds = hours * 3600 + minutes * 60 + seconds;
                        return totalSeconds;
                    }
                    else { throw new FormatException(); }
                }
            }
        }

        private double averageSpeedMpS(int seconds,int km)
        {
            double averageSpeed =Convert.ToDouble(km) * 1000 / Convert.ToDouble(seconds);
            return Math.Round(averageSpeed,2);
        }
        private double averageSpeedKmpH(int seconds,int km)
        {
            double averageSpeed =Convert.ToDouble(km)/(Convert.ToDouble(seconds)/3600);
            return Math.Round(averageSpeed,2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int seconds = Getseconds(ido.Text);
            int km = tavok[tavvalaszto.Text];
            results.Text=$"A versenyző átlagsebessége m/s-ban: {Convert.ToString(
                averageSpeedMpS(seconds,km)
                )}\nA versenyző átlagsebessége km/h-ban: {Convert.ToString(averageSpeedKmpH(seconds,km))}";
        }
    }
}