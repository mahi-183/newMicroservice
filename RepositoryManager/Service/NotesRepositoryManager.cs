// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryManager.Service
{
    using CommanLayer;
    using CommanLayer.Enumerable;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;
    using RepositoryManager.DBContext;
    using RepositoryManager.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// NotesRepositoryManager class implements the interface methods like AddNotes, DeleteNotes, UpdateNotes and GetNotes
    /// </summary>
    /// <seealso cref="INotesRepositoryManager" />
    public class NotesRepositoryManager : INotesRepositoryManager
    {
        /// <summary>
        /// the authentication context reference created.
        /// </summary>
        private readonly AuthenticationContext context;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="NotesRepositoryManager"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="context">The context.</param>
        public NotesRepositoryManager(AuthenticationContext context)
        {
            this.context = context;
            //this.userManager = userManager;
        }

        /// <summary>
        /// AddNotes method is for adding the notes to the databaase
        /// </summary>
        /// <param name="notesModel">notes model.</param>
        /// <returns>return result.</returns>
        public async Task<int> AddNotes(NotesModel notesModel)
        {
                NotesModel note = new NotesModel()
                {
                    UserId = notesModel.UserId,
                    Title = notesModel.Title,
                    Description = notesModel.Description,
                    Color = notesModel.Color,
                    Reminder = notesModel.Reminder,
                    CreatedDate = notesModel.CreatedDate,
                    ModifiedDate = notesModel.ModifiedDate,
                    noteType = notesModel.noteType,
                    IsPin = notesModel.IsPin,
                    Image = notesModel.Image
                };

            try
            {
                this.context.Notes.Add(note);
                int result = await this.context.SaveChangesAsync();
                if(result > 0)
                {
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
        /// Gets all notes.
        /// </summary>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">throw exception.</exception>
        public IList<NotesModel> GetAllNotes()
        {
            try
            {
                List<NotesModel> list = new List<NotesModel>();
                var allNotes = from notes in this.context.Notes
                               select notes;

                foreach (var data in allNotes)
                {
                    list.Add(data);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// Get the notes by user id and note type.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <param name="noteType">note type.</param>
        /// <returns>return the notes.</returns>
        public IList<NotesModel> GetNotesById(string userId, NoteTypeEnum noteType)
        {
            try
            {
                ///note type value isNote=0
                var noteList = new List<NotesModel>();
                var note = from notedata in this.context.Notes where notedata.UserId == userId select notedata;
                foreach (var data in note)
                {
                    if (data.noteType == noteType)
                    {
                        noteList.Add(data);
                    }
                }

                return noteList; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  get the notes by user id
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>return notes data.</returns>
        public IList<NotesModel> GetNotesByUserId(string userId)
        {
            try
            {
                var noteList = new List<NotesModel>();
                var note = from notedata in this.context.Notes where notedata.UserId == userId select notedata;
                foreach (var data in note)
                {
                    noteList.Add(data);
                }

                return noteList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="UserId"></param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">throw exception.</exception>
        public async Task<int> UpdateNotes(NotesModel notesModel, int id)
        {
            try
            {
                var updateData = from notes in this.context.Notes
                                 where notes.Id == id  
                                 select notes;

                foreach (var update in updateData)
                {
                    update.Title = notesModel.Title;
                    update.Description = notesModel.Description;
                    update.Color = notesModel.Color;
                    update.Image = notesModel.Image;
                    update.Reminder = notesModel.Reminder;
                    update.ModifiedDate = notesModel.ModifiedDate;
                    update.noteType = notesModel.noteType;
                    update.IsPin = notesModel.IsPin;
                }

                var Result = await this.SaveAll();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        /// <returns>return result.</returns>
        public async Task<int> SaveAll()
        {
            return await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">throw exception.</exception>
        public async Task<int> DeleteNotes(int id)
        {
            try
            {
                ////NotesModel removeDta = this.context.Notes.Where(note => note.UserId == UserId).FirstOrDefault();
                var removeData = (from notes in this.context.Notes
                                 where notes.Id == id
                                 select notes).FirstOrDefault();
                
                this.context.Notes.Remove(removeData);
                var Result = await this.SaveAll();
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<string> ImageUpload(IFormFile file, int noteId)
        {
            try
            {
                ////create object of the cloudinaryImage class
                CloudinaryImage cloudinary = new CloudinaryImage();
                
                ////Cloudinary UploadImageCloudinary method call
                var uploadUrl = cloudinary.UploadImageCloudinary(file);

                ////Query to get the note data from database 
                var data = this.context.Notes.Where(note => note.Id == noteId).FirstOrDefault();

                ////update the ImageUrl to database Notes table
                data.Image = uploadUrl;

                ////Update save changes in dabase table
                int result = await this.context.SaveChangesAsync();

                ////if result is grater than zero then return the update result 
                if (result > 0)
                { 
                    return data.Image;
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
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> ImageUploadWhileCratingNote(IFormFile file)
        {
            try
            {
                ////create object of the cloudinaryImage class
                CloudinaryImage cloudinary = new CloudinaryImage();

                ////Cloudinary UploadImageCloudinary method call
                var uploadUrl = cloudinary.UploadImageCloudinary(file);
                
                ////Update save changes in dabase table
                int result = await this.context.SaveChangesAsync();

                ////if result is grater than zero then return the update result 
                if (!uploadUrl.Equals(null))
                {
                    return uploadUrl;
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
        /// <returns>return string result.</returns>
        /// <exception cref="Exception">throw exception.</exception>
        public async Task<int> AddCollaborator(CollaboratorModel collaboratorModel)
        {
            try
            {
                var collaborator = new CollaboratorModel()
                {
                    UserId = collaboratorModel.UserId,
                    NoteId = collaboratorModel.NoteId,
                    CreatedBy = collaboratorModel.CreatedBy
                };

                this.context.Collaborator.Add(collaborator);
                var result = await this.context.SaveChangesAsync();
                if (result > 0)
                {
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
        /// Get collaborator by notes id.
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>return the list of the collaborator.</returns>
        public IList<string> GetCollborators(int noteId)
        {
            try
            {
                if (!noteId.Equals(null))
                {   
                    var dbQuery = from note in this.context.Notes
                                  join collaborators in this.context.Collaborator
                                  on note.Id equals collaborators.NoteId
                                  where note.Id == noteId
                                  select new { note.Id, collaborators.UserId };
                    
                    var finalQuery = dbQuery.AsEnumerable().Select(x => string.Format("UserId:{0}; NoteId:{1}", x.UserId, x.Id));
                    foreach (var data in dbQuery)
                    {
                        var userId = data.UserId;
                        Task<IList<ApplicationUserModel>> VerirfyUser1 = VerirfyUser(userId);
                        //string email = VerirfyUser1.EmailId;
                    }
                    string[] result = finalQuery.ToArray();
                    if (!finalQuery.Equals(null))
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
        /// remove the collaborator.
        /// </summary>
        /// <param name="id">Collaborator id.</param>
        /// <returns>return result.</returns>
        public async Task<int> RemoveCollaboratorToNote(int id)
        {
            try
            {
                //// get the collaborator data 
                ///var data = this.context.Collaborator.Where(t => t.Id == id).FirstOrDefault();
                var data = (from collaborator in this.context.Collaborator
                            where (collaborator.Id == id)
                            select collaborator).FirstOrDefault();

                ////remove the collaborator
                this.context.Collaborator.Remove(data);
                ////return the result after update the database
                var result = await this.context.SaveChangesAsync();
                if (!result.Equals(null))
                {
                    return result;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Update the collaborator.
        /// </summary>
        /// <param name="noteId">note id</param>
        /// <param name="id">collaborator id.</param>
        /// <param name="notesModel">notes model data.</param>
        /// <returns></returns>
        public async Task<int> UpdateCollaborator(int noteId, int id, NotesModel notesModel)
        {
            try
            {
                ////fire the qeury to check the note id in collaborator table and Notes Table
                var quer1 = from collab in this.context.Collaborator
                            where collab.Id == id
                            select collab;
                var query = from note in this.context.Notes
                            join collaborator in this.context.Collaborator
                            on note.Id equals collaborator.NoteId
                            select note;

                foreach (var data in query)
                {
                    data.Title = notesModel.Title;
                    data.Description = notesModel.Description;
                    data.Image = notesModel.Image;
                    data.ModifiedDate = notesModel.ModifiedDate;
                    data.Color = notesModel.Color;
                    data.Reminder = notesModel.Reminder;
                    data.IsPin = notesModel.IsPin;
                    data.noteType = notesModel.noteType;
                }

                ////update the notes data in Notes table
                var result = await this.context.SaveChangesAsync();
                if (result > 0)
                {
                    ////return the success result.
                    return result;
                }
                else
                {
                    throw new Exception("The notes are not updated in database");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Delete multiple notes by id.
        /// </summary>
        /// <param name="id">note ids.</param>
        /// <returns>return success result.</returns>
        public async Task<int> BulkDelete(IList<int> id)
        {
            try
            {
                if (!id.Equals(null))
                {
                    foreach (var noteId in id)
                    {
                        var notes = (from note in this.context.Notes
                                    where note.Id == noteId
                                    select note).FirstOrDefault();

                        this.context.Notes.Remove(notes);
                    }

                    var result = await this.context.SaveChangesAsync();
                    if (result > 0)
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
        /// Search the notes by string
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IList<NotesModel> Search(string userId,string searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    var list = new List<NotesModel>();
                    var query = this.context.Notes.Where(s => s.Title.Contains(searchString)
                                                            || s.Description.Contains(searchString) && s.UserId == userId);
                    list = query.ToList();
                    if (!list.Equals(null))
                    {
                        return list;
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
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IList<NotesModel> Reminder(string userId)
        {
            try
            {
                IList<NotesModel> list = new List<NotesModel>();
                var data = from notes in this.context.Notes
                           where (notes.UserId == userId) && (notes.Reminder != null)
                           select notes;

                foreach (var reminderData in data)
                {
                    list.Add(reminderData);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public IList<NotesModel> IsPin(int noteId)
        {
            try
            {
                IList<NotesModel> list = new List<NotesModel>();
                var NoteData = from notes in this.context.Notes
                               where (notes.Id == noteId) && (notes.IsPin == true)
                               select notes;
                foreach (var Data in NoteData)
                {
                    list.Add(Data);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Gets the type of the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns></returns>
        public IList<NotesModel> GetNoteType(NoteTypeEnum noteType)
        {
            try
            {
                IList<NotesModel> list = new List<NotesModel>();
                if (noteType == NoteTypeEnum.IsNote)
                {
                    var NoteData = from notes in this.context.Notes
                                   where (notes.noteType == NoteTypeEnum.IsNote)
                                   select notes;
                    foreach (var data in NoteData)
                    {
                        list.Add(data);
                    }
                }
                else if (noteType == NoteTypeEnum.IsArchive)
                {
                    var NoteData = from notes in this.context.Notes
                                   where (notes.noteType == NoteTypeEnum.IsArchive)
                                   select notes;
                    foreach (var data in NoteData)
                    {
                        list.Add(data);
                    }
                }
                else if (noteType == NoteTypeEnum.IsTrash)
                {
                    var NoteData = from notes in this.context.Notes
                                   where (notes.noteType == NoteTypeEnum.IsTrash)
                                   select notes;
                    foreach (var data in NoteData)
                    {
                        list.Add(data);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// The verify the user and get details.
        /// </summary>
        /// <param name="id">user id.</param>
        /// <returns>return user details.</returns>
        public async Task<IList<ApplicationUserModel>> VerirfyUser(string id)
        {
            try
            {
                IList<ApplicationUserModel> product = null;
                using (var client = new HttpClient())
                {
                    //// calling api from other services
                    var response = await client.GetAsync("https://localhost:44330/api/AccountUser/GetUser?userId=" + id);
                    response.EnsureSuccessStatusCode();
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var responseAsConcreteType = JsonConvert.DeserializeObject<ApplicationUserModel>(responseAsString);
                    ////return responseAsConcreteType;
                    ////product = await response.Content.ReadAsAsync<IList<ApplicationUserModel>>();
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
