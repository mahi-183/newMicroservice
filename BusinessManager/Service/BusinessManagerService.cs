// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessManagerService.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------

namespace BusinessManager.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BusinessManager.Interface;
    using CommanLayer.Enumerable;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using RepositoryManager.Interface;
    using ServiceStack.Redis;

    /// <summary>
    /// the business manager service.
    /// </summary>
    public class BusinessManagerService : IBusinessManager
    {
        /// <summary>
        /// the unique key for redis cache.
        /// </summary>
        private const string Data = "data";

        /// <summary>
        /// create reference of INotesRepository
        /// </summary>
        private INotesRepositoryManager repositoryManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessManagerService"/> class.
        /// </summary>
        /// <param name="repositoryManager">The repository manager.</param>
        public BusinessManagerService(INotesRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Add the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result.</returns>
        public async Task<int> AddNotes(NotesModel notesModel)
        {
            ////RepositoryManager layer method called
            var result = await this.repositoryManager.AddNotes(notesModel);
            return result;
        }

        /// <summary>
        /// Display All notes 
        /// </summary>
        /// <returns>return result.</returns>
        public IList<NotesModel> GetAllNotes()
        {
            try
            {
                ////RepositoryLayer method call
                var Result = this.repositoryManager.GetAllNotes();

                ////if result contains null it throw the exeption 
                if (Result != null)
                {
                    ////return result
                    return Result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get notes by user id and note type.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <param name="noteType">note type.</param>
        /// <returns> return result.</returns>
        public IList<NotesModel> GetNotesById(string userId, NoteTypeEnum noteType)
        {
            try
            {
                ////concate the userId and data string for unique key in redis cache.
                var cachekey = Data + userId;
                using (var redis = new RedisClient())
                {
                    ////clean the redis cache.
                    redis.Remove(cachekey);

                    if (redis.Get(cachekey) == null)
                    {
                        //// repositoryManager layer method called
                        var noteData = this.repositoryManager.GetNotesById(userId, noteType);
                        if (noteData != null)
                        {
                            redis.Set(cachekey, userId);
                        }

                        return noteData;
                    }
                    else
                    {
                        IList<NotesModel> list = new List<NotesModel>();
                        var list1 = redis.Get(cachekey);
                        ////list.Add(list1);
                        return list;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get notes by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>return the notes data.</returns>
        public IList<NotesModel> GetNotesByUserId(string userId)
        {
            try
            {
                if (!userId.Equals(null))
                {
                    ////repository manager layer method call
                    var result = this.repositoryManager.GetNotesByUserId(userId);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("the notes data not fetched");
                    }
                }
                else
                {
                    throw new Exception("the user id is invalid.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">notes model data.</param>
        /// <param name="id">The identifier.</param>
        /// <returns> return result.</returns>
        public async Task<int> UpdateNotes(NotesModel notesModel, int id)
        {
            try
            {
                var result = await this.repositoryManager.UpdateNotes(notesModel, id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Delete the notes.
        /// </summary>
        /// <param name="id">notes id.</param>
        /// <returns> return result.</returns>
        /// <value>
        /// The delete notes.
        /// </value>
        public async Task<int> DeleteNotes(int id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    ////repositoryManager Layer method call.
                    var result = await this.repositoryManager.DeleteNotes(id);
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns> return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<string> ImageUpload(IFormFile formFile, int id)
        {
            try
            {
                if (!formFile.Equals(null))
                {
                    ////RepositoryLayer method called
                    var result = await this.repositoryManager.ImageUpload(formFile, id);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        public async Task<string> ImageUploadWhileCratingNote(IFormFile formFile)
        {
            try
            {
                if (!formFile.Equals(null))
                {
                    ////RepositoryLayer method called
                    var result = await this.repositoryManager.ImageUploadWhileCratingNote(formFile);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>return the string result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<int> AddCollaborator(CollaboratorModel collaboratorModel)
        {
            try
            {
                if (!collaboratorModel.Equals(null))
                {
                    ////repository service method called
                    var result = await this.repositoryManager.AddCollaborator(collaboratorModel);
                    ////result is not null then return the result.
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// get the collaborators
        /// </summary>
        /// <param name="id">collaborator id</param>
        /// <returns> return result.</returns>
        public IList<string> GetCollborators(int id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    ////repositoryManager Layer call
                    var result = this.repositoryManager.GetCollborators(id);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// update the collaborator data.
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="id">collaborator id.</param>
        /// <param name="notesModel">notes model data.</param>
        /// <returns>return the result.</returns>
        public async Task<int> UpdateCollaborator(int noteId, int id, NotesModel notesModel)
        {
            try
            {
                if (!notesModel.Equals(null) && !id.Equals(null) && !noteId.Equals(null))
                {
                    ////repositoryManager Layer method.
                    var result = await this.repositoryManager.UpdateCollaborator(noteId, id, notesModel);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception("collaborator is not successfully updated");
                    }
                }
                else
                {
                    throw new Exception("note id or collaborator id or notes model data are null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// remove collaborator from notes
        /// </summary>
        /// <param name="id">collaborator id.</param>
        /// <returns>return result.</returns>
        public async Task<int> RemoveCollaboratorToNote(int id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    var result = await this.repositoryManager.RemoveCollaboratorToNote(id);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// delete notes by multiple id.
        /// </summary>
        /// <param name="id">List of id.</param>
        /// <returns>return result.</returns>
        public async Task<int> BulkDelete(IList<int> id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    ////NotesRepository Layer method called.
                    var result = await this.repositoryManager.BulkDelete(id);
                    if (!result.Equals(null))
                    {
                        ////return result.
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// search notes or description by string.
        /// </summary>
        /// <param name="userId">userI.</param>
        /// <param name="searchString">input string.</param>
        /// <returns>return result.</returns>
        public IList<NotesModel> Search(string userId, string searchString)
        {
            try
            {
                if (!searchString.Equals(null)&& !userId.Equals(null))
                {
                    ////notes list.
                    IList<NotesModel> result = new List<NotesModel>();

                    ////repositoryManager Layer method call
                    result = this.repositoryManager.Search(userId, searchString);
                    if (!result.Equals(null))
                    {
                        return result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="noteId">The user identifier.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">
        /// throws exception
        /// </exception>
        public IList<NotesModel> Reminder(string userId)
        {
            try
            {
                if (!userId.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var Result = this.repositoryManager.Reminder(userId);
                    return Result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="noteId">note id.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">
        /// throw Exception.
        /// </exception>
        public IList<NotesModel> IsPin(int noteId)
        {
            try
            {
                if (!noteId.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var result = this.repositoryManager.IsPin(noteId);
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the type of the notes.
        /// </summary>
        /// <param name="NoteType">The notes model.</param>
        /// <returns>return result.</returns>
        public IList<NotesModel> GetNoteType(NoteTypeEnum NoteType)
        {
            try
            {
                if (!NoteType.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var result = this.repositoryManager.GetNoteType(NoteType);
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
