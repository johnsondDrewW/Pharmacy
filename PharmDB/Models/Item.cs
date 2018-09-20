using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmDB.Models
{
    [Table("tblItems")]
    public class Item
    {
        [Key]
        public int ID { get; set; }
        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        [Required]
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public string Source { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public int Qauntity { get; set; }
        public string ValueSource { get; set; }
        public string StoredLocation { get; set; }
        public string Comments { get; set; }
        public string Descritption { get; set; }
        [Required]
        public string tags { get; set; }
    }
    public class Report
    {
        public double value {get;set;}
        public int numOfItems { get; set; }
        public List<string> Sources { get; set; }
    }
}