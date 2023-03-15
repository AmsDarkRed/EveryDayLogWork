using Everyday_Work.View;
using Everyday_Work.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Everyday_Work.Establish
{
    /// <summary>
    /// 窗体信息提示
    /// </summary>
    internal class WindowTip:CommandBase<WindowTip>
    {

        int i = 0;
        public static List<NotifyDataTip> _dialogs = new List<NotifyDataTip>();
        /// <summary>
        /// 右边窗体提示
        /// </summary>
        /// <param name="tipStr">提示信息</param>
        public void RightShowTip(string tipStr)
        {
            i++;
            NotifyData data = new NotifyData();
            data.Title = "提示";
            data.Content = tipStr;
            NotifyDataTip dialog = new NotifyDataTip();//new 一个通知  
            dialog.Closed += Dialog_Closed;
            dialog.TopFrom = GetTopFrom();
            dialog.DataContext = data;//设置通知里要显示的数据            
            dialog.Show();
            _dialogs.Add(dialog);
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var closedDialog = sender as NotifyDataTip;
            _dialogs.Remove(closedDialog);
        }

        private double GetTopFrom()
        {
            //屏幕的高度-底部TaskBar的高度。
            double topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            bool isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            while (isContinueFind)
            {
                topFrom = topFrom - 160;//此处100是NotifyWindow的高 160-100剩下的10  是通知之间的间距
                isContinueFind = _dialogs.Any(o => o.TopFrom == topFrom);
            }
            if (topFrom <= 0)
                topFrom = System.Windows.SystemParameters.WorkArea.Bottom - 10;
            return topFrom;
        }
    }
}
