using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;

namespace Igor.Library.Models.Shared
{
    /// <summary>
    /// Global settings for Igor.
    /// </summary>
    public static class IgorSettings
    {
        #region Utils

        /// <summary>
        /// Folder where igor.config is located.
        /// </summary>
        public static readonly string IgorConfigFolderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        /// <summary>
        /// Igor config instance (set once).
        /// </summary>
        private static Configuration _igorConfiguration;
        /// <summary>
        /// Get Igor configuration
        /// </summary>
        public static Configuration IgorConfig
        {
            get
            {
                if (_igorConfiguration == null)
                {
                    string configPath = Path.Combine(IgorConfigFolderPath, "Igor.config");
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = configPath
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    _igorConfiguration = config;
                }

                return _igorConfiguration;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Name of current Igor DNS name.
        /// </summary>
        public static string CurrentIgorAddress
        {
            get
            {
                string defaultAddress = IgorConfig.AppSettings.Settings["IgorUrl"]?.Value;
                string address = defaultAddress;
                string serverNameFile = Path.Combine(IgorConfigFolderPath, "servername.txt");
                if (File.Exists(serverNameFile))
                {
                    address = File.ReadAllText(serverNameFile);
                }

                return address;
            }
        }
        /// <summary>
        /// Version of Igor.
        /// </summary>
        public static string IgorVersion => IgorConfig.AppSettings.Settings["IgorVersion"]?.Value;
        /// <summary>
        /// Url of Igor server.
        /// </summary>
        public static string IgorTargetUrl => IgorConfig.AppSettings.Settings["IgorUrl"]?.Value;
        /// <summary>
        /// Full path to temporary directory to store files downloaded from WebAPI to be returned by MVC.
        /// </summary>
        public static string IgorDirectory => IgorConfig.AppSettings.Settings["TempDir"]?.Value;
        /// <summary>
        /// Current edition of an event that defines the context of terminal use.
        /// </summary>
        public static string CurrentEditionId => IgorConfig.AppSettings.Settings["CurrentEdition"]?.Value;

        public static bool IsDebugMode => ((IgorConfig.AppSettings.Settings["Debug"]?.Value ?? "0") == "1");

        #endregion
    }

    public static class IgorProgressSettings
    {
        #region Utils

        /// <summary>
        /// Folder where igor.config is located.
        /// </summary>
        public static readonly string IgorConfigFolderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        /// <summary>
        /// Igor config instance (set once).
        /// </summary>
        private static Configuration _igorConfiguration;
        /// <summary>
        /// Get Igor configuration
        /// </summary>
        public static Configuration IgorConfig
        {
            get
            {
                if (_igorConfiguration == null)
                {
                    string configPath = Path.Combine(IgorConfigFolderPath, "Igor.config");
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = configPath
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    _igorConfiguration = config;
                }

                return _igorConfiguration;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Determines the amount of learning points that a teacher receives for a private lesson.
        /// </summary>
        /// <returns></returns>
        public static int TeachingPrivateLearningPoints => IgorConfig.AppSettings.Settings["TeachingPrivateLearningPoints"]?.Value.ToInt() ?? 0;
        /// <summary>
        /// Determines the amount of learning points a teacher receives for a school lesson.
        /// </summary>
        /// <returns></returns>
        public static int TeachingSchoolLearningPoints => IgorConfig.AppSettings.Settings["TeachingSchoolLearningPoints"]?.Value.ToInt() ?? 0;
        /// <summary>
        /// Determines a minimum value of proficiency in a crafting schema to be able to teach other characters.
        /// </summary>
        public static int TeachingSchemaLevel => IgorConfig.AppSettings.Settings["TeachingSchemaLevel"]?.Value.ToInt() ?? 0;


        #endregion

        #region Methods
        /// <summary>
        /// Returns an enum type representation of progress system used in display.
        /// </summary>
        public static ProgressDisplayTypes GetDisplayProgressType()
        {
            ProgressDisplayTypes type = ProgressDisplayTypes.Raw;
            if (Enum.TryParse(IgorConfig.AppSettings.Settings["ProgressDisplay"]?.Value, out type))
            {
                return type;
            }
            return ProgressDisplayTypes.Raw;
        }
        /// <summary>
        /// Type of progress system used in display.
        /// </summary>
        public static LearningPointsDisplayTypes GetDisplayLearningPointsType()
        {
            LearningPointsDisplayTypes type = LearningPointsDisplayTypes.Raw;
            if (Enum.TryParse(IgorConfig.AppSettings.Settings["LearningPointsDisplay"]?.Value, out type))
            {
                return type;
            }
            return LearningPointsDisplayTypes.Raw;
        }
        #endregion

        
    }

    public static class TotalProgressModifiers
    {
        #region Utils

        /// <summary>
        /// Folder where igor.config is located.
        /// </summary>
        public static readonly string IgorConfigFolderPath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        /// <summary>
        /// Igor config instance (set once).
        /// </summary>
        private static Configuration _igorConfiguration;
        /// <summary>
        /// Get Igor configuration
        /// </summary>
        public static Configuration IgorConfig
        {
            get
            {
                if (_igorConfiguration == null)
                {
                    string configPath = Path.Combine(IgorConfigFolderPath, "Igor.config");
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap()
                    {
                        ExeConfigFilename = configPath
                    };
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                    _igorConfiguration = config;
                }

                return _igorConfiguration;
            }
        }

        #endregion

        #region Properties
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type crafting.
        /// </summary>
        public static decimal Crafting => IgorConfig.AppSettings.Settings["TotalProgressModifierCrafting"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type identification.
        /// </summary>
        public static decimal Identification => IgorConfig.AppSettings.Settings["TotalProgressModifierIdentification"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type school.
        /// </summary>
        public static decimal School => IgorConfig.AppSettings.Settings["TotalProgressModifierSchool"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type teaching.
        /// </summary>
        public static decimal Teaching => IgorConfig.AppSettings.Settings["TotalProgressModifierTeaching"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type mentoring.
        /// </summary>
        public static decimal Mentoring => IgorConfig.AppSettings.Settings["TotalProgressModifierMentoring"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type specialization.
        /// </summary>
        public static decimal Specialization => IgorConfig.AppSettings.Settings["TotalProgressModifierSpecialization"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type library.
        /// </summary>
        public static decimal Library => IgorConfig.AppSettings.Settings["TotalProgressModifierLibrary"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type merit.
        /// </summary>
        public static decimal Merit => IgorConfig.AppSettings.Settings["TotalProgressModifierMerit"]?.Value.ToDecimal() ?? 0;
        /// <summary>
        /// Modifier used for calculating total progress value for a learning type init.
        /// </summary>
        public static decimal Init => IgorConfig.AppSettings.Settings["TotalProgressModifierInit"]?.Value.ToDecimal() ?? 0;
#endregion
    }
}
