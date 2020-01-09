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
    public class SingerBLL : ISingerBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public IMusicDAL musicDAL { get; set; }

        public ISingerDAL singerDAL { get; set; }

        public static ObjectsMapper<Singer, SingerDTO> SingerMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Singer, SingerDTO>();

        public static ObjectsMapper<SingerDTO, Singer> SingerDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<SingerDTO, Singer>();
        
        /// <summary>
        /// singer单个新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.AddAsync(singer);
        }

        /// <summary>
        /// singer单个删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.DeleteAsync(singer);
        }

        /// <summary>
        /// singer动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Singer, bool>> exp)
        {
            await singerDAL.DeleteAsync(exp);
        }

        /// <summary>
        /// singer单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<SingerDTO> GetEntityAsync(Expression<Func<Singer, bool>> exp)
        {
            var singer=await singerDAL.GetEntityAsync(exp);
            return SingerMapperToDTO.Map(singer);
        }

        /// <summary>
        /// singer动态多个获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<SingerDTO>> GetFiltersAsync(Expression<Func<Singer, bool>> exp)
        {
            var list = await singerDAL.GetFiltersAsync(exp);
            return SingerMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// singer单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.UpdateAsync(singer);
        }
    }
}
