using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsMediaPlayer
{
    public static class Debug
    {
        private static Queue<String> debug = new Queue<string>();
        private static Dictionary<string, bool> append = new Dictionary<string, bool>();

        public static void Add(String str, string path = "debug.txt")
        {
          #if (DEBUG)
            debug.Enqueue(str);
            if (!append.ContainsKey(path))
                append.Add(path, false);
            try
            {
                using (StreamWriter file = new StreamWriter(path, append[path]))
                {
                    append[path] = true;
                    while (debug.Count > 0)
                        file.WriteLine(debug.Dequeue());
                }
            }
            catch
            { }
          #endif
        }

        public static void Aff()
        {
            while (debug.Count > 0)
                System.Diagnostics.Debug.WriteLine(debug.Dequeue());
        }
    }
}
