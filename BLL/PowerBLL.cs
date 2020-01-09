﻿
using DAL;
using DTO;
using EmitMapper;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PowerBLL : IPowerBLL
    {

        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public static ObjectsMapper<Power, PowerDTO> PowerMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Power, PowerDTO>();

        public static ObjectsMapper<PowerDTO, Power> PowerDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<PowerDTO, Power>();

        public static ObjectsMapper<ResponseClass, ResponseClassDTO> ResponseClassMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<ResponseClass, ResponseClassDTO>();

        public async Task AddAsync(PowerDTO t)
        {
            var power = PowerDTOMapperToModel.Map(t);
            await powerDAL.AddAsync(power);
        }

        public async Task DeleteAsync(PowerDTO t)
        {
            var power = PowerDTOMapperToModel.Map(t);
            await powerDAL.DeleteAsync(power);
        }

        public async Task DeleteAsync(Expression<Func<Power, bool>> exp)
        {
            await powerDAL.DeleteAsync(exp);
        }

        public async Task<PowerDTO> GetEntityAsync(Expression<Func<Power, bool>> exp)
        {
            var power=await powerDAL.GetEntityAsync(exp);
            return PowerMapperToDTO.Map(power);
        }

        public async Task<List<PowerDTO>> GetFiltersAsync(Expression<Func<Power, bool>> exp)
        {
            var list=await powerDAL.GetFiltersAsync(exp);
            return PowerMapperToDTO.MapEnum(list).ToList();
        }

        public async Task UpdateAsync(PowerDTO t)
        {
            var power = PowerDTOMapperToModel.Map(t);
            await powerDAL.UpdateAsync(power);
        }


        /// <summary>
        /// 删除对应的power和与之关联的关联表
        /// </summary>
        /// <param Id="Id"></param>
        /// <returns></returns>
        public async Task<ResponseClassDTO> DeletedAsync(long Id)
        {
            var responseClass=await powerDAL.DeletedAsync(Id);
            return ResponseClassMapperToDTO.Map(responseClass);
        }
    }
}
