using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday_Work
{
    /// <summary>
    /// 单例父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommandBase<T> where T : class
    {
        /// <summary>
        /// 子类静态实现
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(() => Activator.CreateInstance(typeof(T), true) as T);

        /// <summary>
        /// 返回单例子类
        /// </summary>
        public static T CanheObject { get { return instance.Value; } }

        /// <summary>
        /// 测试
        /// </summary>
        private DelegateCommand _fieldName;
        public DelegateCommand CommandName =>
            _fieldName ?? (_fieldName = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {

        }

    }
}
