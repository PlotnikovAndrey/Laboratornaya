using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Lab1ISIS
{
    class Context : DbContext
    {
        public DbSet<Text> text { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<MismatchedLines> mismatchedLines { get; set; }
        public DbSet<IniSettings> iniSettings { get; set; }
    }

    [Table("Text")]
    public class Text
    {
        [Key]
        public int id { get; set; }
        [Column]
        public string name { get; set; }
        [Column]
        public string content { get; set; }
    }

    [Table("Users")]
    public class Users
    {
        [Key]
        public int id { get; set; }
        [Column]
        public string login { get; set; }
        [Column]
        public string password { get; set; }
    }

    [Table("MismatchedLines")]
    public class MismatchedLines
    {
        [Key]
        public int id { get; set; }
        [Column]
        public string line1 { get; set; }
        [Column]
        public string line2 { get; set; }
    }

    [Table("IniSettings")]
    public class IniSettings
    {
        [Key]
        public int id { get; set; }
        [Column]
        public int name { get; set; }
        [Column]
        public int value { get; set; }
    }
}

