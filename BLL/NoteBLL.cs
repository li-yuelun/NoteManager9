
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
using System.Threading.Tasks;

namespace BLL
{
    public class NoteBLL : INoteBLL
    {

        public IUserDAL userDAL { get; set; }

        public IRoleDAL roleDAL { get; set; }

        public IPowerDAL powerDAL { get; set; }

        public INoteDAL noteDAL { get; set; }

        public static ObjectsMapper<Note, NoteDTO> NoteMapperToDTO = ObjectMapperManager.DefaultInstance.GetMapper<Note, NoteDTO>();

        public static ObjectsMapper<NoteDTO, Note> NoteDTOMapperToModel = ObjectMapperManager.DefaultInstance.GetMapper<NoteDTO, Note>();

        /// <summary>
        /// note单个新增
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task AddAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.AddAsync(note);
            await noteDAL.CommitAsync();
        }

        /// <summary>
        /// role单个删除(真实删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.DeleteAsync(note);
            await noteDAL.CommitAsync();
        }

        /// <summary>
        /// role单个删除(软删除)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task DeleteBySoftAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.DeleteBySoftAsync(note);
            await noteDAL.CommitAsync();
        }

        /// <summary>
        /// role动态删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Note, bool>> exp)
        {
            await noteDAL.DeleteAsync(exp);
            await noteDAL.CommitAsync();
        }

        /// <summary>
        /// role单个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<NoteDTO> GetEntityAsync(Expression<Func<Note, bool>> exp)
        {
            var note=await noteDAL.GetEntityAsync(exp);
            return NoteMapperToDTO.Map(note);
        }

        /// <summary>
        /// role多个动态获取
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public async Task<List<NoteDTO>> GetFiltersAsync(Expression<Func<Note, bool>> exp)
        {
            var list=(await noteDAL.GetFiltersAsync(exp)).ToList();
            return NoteMapperToDTO.MapEnum(list).ToList();
        }

        /// <summary>
        /// role单个修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task UpdateAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.UpdateAsync(note);
            await noteDAL.CommitAsync();
        }
    }
}
