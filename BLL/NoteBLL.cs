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

        public async Task AddAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.AddAsync(note);
        }

        public async Task DeleteAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.DeleteAsync(note);
        }

        public async Task DeleteAsync(Expression<Func<Note, bool>> exp)
        {
            await noteDAL.DeleteAsync(exp);
        }

        public async Task<NoteDTO> GetEntityAsync(Expression<Func<Note, bool>> exp)
        {
            var note=await noteDAL.GetEntityAsync(exp);
            return NoteMapperToDTO.Map(note);
        }

        public async Task<List<NoteDTO>> GetFiltersAsync(Expression<Func<Note, bool>> exp)
        {
            var list=await noteDAL.GetFiltersAsync(exp);
            return NoteMapperToDTO.MapEnum(list).ToList();
        }

        public async Task UpdateAsync(NoteDTO t)
        {
            var note = NoteDTOMapperToModel.Map(t);
            await noteDAL.UpdateAsync(note);
        }
    }
}