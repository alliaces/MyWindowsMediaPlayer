using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Threading;
using System.ComponentModel;

namespace WindowsMediaPlayer
{
    public enum MediaType { Music, Video, Picture };

    public delegate void LibraryLoadedHandler(List<Media> library);

    public class MediaLibrary
    {
        public static event LibraryLoadedHandler OnLibraryLoaded = delegate { };

        public List<Media> Library { get; private set; }
        StreamWriter XMLWriter;
        StreamReader XMLReader;
        XmlSerializer serializer = new XmlSerializer(typeof(List<Media>));
        MediaCreator creator = new MediaCreator();
        /// create_lib (true), clear lib (true), lib, xml
        /// (de)serialize (false), serialize (true), "", xml
        Queue<Tuple<bool, bool, string, string>> todo;
        bool isRun = false;
        string actual_xml;

        public MediaLibrary(string lib = "", string XmlFile = "Library.xml")
        {
            todo = new Queue<Tuple<bool, bool, string, string>>();
            Library = new List<Media>();
            Deserialize();
            if (!Library.Any() && !String.IsNullOrEmpty(lib))
                CreateLibrary(lib, XmlFile);
        }

        public List<Media> CreateLibrary(string lib, string xmlFile = "Library.xml")
        {
            System.Diagnostics.Debug.WriteLine("Create Library");
            Debug.Add("Create Library");
            todo.Enqueue(new Tuple<bool, bool, string, string>(true, true, lib, xmlFile));
            if (!isRun)
                run();
            return Library;
        }

        private void sendLibrary(object sender, RunWorkerCompletedEventArgs e)
        {
            OnLibraryLoaded(Library);
        }

        public List<Media> AddToLibrary(string lib, string xmlFile = "Library.xml")
        {
            System.Diagnostics.Debug.WriteLine("AddToLibrary");
            Debug.Add("AddToLibrary");
            todo.Enqueue(new Tuple<bool, bool, string, string>(true, false, lib, xmlFile));
            if (!isRun)
                run();
            return Library;
        }

        private void run()
        {
            isRun = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += doCreateLibrary;
            worker.RunWorkerCompleted += sendLibrary;
            worker.RunWorkerAsync();
        }

        private void doCreateLibrary(object sender, DoWorkEventArgs e)
        {
            while (todo.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("Change en " + todo.Peek().ToString());
                Debug.Add("Change en " + todo.Peek().ToString());
                lock (Library)
                {
                    Tuple<bool, bool, string, string> tmp = todo.Dequeue();
                    if (tmp.Item1)
                    {
                        if (tmp.Item2)
                            DeleteLibrary(tmp.Item4);
                        actual_xml = tmp.Item4;
                        addLibrary(tmp.Item3);
                        doSerialize(tmp.Item4);
                    }
                    else
                    {
                        if (tmp.Item2)
                            doSerialize(tmp.Item4);
                        else
                            doDeserialize(tmp.Item4);
                    }
                }
                OnLibraryLoaded(Library);
            }
            isRun = false;
        }

        private void addLibrary(string lib)
        {
            try
            {
                if (File.Exists(lib))
                    SelectGoodFile(lib);
                foreach (String file in Directory.EnumerateFiles(lib))
                    SelectGoodFile(file);
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
            try
            {
                foreach (String dir in Directory.EnumerateDirectories(lib))
                {
                    doSerialize(actual_xml);
                    OnLibraryLoaded(Library);
                    addLibrary(dir);
                }
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        private void SelectGoodFile(String file)
        {
            try
            {
                creator.Create(file);
                if (!Library.Contains(creator.media))
                    Library.Add(creator.media);
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        public void Serialize(String Path = "Library.xml")
        {
            todo.Enqueue(new Tuple<bool, bool, string, string>(false, true, "", Path));
            if (!isRun)
                run();
        }

        private void doSerialize(String Path)
        {
            System.Diagnostics.Debug.WriteLine("Serialize");
            try
            {
                using (XMLWriter = new StreamWriter(Path))
                {
                    serializer.Serialize(XMLWriter, Library);
                }
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        public List<Media> Deserialize(String Path = "Library.xml")
        {
            todo.Enqueue(new Tuple<bool, bool, string, string>(false, false, "", Path));
            if (!isRun)
                run();
            return Library;
        }

        private void doDeserialize(String Path)
        {
            System.Diagnostics.Debug.WriteLine("Deserialize");
            Library.Clear();
            try
            {
                using (XMLReader = new StreamReader(Path))
                {
                    this.Library = (List<Media>)serializer.Deserialize(XMLReader);
                }
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
        }

        public void DeleteLibrary(string xmlFile = "Library.xml")
        {
            Library.Clear();
            try
            {
                Directory.Delete("Covers", true);
            }
            catch (Exception e)
            {
                Debug.Add(e.ToString() + "\n");
            }
            doSerialize(xmlFile);
            Debug.Add("Library deleted !");
            OnLibraryLoaded(Library);
        }

        public void Delete(Media media, string xmlFile = "Library.xml")
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += doDelete;
            worker.RunWorkerCompleted += sendLibrary;
            Tuple<Media, string> tuple = new Tuple<Media, string>(media, xmlFile);
            worker.RunWorkerAsync(tuple);
        }

        private void doDelete(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Delete");
            lock (Library)
            {
                Tuple<Media, string> tuple = e.Argument as Tuple<Media, string>;
                Library.Remove((Media)tuple.Item1);
                doSerialize((string)tuple.Item2);
            }
        }
    }
}
