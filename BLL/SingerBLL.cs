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

        public async Task AddAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.AddAsync(singer);
        }

        public async Task DeleteAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.DeleteAsync(singer);
        }

        public async Task DeleteAsync(Expression<Func<Singer, bool>> exp)
        {
            await singerDAL.DeleteAsync(exp);
        }

        public async Task<SingerDTO> GetEntityAsync(Expression<Func<Singer, bool>> exp)
        {
            var singer=await singerDAL.GetEntityAsync(exp);
            return SingerMapperToDTO.Map(singer);
        }

        public async Task<List<SingerDTO>> GetFiltersAsync(Expression<Func<Singer, bool>> exp)
        {
            var list = await singerDAL.GetFiltersAsync(exp);
            return SingerMapperToDTO.MapEnum(list).ToList();
        }

        public async Task UpdateAsync(SingerDTO t)
        {
            var singer = SingerDTOMapperToModel.Map(t);
            await singerDAL.UpdateAsync(singer);
        }
    }
}
