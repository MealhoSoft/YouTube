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
    }
}
