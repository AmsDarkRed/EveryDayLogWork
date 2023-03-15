using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday_Work.Establish
{
    /// <summary>
    /// 创建文件夹与文件
    /// </summary>
    internal class EstablishFiles:CommandBase<EstablishFiles>
    {
        private EstablishFiles()
        {
            Console.WriteLine("EstablishFiles初始化");
        }
        /// <summary>
        /// 需要创建文件夹的根目录
        /// </summary>
        public string EstablishPath { get; set; }

        /// <summary>
        /// 开始创建文件夹与文件
        /// </summary>
        /// <param name="dateTime">创建时间</param>
        /// <param name="fileName">文件名称</param>
        /// <returns>创建成功文件绝对路径</returns>
        public (string path,string contex) StartEstablish(DateTime dateTime,string fileName)
        {
            EstablishPath = ConfigurationManager.AppSettings["EstablishPath"].ToString();
            if (string.IsNullOrWhiteSpace(EstablishPath))
            {
                EstablishPath = System.Environment.CurrentDirectory;
            }
            SetValue("EstablishPath", EstablishPath);
            SetValue("FileName", fileName);
            int year = dateTime.Year;
            string month = dateTime.Month.ToString().PadLeft(2, '0');
            string day = dateTime.Day.ToString().PadLeft(2, '0');
            DateTime dateValue = new DateTime(year, int.Parse(month), int.Parse(day));
            string week=string.Empty;
            switch (dateValue.DayOfWeek)
            {
                case DayOfWeek.Monday: week = "星期一"; break;
                case DayOfWeek.Tuesday: week = "星期二"; break;
                case DayOfWeek.Wednesday: week = "星期三"; break;
                case DayOfWeek.Thursday: week = "星期四"; break;
                case DayOfWeek.Friday: week = "星期五"; break;
                case DayOfWeek.Saturday: week = "星期六"; break;
                case DayOfWeek.Sunday: week = "星期天"; break;
            }
            string text_context = string.Empty;
            string create_path = $@"{EstablishPath}\{year}\{year}-{month}月\{year}-{month}-{day}({week})";
            string path_list = create_path + $@"\{fileName}.txt";
            Directory.CreateDirectory(create_path);
            if (!IsFileExists(path_list))
            {
                using (File.Create(path_list)) { }
            }
            else
            {
                text_context= File.ReadAllText(path_list);
            }
            return (path_list, text_context);
        }

        /// <summary>
        ///  判断文件是否存在
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        private  bool IsFileExists(string strPath)
        {
            bool result = false;
            try
            {
                if (string.IsNullOrWhiteSpace(strPath)) return result;
                result = File.Exists(strPath);
                return result;
            }
            catch (Exception ex)
            {
                //ErrorWriteLog("EP_Log", "IsFileExists", $"路径地址为:{strPath},异常信息为:{ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 修改配置文件信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");//重新加载新的配置文件   
        }
    }
}
