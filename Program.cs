using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace PlaylistCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("usage : PlaylistCreator [Directory]");
                Console.WriteLine("");
                Console.ReadKey();
            }
            else
            {
                DirSearch(args[0]);
            }
        }

        static void DirSearch(string sDir)
        {
            string[] extensionArray = { ".APE", ".FLAC", ".M4A", ".MP3", ".OGG", ".WAV" };

            string albumTitle = "";
            string[] path;
            string[] paths;

            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    bool done = false;
                    foreach (string f in Directory.GetFiles(d))
                    {
                        DirectoryInfo tmp = new DirectoryInfo(f);
                        if (!done)
                        {
                            try
                            {
                                path = d.Split('\\');                                
                                paths = path[path.Length - 1].Split('-');

                                if (paths.Length != 1)
                                    albumTitle = paths[1].Trim();
                                else
                                    albumTitle = path[path.Length - 1];


                                if (File.Exists(d + "\\" + albumTitle + ".m3u"))
                                {
                                    File.Delete(d + "\\" + albumTitle + ".m3u");
                                }

                                StreamWriter sw = File.CreateText(d + "\\" + albumTitle + ".m3u");
                                sw.Close();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("");
                            }
                        }
                        done = true;
                        if (Array.IndexOf(extensionArray, tmp.Extension.ToUpper()) > -1 )
                        {
                            using (StreamWriter w = File.AppendText(d + "\\" + albumTitle + ".m3u"))
                            {
                                w.WriteLine(tmp.Name);
                            }
                        }
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

    }
}
