using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.IO;


namespace DaviTeste.Helpers.Utils
{
    public static class JsonHelper
    {
        public static string serialize<T>(T value)
        {
            Type type = value.GetType();
            JsonSerializer json = new JsonSerializer();

            //ignorando propriedades nulas
            json.NullValueHandling = NullValueHandling.Ignore;

            // formato da data, utilizar o iso
            json.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            if (type == typeof(DataTable))
                json.Converters.Add(new DataTableConverter());
            else if (type == typeof(DataSet))
                json.Converters.Add(new DataSetConverter());

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented;

            writer.QuoteChar = '"';
            json.Serialize(writer, value);

            string output = sw.ToString();
            writer.Close();
            sw.Close();

            return output;
        }

        public static string serialize<T>(List<T> value)
        {
            return serialize<T>(value, true);
        }


        public static string serialize<T>(List<T> value, bool intoArray)
        {

            Type type = value.GetType();
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();

            //ignorando propriedades nulas
            json.NullValueHandling = NullValueHandling.Ignore;

            // formato da data, utilizar o iso
            json.DateFormatHandling = DateFormatHandling.IsoDateFormat;

            if (type == typeof(DataTable))
                json.Converters.Add(new DataTableConverter());
            else if (type == typeof(DataSet))
                json.Converters.Add(new DataSetConverter());

            StringWriter sw = new StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);

            writer.Formatting = Formatting.Indented;

            writer.QuoteChar = '"';
            json.Serialize(writer, value);

            string output = sw.ToString();
            writer.Close();
            sw.Close();

            string endLine = System.Environment.NewLine;

            if (!intoArray)
            {
                int initJsonPosition = output.IndexOf("{");
                output = output.Remove(0, (initJsonPosition - 1));

                int finishJsonPosition = output.LastIndexOf("}");
                output = output.Substring(0, finishJsonPosition + 1);

                return output;
            }


            return output;
        }

        public static T Deserialize<T>(string value) where T : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            // se alguma propriedade nao existir no objeto, ignorar
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            return JsonConvert.DeserializeObject<T>(value);
        }

    }
}
