using System;

//单例基类
//从其他脚本可以直接调用

namespace IceFramework
{
    public class SingletonBace<T> where T : SingletonBace<T>, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    return instance;
                }

                return instance;
            }
        }

        protected class IceFrameworkException : ApplicationException
        {
            public IceFrameworkException(string message) : base(message)
            {
            }
        }
    }
}
