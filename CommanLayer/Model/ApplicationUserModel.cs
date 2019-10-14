// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserModel.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace CommanLayer.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Application User Model for getting user details.
    /// </summary>
    public class ApplicationUserModel 
    {

        /// <summary>
        /// Gets or sets the userId.
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the EmailId. 
        /// </summary>
        [Required]
        [EmailAddress]
        public string EmailId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        //// [Required(ErrorMessage = "Please select file.")]
        //// [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the UserType.
        /// </summary>
        /// <value>
        /// The UserType.
        /// </value>
        public string UserType { get; set; }

        /// <summary>
        /// Gets or sets the ServiceId.
        /// </summary>
        /// <value>
        /// The ServiceId.
        /// </value>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or sets the NotificationToken.
        /// </summary>
        /// <value>
        /// The NotificationToken.
        /// </value>
        public string NotificationToken { get; set; }
    }
}
