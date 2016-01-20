using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsMediaPlayer
{
    class playlistManager
    {

        private MediaCreator _mediaCreator = new MediaCreator(); //TODO : Event

        public playlistManager()
        {
        }

        public ObservableCollection<Media> loadPlayListFile(string pathFile)
        {
            string line;
            string lineInfos;
            int advanced = 1;
            Media tmp = null;
            ObservableCollection<Media> playList = new ObservableCollection<Media>();

            System.IO.StreamReader file = new System.IO.StreamReader(pathFile);
            line = file.ReadLine();
            if (line == null)
            {
                file.Close();
                return playList;
            }
            if (line.IndexOf("#EXTM3U") == -1)
                advanced = 0;
            lineInfos = "";
            while ((line = file.ReadLine()) != null)
            {
                if (advanced == 1)
                {
                    if (line.IndexOf("#EXTINF:") != 0)
                    {
                        if (line.IndexOf("#EXTREM:") == -1)
                        {
                            try
                            {
                                tmp = _mediaCreator.Create(line);
                                playList.Add(tmp);
                                lineInfos = "";
                            }
                            catch (Exception e)
                            {
                                Debug.Add(e.ToString());
                            }
                        }
                    }
                    else
                        lineInfos = line.Substring(8);
                }
                else
                {
                    try
                    {
                        tmp = _mediaCreator.Create(line);
                        playList.Add(tmp);
                        lineInfos = "";
                    }
                    catch (Exception e)
                    {
                        Debug.Add(e.ToString());
                    }
                }
            }
            file.Close();
            return playList;
        }

        public void savePlayList(ObservableCollection<Media> playList, string path)
        {
            if (playList == null)
                return;
            string[] test = new string[playList.Count() + 1];
            int i = 1;

            test[0] = "#EXTM3U";
            foreach (Media plop in playList)
            {
                //if (plop.infos == "")
                    test[i] = plop.Path;
                //else
                    //test[i] = "#EXTINF:" + plop.infos + "\n" + plop.path;
                i++;
            }
            System.IO.File.WriteAllLines(path, test);
        }
    }
}
