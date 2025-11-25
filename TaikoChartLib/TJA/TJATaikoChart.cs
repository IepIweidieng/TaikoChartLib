using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoChartLib.TJA
{
    public class TJATaikoChart : TaikoChart
    {
        public static TJATaikoChart LoadFromText(string text, string path)
        {
            TJATaikoChart taikoChart = TJAReader.LoadFromText(text);
            taikoChart.FilePath = path;

            return taikoChart;
        }


        public static TJATaikoChart LoadFromFilePath(string path)
        {
            StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("SHIFT_JIS"));
            string text = streamReader.ReadToEnd();
            streamReader.Dispose();

            return LoadFromText(text, path);
        }


        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="taikoChart"></param>
        /// <returns></returns>
        public static string ChartToTJAText(TaikoChart taikoChart)
        {
            return "";
        }


        public string FilePath { get; set; } = "";
        public string DirectoryPath => Path.GetDirectoryName(FilePath);

        public string SongFileName { get; set; } = "";
        public string JacketFileName { get; set; } = "";

        public TJACompat TJACompat { get; set; } = TJACompat.OOS;

        public string GetPath(string path) => Path.Combine(DirectoryPath, path);

        public Stream GetStream(string fileName)
        {
            string path = GetPath(fileName);
            if (File.Exists(path))
            {
                return File.OpenRead(path);
            }

            return null;
        }

        public override Stream GetAudio() => GetStream(SongFileName);
        public override Stream GetPreimage() => GetStream(JacketFileName);
    }
}
