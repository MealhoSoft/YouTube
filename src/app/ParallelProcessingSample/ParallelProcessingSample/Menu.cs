using System;
using System.Windows.Input;

namespace ParallelProcessingSample
{
    public class Menu
    {
        public string Header { get; set; }

        public ICommand Function { get; set; }
    }
}
