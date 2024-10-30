using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadLog.Core
{
    public class AppConfig : ConfigurationSection
    {
        private readonly string _defaultConfigFilePath;
        public AppConfig()
        {
            try
            {
                _defaultConfigFilePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Stores/Data.json");
            }
            catch
            {
                _defaultConfigFilePath = "";
            }
        }

        [ConfigurationProperty("dataFile", DefaultValue = "")]
        public string DataFilePath
        {
            get { return string.IsNullOrEmpty((string)this["dataFile"]) ? _defaultConfigFilePath : (string)this["dataFile"]; }
            set { this["dataFile"] = value; }
        }
    }
}
