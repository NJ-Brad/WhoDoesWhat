using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WhoDoesWhat
{
    internal class FileManager
    {
        public string FileName { get; set; } = string.Empty;
        public string Filter { get; set; } = string.Empty;

        public T Open<T>(string fileName = "")
        {
            T rtnVal = default;

            if (string.IsNullOrEmpty(fileName))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Select File";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.FileName = fileName;
                dialog.Filter = Filter;

                //if (dialog.ShowDialog() == DialogResult.OK)
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = dialog.FileName;
                }
            }
            else
            {
                FileName = fileName;
            }

            if (!string.IsNullOrEmpty(FileName))
            {
                //FileName = Path.ChangeExtension(FileName, "c4y");
                string docText = File.ReadAllText(FileName);

                if (!string.IsNullOrEmpty(docText))
                {
                    //rtnVal = (T)JsonSerializer.Deserialize(docText, typeof(T));

                    var deserializer = new DeserializerBuilder()
                        //.WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
                        .WithNamingConvention(PascalCaseNamingConvention.Instance)
                        .IgnoreUnmatchedProperties()    // https://stackoverflow.com/questions/44470352/yamldotnet-need-deserializer-to-ignore-extra-nodes-or-be-okay-with-missing-nod
                        .Build();

                    //yml contains a string containing your YAML
                    //rtnVal = deserializer.Deserialize<Person>(yml);
                    rtnVal = deserializer.Deserialize<T>(docText);
                }
            }


            return rtnVal;
        }

        public void Save<T>(T saveObject)
        {
            if (string.IsNullOrEmpty(FileName))
                SaveAs(saveObject);

            //string docText = JsonSerializer.Serialize<T>(saveObject, new JsonSerializerOptions { WriteIndented = true }); ;

            //FileName = Path.ChangeExtension(FileName, "c4y");

            var serializer = new SerializerBuilder()
                //.WithNamingConvention(CamelCaseNamingConvention.Instance)
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();
            string docText = serializer.Serialize(saveObject);

            if (!string.IsNullOrEmpty(docText))
                File.WriteAllText(FileName, docText);
        }

        public void SaveAs<T>(T saveObject)
        {
            FileName = string.Empty;
            SaveFileDialog dialog = new();
            dialog.Title = "Save File";
            dialog.OverwritePrompt = true;
            dialog.FileName = FileName;
            dialog.Filter = Filter;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileName = dialog.FileName;
                Save(saveObject);
            }
        }
    }
}
