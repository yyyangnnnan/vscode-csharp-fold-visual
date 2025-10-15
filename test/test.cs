using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoldToDefinitionDemo
{
    /// <summary>
    /// 测试类：包含各种折叠场景
    /// </summary>
    public class AdvancedDemo
    {
        #region Fields
        //1. 字段折叠测试
        private int _counter;
        private List<string> _items;
        #endregion

        #region Properties
        /// <summary>
        /// 公共属性
        /// </summary>
        public int Counter
        {
            get => _counter;
            set => _counter = value;
        }

        public IReadOnlyList<string> Items => _items.AsReadOnly();
        #endregion

        #region Constructors
        public AdvancedDemo()
        {
            _counter = 0;
            _items = new List<string>();
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="initialValue"></param>
        public AdvancedDemo(int initialValue)
        {
            _counter = initialValue;
            _items = new List<string>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 增加计数器
        /// </summary>
        /// <param name="value"></param>
        public void Increment(int value = 1)
        {
            _counter += value;

            // 内部循环折叠测试
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"循环 {i}");
                if (i % 2 == 0)
                {
                    Console.WriteLine("偶数");
                }
                else
                {
                    Console.WriteLine("奇数");
                }

                void LocalFunction()
                {
                    Console.WriteLine("局部函数折叠");
                }

                LocalFunction();
            }

            // try/catch/finally 测试折叠
            try
            {
                var result = 10 / (_counter - _counter); // 会异常
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("捕获异常：" + ex.Message);
            }
            finally
            {
                Console.WriteLine("finally 块");
            }
        }

        /// <summary>
        /// 异步任务
        /// </summary>
        public async Task AsyncTaskDemo()
        {
            await Task.Delay(500);
            Console.WriteLine("异步任务完成");

            Action lambda = () =>
            {
                Console.WriteLine("Lambda 折叠测试");
            };
            lambda();
        }

        /// <summary>
        /// 静态方法
        /// </summary>
        public static void StaticDemo()
        {
            Console.WriteLine("静态方法调用");
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        private void PrivateDemo()
        {
            Console.WriteLine("私有方法调用");
        }

        #endregion

        #region Run Demo
        /// <summary>
        /// 调用演示
        /// </summary>
        public void Run()
        {
            Increment();
            AsyncTaskDemo().Wait();
            StaticDemo();
            PrivateDemo();

            // Linq 测试折叠
            var evens = _items.Where(x => x.Length % 2 == 0).ToList();
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            AdvancedDemo demo = new AdvancedDemo(3);
            demo.Run();
            Console.WriteLine("Demo 执行完毕");
        }
    }
}
