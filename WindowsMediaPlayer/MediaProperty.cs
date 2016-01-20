using System;
using System.Xml.Serialization;

namespace WindowsMediaPlayer
{
    [Serializable]
    public class MediaProperty
    {
        [XmlAttribute()]
        public int AudioBitrate { get; set; }
        [XmlAttribute()]
        public int AudioChannels { get; set; }
        [XmlAttribute()]
        public int AudioSampleRate { get; set; }
        [XmlAttribute()]
        public int BitsPerSample { get; set; }
        [XmlAttribute()]
        public string Description { get; set; }
        [XmlAttribute()]
        public int PhotoHeight { get; set; }
        [XmlAttribute()]
        public int PhotoQuality { get; set; }
        [XmlAttribute()]
        public int PhotoWidth { get; set; }
        [XmlAttribute()]
        public int VideoHeight { get; set; }
        [XmlAttribute()]
        public int VideoWidth { get; set; }

        public MediaProperty()
        { }

        public MediaProperty(TagLib.Properties prop)
        {
            this.AudioBitrate = prop.AudioBitrate;
            this.AudioChannels = prop.AudioChannels;
            this.AudioSampleRate = prop.AudioSampleRate;
            this.BitsPerSample = prop.BitsPerSample;
            this.Description = prop.Description;
            this.PhotoHeight = prop.PhotoHeight;
            this.PhotoQuality = prop.PhotoQuality;
            this.PhotoWidth = prop.PhotoWidth;
            this.VideoHeight = prop.VideoHeight;
            this.VideoWidth = prop.VideoWidth;
        }
    }
}
