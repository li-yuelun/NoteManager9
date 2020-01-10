﻿using SqlSugar;
using System;

namespace Model
{
    [SugarTable("t_powers")]
    public class Power : IModel
    {
        /// <summary>
        /// 权限模型
        /// </summary>
        //指定主键和自增列，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }
        /// <summary>
        /// 权限名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; } = false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 标志
        /// </summary>
        public string Mark { get; set; }
    }
}
