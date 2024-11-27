using HOLTEC_ASIA_API.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace HOLTEC_ASIA_API.Models
{
    [MetadataType(typeof(employeemetadata))]
    [DataContract]
    [KnownType(typeof(employee))]
    public partial class employee
    {
      
    }

    public class employeemetadata
    {
        [Key]
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string firstname { get; set; }
        [DataMember]

        public string lastname { get; set; }

        [DataMember]
        public int gender { get; set; }

        [DataMember]
        public Nullable<int> age { get; set; }
        [DataMember]

        public Nullable<System.DateTime> dob { get; set; }

        [DataMember]
        public string address { get; set; }

    }
}