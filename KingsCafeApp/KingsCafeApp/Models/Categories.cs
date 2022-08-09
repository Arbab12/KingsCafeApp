using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace KingsCafeApp.Models
{
    internal class Categories
    {
        [PrimaryKey,AutoIncrement]
        public int CatId { get; set; }
        public string CatName { get; set; }
        public string CatImage { get; set; }
       

    }
}
