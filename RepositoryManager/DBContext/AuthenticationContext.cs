// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationContext.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace RepositoryManager.DBContext
{
    using CommanLayer.Model;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Authentication user class derived from database context class provided by microsoft entityFramework core
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class AuthenticationContext : DbContext
    {
        /// <summary>
        /// pass the instance of the database context options class to the base database context class<see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AuthenticationContext(DbContextOptions options) : base(options) 
        {
        }

        /// <summary>
        /// we will use this database Notes to query and save the instances of the Notes model
        /// </summary>
        public DbSet<NotesModel> Notes { get; set; }

        /// <summary>
        /// we will use this database Notes to query and save the instances of the Notes model
        /// </summary>
        public DbSet<LabelModel> Label { get; set; }
        
        /// <summary>
        /// notes Label table
        /// </summary>
        public DbSet<NotesLabelModel> NotesLabel { get; set; }
        
        /// <summary>
        /// we will use this database Notes to query and save the instances of the Collaborator model 
        /// </summary>
        public DbSet<CollaboratorModel> Collaborator { get; set; }
    }
}
