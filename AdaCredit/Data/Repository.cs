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
using AdaCredit.UI.UseCases;

namespace AdaCredit.Data
{
    public class Repository<T> 
    {
        public List<T> _data { get; private set; } = new List<T>();
        public string filepath;
        public CsvConfiguration config;
        public Repository(string filename) 
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if(!Directory.Exists($"{desktopPath}\\Transactions"))
            {
                Directory.CreateDirectory($"{desktopPath}\\Transactions");
                Directory.CreateDirectory($"{desktopPath}\\Transactions\\Failed");
                Directory.CreateDirectory($"{desktopPath}\\Transactions\\Completed");
                Directory.CreateDirectory($"{desktopPath}\\Transactions\\Pending");

            }
            config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ",",
            };
            this.filepath = $"{desktopPath}\\{filename}";

            if (!File.Exists(filepath))
            {
                using (var sw = new StreamWriter(this.filepath))
                using (var cw = new CsvWriter(sw, config))
                {
                    cw.WriteRecords(_data);
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

        public void CleanFile()
        {
            _data = new List<T>();
            UpdateData();
        }

        public int Count() => this._data.Count();
    }
}
