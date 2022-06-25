//using System;

//namespace MusicCatalogServer
//{
//    ///<summary>Info about product</summary>
//    [System.Serializable]
//    public class Song
//    {
//        public Guid ID { get; set; }
//        public string Title { get; set; }
//        public double DurationSecs { get; set; }
//        List<string> Genres { get; set; }
//        List<string> Singers { get; set; }

//        public Song() { }

//        public Song(string title, double durationSecs, List<string> gernres, List<string> singers)
//        {
//            ID = Guid.NewGuid();
//            Title = title;
//            DurationSecs = durationSecs;
//            Genres = gernres;
//            Singers = singers;
//        }

//        public Song(Guid id, string title, double durationSecs, List<string> gernres, List<string> singers)
//        {
//            ID = id;
//            Title = title;
//            DurationSecs = durationSecs;
//            Genres = gernres;
//            Singers = singers;
//        }
//    }
//}
