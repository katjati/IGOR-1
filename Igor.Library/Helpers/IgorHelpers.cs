using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Igor.Library.Global;
using Igor.Library.Models;
using Igor.Library.Models.Shared;

namespace Igor.Library.Helpers
{
    public static class IgorHelpers
    {
        /// <summary>
        /// Returns progress base item.
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static IProgressBase GetProgressBase(this LearningProgress progress)
        {
            switch (progress.Type)
            {
                case LearningTypeTypes.Specialization:
                    return progress.Specialization;
                case LearningTypeTypes.Crafting:
                    return progress.Schema;
                case LearningTypeTypes.Identification:
                    return progress.IdentifiedItem;
                case LearningTypeTypes.School:
                case LearningTypeTypes.Teaching:
                    return progress.Lesson;
            }
            return null;
        }
        /// <summary>
        /// Returns a domain name string.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetDomainNameString(this DomainTypes domain)
        {
            switch (domain)
            {
                case DomainTypes.Medicine:
                    return "Medicine";
                case DomainTypes.Technology:
                    return "Technology";
                case DomainTypes.Biology:
                    return "Biology";
                case DomainTypes.Chemistry:
                    return "Chemistry";
            }
            return "";
        }
        /// <summary>
        /// Returns an abbreviation of a knowledge domain.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static string GetDomainNameAbbreviationString(this DomainTypes domain)
        {
            switch (domain)
            {
                case DomainTypes.Medicine:
                    return "M";
                case DomainTypes.Technology:
                    return "T";
                case DomainTypes.Biology:
                    return "B";
                case DomainTypes.Chemistry:
                    return "C";
            }
            return "";
        }
        /// <summary>
        /// Returns a domain name.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static DomainTypes GetDomainNameString(this string domain)
        {
            switch (domain.ToLower())
            {
                case "medicine":
                    return DomainTypes.Medicine;
                case "technology":
                    return DomainTypes.Technology;
                case "biology":
                    return DomainTypes.Biology;
                case "chemistry":
                    return DomainTypes.Chemistry;
            }
            return DomainTypes.Unknown;
        }
        /// <summary>
        /// Returns a sum of all modifiers of a certain domain type for a character.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="type"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static decimal GetProgressBaseSum(this Character character, LearningTypeTypes type, DomainTypes domain)
        {
            return character.LearningProgresses.Where(f => f.Type == type && f.Domain == domain).Sum(s=>s.Modifier);
        }
        /// <summary>
        /// Determines whether character is a teacher during current edition.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsTeacher(this Character character)
        {
            if (character == null) return false;
            return character?.LessonsConducted?.Any(a => a.Edition.IsCurrentEdition()) ?? false;
        }
        /// <summary>
        /// Determines whether an edition is a current one.
        /// </summary>
        /// <param name="edition"></param>
        /// <returns></returns>
        public static bool IsCurrentEdition(this Edition edition)
        {
            if (edition == null) return false;
            return edition.EditionId == IgorSettings.CurrentEditionId.ToInt();
        }
        /// <summary>
        /// Determines whether a character knows a schema
        /// </summary>
        /// <param name="character"></param>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        public static bool KnowsSchema(this Character character, int schemaId)
        {
            if (character == null || schemaId < 1) return false;
            return character.KnownSchemas.Select(s=>s.ItemSchemaId).Contains(schemaId);
        }
        /// <summary>
        /// Determines whether a character is proficient enough in using schema to teach other characters.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        public static bool CanTeachSchema(this Character character, int schemaId)
        {
            ItemSchema schema = character?.KnownSchemas.FirstOrDefault(f => f.ItemSchemaId == schemaId);
            if (character == null || schema == null) return false;
            decimal progress = character.GetProgressInSchema(schemaId);
            return (progress > IgorProgressSettings.TeachingSchemaLevel);
        }
        /// <summary>
        /// Returns a total character's progress in using a schema.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        public static decimal GetProgressInSchema(this Character character, int schemaId)
        {
            if (character == null || schemaId < 1) return 0;
            return character.LearningProgresses.Where(w => w.Type == LearningTypeTypes.Crafting && w.Schema != null && w.Schema.ItemSchemaId == schemaId).Sum(s => s.Modifier);
        }
        /// <summary>
        /// Returns number of collected knowledge points of a character.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static AvailableKnowledgePoints GetAvailableKnowledgePoints(this Character character)
        {
            // TODO: introduce algorithm with weights and limits
            if (character == null) return null;
            AvailableKnowledgePoints result = new AvailableKnowledgePoints();
            foreach (DomainTypes type in new List<DomainTypes>() {DomainTypes.Medicine, DomainTypes.Technology, DomainTypes.Biology, DomainTypes.Chemistry})
            {
                result.DomainPoints.Add(new AvailableKnowledgePointsDomains(type, character.GetKnowledgePointsByType(type)));
            }
            return result;
        }
        /// <summary>
        /// Returns a list of character's progresses associated with a domain.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<LearningProgress> GetLearningProgressesByDomain(this Character character, DomainTypes domain, bool hasPoints)
        {
            if (character == null) return new List<LearningProgress>();
            if(hasPoints)
                return character.LearningProgresses.Where(w => w.Domain == domain && w.HasActiveLearningPoints && w.ActiveLearningPoints >= 1).ToList();
            return character.LearningProgresses.Where(w => w.Domain == domain).ToList();
        }
        /// <summary>
        /// Provides a string representation of progress based on a model chosen in configuration.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetProgressRepresentation(this decimal value)
        {
            ProgressDisplayTypes type = IgorProgressSettings.GetDisplayProgressType();
            switch (type)
            {
                case ProgressDisplayTypes.Hundreds:
                    return Convert.ToInt16(value * 100).ToString();
                case ProgressDisplayTypes.LevelsThree:
                    if (value < 6) return "0";
                    if (value < 15) return "1";
                    if (value < 30) return "2";
                    return "3";
                default:
                    return value.ToString();
            }
        }
        /// <summary>
        /// Returns a representation of a schema progress displayed in views.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetSchemasRepresentation(this decimal value)
        {
            ProgressDisplayTypes type = IgorProgressSettings.GetDisplayProgressType();
            switch (type)
            {
                case ProgressDisplayTypes.Hundreds:
                    return Convert.ToInt16(value * 100).ToString();
                case ProgressDisplayTypes.LevelsThree:
                    if (value < 7) return "1";
                    if (value < 14) return "2";
                    return "3";
                default:
                    return value.ToString();
            }
        }
        /// <summary>
        /// Returns a representation of learning points displayed in views.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetLearningPointsRepresentation(this decimal value)
        {
            ProgressDisplayTypes type = IgorProgressSettings.GetDisplayProgressType();
            switch (type)
            {
                case ProgressDisplayTypes.Hundreds:
                    return Convert.ToInt16(value * 100).ToString();
                default:
                    return value.ToString();
            }
        }
        /// <summary>
        /// Returns a string for a learning type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetLearningTypeString(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                    return "Crafting";
                case LearningTypeTypes.Identification:
                    return "Identification";
                case LearningTypeTypes.Library:
                    return "Books";
                case LearningTypeTypes.Mentoring:
                    return "Private lessons";
                case LearningTypeTypes.Teaching:
                    return "Tutoring";
                case LearningTypeTypes.School:
                    return "School lessons";
                case LearningTypeTypes.Specialization:
                    return "Specialized knowledge";
                case LearningTypeTypes.Merit:
                    return "Other";
            }
            return "";
        }

        /// <summary>
        /// Determines whether partial progress can be known to user.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsKnownToUser(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                case LearningTypeTypes.Identification:
                    return false;
                case LearningTypeTypes.Library:
                case LearningTypeTypes.Mentoring:
                case LearningTypeTypes.Teaching:
                case LearningTypeTypes.School:
                    return true;
                case LearningTypeTypes.Specialization:
                    return false;
                case LearningTypeTypes.Merit:
                    return false;
            }
            return false;
        }

        /// <summary>
        /// Returns a modifier for a learning type used to calculate a total learning progress.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static decimal GetProgressModifier(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                    return TotalProgressModifiers.Crafting;
                case LearningTypeTypes.Identification:
                    return TotalProgressModifiers.Identification;
                case LearningTypeTypes.Library:
                    return TotalProgressModifiers.Library;
                case LearningTypeTypes.Mentoring:
                    return TotalProgressModifiers.Mentoring;
                case LearningTypeTypes.Teaching:
                    return TotalProgressModifiers.Teaching;
                case LearningTypeTypes.School:
                    return TotalProgressModifiers.School;
                case LearningTypeTypes.Specialization:
                    return TotalProgressModifiers.Specialization;
                case LearningTypeTypes.Merit:
                    return TotalProgressModifiers.Mentoring;
            }
            return 0;
        }

        /// <summary>
        /// Returns a modifier for a learning type used to calculate available learning points.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static decimal GetLearningPointsModifier(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                    return 0.1m;
                case LearningTypeTypes.Identification:
                    return 0.1m;
                case LearningTypeTypes.Library:
                    return 1m;
                case LearningTypeTypes.Mentoring:
                    return 2;
                case LearningTypeTypes.Teaching:
                    return 3;
                case LearningTypeTypes.School:
                    return 1;
                case LearningTypeTypes.Merit:
                    return 5;
            }
            return 0;
        }

        /// <summary>
        /// Returns a maximal value of progress achievable with a given method.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetMaxProgressValue(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                    return 30;
                case LearningTypeTypes.Identification:
                    return 30;
                case LearningTypeTypes.Library:
                    return 5;
                case LearningTypeTypes.Mentoring:
                    return 20;
                case LearningTypeTypes.Teaching:
                    return 30;
                case LearningTypeTypes.School:
                    return 10;
                case LearningTypeTypes.Specialization:
                    return 30;
                case LearningTypeTypes.Merit:
                    return 100;
            }
            return 0;
        }

        /// <summary>
        /// Returns a maximal value of learning type points that can be spent at once.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int GetMaxLearningPointsExchanger(this LearningTypeTypes type)
        {
            switch (type)
            {
                case LearningTypeTypes.Crafting:
                    return 5;
                case LearningTypeTypes.Identification:
                    return 5;
                case LearningTypeTypes.Library:
                    return 2;
                case LearningTypeTypes.Mentoring:
                    return 20;
                case LearningTypeTypes.Teaching:
                    return 20;
                case LearningTypeTypes.School:
                    return 20;
                case LearningTypeTypes.Merit:
                    return 20;
            }
            return 0;
        }
    }
}
