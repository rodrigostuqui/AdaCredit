using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdaCredit.Domain;
using System.Formats.Asn1;


namespace AdaCredit.Data
{
    public class Repository<T> 
    {
        public List<T> _data { get; private set; } = new List<T>();
        public string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public CsvConfiguration config;
        public Repository(string filename) 
        {
            config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
                Delimiter = ",",
            };
            this.filepath = $"{filepath}\\{filename}";
            if (!File.Exists(filepath))
            {
                using (var sw = new StreamWriter(this.filepath))
                using (var cw = new CsvWriter(sw, config))
                {
                    cw.WriteHeader<T>();
                }
            }
        }
        public void loadData()
        {
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, config))
            {

                _data = csv.GetRecords<T>().ToList();
            };
        }

        public void saveData(T data)
        {
            this.loadData();
            _data.Add(data);
            using (var writer = new StreamWriter(this.filepath))
            using (var csv = new CsvWriter(writer, this.config))
            {
                csv.WriteRecords(_data);
            }
        }

        public void UpdateData()
        {
            using (var writer = new StreamWriter(this.filepath))
            using (var csv = new CsvWriter(writer, this.config))
            {
                csv.WriteRecords(_data);
            }
        }

        public int Count() => this._data.Count();
    }
}
