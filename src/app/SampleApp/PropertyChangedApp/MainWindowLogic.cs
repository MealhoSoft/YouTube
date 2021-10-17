using MaterialDesignThemes.Wpf;
using Mealho.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealho
{
    class MainWindowLogic : ModelBase
    {
        private PaletteHelper paletteHelper;

        public string StatusMessage {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private string statusMessage;

        public bool IsDarkMode
        {
            get
            {
                return paletteHelper.GetTheme().GetBaseTheme() == BaseTheme.Dark;
            }
            set
            {
                var theme = paletteHelper.GetTheme();
                theme.SetBaseTheme((paletteHelper.GetTheme().GetBaseTheme() == BaseTheme.Dark) ? Theme.Light : Theme.Dark);
                paletteHelper.SetTheme(theme);
            }
        }

        public MainWindowLogic()
        {
            paletteHelper = new PaletteHelper();
        }
    }
}
