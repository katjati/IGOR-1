using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Igor.Library.Migrations
{
    using Igor.Library.DataAccess;
    using Igor.Library.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LarpContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.EntityFramework.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(LarpContext context)
        {
            base.Seed(context);

            #region Roles

            var roleStore = new RoleStore<IgorRole>(context);
            var roleManager = new RoleManager<IgorRole>(roleStore);
            var roles = new List<IgorRole>();

            roles.Add(new IgorRole
            {
                Name = RoleConstants.SuperAdminRole,
                ShortName = "SuperAdmin"
            });
            roles.Add(new IgorRole
            {
                Name = RoleConstants.AdminRole,
                ShortName = "Admin"
            });
            roles.Add(new IgorRole
            {
                Name = RoleConstants.HeadmasterRole,
                ShortName = "Hm"
            });
            foreach (IgorRole role in roles) roleManager.Create(role);

            #endregion

            #region Users

            var userStore = new UserStore<IgorUser>(context);
            var userManager = new IgorUserManager(userStore);

            IgorUser janeUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Jane",
                LastName = "Doe",
                UserName = "jane"
            };
            IgorUser kasUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Kasia",
                LastName = "Polanska",
                UserName = "kas"
            };
            IgorUser wizzUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Grzegorz",
                LastName = "Kowalski",
                UserName = "wizzl",
                IsForeigner = false
            };
            IgorUser duchUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Marcin",
                LastName = "Darko",
                UserName = "duch",
                IsForeigner = false
            }; 
            IgorUser jacobiUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Jakub",
                LastName = "Teller",
                UserName = "jacobi",
                IsForeigner = false
            };
            IgorUser tarotUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Tarot",
                LastName = "Beth",
                UserName = "tarot",
                IsForeigner = true
            };
            IgorUser kunaUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Pawel",
                LastName = "Turrell",
                UserName = "kuna",
                IsForeigner = false
            };
            IgorUser fatumUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Magdalena",
                LastName = "Kot",
                UserName = "fatum",
                IsForeigner = false
            };
            IgorUser wruUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Jakub",
                LastName = "Foxglow",
                UserName = "wrubel",
                IsForeigner = false
            };
            IgorUser gibbsUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Marek",
                LastName = "Foxglow",
                UserName = "gibbs",
                IsForeigner = false
            };
            IgorUser radzuUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Marcin",
                LastName = "Foxglow",
                UserName = "radzu",
                IsForeigner = false
            };
            IgorUser krzesioUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Krzesimir",
                LastName = "Foxglow",
                UserName = "krzes",
                IsForeigner = false
            };
            IgorUser dzxUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Sylwia",
                LastName = "Foxglow",
                UserName = "dzx",
                IsForeigner = false
            };
            IgorUser sandaczUser = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Łukasz",
                LastName = "Foxglow",
                UserName = "sandacz",
                IsForeigner = false
            };
            IgorUser zarowa = new IgorUser()
            {
                Email = "participant1@test.com",
                EmailConfirmed = true,
                FirstName = "Kaja",
                LastName = "Foxglow",
                UserName = "zarowa",
                IsForeigner = false
            };

            var users = new List<IgorUser>();
            users.AddRange(new List<IgorUser>() { wizzUser, duchUser, tarotUser, radzuUser, krzesioUser, dzxUser, kunaUser, fatumUser, wruUser, sandaczUser, gibbsUser, janeUser, kasUser, zarowa });
            foreach (IgorUser user in users)
            {
                userManager.Create(user, user.UserName);
            }
            //context.SaveChanges();

            userManager.AddToRole(kasUser.Id, RoleConstants.HeadmasterRole);
            userManager.AddToRole(janeUser.Id, RoleConstants.HeadmasterRole);
            userManager.AddToRole(kasUser.Id, RoleConstants.AdminRole);
            context.SaveChanges();

            #endregion

            #region Items


            Item blood = new Item()
            {
                Name = "Blood",
                Description = "A portion of pure human blood.",
                Mechanics = "Can be obtained by blood donation in hospitals with necessary equipment. A character must be sober to donate blood. A character is considered wounded for an hour after donating blood. Second donation within 24 hours results in a critical injury.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item bones = new Item()
            {
                Name = "Bones",
                Description = "A portion of bones Are these animal bones?",
                Mechanics = "",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item ivy = new Item()
            {
                Name = "Ivy",
                Description = "A single branch of ivy.",
                Mechanics = "",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item electronics = new Item()
            {
                Name = "Electronics",
                Description = "A piece of electronics.",
                Mechanics = ".",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item scrap = new Item()
            {
                Name = "Scrap",
                Description = "A nice piece of scrap. Might be useful for repairs or crafts.",
                Mechanics = ".",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item saltpeter = new Item()
            {
                Name = "Saltpeter",
                Description = "A piece of saltpeter. Careful with this.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Raw,
            };
            Item greenPowder = new Item()
            {
                Name = "Green powder",
                Description = "A portion of green powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item whitePowder = new Item()
            {
                Name = "White powder",
                Description = "A portion of white powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item redPowder = new Item()
            {
                Name = "Red powder",
                Description = "A portion of red powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item yellowPowder = new Item()
            {
                Name = "Yellow powder",
                Description = "A portion of yellow powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item brownPowder = new Item()
            {
                Name = "Brown powder",
                Description = "A portion of brown powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item blackPowder = new Item()
            {
                Name = "Black powder",
                Description = "A portion of black powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item bluePowder = new Item()
            {
                Name = "Blue powder",
                Description = "A portion of blue powder.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Powder,
            };
            Item stimpak = new Item()
            {
                Name = "Stimpak",
                Description = "A substance that works miracles on any wounds.",
                Mechanics = "Instantly heals any wounds for human and mutant characters. Does not apply to critical injnury.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item superStimpak = new Item()
            {
                Name = "Superstimpak",
                Description = "A substance that puts any cyborg back on his legs.",
                Mechanics = "Instantly heals any wounds for cyborg characters. Does not apply to critical injury.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item antibiotic = new Item()
            {
                Name = "Antibiotic",
                Description = "A substance that prevents infections. It's not a stimpak, You need to see a doctor first.",
                Mechanics = "Perscribed by doctors as a result of treatment. Should be used according to doctor's instructions.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item narcotic = new Item()
            {
                Name = "Narcotic",
                Description = "A highly addictive psychoactive substance that should not be recklessly used.",
                Mechanics = "Causes effects according to an instruction. Exposure to a substance twice within 2h causes addiction. A character must take a dose once a day or suffers a critical injury. Can be treated by a specialist.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item hypno = new Item()
            {
                Name = "Hypno",
                Description = "A psychoactive substance that renders a character open to suggestion.",
                Mechanics = "A character is open to a single suggestion that does not directly endanger his or her life. Effect wears off within 1 hour. Exposure to a substance twice within 2h causes addiction. A character must take a dose once a day or suffers a critical injury. Can be treated by a specialist.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item serum = new Item()
            {
                Name = "Serum of truth",
                Description = "A psychoactive substance that causes a character to tell nothing but truth.",
                Mechanics = "A character must answer truthfully either to 3 questions or for 3 minutes from injection.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item radaway = new Item()
            {
                Name = "Radaway",
                Description = "A substance that removes radioactive particles from the system.",
                Mechanics = "Completely removes active radiation effect from a character.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item radX = new Item()
            {
                Name = "RadX",
                Description = "A substance that protects a character from exposure to radiation.",
                Mechanics = "Reduces radiation tolerance for a character by one level for 10 minutes. RadX effects do not accumulate.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item poison = new Item()
            {
                Name = "Poison",
                Description = "A substance that poisons a character.",
                Mechanics = "A character is poisoned and if not cured from the effect within 3 hours suffers critical injury.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item antidote = new Item()
            {
                Name = "Antidote",
                Description = "A substance that eradicates any poison from the body of a character.",
                Mechanics = "Completely removes active poisoning effect from a character.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };
            Item sleeper = new Item()
            {
                Name = "Sleeper",
                Description = "A substance that causes a character to fall asleep.",
                Mechanics = "A character falls asleep instantly and remains in this state for an hour.",
                Commonness = ItemCommonnessTypes.Common,
                HasRfid = false,
                IsActive = true,
                Type = ItemTypes.Syringe,
            };

            context.Items.AddRange(new List<Item> { blood, ivy, bones, scrap, saltpeter, electronics, greenPowder, whitePowder, redPowder, yellowPowder, greenPowder, blackPowder, brownPowder, bluePowder,
                stimpak, antibiotic, superStimpak, sleeper, radX, radaway, hypno, serum, narcotic, poison, antidote });

            context.SaveChanges();


            #endregion

            #region Languages

            Language pl = new Language("polish", "pl");
            Language en = new Language("english", "en");
            context.Languages.AddRange(new List<Language> { pl, en });

            context.SaveChanges();

            #endregion

            #region ItemSchemas

            
            ItemSchema swhite = new ItemSchema()
            {
                InputItems = new List<Item> {blood},
                OutputItems = new List<Item> {whitePowder},
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 0, 0, 1, 0)}
            };
            ItemSchema swhite2 = new ItemSchema()
            {
                InputItems = new List<Item> { bones },
                OutputItems = new List<Item> { whitePowder },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 0, 0, 1, 0)}
            };
            ItemSchema sgreen = new ItemSchema()
            {
                InputItems = new List<Item> {ivy},
                OutputItems = new List<Item> { greenPowder },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 1, 0, 0, 0)}
            };
            ItemSchema sred = new ItemSchema()
            {
                InputItems = new List<Item> {blood},
                OutputItems = new List<Item> { redPowder },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 0, 0, 1, 0)}
            };
            ItemSchema syellow = new ItemSchema()
            {
                InputItems = new List<Item> { electronics },
                OutputItems = new List<Item> { yellowPowder },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 0,0,0,1)}
            };
            ItemSchema sbrown = new ItemSchema()
            {
                InputItems = new List<Item> { saltpeter },
                OutputItems = new List<Item> { brownPowder },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5, 0,1,0,0)}
            };
            ItemSchema sSuperstimpak = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, redPowder, yellowPowder, whitePowder, greenPowder },
                OutputItems = new List<Item> { superStimpak },
                SchemaType = ItemSchemaTypes.Crafting,
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(5,0,0,0,5)}
            };
            ItemSchema sstimpak = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, redPowder, greenPowder, redPowder, whitePowder },
                OutputItems = new List<Item> { stimpak },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,0,1,1,1)}
            };
            ItemSchema santibiotic = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, greenPowder, whitePowder },
                OutputItems = new List<Item> { antibiotic },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,0,1,0)}
            };
            ItemSchema santidote = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, blackPowder, whitePowder },
                OutputItems = new List<Item> { antidote },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,1,0,0)}
            };
            ItemSchema spoison = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, blackPowder, greenPowder, brownPowder },
                OutputItems = new List<Item> { poison },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,1,0,0)}
            };
            ItemSchema shypno = new ItemSchema()
            {
                InputItems = new List<Item> { whitePowder, bluePowder, redPowder, brownPowder, bluePowder },
                OutputItems = new List<Item> { hypno },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(8,1,1,1,0)}
            };
            ItemSchema snarcotic = new ItemSchema()
            {
                InputItems = new List<Item> { yellowPowder, whitePowder, bluePowder, brownPowder, blackPowder },
                OutputItems = new List<Item> { narcotic },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,1,0,0)}
            };
            ItemSchema sradX = new ItemSchema()
            {
                InputItems = new List<Item> { yellowPowder, brownPowder, blackPowder, yellowPowder, greenPowder },
                OutputItems = new List<Item> { radX },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,0,1,0,1)}
            };
            ItemSchema sradaway = new ItemSchema()
            {
                InputItems = new List<Item> { yellowPowder, greenPowder, redPowder, brownPowder, blackPowder },
                OutputItems = new List<Item> { radaway },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,0,1,1,0)}
            };
            ItemSchema ssleeper = new ItemSchema()
            {
                InputItems = new List<Item> { greenPowder, brownPowder, whitePowder, brownPowder, greenPowder },
                OutputItems = new List<Item> { sleeper },
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,1,0,0)}
            };
            ItemSchema sserum = new ItemSchema()
            {
                InputItems = new List<Item> { yellowPowder, greenPowder, redPowder, bluePowder, blackPowder },
                OutputItems = new List<Item> {serum},
                Attainability = AttainabilityTypes.Basic,
                LearnConditions = new List<ItemSchemaLearningCondition>(){ new ItemSchemaLearningCondition(6,1,1,1,0)}
            };
            context.ItemSchemas.AddRange(new List<ItemSchema>(){sserum, sSuperstimpak, sstimpak,shypno, snarcotic, sserum, ssleeper, santibiotic, santidote, spoison, sradX, sradaway, swhite, swhite2, sred, sgreen, syellow, sbrown});
            context.SaveChanges();

            #endregion

            #region Conglomerates

            Conglomerate zr = new Conglomerate()
            {
                Name = "Roundabout Union",
                Abbreviation = "ZR",
            };
            Conglomerate cz = new Conglomerate()
            {
                Name = "Czeska",
                Abbreviation = "CZ",
            };
            Conglomerate fh = new Conglomerate()
            {
                Name = "Trade Federation",
                Abbreviation = "FH",
            };
            Conglomerate gm = new Conglomerate()
            {
                Name = "Upper City",
                Abbreviation = "GM",
            };
            Conglomerate d = new Conglomerate()
            {
                Name = "Hole",
                Abbreviation = "D",
            };
            context.Conglomerates.AddRange(new List<Conglomerate>() { zr, cz, fh, gm, d });
            context.SaveChanges();
            #endregion


            #region Factions

            Faction shperacze = new Faction()
            {
                Name = "Shperacze",
                Conglomerate = zr,
                Motto = "Wasze jutro to nasze wczoraj",
            };
            Faction gryfy = new Faction()
            {
                Name = "Gryfy",
                Conglomerate = zr,
                Motto = "Pancerz!",
            };
            Faction alkochemicy = new Faction()
            {
                Name = "Alkochemicy",
                Conglomerate = zr,
                Motto = "Karówki nigdy za mało",
            };
            Faction bob = new Faction()
            {
                Name = "Brotherhood of Beer",
                Conglomerate = zr,
                Motto = "Floow the beer",
            };
            Faction wataha = new Faction()
            {
                Name = "Wataha",
                Conglomerate = zr,
                Motto = "Auuu!",
            };
            Faction szarancza = new Faction()
            {
                Name = "Szarancza",
                Conglomerate = zr,
                Motto = "Rój!",
            };
            Faction outcast = new Faction()
            {
                Name = "Outcast",
                Conglomerate = zr,
                Motto = "Together info future",
            };
            Faction zakon = new Faction()
            {
                Name = "Zakon Świętego Płomienia",
                Conglomerate = cz,
                Motto = "Chwała Świętemu Płomieniowi!",
            };
            Faction azyl = new Faction()
            {
                Name = "Azyl",
                Conglomerate = cz,
                Motto = "Wyskakuj z kapselków",
            };
            Faction radratz = new Faction()
            {
                Name = "RadRatz",
                Conglomerate = cz,
                Motto = "Where's your bike",
            };
            Faction kosmodrom = new Faction()
            {
                Name = "Kosmodrom",
                Conglomerate = gm,
                Motto = "I'll make it work",
            };
            Faction vultures = new Faction()
            {
                Name = "Vultures",
                Conglomerate = gm,
                Motto = "Roll the wheel",
            };
            context.Factions.AddRange(new List<Faction>() { shperacze, gryfy, zakon, alkochemicy, wataha, vultures, azyl, radratz, kosmodrom, szarancza, outcast });
            context.SaveChanges();

            #endregion

            #region Characters

            Character kas = new Character()
            {
                Faction = shperacze,
                Name = "Kas",
                IsNpc = false,
                Player = kasUser,
                Status = CharacterStatusTypes.Active,
            };
            Character sudo = new Character()
            {
                Faction = shperacze,
                Name = "Sudo",
                IsNpc = true,
                Player = kasUser,
                Status = CharacterStatusTypes.Missing
            };
            Character jacobi = new Character()
            {
                Faction = zakon,
                Name = "Jacobi",
                IsNpc = false,
                Player = jacobiUser,
                Status = CharacterStatusTypes.Active
            };
            Character wizzl = new Character()
            {
                Faction = alkochemicy,
                Name = "Wizzl",
                IsNpc = false,
                Player = wizzUser,
                Status = CharacterStatusTypes.Active
            };
            Character wizzl2 = new Character()
            {
                Faction = alkochemicy,
                Name = "Von Waffel",
                IsNpc = true,
                Player = wizzUser,
                Status = CharacterStatusTypes.Active
            };
            Character tarot = new Character()
            {
                Faction = alkochemicy,
                Name = "Tarot",
                IsNpc = false,
                Player = tarotUser,
                Status = CharacterStatusTypes.Active
            };
            Character duch = new Character()
            {
                Faction = alkochemicy,
                Name = "Duch",
                IsNpc = false,
                Player = duchUser,
                Status = CharacterStatusTypes.Active
            };
            Character kuna = new Character()
            {
                Faction = wataha,
                Name = "Kuna",
                IsNpc = false,
                Player = kunaUser,
                Status = CharacterStatusTypes.Active
            };
            Character fatum = new Character()
            {
                Faction = wataha,
                Name = "Fatum",
                IsNpc = true,
                Player = fatumUser,
                Status = CharacterStatusTypes.Active
            };
            Character wru = new Character()
            {
                Faction = gryfy,
                Name = "Wrubel",
                IsNpc = false,
                Player = wruUser,
                Status = CharacterStatusTypes.Active
            };
            Character gibbs = new Character()
            {
                Faction = outcast,
                Name = "Gibbs",
                IsNpc = false,
                Player = gibbsUser,
                Status = CharacterStatusTypes.Active
            };
            Character zarowka = new Character()
            {
                Faction = szarancza,
                Name = "Zarowa",
                IsNpc = false,
                Player = gibbsUser,
                Status = CharacterStatusTypes.Active
            };
            Character sandacz = new Character()
            {
                Faction = kosmodrom,
                Name = "Sandacz",
                IsNpc = false,
                Player = sandaczUser,
                Status = CharacterStatusTypes.Active
            };
            Character radzu = new Character()
            {
                Faction = shperacze,
                Name = "Radzu",
                IsNpc = false,
                Player = radzuUser,
                Status = CharacterStatusTypes.Active
            };
            Character radeon = new Character()
            {
                Faction = shperacze,
                Name = "Radeon",
                IsNpc = false,
                Player = radzuUser,
                Status = CharacterStatusTypes.Active
            };
            Character osemka = new Character()
            {
                Faction = shperacze,
                Name = "Ósemka",
                IsNpc = true,
                Player = radzuUser,
                Status = CharacterStatusTypes.Active
            };
            Character krzes = new Character()
            {
                Faction = shperacze,
                Name = "Krzesio",
                IsNpc = false,
                Player = krzesioUser,
                Status = CharacterStatusTypes.Active
            };
            Character dziks = new Character()
            {
                Faction = shperacze,
                Name = "Dzx",
                IsNpc = false,
                Player = dzxUser,
                Status = CharacterStatusTypes.Active
            };

            context.Characters.AddRange(new List<Character>() { kas, sudo, radzu, radeon, osemka, dziks, krzes, wizzl, wizzl2, wru, fatum, kuna, gibbs, zarowka, sandacz, jacobi, duch, tarot });
            context.SaveChanges();

            #endregion


            #region Events

            Event ot = new Event()
            {
                Name = "OldTown",
                MainOrganizer = janeUser
            };
            Event lyzkon = new Event()
            {
                Name = "Lyzkon",
                MainOrganizer = wizzUser
            };
            context.Events.AddRange(new List<Event>() { ot, lyzkon });
            context.SaveChanges();

            #endregion


            #region Editions

            Edition ot18 = new Edition()
            {
                Event = ot,
                DateStart = Convert.ToDateTime("06/07/2018"),
                DateEnd = Convert.ToDateTime("09/07/2018"),
                IsArchived = true
            };

            Edition ot19 = new Edition()
            {
                Event = ot,
                DateStart = Convert.ToDateTime("06/07/2019"),
                DateEnd = Convert.ToDateTime("09/07/2019"),
                IsArchived = true
            };
            Edition ot21 = new Edition()
            {
                Event = ot,
                DateStart = Convert.ToDateTime("06/07/2021"),
                DateEnd = Convert.ToDateTime("09/07/2021"),
            };
            context.Editions.AddRange(new List<Edition>() { ot18, ot19, ot21 });
            context.SaveChanges();

            #endregion

            #region Conglomerate coordinators

            ConglomerateCoordination zrc = new ConglomerateCoordination()
            {
                Conglomerate = zr,
                ConglomerateCoordinator = wizzUser,
                Edition = ot21
            };
            ConglomerateCoordination czc = new ConglomerateCoordination()
            {
                Conglomerate = cz,
                ConglomerateCoordinator = jacobiUser,
                Edition = ot21
            };
            ConglomerateCoordination fhc = new ConglomerateCoordination()
            {
                Conglomerate = fh,
                ConglomerateCoordinator = gibbsUser,
                Edition = ot21
            };
            ConglomerateCoordination gmc = new ConglomerateCoordination()
            {
                Conglomerate = gm,
                ConglomerateCoordinator = sandaczUser,
                Edition = ot21
            };
            ConglomerateCoordination dc = new ConglomerateCoordination()
            {
                Conglomerate = d,
                ConglomerateCoordinator = tarotUser,
                Edition = ot21
            };
            context.ConglomerateCoordinators.AddRange(new List<ConglomerateCoordination>() { zrc, czc, fhc, gmc, dc });
            context.SaveChanges();
            #endregion

            #region Accreditations

            context.Accreditations.AddRange(new List<Accreditation>()
            {
                new Accreditation()
                {
                Edition = ot21,
                IgorUser = kasUser,
                Price = 269,
                Type = ParticipationTypes.Participant,
                Registered = Convert.ToDateTime("06/05/2020"),
                IsActive = true,
                ActiveCharacter = kas,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = wizzUser,
                    Price = 0,
                    Type = ParticipationTypes.Volunteer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = wizzl,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = tarotUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = tarot,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = duchUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = duch,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = radzuUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = radzu,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = jacobiUser,
                    Price = 269,
                    Type = ParticipationTypes.Volunteer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = jacobi,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = janeUser,
                    Price = 0,
                    Type = ParticipationTypes.Organizer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = kas,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = gibbsUser,
                    Price = 0,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = gibbs,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = krzesioUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = krzes,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = dzxUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = dziks,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = kunaUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = kuna,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = fatumUser,
                    Price = 0,
                    Type = ParticipationTypes.Organizer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = fatum,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = wruUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = wru,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = sandaczUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = sandacz,
                },
                new Accreditation()
                {
                    Edition = ot21,
                    IgorUser = zarowa,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = zarowka,
                },

                new Accreditation()
                {
                Edition = ot19,
                IgorUser = kasUser,
                Price = 269,
                Type = ParticipationTypes.Participant,
                Registered = Convert.ToDateTime("06/05/2020"),
                IsActive = true,
                ActiveCharacter = kas,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = wizzUser,
                    Price = 0,
                    Type = ParticipationTypes.Volunteer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = wizzl,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = tarotUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = tarot,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = duchUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = duch,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = radzuUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = radzu,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = jacobiUser,
                    Price = 269,
                    Type = ParticipationTypes.Volunteer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = jacobi,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = janeUser,
                    Price = 0,
                    Type = ParticipationTypes.Organizer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = kas,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = gibbsUser,
                    Price = 0,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = gibbs,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = krzesioUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = krzes,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = dzxUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = dziks,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = kunaUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = true,
                    ActiveCharacter = kuna,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = fatumUser,
                    Price = 0,
                    Type = ParticipationTypes.Organizer,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = false,
                    ActiveCharacter = fatum,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = wruUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = false,
                    ActiveCharacter = wru,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = sandaczUser,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = false,
                    ActiveCharacter = sandacz,
                },
                new Accreditation()
                {
                    Edition = ot19,
                    IgorUser = zarowa,
                    Price = 269,
                    Type = ParticipationTypes.Participant,
                    Registered = Convert.ToDateTime("06/05/2020"),
                    IsActive = false,
                    ActiveCharacter = zarowka,
                },

        });
            context.SaveChanges();


            #endregion

            #region Coordination

            context.FactionCoordinators.AddRange(new List<FactionCoordination>()
            {
                new FactionCoordination()
                {
                    FactionCoordinator = kasUser,
                    Edition = ot21,
                    Faction = shperacze,
                    IsMainCoordinator = true
                },
                new FactionCoordination()
                {
                    FactionCoordinator = radzuUser,
                    Edition = ot21,
                    Faction = shperacze,
                    IsMainCoordinator = false
                },
                new FactionCoordination()
                {
                    FactionCoordinator = kunaUser,
                    Edition = ot21,
                    Faction = wataha,
                    IsMainCoordinator = true
                },
                new FactionCoordination()
                {
                    FactionCoordinator = wizzUser,
                    Edition = ot21,
                    Faction = alkochemicy,
                    IsMainCoordinator = false
                },
            });
            context.SaveChanges();

            #endregion


            #region Lessons

            Lesson les1 = new Lesson()
            {
                Edition = ot19,
                Domain = DomainTypes.Medicine,
                Language = LanguageTypes.Polish,
                Teacher = kas,
                Topic = "Medycyna pola walki",
                StartTime = Convert.ToDateTime("07 Jul 2019 2:00:00 PM"),
                EndTime = Convert.ToDateTime("07 Jul 2019 3:00:00 PM"),
                Comment = "Conducted at arena."
            };
            Lesson les2 = new Lesson()
            {
                Edition = ot19,
                Domain = DomainTypes.Technology,
                Language = LanguageTypes.English,
                Teacher = krzes,
                Topic = "Wireless connectivity",
                StartTime = Convert.ToDateTime("08 Jul 2019 3:00:00 PM"),
                EndTime = Convert.ToDateTime("08 Jul 2019 4:00:00 PM"),
                AdditionalInformation = "Conducted at arena.",
            };
            Lesson les3 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Medicine,
                Language = LanguageTypes.Polish,
                Teacher = kas,
                Topic = "Medycyna pola walki",
                StartTime = Convert.ToDateTime("09 Jul 2021 2:00:00 PM"),
                EndTime = Convert.ToDateTime("09 Jul 2021 3:00:00 PM"),
                AdditionalInformation = "Conducted at arena."
            };
            Lesson les9 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Medicine,
                Language = LanguageTypes.Polish,
                Teacher = kas,
                Topic = "Medycyna pola walki",
                StartTime = Convert.ToDateTime("07 Jul 2021 2:00:00 PM"),
                EndTime = Convert.ToDateTime("07 Jul 2021 3:00:00 PM"),
                AdditionalInformation = "Conducted at arena."
            };
            Lesson les12 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Chemistry,
                Language = LanguageTypes.Polish,
                Teacher = sandacz,
                Topic = "Post-war lab equipment",
                StartTime = Convert.ToDateTime("07 Jul 2021 4:00:00 PM"),
                EndTime = Convert.ToDateTime("07 Jul 2021 5:00:00 PM"),
            };
            Lesson les4 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Biology,
                Language = LanguageTypes.English,
                Teacher = jacobi,
                Topic = "Heretic plants",
                StartTime = Convert.ToDateTime("08 Jul 2021 2:00:00 PM"),
                EndTime = Convert.ToDateTime("08 Jul 2021 3:00:00 PM"),
            };
            Lesson les5 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Chemistry,
                Language = LanguageTypes.English,
                Teacher = wizzl,
                Topic = "Rad-X",
                StartTime = Convert.ToDateTime("08 Jul 2021 3:00:00 PM"),
                EndTime = Convert.ToDateTime("08 Jul 2021 4:00:00 PM"),
            };
            Lesson les6 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Technology,
                Language = LanguageTypes.Polish,
                Teacher = kas,
                Topic = "Introduction to cybernetics",
                StartTime = Convert.ToDateTime("08 Jul 2021 4:00:00 PM"),
                EndTime = Convert.ToDateTime("08 Jul 2021 5:00:00 PM"),
                AdditionalInformation = "Class conducted in lab."
            };
            Lesson les10 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Technology,
                Language = LanguageTypes.Polish,
                Teacher = krzes,
                Topic = "Jan Pawel Bocian XII",
                StartTime = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                EndTime = Convert.ToDateTime("08 Jul 2021 8:00:00 PM"),
                AdditionalInformation = "Class conducted in lab."
            };
            Lesson les11 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Technology,
                Language = LanguageTypes.Polish,
                Teacher = kas,
                Topic = "Introduction to cybernetics",
                StartTime = Convert.ToDateTime("06 Jul 2021 2:00:00 PM"),
                EndTime = Convert.ToDateTime("06 Jul 2021 3:30:00 PM"),
                AdditionalInformation = "Class conducted in lab."
            };
            Lesson les7 = new Lesson()
            {
                Edition = ot21,
                Domain = DomainTypes.Biology,
                Language = LanguageTypes.Polish,
                Teacher = dziks,
                Topic = "Poisonous plans",
                StartTime = Convert.ToDateTime("06 Jul 2021 11:00:00 AM"),
                EndTime = Convert.ToDateTime("06 Jul 2021 1:00:00 PM"),
            };

            context.Lessons.AddRange(new List<Lesson>() { les1, les2, les3, les4, les5, les6, les7, les9, les9, les12, les11, les10 });
            context.SaveChanges();


            #endregion


            #region Specializations

            Specialization cybernetics = new Specialization("cybernetics");
            Specialization microchips = new Specialization("microchips");
            Specialization neurodiscs = new Specialization("neurodiscs");
            Specialization satellites = new Specialization("satellites");
            Specialization addiction = new Specialization("addiction treatment");
            Specialization piro = new Specialization("pirotechnics");

            context.Specializations.AddRange(new List<Specialization>() { cybernetics, neurodiscs, satellites, microchips, addiction, piro });

            #endregion

            #region Learning progresses

            context.LearningProgresses.AddRange(new List<LearningProgress>()
            {
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    Lesson = les1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Teaching
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 2,
                    Lesson = les2,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 2
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Library,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Library,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    Lesson = les1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    Lesson = les1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = jacobi,
                    Modifier = 1,
                    Lesson = les1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = dziks,
                    Modifier = 1,
                    Lesson = les1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    Specialization = cybernetics,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Specialization
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    Specialization = neurodiscs,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Specialization
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = radzu,
                    Modifier = 1,
                    Specialization = neurodiscs,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Specialization
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = radzu,
                    Modifier = 1,
                    Specialization = satellites,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Specialization
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = krzes,
                    Modifier = 1,
                    Specialization = piro,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Specialization
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = santidote,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = santidote,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    IdentifiedItem = greenPowder,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Biology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Identification
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 8,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Merit
                },
                new LearningProgress()
                {
                    Edition = ot19,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Mentoring,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Mentoring,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Mentoring,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = radzu,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Mentoring,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Lesson = les4,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Biology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Lesson = les5,
                    Modifier = 2,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Medicine,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 2
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Lesson = les7,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Teaching,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = duch,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = dziks,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = sandacz,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = krzes,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = jacobi,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = wru,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = fatum,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kuna,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },

                new LearningProgress()
                {
                    Edition = ot21,
                    Character = zarowka,
                    Lesson = les6,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.School,
                    ActiveLearningPoints = 1
                },

                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Library,
                    ActiveLearningPoints = 1
                },

                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Library,
                    ActiveLearningPoints = 1
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Technology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2021 7:00:00 PM"),
                    Type = LearningTypeTypes.Mentoring,
                    ActiveLearningPoints = 1
                },

                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = sstimpak,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = santidote,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    ApprovedBy = janeUser,
                    Schema = santidote,
                    Domain = DomainTypes.Chemistry,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Crafting
                },
                new LearningProgress()
                {
                    Edition = ot21,
                    Character = kas,
                    Modifier = 1,
                    IdentifiedItem = greenPowder,
                    ApprovedBy = janeUser,
                    Domain = DomainTypes.Biology,
                    TimeStamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                    Type = LearningTypeTypes.Identification
                },
            });
            context.SaveChanges();

            #endregion

            #region Perks

            Perk perk1 = new Perk()
            {
                Character = wizzl,
                ApprovedBy = janeUser,
                ApprovalTimestamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                Background = "Result of critical injury.",
                Mechanic = "Eats people.",
                Comment = "Coordinated by Wrubel.",
                Name = "Mushroom-man",
            };
            Perk perk2 = new Perk()
            {
                Character = kas,
                ApprovedBy = janeUser,
                ApprovalTimestamp = Convert.ToDateTime("08 Jul 2019 7:00:00 PM"),
                Background = "Result of critical injury.",
                Mechanic = "Cannot speak.",
                Comment = "Can be cured by transplantation of vocal cords.",
                Name = "Mute",
            };

            context.Perks.Add(perk1);
            context.Perks.Add(perk2);
            context.SaveChanges();

            #endregion

            #region Roles


            #endregion
            


        }
    }
}
