using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class SearchTerm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserCount { get; set; }
        public int ProductCount { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsActive { get; set; }
    }


    public class PostalCodeMap
    {
        [Key]
        public int Id { get; set; }

        public string PostalCode { get; set; }
        public string PlaceName { get; set; }
        public string StateAbbreviation { get; set; }
        public string State { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool IsActive { get; set; }
    }
}
