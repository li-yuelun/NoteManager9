using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 单例模式获取单个redis连接
    /// </summary>
    public static class RedisManager
    {
        //redis的连接
        private static ConnectionMultiplexer Redis { get; set; }

        //锁的引入
        private static readonly object locker = new object();

        public static ConnectionMultiplexer GetConnection()
        {
            if (Redis == null)
            {
                lock (locker)
                {
                    if (Redis == null)
                    {
                        Redis= ConnectionMultiplexer.Connect("localhost:6379");
                    }
                }
            }
            return Redis;
        }
    }
}
