using System;
using System.Drawing;
using System.IO;

namespace RPGGoose
{
    class Config
    {
        public static string PathToConfig;

        public static void ParseConfig()
        {
            try
            {
                using (TextReader TextReader = new StreamReader(new FileStream(PathToConfig, FileMode.Open)))
                {
                    string Text;
                    while ((Text = TextReader.ReadLine()) != null)
                    {
                        if (Text.StartsWith("Disable_Monstress"))
                        {
                            int Num = Text.IndexOf("=") + 1;
                            Boolean.TryParse(Text.Substring(Num, Text.Length - Num).Trim(), out ModEntryPoint.DisableMonstress);
                            continue;
                        }
                        if (Text.StartsWith("Disable_Sounds"))
                        {
                            int Num = Text.IndexOf("=") + 1;
                            Boolean.TryParse(Text.Substring(Num, Text.Length - Num).Trim(), out ModEntryPoint.DisableSounds);
                            continue;
                        }
                        if (Text.StartsWith("Text_Brush"))
                        {
                            int Num = Text.IndexOf("=") + 1;
                            Draw.TextBrush = new SolidBrush(ColorTranslator.FromHtml(Text.Substring(Num, Text.Length - Num).Trim()));
                            continue;
                        }
                        if (Text.StartsWith("Fone_Brush"))
                        {
                            int Num = Text.IndexOf("=") + 1;
                            Draw.FoneBrush = new SolidBrush(ColorTranslator.FromHtml(Text.Substring(Num, Text.Length - Num).Trim()));
                            continue;
                        }
                        
                    }
                }
            }
            catch
            {
                using (StreamWriter streamWriter = File.Exists(PathToConfig) ? File.AppendText(PathToConfig) : File.CreateText(PathToConfig))
                {
                    Console.WriteLine("Config.txt for RPGGoose was not found. Making config file...");
                    streamWriter.WriteLine("Disable_Monstress=False");
                    streamWriter.WriteLine("Disable_Sounds=False");
                    streamWriter.WriteLine("Text_Brush=#4AF626");
                    streamWriter.WriteLine("Fone_Brush=#000000");
                }
            }
        }
    }
}
