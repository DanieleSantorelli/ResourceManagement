using DevOpsInspector.Data.Models.AppBaseModels;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace DevOpsInspector
{
    public class Global
    {
        #region public fields
        public Configuration Configuration { get; set; }
        #endregion public fields

        #region private fields
        private static Global _Instance = null;
        private string CONFIG_FILE_NAME = "\\Configuration.json";
        #endregion private fields

        #region configuration
        public Global()
        {
            Configuration = GetConfiguration();
        }

        static internal Global Instance()
        {
            if (_Instance == null)
            {
                _Instance = new Global();
            }
            return _Instance;
        }
        #endregion configuration

        #region private methods
        private Configuration GetConfiguration()
        {
            try
            {
                var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var rootDirectory = Path.GetFullPath(Path.Combine(binDirectory, ".."));

                using FileStream openStream = File.OpenRead(rootDirectory + CONFIG_FILE_NAME);
                return JsonSerializer.DeserializeAsync<Configuration>(openStream).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion private methods
    }
}
