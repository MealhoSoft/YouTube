using System.Windows;

namespace ParallelProcessingSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowLogic logic = new MainWindowLogic();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = logic;
        }
    }
}
