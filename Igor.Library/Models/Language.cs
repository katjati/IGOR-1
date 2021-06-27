using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// A class that represents a language.
    /// </summary>
    public class Language
    {
        #region Properties
        /// <summary>
        /// DB Id.
        /// </summary>
        [Key]
        public int LanguageId { get; set; }
        /// <summary>
        /// Language name.
        /// </summary>
        [Index("LanguageName")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        /// <summary>
        /// Abbreviation of language name.
        /// </summary>
        [Index("Language_Abb")]
        [Required]
        [MaxLength(10)]
        public string Abbreviation { get; set; }
        #endregion

        #region Constructors
        public Language() { }

        public Language(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
        #endregion
    }
}
