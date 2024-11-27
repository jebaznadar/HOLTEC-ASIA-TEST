using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using static HOLTEC_ASIA_MVC.Controllers.employeesController;


namespace HOLTEC_ASIA_MVC.Models
{
    [MetadataType(typeof(employeemetadata))]
    [DataContract]
    [KnownType(typeof(employee))]
    public partial class employee
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string lastname { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        public int gender { get; set; }
        public Nullable<int> age { get; set; }


        [Required(ErrorMessage = "Date of Birth is required.")]
        public Nullable<System.DateTime> dob { get; set; }


        [Required(ErrorMessage = "Address is required.")]
        public string address { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }
      
       /* public Gender GenderEnum
        {
            get => (Gender)this.gender; // Map the int value to the Gender enum
            set => this.gender = (int)value; // Map the enum value back to int
        }*/


        public string GenderAsString => Enum.GetName(typeof(Gender), gender);  // This is just for display purposes


    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }


    public class employeemetadata
    {
      
            [Key]
            [DataMember]
            public string Email { get; set; }

            [Required]
            [DataMember]
            public string firstname { get; set; }

            [Required]
            [DataMember]
            public string lastname { get; set; }

            [DataMember]
            public int gender { get; set; } // Should remain int

           // [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
            [DataMember]
            public Nullable<int> age { get; set; }

            [DataType(DataType.Date)]
            [DataMember]
            public Nullable<System.DateTime> dob { get; set; }

            [StringLength(250)]
            [DataMember]
            public string address { get; set; }
        


    }
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool IsBodyHtml { get; set; }
    }




}