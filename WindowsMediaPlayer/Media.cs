using System;
using System.Xml.Serialization;
using Lastfm.Services;
using System.Net;
using System.IO;
using TMDbLib.Client;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.Movies;

namespace WindowsMediaPlayer
{
    [Serializable]
    public class Media
    {
        public MediaProperty Properties { get; set; }
        [XmlAttribute()]
        public String Path { get; set; }
        [XmlAttribute()]
        public string AmazonId { get; set; }
        [XmlAttribute()]
        public string[] Artists { get; set; }
        [XmlAttribute()]
        public uint BPM { get; set; }
        [XmlAttribute()]
        public string Comment { get; set; }
        [XmlAttribute()]
        public string[] Composers { get; set; }
        [XmlAttribute()]
        public string Conductor { get; set; }
        [XmlAttribute()]
        public string Copyright { get; set; }
        [XmlAttribute()]
        public uint Disc { get; set; }
        [XmlAttribute()]
        public uint DiscCount { get; set; }
        [XmlAttribute()]
        public string[] Genres { get; set; }
        [XmlAttribute()]
        public string Lyrics { get; set; }
        [XmlAttribute()]
        public string[] Performers { get; set; }
        public string Picture { get; set; }
        [XmlAttribute()]
        public string Title { get; set; }
        [XmlAttribute()]
        public uint Track { get; set; }
        [XmlAttribute()]
        public uint TrackCount { get; set; } 
        public MediaType Type { get; set; }
        [XmlAttribute()]
        public String Album { get; set; }
        [XmlAttribute()]
        public uint Year { get; set; }
        [XmlAttribute()]
        public long Length { get; set; }
        [XmlAttribute()]
        public string Wiki { get; set; }
        [XmlAttribute()]
        public string Url { get; set; }
        [XmlAttribute()]
        public bool Adult { get; set; }

        public Media()
        { }

        public void AddTags(TagLib.File tag, string path)
        {
            if (tag == null)
            {
                this.Path = path;
                this.Title = System.IO.Path.GetFileNameWithoutExtension(Path);
                selectPicture(null);
                return;
            }
            this.Properties = new MediaProperty(tag.Properties);
            this.Path = tag.Name;
            this.Album = tag.Tag.Album;
            this.AmazonId = tag.Tag.AmazonId;
            this.Artists = tag.Tag.AlbumArtists;
            this.BPM = tag.Tag.BeatsPerMinute;
            this.Comment = tag.Tag.Comment;
            this.Composers = tag.Tag.Composers;
            this.Conductor = tag.Tag.Conductor;
            this.Copyright = tag.Tag.Copyright;
            this.Disc = tag.Tag.Disc;
            this.DiscCount = tag.Tag.DiscCount;
            this.Genres = tag.Tag.Genres;
            this.Lyrics = tag.Tag.Lyrics;
            this.Performers = tag.Tag.Performers;
            this.Title = tag.Tag.Title;
            if (Title == "" || Title == null)
                this.Title = System.IO.Path.GetFileNameWithoutExtension(Path);
            try
            {
                selectPicture(tag);
            }
            catch
            {
                this.Picture = @"/WindowsMediaPlayer;component/assets/no_cover.png";
            }
            this.Track = tag.Tag.Track;
            this.TrackCount = tag.Tag.TrackCount;
            this.Year = tag.Tag.Year;
            this.Length = tag.Length;
        }

        private void selectPicture(TagLib.File tag)
        {
            if (this.Type == MediaType.Picture)
            {
                Picture = Path;
                return;
            }
            if (this.Type == MediaType.Video)
            {
                Picture = @"/WindowsMediaPlayer;component/assets/video_cover.png";
                return;
            }
            if (tag != null && tag.Tag.Pictures.Length > 0)
            {
                this.Picture = Directory.GetCurrentDirectory() + @"\Covers\" + Path.GetHashCode() + ".png";
                System.Drawing.Image tmp = null;
                TagLib.IPicture pic = tag.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                if (ms != null && ms.Length > 4096)
                {
                    tmp = System.Drawing.Image.FromStream(ms);
                    tmp.Save(this.Picture, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                    throw new Exception();
                ms.Close();
            }
            else
                this.Picture = @"/WindowsMediaPlayer;component/assets/no_cover.png";
            return;
        }

        public void CompleteData(object session)
        {
            if (this.Type == MediaType.Music)
                LastfmComplete(session as Lastfm.Services.Session);
            if (this.Type == MediaType.Video)
                TmdbComplete(session as TMDbLib.Client.TMDbClient);
        }

        private void TmdbComplete(TMDbClient tmdb)
        {
            try
            {
                String[] split = Title.Split(new char[] { ' ', '.', '_', '-' });
                TMDbLib.Objects.General.SearchContainer<SearchMovie> results = tmdb.SearchMovie(this.Title);
                for (int i = split.Length; results.TotalResults == 0 && i > 0 ; --i)
                {
                    String name = null;
                    for (int j = 0; j < i; ++j)
                        name += " " + split[j];
                    results = tmdb.SearchMovie(name);
                }
                Movie mov = tmdb.GetMovie(results.Results[0].Id);
                this.Adult = mov.Adult;
                this.Url = mov.Homepage;
                this.Title = mov.Title;
                if (mov != null && this.Picture == @"/WindowsMediaPlayer;component/assets/video_cover.png")
                    SetImage(@"https://image.tmdb.org/t/p/w185" + mov.PosterPath, Lastfm.Utilities.md5(Path));
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        private void LastfmComplete(Session lastfm)
        {
            try
            {
                Debug.Add("Title = " + this.Title);
                Debug.Add("Artist = " + this.Artists[0]);
                Lastfm.Services.Track tr = new Track(this.Artists[0], this.Title, lastfm);
                Url = tr.URL;
                Lastfm.Services.Album al = tr.GetAlbum();
                if (al != null && this.Picture == @"/WindowsMediaPlayer;component/assets/no_cover.png")
                    SetImage(al.GetImageURL(AlbumImageSize.ExtraLarge), al.GetMBID());
                Wiki = tr.Wiki.getContent();
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        private void SetImage(string url, string id)
        {
            try
            {
                this.Picture = Directory.GetCurrentDirectory() + @"\Covers\" + id + ".png";
                if (!System.IO.File.Exists(this.Picture))
                {
                    WebRequest requestPic = WebRequest.Create(url);
                    WebResponse responsePic = requestPic.GetResponse();
                    System.Drawing.Image pic = System.Drawing.Image.FromStream(responsePic.GetResponseStream());
                    Debug.Add(id);
                    if (!Directory.Exists("Covers"))
                        Directory.CreateDirectory("Covers");
                    pic.Save(this.Picture, System.Drawing.Imaging.ImageFormat.Png);
                    Debug.Add("Image created !");
                }
            }
            catch (Exception e)
            {
                if (this.Type == MediaType.Music)
                    this.Picture = @"/WindowsMediaPlayer;component/assets/no_cover.png";
                else
                    this.Picture = @"/WindowsMediaPlayer;component/assets/video_cover.png";
                Debug.Add(e.ToString() + "\n");
            }
        }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;
            Media med = obj as Media;
            if ((System.Object)med == null)
                return false;
            return (Path == med.Path);
        }

        public bool Equals(Media med)
        {
            if ((object)med == null)
                return false;
            return (Path == med.Path);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
