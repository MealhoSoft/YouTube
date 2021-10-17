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

namespace Mealho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowLogic logic = new MainWindowLogic();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = logic;

            logic.StatusMessage = "Hello World.";

            logic.PropertyChanged += Logic_PropertyChanged;
        }

        private void Logic_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(logic.IsDarkMode))
            {
                var app = Application.Current;
                ResourceDictionary darkResourceDictionary = new ResourceDictionary() { Source = new Uri(@"resources\DarkResourceDictionary.xaml", UriKind.Relative) };

                if (logic.IsDarkMode)
                {
                    Resources.MergedDictionaries.Add(darkResourceDictionary);
                }
                else
                {
                    for (int i = 0; i < Resources.MergedDictionaries.Count; i++)
                    {
                        if (Resources.MergedDictionaries[i].Source == darkResourceDictionary.Source)
                        {
                            Resources.MergedDictionaries.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
    }
}
