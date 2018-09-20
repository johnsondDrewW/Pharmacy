using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace PharmDB.Models
{
    [Table("tblCategories")]
    public class Category
    {

        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ParentCategory { get; set; }
        public string Description { get; set; }
    }
    public class TagList
    {
        public string Name { get; set; }
        public string MainCat { get; set; }
    }
       
    
        
}