// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelBusinessService.cs" company="Bridgelabz">
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
    using CommanLayer.Model;
    using RepositoryManager.Interface;

    /// <summary>
    /// The Label business manager service class.
    /// </summary>
    public class LabelBusinessService : ILabelBusinessManager
    {
        /// <summary>
        /// the cache key for unique
        /// </summary>
        private const string data = "data";

        /// <summary>
        /// create reference of repositoryManager layer.
        /// </summary>
        private ILabelRepositoryManager repositoryManager;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBusinessService"/> class.
        /// </summary>
        /// <param name="repositoryManager">The repository manager.</param>
        public LabelBusinessService(ILabelRepositoryManager repositoryManager)
        {
            this.repositoryManager = repositoryManager;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns> return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<int> AddLabel(LabelModel labelModel)
        {
            try
            {
                if (!labelModel.Equals(null))
                {
                    ////repositoryManager layer method called
                    var result = await this.repositoryManager.AddLabel(labelModel);
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
        /// Gets all label.
        /// </summary>
        /// <returns>return all labels.</returns>
        /// <exception cref="Exception">
        /// throw exception
        /// </exception>
        public IList<LabelModel> GetAllLabel()
        {
            try
            {
                ////repositoryManager layer method called
                var result = this.repositoryManager.GetAllLabel();
                if (!result.Equals(null))
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
        /// Gets the label by identifier.
        /// </summary>
        /// <param name="userId">The label identifier.</param>
        /// <returns> return result.</returns>
        public IList<LabelModel> GetLabelById(string userId)
        {
            if (userId.Equals(null))
            {
                try
                {
                    var result = this.repositoryManager.GetLabelById(userId);
                    if (!result.Equals(null))
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
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="labelId">Label id.</param>
        /// <returns>return success result.</returns>
        /// <exception cref="Exception">
        /// throw exceptions.
        /// </exception>
        public async Task<int> UpdateLabel(LabelModel labelModel, int labelId)
        {
            try
            {
                if (labelModel.Equals(null) && labelId.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var result = await this.repositoryManager.UpdateLabel(labelModel, labelId);
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
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<int> DeleteLabel(int labelId)
        {
            var cacheKey = data + labelId;
            try
            {
                if (!labelId.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var result = await this.repositoryManager.DeleteLabel(labelId);
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
        /// Adds label on notes.
        /// </summary>
        /// <param name="notesLabelModel">The NotesLabel model data.</param>
        /// <returns> return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<int> AddNoteLabel(NotesLabelModel notesLabelModel)
        {
            try
            {
                ///check notes label model data is not null
                if (!notesLabelModel.Equals(null))
                {
                    ////repositoryManager layer method called
                    var result = await this.repositoryManager.AddNoteLabel(notesLabelModel);
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
        /// get the label which is on note.
        /// </summary>
        /// <param name="notesLabelModel">note label data model.</param>
        /// <returns>return the label data.</returns>
        public IList<LabelModel> GetNoteLabelById(NotesLabelModel notesLabelModel)
        {
            if (notesLabelModel.Equals(null))
            {
                try
                {
                    var result = this.repositoryManager.GetNoteLabelById(notesLabelModel);
                    if (!result.Equals(null))
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
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>return result.</returns>
        /// <exception cref="Exception">
        /// throw exception.
        /// </exception>
        public async Task<int> DeleteNoteLabel(int labelId,int notesId)
        {
            var cacheKey = data + labelId;
            try
            {
                if (!labelId.Equals(null) && !notesId.Equals(null))
                {
                    ////repositoryManager Layer method call
                    var result = await this.repositoryManager.DeleteNoteLabel(labelId, notesId);
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
    }
}
