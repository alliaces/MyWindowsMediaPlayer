using System;
using Lastfm.Services;
using System.Collections.Generic;
using TMDbLib.Client;

namespace WindowsMediaPlayer
{
    class MediaCreator
    {
        Tuple<List<String>, object> AuthorizedAudioExtension = new Tuple<List<String>, object>(new List<String>(new String[] { "wav", "flac", "alac", "ac3", "mp3", "aac", "wma", "m4a", "mp2" }), new Session("0fbff0a24055b4a8f0ae34dde99df96c", "5fec2231adf6e3b6dbc3ff1e4a59f6d2"));
        Tuple<List<String>, object> AuthorizedVideoExtension = new Tuple<List<String>, object>(new List<String>(new String[] { "avi", "mov", "wmv", "divx", "xvid", "mkv", "mp4", "flv" }), new TMDbClient("f3a09751e2354e194f1344cb7823b03e"));
        Tuple<List<String>, object> AuthorizedPictureExtension = new Tuple<List<String>, object>(new List<String>(new String[] { "jpg", "gif", "bmp", "tif", "pct", "png", "jpeg", "raw" }), null);
        Dictionary<MediaType, Tuple<List<String>, object>> AuthorizedExtension = new Dictionary<MediaType, Tuple<List<string>, object>>();
        bool created;

        public Media media { get; set; }
        private TagLib.File tag;

        public MediaCreator()
        {
            AuthorizedExtension.Add(MediaType.Music, AuthorizedAudioExtension);
            AuthorizedExtension.Add(MediaType.Video, AuthorizedVideoExtension);
            AuthorizedExtension.Add(MediaType.Picture, AuthorizedPictureExtension);
        }

        public Media Create(String path)
        {
            created = false;
            int point = path.LastIndexOf('.');
            if (point != -1)
            {
                String ext = path.Substring(point + 1);
                foreach (KeyValuePair<MediaType, Tuple<List<String>, object>> dic in AuthorizedExtension)
                {
                    if (dic.Value.Item1.Contains(ext.ToLower()))
                    {
                        media = new Media();
                        media.Type = dic.Key;
                        created = true;
                        Debug.Add("Media " + path + " created !");
                    }
                }
            }
            if (!created)
                throw new InvalidMediaException(path + " is not a valid media");
            try
            {
                tag = null;
                tag = TagLib.File.Create(path);
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
            media.AddTags(tag, path);
            media.CompleteData(AuthorizedExtension[media.Type].Item2);
            Debug.Add("Media datas completed !");
            return media;
        }
    }

    [Serializable]
    public class InvalidMediaException : Exception
    {
        public InvalidMediaException() { }
        public InvalidMediaException(string message) : base(message) { }
        public InvalidMediaException(string message, Exception inner) : base(message, inner) { }
        protected InvalidMediaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
