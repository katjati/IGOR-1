namespace Igor.Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accreditations",
                c => new
                    {
                        AccreditationId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Registered = c.DateTime(nullable: false, precision: 0),
                        AccreditationIdentifier = c.String(maxLength: 50, storeType: "nvarchar"),
                        IsActive = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IgorUser_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Edition_EditionId = c.Int(nullable: false),
                        ActiveCharacter_CharacterId = c.Int(),
                    })
                .PrimaryKey(t => t.AccreditationId)
                .ForeignKey("dbo.AspNetUsers", t => t.IgorUser_Id)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.ActiveCharacter_CharacterId)
                .Index(t => t.AccreditationIdentifier)
                .Index(t => t.IgorUser_Id)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.ActiveCharacter_CharacterId);
            
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        CharacterId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IsNpc = c.Boolean(nullable: false),
                        Race = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Faction_FactionId = c.Int(),
                        Player_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.CharacterId)
                .ForeignKey("dbo.Factions", t => t.Faction_FactionId)
                .ForeignKey("dbo.AspNetUsers", t => t.Player_Id)
                .Index(t => t.Name, name: "Character_Name")
                .Index(t => t.Faction_FactionId)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.Factions",
                c => new
                    {
                        FactionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Motto = c.String(unicode: false),
                        Link = c.String(unicode: false),
                        IsArchived = c.Boolean(nullable: false),
                        Conglomerate_ConglomerateId = c.Int(),
                    })
                .PrimaryKey(t => t.FactionId)
                .ForeignKey("dbo.Conglomerates", t => t.Conglomerate_ConglomerateId)
                .Index(t => t.Name, name: "Faction_Name")
                .Index(t => t.Conglomerate_ConglomerateId);
            
            CreateTable(
                "dbo.Conglomerates",
                c => new
                    {
                        ConglomerateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Abbreviation = c.String(nullable: false, unicode: false),
                        MainCoordinator_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                        SecondCoordinator_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ConglomerateId)
                .ForeignKey("dbo.AspNetUsers", t => t.MainCoordinator_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SecondCoordinator_Id)
                .Index(t => t.MainCoordinator_Id)
                .Index(t => t.SecondCoordinator_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FirstName = c.String(nullable: false, unicode: false),
                        LastName = c.String(nullable: false, unicode: false),
                        EmergencyContactPerson = c.String(unicode: false),
                        EmergencyContactTelephoneNumber = c.String(unicode: false),
                        MedicalInformation = c.String(unicode: false),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Perks",
                c => new
                    {
                        PerkId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Mechanic = c.String(unicode: false),
                        Background = c.String(unicode: false),
                        ApprovalTimestamp = c.DateTime(nullable: false, precision: 0),
                        Comment = c.String(unicode: false),
                        PerkType = c.Int(nullable: false),
                        ApprovedBy_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Character_CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerkId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId, cascadeDelete: true)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.Character_CharacterId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FactionCoordinators",
                c => new
                    {
                        FactionCoordinatorsId = c.Int(nullable: false, identity: true),
                        Comment = c.String(unicode: false),
                        IsMainCoordinator = c.Boolean(nullable: false),
                        Edition_EditionId = c.Int(nullable: false),
                        Coordinator_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Faction_FactionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FactionCoordinatorsId)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Coordinator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Factions", t => t.Faction_FactionId, cascadeDelete: true)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Coordinator_Id)
                .Index(t => t.Faction_FactionId);
            
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        EditionId = c.Int(nullable: false, identity: true),
                        DateStart = c.DateTime(nullable: false, precision: 0),
                        DateEnd = c.DateTime(nullable: false, precision: 0),
                        IsArchived = c.Boolean(nullable: false),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EditionId)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.Event_EventId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        MainOrganizer_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AspNetUsers", t => t.MainOrganizer_Id)
                .Index(t => t.Name, name: "Event_Name")
                .Index(t => t.MainOrganizer_Id);
            
            CreateTable(
                "dbo.Quests",
                c => new
                    {
                        QuestId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                        IsArchived = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Coordinator_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.QuestId)
                .ForeignKey("dbo.AspNetUsers", t => t.Coordinator_Id)
                .Index(t => t.Coordinator_Id);
            
            CreateTable(
                "dbo.IProgressBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Type = c.Int(),
                        Commonness = c.Int(),
                        IsActive = c.Boolean(),
                        HasRfid = c.Boolean(),
                        Comment = c.String(unicode: false),
                        Name1 = c.String(unicode: false),
                        Description1 = c.String(unicode: false),
                        SchemaType = c.Int(),
                        Attainability = c.Int(),
                        Domain = c.Int(),
                        Language = c.Int(),
                        StartTime = c.DateTime(precision: 0),
                        EndTime = c.DateTime(precision: 0),
                        Topic = c.String(unicode: false),
                        AdditionalInformation = c.String(unicode: false),
                        Comment1 = c.String(unicode: false),
                        Name2 = c.String(unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProductionTime_SuccessCurveId = c.Int(),
                        SuccessRate_SuccessCurveId = c.Int(),
                        Item_Id = c.Int(),
                        Edition_EditionId = c.Int(),
                        Teacher_CharacterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuccessCurves", t => t.ProductionTime_SuccessCurveId)
                .ForeignKey("dbo.SuccessCurves", t => t.SuccessRate_SuccessCurveId)
                .ForeignKey("dbo.IProgressBases", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Teacher_CharacterId, cascadeDelete: true)
                .Index(t => t.Name, name: "Item_Name")
                .Index(t => t.ProductionTime_SuccessCurveId)
                .Index(t => t.SuccessRate_SuccessCurveId)
                .Index(t => t.Item_Id)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Teacher_CharacterId);
            
            CreateTable(
                "dbo.CraftConditions",
                c => new
                    {
                        CraftConditionId = c.Int(nullable: false, identity: true),
                        ItemSchema_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CraftConditionId)
                .ForeignKey("dbo.IProgressBases", t => t.ItemSchema_Id, cascadeDelete: true)
                .Index(t => t.ItemSchema_Id);
            
            CreateTable(
                "dbo.SuccessCurves",
                c => new
                    {
                        SuccessCurveId = c.Int(nullable: false, identity: true),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SuccessCurveId);
            
            CreateTable(
                "dbo.IdentificationConditions",
                c => new
                    {
                        IdentificationConditionsId = c.Int(nullable: false, identity: true),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdentificationConditionsId)
                .ForeignKey("dbo.IProgressBases", t => t.Item_Id, cascadeDelete: true)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.LearningProgresses",
                c => new
                    {
                        LearningProgressId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Modifier = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Domain = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false, precision: 0),
                        Comment = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        IsSuccessful = c.Boolean(nullable: false),
                        HasActiveLearningPoints = c.Boolean(nullable: false),
                        ApprovedBy_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Edition_EditionId = c.Int(nullable: false),
                        Lesson_Id = c.Int(),
                        Specialization_Id = c.Int(),
                        ProgressSource_Id = c.Int(),
                        Character_CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LearningProgressId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.IProgressBases", t => t.Lesson_Id)
                .ForeignKey("dbo.IProgressBases", t => t.Specialization_Id)
                .ForeignKey("dbo.IProgressBases", t => t.ProgressSource_Id)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId, cascadeDelete: true)
                .Index(t => t.Type)
                .Index(t => t.Domain)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Lesson_Id)
                .Index(t => t.Specialization_Id)
                .Index(t => t.ProgressSource_Id)
                .Index(t => t.Character_CharacterId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Abbreviation = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.LanguageId)
                .Index(t => t.Name, name: "LanguageName")
                .Index(t => t.Abbreviation, name: "Language_Abb");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                        ShortName = c.String(maxLength: 10, storeType: "nvarchar"),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.component_schemas",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemSchemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemId, t.ItemSchemaId })
                .ForeignKey("dbo.IProgressBases", t => t.ItemId)
                .ForeignKey("dbo.IProgressBases", t => t.ItemSchemaId)
                .Index(t => t.ItemId)
                .Index(t => t.ItemSchemaId);
            
            CreateTable(
                "dbo.items_quests",
                c => new
                    {
                        QuestId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestId, t.ItemId })
                .ForeignKey("dbo.Quests", t => t.QuestId, cascadeDelete: true)
                .ForeignKey("dbo.IProgressBases", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.QuestId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.factions_quests",
                c => new
                    {
                        FactionId = c.Int(nullable: false),
                        QuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FactionId, t.QuestId })
                .ForeignKey("dbo.Factions", t => t.FactionId, cascadeDelete: true)
                .ForeignKey("dbo.Quests", t => t.QuestId, cascadeDelete: true)
                .Index(t => t.FactionId)
                .Index(t => t.QuestId);
            
            CreateTable(
                "dbo.character_schema",
                c => new
                    {
                        ItemSchemaId = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemSchemaId, t.CharacterId })
                .ForeignKey("dbo.Characters", t => t.ItemSchemaId, cascadeDelete: true)
                .ForeignKey("dbo.IProgressBases", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.ItemSchemaId)
                .Index(t => t.CharacterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Accreditations", "ActiveCharacter_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Perks", "Character_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.LearningProgresses", "Character_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.LearningProgresses", "ProgressSource_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.LearningProgresses", "Specialization_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.IProgressBases", "Teacher_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.LearningProgresses", "Lesson_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.IProgressBases", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.LearningProgresses", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.LearningProgresses", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.character_schema", "CharacterId", "dbo.IProgressBases");
            DropForeignKey("dbo.character_schema", "ItemSchemaId", "dbo.Characters");
            DropForeignKey("dbo.factions_quests", "QuestId", "dbo.Quests");
            DropForeignKey("dbo.factions_quests", "FactionId", "dbo.Factions");
            DropForeignKey("dbo.FactionCoordinators", "Faction_FactionId", "dbo.Factions");
            DropForeignKey("dbo.Conglomerates", "SecondCoordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Factions", "Conglomerate_ConglomerateId", "dbo.Conglomerates");
            DropForeignKey("dbo.Conglomerates", "MainCoordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quests", "Coordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.items_quests", "ItemId", "dbo.IProgressBases");
            DropForeignKey("dbo.items_quests", "QuestId", "dbo.Quests");
            DropForeignKey("dbo.IdentificationConditions", "Item_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.IProgressBases", "Item_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.IProgressBases", "SuccessRate_SuccessCurveId", "dbo.SuccessCurves");
            DropForeignKey("dbo.IProgressBases", "ProductionTime_SuccessCurveId", "dbo.SuccessCurves");
            DropForeignKey("dbo.CraftConditions", "ItemSchema_Id", "dbo.IProgressBases");
            DropForeignKey("dbo.component_schemas", "ItemSchemaId", "dbo.IProgressBases");
            DropForeignKey("dbo.component_schemas", "ItemId", "dbo.IProgressBases");
            DropForeignKey("dbo.FactionCoordinators", "Coordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FactionCoordinators", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.Events", "MainOrganizer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Editions", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Accreditations", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Characters", "Player_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Perks", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accreditations", "IgorUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Characters", "Faction_FactionId", "dbo.Factions");
            DropIndex("dbo.character_schema", new[] { "CharacterId" });
            DropIndex("dbo.character_schema", new[] { "ItemSchemaId" });
            DropIndex("dbo.factions_quests", new[] { "QuestId" });
            DropIndex("dbo.factions_quests", new[] { "FactionId" });
            DropIndex("dbo.items_quests", new[] { "ItemId" });
            DropIndex("dbo.items_quests", new[] { "QuestId" });
            DropIndex("dbo.component_schemas", new[] { "ItemSchemaId" });
            DropIndex("dbo.component_schemas", new[] { "ItemId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Languages", "Language_Abb");
            DropIndex("dbo.Languages", "LanguageName");
            DropIndex("dbo.LearningProgresses", new[] { "Character_CharacterId" });
            DropIndex("dbo.LearningProgresses", new[] { "ProgressSource_Id" });
            DropIndex("dbo.LearningProgresses", new[] { "Specialization_Id" });
            DropIndex("dbo.LearningProgresses", new[] { "Lesson_Id" });
            DropIndex("dbo.LearningProgresses", new[] { "Edition_EditionId" });
            DropIndex("dbo.LearningProgresses", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.LearningProgresses", new[] { "Domain" });
            DropIndex("dbo.LearningProgresses", new[] { "Type" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.IdentificationConditions", new[] { "Item_Id" });
            DropIndex("dbo.CraftConditions", new[] { "ItemSchema_Id" });
            DropIndex("dbo.IProgressBases", new[] { "Teacher_CharacterId" });
            DropIndex("dbo.IProgressBases", new[] { "Edition_EditionId" });
            DropIndex("dbo.IProgressBases", new[] { "Item_Id" });
            DropIndex("dbo.IProgressBases", new[] { "SuccessRate_SuccessCurveId" });
            DropIndex("dbo.IProgressBases", new[] { "ProductionTime_SuccessCurveId" });
            DropIndex("dbo.IProgressBases", "Item_Name");
            DropIndex("dbo.Quests", new[] { "Coordinator_Id" });
            DropIndex("dbo.Events", new[] { "MainOrganizer_Id" });
            DropIndex("dbo.Events", "Event_Name");
            DropIndex("dbo.Editions", new[] { "Event_EventId" });
            DropIndex("dbo.FactionCoordinators", new[] { "Faction_FactionId" });
            DropIndex("dbo.FactionCoordinators", new[] { "Coordinator_Id" });
            DropIndex("dbo.FactionCoordinators", new[] { "Edition_EditionId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Perks", new[] { "Character_CharacterId" });
            DropIndex("dbo.Perks", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Conglomerates", new[] { "SecondCoordinator_Id" });
            DropIndex("dbo.Conglomerates", new[] { "MainCoordinator_Id" });
            DropIndex("dbo.Factions", new[] { "Conglomerate_ConglomerateId" });
            DropIndex("dbo.Factions", "Faction_Name");
            DropIndex("dbo.Characters", new[] { "Player_Id" });
            DropIndex("dbo.Characters", new[] { "Faction_FactionId" });
            DropIndex("dbo.Characters", "Character_Name");
            DropIndex("dbo.Accreditations", new[] { "ActiveCharacter_CharacterId" });
            DropIndex("dbo.Accreditations", new[] { "Edition_EditionId" });
            DropIndex("dbo.Accreditations", new[] { "IgorUser_Id" });
            DropIndex("dbo.Accreditations", new[] { "AccreditationIdentifier" });
            DropTable("dbo.character_schema");
            DropTable("dbo.factions_quests");
            DropTable("dbo.items_quests");
            DropTable("dbo.component_schemas");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Languages");
            DropTable("dbo.LearningProgresses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.IdentificationConditions");
            DropTable("dbo.SuccessCurves");
            DropTable("dbo.CraftConditions");
            DropTable("dbo.IProgressBases");
            DropTable("dbo.Quests");
            DropTable("dbo.Events");
            DropTable("dbo.Editions");
            DropTable("dbo.FactionCoordinators");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Perks");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Conglomerates");
            DropTable("dbo.Factions");
            DropTable("dbo.Characters");
            DropTable("dbo.Accreditations");
        }
    }
}
