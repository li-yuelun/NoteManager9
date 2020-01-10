using Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyDbContext<T> where T:class, IModel,new()
    {
        public MyDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=notemanager-sqlsugar",
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" + Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }
        //用来处理事务多表查询和复杂的操作
        public SqlSugarClient Db;

        public SimpleClient<User> UserDb { get { return new SimpleClient<User>(Db); } }

        public SimpleClient<Role> RoleDb { get { return new SimpleClient<Role>(Db); } }

        public SimpleClient<Power> PowerDb { get { return new SimpleClient<Power>(Db); } }

        public SimpleClient<Note> NoteDb { get { return new SimpleClient<Note>(Db); } }

        public SimpleClient<Singer> SingerDb { get { return new SimpleClient<Singer>(Db); } }

        public SimpleClient<ChatUser> ChatUserDb { get { return new SimpleClient<ChatUser>(Db); } }

        public SimpleClient<UserRoleRelation> UserRoleRelationDb { get { return new SimpleClient<UserRoleRelation>(Db); } }

        public SimpleClient<RolePowerRelation> RolePowerRelationDb { get { return new SimpleClient<RolePowerRelation>(Db); } }

        //用来处理事务多表查询和复杂的操作
        public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } } //用来处理T表的常用操作
    }
}
