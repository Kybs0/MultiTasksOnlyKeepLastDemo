using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
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

namespace MultiTasksOnlyKeepLastDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _asyncTaskQueue = new AsyncTaskQueue
            {
                AutoCancelPreviousTask = true,
                UseSingleThread = true
            };
        }
        private AsyncTaskQueue _asyncTaskQueue;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // 快速启动10个任务
            for (var i = 1; i < 10; i++)
            {
                Test(_asyncTaskQueue, i);
            }
        }
        public static async void Test(AsyncTaskQueue taskQueue, int num)
        {
            var result = await taskQueue.ExecuteAsync(async () =>
            {
                Debug.WriteLine("输入:" + num);
            // 长时间耗时任务
            await Task.Delay(TimeSpan.FromSeconds(5));
                return num * 100;
            });
            Debug.WriteLine($"{num}输出的:" + result);
        }
    }
}
