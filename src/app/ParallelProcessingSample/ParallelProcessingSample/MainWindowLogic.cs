using Mealho.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProcessingSample
{
    public class MainWindowLogic : ModelBase
    {
        public HeavyClass HC { get; set; } = new HeavyClass();

        public List<Menu> MenuList { get; set; }

        private void Init([CallerMemberName]string callerMethodName = "")
        {
            HC.entryCnt = 0;
            HC.exitcnt = 0;

            HC.Log = "";
            HC.Log = $"<<<{callerMethodName}>>>\r\n";
            ThreadPool.GetMaxThreads(out int workerThreads, out int competionPortThreads);
            HC.Log = $"MaxThreadCount:" + workerThreads + "/" + competionPortThreads + "\r\n";
            HC.InitTime = DateTime.Now;
            HC.Log = $"[------]{HC.InitTime.ToString("HH:mm:ss.fff")}\r\n";
        }

        public MainWindowLogic()
        {
            MenuList = new List<Menu>()
            {
                new Menu()
                {
                    Header = "スレッドなしで直列で10回呼び出し(for文)",
                    Function = new RelayCommand(FuncNoThreadSeries),
                },
                new Menu()
                {
                    Header = "ParallelForで10回実行",
                    Function = new RelayCommand(FuncParallelFor),
                },
                new Menu()
                {
                    Header = "ParallelLinqで10回実行",
                    Function = new RelayCommand(FuncParallelLinq),
                },
                new Menu()
                {
                    Header = "for文でThreadを10回new",
                    Function = new RelayCommand(FuncThreadFor),
                },
                new Menu()
                {
                    Header = "for文でTaskを10回new",
                    Function = new RelayCommand(FuncTaskFor),
                },
                new Menu()
                {
                    Header = "async/awaitで10回実行",
                    Function = new RelayCommand(FuncAsyncAwait),
                },
            };
        }

        private void FuncNoThreadSeries()
        {
            Init();

            for (int i = 0; i < 10; i++)
            {
                HC.HeavyFunc();
            }
        }
        private void FuncParallelFor()
        {
            Init();

            Parallel.For(0, 10, i =>
            {
                HC.HeavyFunc();
            });
        }
        private void FuncParallelLinq()
        {
            Init();

            Enumerable.Range(0, 10).AsParallel().ForAll(x => 
            {
                HC.HeavyFunc();
            });
        }

        private void FuncThreadFor()
        {
            Init();

            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(new ThreadStart(HC.HeavyFunc));
                thread.Start();
            }
        }

        private void FuncTaskFor()
        {
            Init();

            for (int i = 0; i < 10; i++)
            {
                Task thread = Task.Factory.StartNew(() => HC.HeavyFunc());
            }
        }

        private async void FuncAsyncAwait()
        {
            Init();

            var task0 = Task.Run(() => HC.HeavyFunc());
            var task1 = Task.Run(() => HC.HeavyFunc());
            var task2 = Task.Run(() => HC.HeavyFunc());
            var task3 = Task.Run(() => HC.HeavyFunc());
            var task4 = Task.Run(() => HC.HeavyFunc());
            var task5 = Task.Run(() => HC.HeavyFunc());
            var task6 = Task.Run(() => HC.HeavyFunc());
            var task7 = Task.Run(() => HC.HeavyFunc());
            var task8 = Task.Run(() => HC.HeavyFunc());
            var task9 = Task.Run(() => HC.HeavyFunc());
            await Task.WhenAll(task0, task1, task2, task3, task4, task5, task6, task7, task8, task9);
        }
    }
}
