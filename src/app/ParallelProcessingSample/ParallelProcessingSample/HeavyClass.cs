using Mealho.MVVM;
using System;
using System.Diagnostics;
using System.Threading;

namespace ParallelProcessingSample
{
    public class HeavyClass : ModelBase
    {
        public string Log
        {
            set
            {
                LogHistory = value;
                Debug.Write(value);
                RaisePropertyChanged(nameof(Log));
            }
        }

        public string LogHistory
        {
            get
            {
                return logHistory;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    logHistory = "";
                }
                else
                {
                    logHistory += value;
                }
                RaisePropertyChanged(nameof(LogHistory));
            }
        }
        private static string logHistory;

        public DateTime InitTime { get; set; }
        public int entryCnt { get; set; }
        public int exitcnt { get; set; }

        private object lockObject = new object();

        public void HeavyFunc()
        {
            lock (lockObject)
            {
                DateTime entryTime = DateTime.Now;
                Log = $"[Entry{entryCnt.ToString("D2")}]{entryTime.ToString("HH:mm:ss.fff")}[↑{((entryTime - InitTime).TotalMilliseconds / 1000).ToString("F3")}]\r\n";
                entryCnt++;
            }

            Thread.Sleep(1000);

            lock (lockObject)
            {
                DateTime exitTime = DateTime.Now;
                Log = $"[Exit {exitcnt.ToString("D2")}]{exitTime.ToString("HH:mm:ss.fff")}[↑{((exitTime - InitTime).TotalMilliseconds / 1000).ToString("F3")}]\r\n";
                exitcnt++;
            }
        }
    }
}
