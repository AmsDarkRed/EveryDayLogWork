using Everyday_Work.Establish;
using Everyday_Work.Properties;
using System;
using System.Configuration;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace Everyday_Work
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

    /*
    *  1.在每次开机后获取当前根目录是否创建当前日期文件夹,并创建当日日志.txt 文件 
    *  2.当日日志.txt 文件 中包含三个标题:  当日记录,当日花销,当日学习记录  
    *  3.如果没有创建则 创建当前日期,当前月,当前年的文件夹  三极目录  E:\个人日志\2022\2022-02月\2022-02-07(星期一)
    *  4.如果中间间隔一段时间没有打开电脑,那么就自动创建中间几个文件夹与文件.
    *  5.在当日日志.txt创建完成后,创建桌面快捷方式并删除昨日的快捷方式
    *  6.后期在考虑在软件中能进行阅读日志,并且能根据时间来选择批量阅读
    *  7.后期根据研究来考虑,文本中的 金额与电量等需要计算的数据处理
    */

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        public void ShowTip(string tipStr)
        {
            var _notifyIcon = new NotifyIcon
            {
                BalloonTipText = $@"{tipStr}",
                Text = @"Hello, NotifyIcon!",
                Icon = Properties.Resources.workdays_wor,
                Visible = true
            };
            _notifyIcon.ShowBalloonTip(2000);
        }

        private EstablishFiles establishFiles = EstablishFiles.CanheObject;
        private void AnewBtn_Click(object sender, RoutedEventArgs e)
        {
            //DateTime dateTime = new DateTime(2022, 10, 29);
            var list_str = establishFiles.StartEstablish(DateTime.Now, "今日日志");
            //取最新的日志
            string str_path = list_str.path;
            //创建快捷方式
            ShortcutCreator.CreateShortcutOnDesktop("今日日志", str_path);
            ShowTip("创建成功");
        }
    }
}
