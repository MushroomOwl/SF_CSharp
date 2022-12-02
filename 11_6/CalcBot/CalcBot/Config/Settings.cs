using CalcBot.Models;
using CalcBot.Utilities;

namespace CalcBot.Config
{
    internal class Settings
    {
        // Config variables list
        public const string BotTokenVarName = "BOT_TOKEN";

        static private List<string> requiredVars = new List<string>() { BotTokenVarName };

        static private string configPath = "config.ini";

        public string? BotToken { get; set; }

        static public Settings InitFromFile(ILogger logger)
        {
            Settings settings = new Settings();
            Dictionary<string, string> vars = new Dictionary<string, string>();
            
            try
            {
                FileInfo configFile = new FileInfo(configPath);
                using (StreamReader sr = configFile.OpenText())
                {
                    string? row;
                    while ((row = sr.ReadLine()) != null)
                    {
                        string[] colls = row.Split('=');
                        if (colls.Length != 2)
                        {
                            throw new BadConfigFileFormat("Bad config file format. Expecting `VARIABLE=VALUE` format");
                        }

                        vars.Add(colls[0], colls[1]);
                    }
                }
            }
            catch (BadConfigFileFormat ex)
            {
                logger.Error(ex);
                return settings;
            }
            catch (Exception ex)
            {
                logger.Error(ex, string.Format("Cannot open config file. Please make sure you have {0} in folder with app and it's available for current user", configPath));
                return settings;
            }

            try
            {
                foreach (string variable in requiredVars) {
                    if (!vars.ContainsKey(variable)) {
                        throw new MissingRequiredValueException(string.Format("Missing `{0}` variable in config file", variable));
                    }
                }

                foreach (string key in vars.Keys)
                {
                    string value = vars[key];
                    switch (key) {
                        case BotTokenVarName:
                            settings.BotToken = value;
                            break;
                    }
                }
            } 
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            logger.Event("Config loaded successfully");

            return settings;
        }
    }
}
