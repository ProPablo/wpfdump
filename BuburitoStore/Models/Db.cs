using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuburitoStore.Models
{
    public static class Db
    {
        //In reality you would treat this as a DTO holder and automap the DTO to an observable version of Game

        public static Game[] games = {
            new Game{
                Name= "Risk of Pain",
                URL="steam://rungameid/632360",
                ImageURL="https://m.media-amazon.com/images/M/MV5BZTNmYTFjMGQtNjIyNC00Yzc2LTgwZjYtNGJjNDFlYjdiYzVmXkEyXkFqcGdeQXVyMTA0MTM5NjI2._V1_.jpg"
            },
            new Game("purgatorial"){
                Name ="The Purgatorial",
                URL= Path.Combine("Purgatorial", "The Purgatorial.exe"),
                //ImageURL="https://img.itch.zone/aW1nLzgzMDUwNjAuanBlZw==/315x250%23c/Z7fYYg.jpeg"
            }
        };
    }
}
