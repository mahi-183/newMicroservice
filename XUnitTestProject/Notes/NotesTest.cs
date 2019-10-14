// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesTest.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace XUnitTestProject.Notes
{
    using BusinessManager.Service;
    using CommanLayer.Model;
    using Moq;
    using RepositoryManager.Interface;
    using Xunit;

    /// <summary>
    /// Test the all functionality of NotesModel
    /// </summary>
    public class NotesTest
    {
        /// <summary>
        /// Tests this instance.
        /// </summary>
        [Fact]
        public void TestAddNotes()
        {
            var notesData = new Mock<INotesRepositoryManager>();
            var addData = new BusinessManagerService(notesData.Object);

            var data = new NotesModel()
            {
                UserId = string.Empty,
                Title = string.Empty,
                Description = string.Empty,
                Color = string.Empty,
                Image = string.Empty
            };

            ////Act 
            var datas = addData.AddNotes(data);

            ////Assert
            Assert.NotNull(datas);
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        [Fact]
        public void UpdateNote()
        {
            var notesData = new Mock<INotesRepositoryManager>();
            var addData = new BusinessManagerService(notesData.Object);

            var data = new NotesModel()
            {
                UserId = string.Empty,
                Title = string.Empty,
                Description = string.Empty,
                Color = string.Empty,
                Image = string.Empty
            };

            var id = 1;
            
            ////Act
            var Data = addData.UpdateNotes(data, id);

            ////Assert
            Assert.NotNull(Data);
        }
    }
}