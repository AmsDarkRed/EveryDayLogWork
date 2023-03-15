using Everyday_Work.Establish;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Everyday_Work
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            _taskbar = (TaskbarIcon)FindResource("Taskbar");
            base.OnStartup(e);
            LoadWindow();
        }
        private TaskbarIcon _taskbar;


        private EstablishFiles establishFiles = EstablishFiles.CanheObject;
        private void LoadWindow()
        {
            string file_name = ConfigurationManager.AppSettings["FileName"].ToString();
            if (string.IsNullOrWhiteSpace(file_name)) file_name = "今日日志";
            string name = DateTime.Now.ToString("MM-dd");
            var list_str = establishFiles.StartEstablish(DateTime.Now, file_name);
            //创建快捷方式
            ShortcutCreator.CreateShortcutOnDesktop($"{name} 今日日志", list_str.path);
        }
    }
}
