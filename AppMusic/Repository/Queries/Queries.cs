using System;
using System.Collections.Generic;
using AppMusic.Entities.Enums;

namespace AppMusic.Repository.Queries
{
    public static class Queries
    {
        public const string QUERY_MUSIC_RECOVER_ALL = "SELECT * FROM `music_store`";
        public const string QUERY_MUSIC_RECOVER_SINGLE = "SELECT * FROM `music_stor`e WHERE id = '";
        public const string QUERY_MUSIC_RECOVER_AVAILABLE = "SELECT * FROM `music_store` WHERE music_available = 1";
        public const string QUERY_MUSIC_UPDATING = "UPDATE `music_store` SET music_available = 0 WHERE id = {music.Id}";
    }
}
