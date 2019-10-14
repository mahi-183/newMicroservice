// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabelBusinessManager.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace BusinessManager.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommanLayer.Model;

    /// <summary>
    /// Interface of label business manager
    /// </summary>
    public interface ILabelBusinessManager
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>return result.</returns>
        Task<int> AddLabel(LabelModel labelModel);

        /// <summary>
        /// Gets all label.
        /// </summary>
        /// <returns>return result.</returns>
        IList<LabelModel> GetAllLabel();

        /// <summary>
        /// Gets the label by identifier.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>return result.</returns>
        IList<LabelModel> GetLabelById(string userID);

        /// <summary>
        /// update the label by its id.
        /// </summary>
        /// <param name="labelModel">label model data.</param>
        /// <param name="labelId">label id.</param>
        /// <returns>return result.</returns>
        Task<int> UpdateLabel(LabelModel labelModel, int labelId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>return result.</returns>
        Task<int> DeleteLabel(int labelId);

        /// <summary>
        /// Adds label on notes.
        /// </summary>
        /// <param name="labelModel">The Noteslabel model data.</param>
        /// <returns>return result.</returns>
        Task<int> AddNoteLabel(NotesLabelModel notesLabelModel);
        
        /// <summary>
        /// get the note label
        /// </summary>
        /// <param name="notesLabelModel">note label model data.</param>
        /// <returns>return the label on note.</returns>
        IList<LabelModel> GetNoteLabelById(NotesLabelModel notesLabelModel);
        
        /// <summary>
        /// Deletes the note label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>return result.</returns>
        Task<int> DeleteNoteLabel(int labelId, int notesId);

    }
}
