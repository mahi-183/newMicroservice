// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INotesRepositoryManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommanLayer.Enumerable;
    using CommanLayer.Model;
    using Microsoft.AspNetCore.Http;

    public interface INotesRepositoryManager
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result.</returns>
        Task<int> AddNotes(NotesModel notesModel);

        /// <summary>
        /// Gets all notes.
        /// </summary>
        /// <returns>return result.</returns>
        IList<NotesModel> GetAllNotes();

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result.</returns>
        IList<NotesModel> GetNotesById(string userId, NoteTypeEnum noteType);

        /// <summary>
        /// get notes by user id.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>return result.</returns>
        IList<NotesModel> GetNotesByUserId(string userId);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return result.</returns>
        Task<int> UpdateNotes(NotesModel notesModel, int id);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return result.</returns>
        Task<int> DeleteNotes(int id);

        /// <summary>
        /// Images the upload.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return result.</returns>
        Task<string> ImageUpload(IFormFile formFile, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        Task<string> ImageUploadWhileCratingNote(IFormFile formFile);
        
        /// <summary>
        /// Adds the collaborator.
        /// </summary>
        /// <param name="collaboratorModel">The collaborator model.</param>
        /// <returns>return the value.</returns>
        Task<int> AddCollaborator(CollaboratorModel collaboratorModel);

        /// <summary>
        /// Get the collaborator.
        /// </summary>
        /// <param name="email">email.</param>
        /// <returns>return the collaborator data.</returns>
        IList<string> GetCollborators(int id);

        /// <summary>
        /// Remove Collaborator method.
        /// </summary>
        /// <param name="id">collaborator id.</param>
        /// <returns>return result.</returns>
        Task<int> RemoveCollaboratorToNote(int id);

        /// <summary>
        /// update the collaborator.
        /// </summary>
        /// <param name="noteId">note id.</param>
        /// <param name="id">note id.</param>
        /// <param name="notesModel">notes model.</param>
        /// <returns>return result.</returns>
        Task<int> UpdateCollaborator(int noteId, int id, NotesModel notesModel);

        /// <summary>
        /// Delete the multiple notes.
        /// </summary>
        /// <param name="id">notes id.</param>
        /// <returns>return result.</returns>
        Task<int> BulkDelete(IList<int> id);

        /// <summary>
        /// search the title description by string.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <param name="searchString">search string.</param>
        /// <returns>return result.</returns>
        IList<NotesModel> Search(string userId,string searchString);

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>return result.</returns>
        IList<NotesModel> Reminder(string userId);

        /// <summary>
        /// Determines whether the specified user identifier is pin.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns>return result.</returns>
        IList<NotesModel> IsPin(int noteId);

        /// <summary>
        /// Gets the type of the note.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>return result</returns>
        IList<NotesModel> GetNoteType(NoteTypeEnum NoteType);
    }
}
