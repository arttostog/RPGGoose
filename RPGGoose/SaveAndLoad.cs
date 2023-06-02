using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGoose
{
    class SaveAndLoad
    {
        public static string PathToSave;

        public static void Save()
        {
            using (StreamWriter StreamWriter = File.CreateText(PathToSave)) StreamWriter.WriteLine(JsonConvert.SerializeObject(ModEntryPoint.Goose));
        }

        public static Goose Load()
        {
            try
            {
                using (TextReader TextReader = new StreamReader(new FileStream(PathToSave, FileMode.Open)))
                {
                    return JsonConvert.DeserializeObject<Goose>(TextReader.ReadLine());
                }
            }
            catch
            {
                return new Goose();
            }
        }
    }
}
