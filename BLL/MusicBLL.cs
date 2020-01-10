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
    public class MusicBLL : IMusicBLL
    {
        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public ISingerDAL singerDAL { get; set; }

        public IMusicDAL musicDAL { get; set; }

        public static ObjectsMapper<Music, MusicDTO> MusicMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Music, MusicDTO>();

        public static ObjectsMapper<MusicDTO, Music> MusicDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<MusicDTO, Music>();

        /// <summary>
        /// 单个音乐新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(MusicDTO t)
        {
            var music = MusicDTOMapperToModel.Map(t);
            await musicDAL.AddAsync(music);
            await musicDAL.CommitAsync();
        }

        /// <summary>
        /// 单个音乐删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(MusicDTO t)
        {
            var music = MusicDTOMapperToModel.Map(t);
            await musicDAL.DeleteAsync(music);
            await musicDAL.CommitAsync();
        }

        /// <summary>
        /// 单个音乐删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(MusicDTO t)
        {
            var music = MusicDTOMapperToModel.Map(t);
            await musicDAL.DeleteBySoftAsync(music);
            await musicDAL.CommitAsync();
        }

        /// <summary>
        /// 根据条件动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Music, bool>> exp)
        {
            await musicDAL.DeleteAsync(exp);
            await musicDAL.CommitAsync();
        }

        /// <summary>
        /// 根据条件动态单个获取音乐
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<MusicDTO> GetEntityAsync(Expression<Func<Music, bool>> exp)
        {
            var user = await musicDAL.GetEntityAsync(exp);
            return MusicMapperToDTO.Map(user);
        }

        /// <summary>
        /// 根据条件动态查询音乐集合
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<MusicDTO>> GetFiltersAsync(Expression<Func<Music, bool>> exp)
        {
            var list = (await musicDAL.GetFiltersAsync(exp)).ToList();
            return MusicMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// 单个修改音乐
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(MusicDTO t)
        {
            var music = MusicDTOMapperToModel.Map(t);
            await musicDAL.UpdateAsync(music);
            await musicDAL.CommitAsync();
        }

    }
}
