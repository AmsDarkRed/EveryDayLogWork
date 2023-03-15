using Everyday_Work.Establish;
using Hardcodet.Wpf.TaskbarNotification;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Everyday_Work.Command
{
    public class MyDelegateCommand
    {

        

        /// <summary>
        /// 是否显示主页面
        /// </summary>
        private bool IsShowVariate { get; set; } = false;

        /// <summary>
        /// 如果窗口没显示，就显示窗口
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                var delegate_command= new DelegateCommand<object>((obj) =>
                {
                    if (!IsShowVariate)
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                        IsShowVariate = true;
                    }
                });
                return delegate_command;
            }
        }

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    if (IsShowVariate)
                    {
                        Application.Current.MainWindow.Close();
                        IsShowVariate = false;
                    }
                });
            }
        }

        /// <summary>
        /// 关闭软件
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    IsShowVariate = false;
                    Application.Current.Shutdown();
                });
            }
        }
    }
}
