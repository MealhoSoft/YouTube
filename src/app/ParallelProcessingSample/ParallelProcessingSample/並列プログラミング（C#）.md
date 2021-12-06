﻿# 並列プログラミング（C# / .NET Framework）
- 排他制御やデッドロックなどには要注意
- 参考サイト
  - 基礎：https://ufcpp.net/study/csharp/AsyncVariation.html
  - なぜ非同期？https://atmarkit.itmedia.co.jp/fdotnet/chushin/masterasync_01/masterasync_01_01.html




## 並列処理（parallel/マルチプロセス） - 複数レジ -

- .NET Framework 4.0～

  - Parallelクラス（System.Threading.Tasks）

    以下、3つの静的メソッドがある

    - Invoke
      ```c#
      Parallel.Invoke(A, B, C);
      ```
    - For
      ```c#
      Parallel.For(0, N, i =>
      {
          Console.WriteLine(i * i);
      });
      ```
    - ForEach
      ```c#
      var data = Enumerable.Range(0, N);
       
      Parallel.ForEach(data, x =>
      {
          Console.WriteLine(x * x);
      });
      ```
    
  - ParallelEnumerable クラス（System.Linq）




## 並行処理/いわゆる非同期処理（concurrent/マルチスレッド） - 単一レジ -

- .NET Framework1.1～
  - Threadクラス（System.Threading）
    - スレッドの生成／破棄には思いのほか、コストがかかる。例えば、スレッドを1つ生成すると、約1Mbytesのメモリを消費するし（＝空間的なコスト）、スレッドの生成／破棄のたびにプロセス内にロードされている全ての.dllファイルのDllMain関数が呼び出される（＝時間的なコスト）。
- .NET Framework2.0～
  - ThreadPoolクラス（System.Threading）→.NET Framework4.0で改善
    - Threadクラスのオーバーヘッドを極小にするために考えられたのが「スレッドプール」。
    - MAXは、2.0でコア数の50倍、2.0でコア数の250倍、4.0以降はOSリソースに応じて動的。
  - BeginInvoke
  - Timerクラス（System.Threading）
- .NET Framework 4.0～
  - Task（System.Threading.Tasks）
    - 非同期で実施した処理の状態(実行中、完了、キャンセル、エラー)を知ることができる
    - 例外を補足することができる
    - 非同期処理の実行順序を制御できる
- C#5.0～（Visual Studio 2012以降）
  - async / await（言語仕様）
- .NET Framework 3.5～ ＋ 追加準標準ライブラリ（NugetでSystem.Reactive）
  - Reactive Extensions（Rx）
    - LINQが適用できるデータソースの概念を「非同期」と「イベント」に広げた、いわば「LINQ to Asynchronous」「LINQ to Events」
    - 従来では手間のかかった複雑な非同期処理やイベント処理、時間が関係する処理などを、LINQの形で、簡単に、宣言的に記述できる
    - https://atmarkit.itmedia.co.jp/fdotnet/introrx/index/index.html




## 例：1秒かかるHeavyFuncを10回処理する
### スレッドなしの直列

```c#
for (int i = 0; i < 10; i++)
{
    HC.HeavyFunc();
}
```

- 実行完了時間（10回）：10.021～10.035秒

### Parallelクラス(For)の利用

```c#
Parallel.For(0, 10, i =>
{
    HC.HeavyFunc();
});
```

- 実行完了時間（10回）：1.040～2.004秒
- すべての処理が完了し、For文を抜けるまでメインスレッドが待たされる

### Parallel LINQの利用

```c#
Enumerable.Range(0, 10).AsParallel().ForAll(x => 
{
    HC.HeavyFunc();
});
```

- 実行完了時間（10回）：1.040～2.004秒
- すべての処理が完了し、For文を抜けるまでメインスレッドが待たされる

### Threadクラスの利用

```c#
for (int i = 0; i < 10; i++)
{
    Thread thread = new Thread(new ThreadStart(HC.HeavyFunc));
    thread.Start();
}
```

- 実行完了時間（10回）：1.02～1.184秒

### Taskクラスの利用

```c#
for (int i = 0; i < 10; i++)
{
    Task thread = Task.Factory.StartNew(() => HC.HeavyFunc());
}
```

- 実行完了時間（10回）：1.056～2.04秒

