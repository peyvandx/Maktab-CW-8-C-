using Newtonsoft.Json;

namespace cw8_2.Context
{
    public class Serialization<T>
    {
        public bool SerializeToJsonFile(string filePath, List<T> objects)
        {
            File.Create(filePath).Close();
            try
            {
                string[] allLines = new string[0];
                foreach (var obj in objects)
                {
                    allLines = allLines.Append(SerializeObject(obj)).ToArray(); ;
                }
                File.WriteAllLines(filePath, allLines);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<T> DeserializeFromJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            List<T> objects = new List<T>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    T obj = DeSerializeToObject(line);
                    objects.Add(obj);

                }
            }
            return objects;
        }

        public string SerializeObject(T obj)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.SerializeObject(obj, settings);
        }

        public T DeSerializeToObject(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
