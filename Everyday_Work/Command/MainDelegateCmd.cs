using Everyday_Work.Establish;
using ICSharpCode.AvalonEdit.Document;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Everyday_Work.Command
{
    public class MainDelegateCmd:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private EstablishFiles establishFiles = EstablishFiles.CanheObject;

        private WindowTip windowTip = WindowTip.CanheObject;

        private string windows_tip_str;

        public string WindowsTipStr
        {
            get { return windows_tip_str; }
            set
            {
                windows_tip_str = value;
                Console.WriteLine(windows_tip_str);
                NotifyPropertyChanged("WindowsTipStr");
            }
        }

        private string texte_ditor_contex = "";

        /// <summary>
        /// 文本控件显示的内容
        /// </summary>
        public string TexteDitorContex
        {
            get
            {
                return texte_ditor_contex;
            }
            set
            {
                texte_ditor_contex = value;
                Console.WriteLine(texte_ditor_contex);
                NotifyPropertyChanged("TexteDitorContex");
            }
        }

        private TextDocument textDocument=new TextDocument();
        public TextDocument MainTextDocument
        {
            get
            {
                return textDocument;
            }
            set
            {
                textDocument = value;
                NotifyPropertyChanged("MainTextDocument");
            }
        }


        public ICommand LoadedCmd
        {
            get
            {
                return new DelegateCommand(()=>
                {
                    string path = ConfigurationManager.AppSettings["EstablishPath"].ToString();
                    WindowsTipStr = "当前生成路径为:" + path;
                });
            }
        }

        /// <summary>
        /// 重新生成文件与文件路径
        /// </summary>
        public ICommand ANewCreateCmd
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    Console.WriteLine("ANewCreateCmd命令触发");
                    ANewCreate();
                });
            }
        }


        private void ANewCreate()
        {
            try
            {
                string file_name = ConfigurationManager.AppSettings["FileName"].ToString();
                if (string.IsNullOrWhiteSpace(file_name)) file_name = "今日日志";
                string name = DateTime.Now.ToString("MM-dd");
                var list_str = establishFiles.StartEstablish(DateTime.Now, file_name);
                //创建快捷方式
                ShortcutCreator.CreateShortcutOnDesktop($"{name} 今日日志", list_str.path);
                windowTip.RightShowTip("创建成功");
                TexteDitorContex = list_str.path;

            }
            catch (Exception ex)
            {
                windowTip.RightShowTip("发生异常,请检查日志");
            }
        }

        /// <summary>
        /// 设置生成目录
        /// </summary>
        public ICommand SetPathCmd
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderBrowserDialog.ShowNewFolderButton = true;
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = folderBrowserDialog.SelectedPath;
                        WindowsTipStr = $"设置路径地址为:{path}";
                        establishFiles.SetValue("EstablishPath", path);
                    }
                    Console.WriteLine("ANewCreateCmd命令触发");
                });
            }
        }

        private int InputIndex { get; set; }
        /// <summary>
        /// 同步日志
        /// </summary>
        public ICommand SyncFile
        {
            get
            {
                return new DelegateCommand<object>((obj) =>
                {
                    InputIndex++;
                    MainTextDocument.Text= "打打谁打的吧a" + TexteDitorContex+InputIndex;
                    windowTip.RightShowTip("弹框测试");
                });
            }
        }
    }
}
