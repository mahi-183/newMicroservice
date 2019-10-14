// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelTest.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace XUnitTestProject.Label
{
    using BusinessManager.Service;
    using CommanLayer.Model;
    using Moq;
    using RepositoryManager.Interface;
    using Xunit;

    /// <summary>
    /// the Label test class.
    /// </summary>
    public class LabelTest
    {
        /// <summary>
        /// the test AddLabel method
        /// </summary>
        [Fact]
        public void Test()
        {
            var label = new Mock<ILabelRepositoryManager>();
            var labelData = new LabelBusinessService(label.Object);

            var data = new LabelModel()
            {
                UserId = "UserID",
                LabelName = "Lebel"
            };

            ////Act
            var Data = labelData.AddLabel(data);

            ////Asert
            Assert.NotNull(Data);
        }
    }
}
