using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CovarageJson
{
    public class Range
    {
        public int start { get; set; }
        public int end { get; set; }
    }

    public class Root
    {
        public string url { get; set; }
        public List<Range> ranges { get; set; }
        public string text { get; set; }
    }


    internal class Program
    {
        static string GetText( string str)
        {
            string str_ = "";
            foreach (Match item in Regex.Matches(str , @"[\w]+"))
            {
                str_ += item.Value;
            }
            return str_;
        }
        static void Main(string[] args)
        {
            string data_ = "";
            string name_data = "";
            string new_name_ = "";
            string data_text_ = "";
            FileInfo fileInfo;
            if (!Directory.Exists("css"))
            {
                Directory.CreateDirectory("css");

            }
            while (true)
            {
                data_ = Console.ReadLine();
                if (File.Exists(data_))
                {
                    fileInfo = new FileInfo(data_);
                    if (fileInfo.Extension == ".json")
                    {
                        List<Root> myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(File.ReadAllText(data_));
                        data_text_ = "";


                        foreach (Root item in myDeserializedClass)
                        {
                            if (!Regex.IsMatch(item.url, "[\\w]+?\\.css"))
                                continue;
                            new_name_ = Regex.Match(item.url, "[\\w]+?\\.css").Value.Replace(fileInfo.Extension, "");


                            foreach (Range range in item.ranges)
                            {
                                data_text_ += item.text.Substring(range.start, range.end - range.start) + "\n";
                            }
                            File.WriteAllText(
                                Path.Combine("css", string.Join("", GetText(new_name_)) + ".css")
                                , data_text_);
                        }







                        #region hide
                        //foreach (var item in myDeserializedClass)
                        //{
                        //    if (!Regex.IsMatch(item.url, "[\\w]+?\\.css"))
                        //        continue;

                        //    new_name_ = Regex.Match(item.url, "[\\w]+?\\.css").Value.Replace(fileInfo.Extension, "");


                        //    File.WriteAllText(
                        //        Path.Combine("css", string.Join("", GetText(new_name_)) + ".css")
                        //        , item.text);
                        //}
                        //Console.WriteLine(".end"); 
                        #endregion
                    }
                }
                
 
            }
        }
    }
}
