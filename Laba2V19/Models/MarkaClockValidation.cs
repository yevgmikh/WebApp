using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laba2V19.Models
{
    [MetadataType(typeof(MarkaClockMetaData))]
    public partial class MarkaClock
    {
        [Bind(Exclude = "IdMarka")]
        public class MarkaClockMetaData
        {
            [ScaffoldColumn(false)]
            public int IdMarka { get; set; }

            [DisplayName("Назва")]
            [Required(ErrorMessage = "Назва не вказана.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Name { get; set; }

            [DisplayName("Країна")]
            [Required(ErrorMessage = "Країна не вказана.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [StringLength(50)]
            public string Country { get; set; }
        }
    }
    /*public class MarkaClockValidation
    {
    }*/
}