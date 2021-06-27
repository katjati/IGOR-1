using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.Models
{
    /// <summary>
    /// Types of item commonnsess.
    /// </summary>
    public enum ItemCommonnessTypes
    {
        Unknown,
        Common,
        Rare,
        Unique
    }
    /// <summary>
    /// Types of items.
    /// </summary>
    public enum ItemTypes
    {
        Unknown,
        Custom,
        Powder,
        Syringe,
        Ammo,
        Raw,
    }
    /// <summary>
    /// Types of edition's participants.
    /// </summary>
    public enum ParticipationTypes
    {
        Unknown,
        Organizer,
        Volunteer,
        Participant
    }
    /// <summary>
    /// Types of volounteers.
    /// </summary>
    public enum VolunteerTypes
    {
        Other,
        Coordinator,
        Medic,
        Janusz,
        NPC,
        Artist,
        Infopoint,
        Bartender,
    }
    /// <summary>
    /// Types of faction profiles.
    /// </summary>
    public enum FactionProfileTypes
    {
        Unknown,
        Technological,
        Religious,
        Merchant,
        Militant,
        Motorized,
        Raider,
        Tribal
    }
    /// <summary>
    /// Types of character races.
    /// </summary>
    public enum RaceTypes
    {
        Unknown,
        Human,
        Mutant,
        Cyborg,
        Other
    }
    /// <summary>
    /// Types of character statuses.
    /// </summary>
    public enum CharacterStatusTypes
    {
        Unknown,
        Active,
        Missing,
        Dead
    }
    /// <summary>
    /// Types of sources of learning progress.
    /// </summary>
    public enum LearningTypeTypes
    {
        School,
        Mentoring,
        Library,
        Teaching,
        Specialization,
        Identification,
        Crafting,
        Merit,
        Init,
        Other
    }
    /// <summary>
    /// Types of curves.
    /// </summary>
    public enum CurveTypes
    {
        Linear,
        Sigmoid,
        Exponential,
        Normal,
    }
    /// <summary>
    /// Types f item schemas.
    /// </summary>
    public enum ItemSchemaTypes
    {
        Scraping,
        Crafting
    }
    /// <summary>
    /// Types of penalties.
    /// </summary>
    public enum SkillFailPenaltyTypes
    {
        Delay,
        Fail,
        Exhauston
    }
    /// <summary>
    /// Types of known languages.
    /// </summary>
    public enum LanguageTypes
    {
        Polish,
        English
    }
    /// <summary>
    /// Types of knowledge domains.
    /// </summary>
    public enum DomainTypes
    {
        Unknown,
        Technology,
        Chemistry,
        Biology,
        Medicine
    }
    /// <summary>
    /// Types of perks.
    /// </summary>
    public enum PerkTypes
    {
        Individual,
        CritialInjury
    }
    /// <summary>
    /// Types of difficulty levels.
    /// </summary>
    public enum DifficultyLevels
    {
        Unknown,
        Easy,
        Medium,
        Hard,
        Extreme
    }
    /// <summary>
    /// Types of item attainability.
    /// </summary>
    public enum AttainabilityTypes
    {
        Common,
        Basic,
        Secret
    }
    /// <summary>
    /// Types of progress display.
    /// </summary>
    public enum ProgressDisplayTypes
    {
        Raw,
        Hundreds,
        LevelsThree
    }
    /// <summary>
    /// Types of learning points display.
    /// </summary>
    public enum LearningPointsDisplayTypes
    {
        Raw,
        Hundreds
    }
}
