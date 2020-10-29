﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VYT.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VYT_TTKTEntities : DbContext
    {
        public VYT_TTKTEntities()
            : base("name=VYT_TTKTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FileStorage> FileStorages { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<usp_Job_Add_Result> usp_Job_Add(Nullable<int> userId, string name, string languages)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var languagesParameter = languages != null ?
                new ObjectParameter("languages", languages) :
                new ObjectParameter("languages", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_Add_Result>("usp_Job_Add", userIdParameter, nameParameter, languagesParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> usp_Job_GetTotal()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("usp_Job_GetTotal");
        }
    
        public virtual int usp_Job_Delete(Nullable<int> jobId)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("jobId", jobId) :
                new ObjectParameter("jobId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Job_Delete", jobIdParameter);
        }
    
        public virtual int usp_Job_Update(Nullable<int> id, Nullable<int> state, Nullable<long> duration, string notes, Nullable<int> documentPages, Nullable<System.DateTime> processed)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var stateParameter = state.HasValue ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(int));
    
            var durationParameter = duration.HasValue ?
                new ObjectParameter("duration", duration) :
                new ObjectParameter("duration", typeof(long));
    
            var notesParameter = notes != null ?
                new ObjectParameter("notes", notes) :
                new ObjectParameter("notes", typeof(string));
    
            var documentPagesParameter = documentPages.HasValue ?
                new ObjectParameter("documentPages", documentPages) :
                new ObjectParameter("documentPages", typeof(int));
    
            var processedParameter = processed.HasValue ?
                new ObjectParameter("processed", processed) :
                new ObjectParameter("processed", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("usp_Job_Update", idParameter, stateParameter, durationParameter, notesParameter, documentPagesParameter, processedParameter);
        }
    
        public virtual ObjectResult<usp_Job_GetFile_Result> usp_Job_GetFile(Nullable<int> jobId, Nullable<int> type)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("jobId", jobId) :
                new ObjectParameter("jobId", typeof(int));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_GetFile_Result>("usp_Job_GetFile", jobIdParameter, typeParameter);
        }
    
        public virtual ObjectResult<usp_JobLog_Add_Result> usp_JobLog_Add(string name, Nullable<System.DateTime> created, Nullable<int> state, Nullable<int> documentPages, Nullable<long> duration, Nullable<System.DateTime> processed, string notes)
        {
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var createdParameter = created.HasValue ?
                new ObjectParameter("created", created) :
                new ObjectParameter("created", typeof(System.DateTime));
    
            var stateParameter = state.HasValue ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(int));
    
            var documentPagesParameter = documentPages.HasValue ?
                new ObjectParameter("documentPages", documentPages) :
                new ObjectParameter("documentPages", typeof(int));
    
            var durationParameter = duration.HasValue ?
                new ObjectParameter("duration", duration) :
                new ObjectParameter("duration", typeof(long));
    
            var processedParameter = processed.HasValue ?
                new ObjectParameter("processed", processed) :
                new ObjectParameter("processed", typeof(System.DateTime));
    
            var notesParameter = notes != null ?
                new ObjectParameter("notes", notes) :
                new ObjectParameter("notes", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_JobLog_Add_Result>("usp_JobLog_Add", nameParameter, createdParameter, stateParameter, documentPagesParameter, durationParameter, processedParameter, notesParameter);
        }
    
        public virtual ObjectResult<usp_JobLog_GetPage_Result> usp_JobLog_GetPage(Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("pageIndex", pageIndex) :
                new ObjectParameter("pageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_JobLog_GetPage_Result>("usp_JobLog_GetPage", pageIndexParameter, pageSizeParameter);
        }
    
        public virtual ObjectResult<usp_Job_AddFile_Result> usp_Job_AddFile(Nullable<int> jobId, Nullable<int> type, Nullable<long> fileSize, string fileType, string filePath)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("jobId", jobId) :
                new ObjectParameter("jobId", typeof(int));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            var fileSizeParameter = fileSize.HasValue ?
                new ObjectParameter("fileSize", fileSize) :
                new ObjectParameter("fileSize", typeof(long));
    
            var fileTypeParameter = fileType != null ?
                new ObjectParameter("fileType", fileType) :
                new ObjectParameter("fileType", typeof(string));
    
            var filePathParameter = filePath != null ?
                new ObjectParameter("filePath", filePath) :
                new ObjectParameter("filePath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_AddFile_Result>("usp_Job_AddFile", jobIdParameter, typeParameter, fileSizeParameter, fileTypeParameter, filePathParameter);
        }
    
        public virtual ObjectResult<usp_Job_Get_Result> usp_Job_Get(Nullable<int> jobId)
        {
            var jobIdParameter = jobId.HasValue ?
                new ObjectParameter("jobId", jobId) :
                new ObjectParameter("jobId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_Get_Result>("usp_Job_Get", jobIdParameter);
        }
    
        public virtual ObjectResult<usp_Job_GetByState_Result> usp_Job_GetByState(Nullable<int> userId, Nullable<int> state, Nullable<int> limit)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var stateParameter = state.HasValue ?
                new ObjectParameter("state", state) :
                new ObjectParameter("state", typeof(int));
    
            var limitParameter = limit.HasValue ?
                new ObjectParameter("limit", limit) :
                new ObjectParameter("limit", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_GetByState_Result>("usp_Job_GetByState", userIdParameter, stateParameter, limitParameter);
        }
    
        public virtual ObjectResult<usp_Job_GetPage_Result> usp_Job_GetPage(Nullable<int> userId, Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("userId", userId) :
                new ObjectParameter("userId", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("pageIndex", pageIndex) :
                new ObjectParameter("pageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("pageSize", pageSize) :
                new ObjectParameter("pageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_Job_GetPage_Result>("usp_Job_GetPage", userIdParameter, pageIndexParameter, pageSizeParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual ObjectResult<usp_User_Add_Result> usp_User_Add(string email, string passwordHash)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordHashParameter = passwordHash != null ?
                new ObjectParameter("passwordHash", passwordHash) :
                new ObjectParameter("passwordHash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_User_Add_Result>("usp_User_Add", emailParameter, passwordHashParameter);
        }
    
        public virtual ObjectResult<usp_User_Get_Result> usp_User_Get(string email, string passwordHash)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordHashParameter = passwordHash != null ?
                new ObjectParameter("passwordHash", passwordHash) :
                new ObjectParameter("passwordHash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<usp_User_Get_Result>("usp_User_Get", emailParameter, passwordHashParameter);
        }
    }
}
