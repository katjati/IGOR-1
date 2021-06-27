namespace Igor.Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
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
                    })
                .PrimaryKey(t => t.ConglomerateId);
            
            CreateTable(
                "dbo.ConglomerateCoordinations",
                c => new
                    {
                        CoordinatorId = c.Int(nullable: false, identity: true),
                        Comment = c.String(unicode: false),
                        IsMainCoordinator = c.Boolean(nullable: false),
                        ConglomerateCoordinator_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Edition_EditionId = c.Int(nullable: false),
                        Conglomerate_ConglomerateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoordinatorId)
                .ForeignKey("dbo.AspNetUsers", t => t.ConglomerateCoordinator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Conglomerates", t => t.Conglomerate_ConglomerateId, cascadeDelete: true)
                .Index(t => t.ConglomerateCoordinator_Id)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Conglomerate_ConglomerateId);
            
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
                        IsForeigner = c.Boolean(nullable: false),
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
                "dbo.FactionCoordinations",
                c => new
                    {
                        CoordinatorId = c.Int(nullable: false, identity: true),
                        Comment = c.String(unicode: false),
                        IsMainCoordinator = c.Boolean(nullable: false),
                        Edition_EditionId = c.Int(nullable: false),
                        FactionCoordinator_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Faction_FactionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoordinatorId)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FactionCoordinator_Id, cascadeDelete: true)
                .ForeignKey("dbo.Factions", t => t.Faction_FactionId, cascadeDelete: true)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.FactionCoordinator_Id)
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
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        Type = c.Int(nullable: false),
                        Commonness = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        HasRfid = c.Boolean(nullable: false),
                        Mechanics = c.String(unicode: false),
                        Comment = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .Index(t => t.Name, name: "Item_Name");
            
            CreateTable(
                "dbo.ItemSchemas",
                c => new
                    {
                        ItemSchemaId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        Description = c.String(unicode: false),
                        SchemaType = c.Int(nullable: false),
                        Attainability = c.Int(nullable: false),
                        ProductionTime_SuccessCurveId = c.Int(),
                        SuccessRate_SuccessCurveId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemSchemaId)
                .ForeignKey("dbo.SuccessCurves", t => t.ProductionTime_SuccessCurveId)
                .ForeignKey("dbo.SuccessCurves", t => t.SuccessRate_SuccessCurveId)
                .Index(t => t.ProductionTime_SuccessCurveId)
                .Index(t => t.SuccessRate_SuccessCurveId);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDefault = c.Boolean(nullable: false),
                        Total = c.Int(nullable: false),
                        AllowMixing = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Specialization_SpecializationId = c.Int(),
                        LearnedItemSchema_ItemSchemaId = c.Int(),
                        UsedItemSchema_ItemSchemaId = c.Int(),
                        IdentifiedItem_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specializations", t => t.Specialization_SpecializationId)
                .ForeignKey("dbo.ItemSchemas", t => t.LearnedItemSchema_ItemSchemaId, cascadeDelete: true)
                .ForeignKey("dbo.ItemSchemas", t => t.UsedItemSchema_ItemSchemaId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.IdentifiedItem_ItemId, cascadeDelete: true)
                .Index(t => t.Specialization_SpecializationId)
                .Index(t => t.LearnedItemSchema_ItemSchemaId)
                .Index(t => t.UsedItemSchema_ItemSchemaId)
                .Index(t => t.IdentifiedItem_ItemId);
            
            CreateTable(
                "dbo.ConditionDomains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        ParentCondition_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditions", t => t.ParentCondition_Id, cascadeDelete: true)
                .Index(t => t.ParentCondition_Id);
            
            CreateTable(
                "dbo.Specializations",
                c => new
                    {
                        SpecializationId = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                        IsHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SpecializationId);
            
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
                        ActiveLearningPoints = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ApprovedBy_Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Edition_EditionId = c.Int(nullable: false),
                        IdentifiedItem_ItemId = c.Int(),
                        Lesson_LessonId = c.Int(),
                        Schema_ItemSchemaId = c.Int(),
                        Specialization_SpecializationId = c.Int(),
                        Character_CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LearningProgressId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.IdentifiedItem_ItemId)
                .ForeignKey("dbo.Lessons", t => t.Lesson_LessonId)
                .ForeignKey("dbo.ItemSchemas", t => t.Schema_ItemSchemaId)
                .ForeignKey("dbo.Specializations", t => t.Specialization_SpecializationId)
                .ForeignKey("dbo.Characters", t => t.Character_CharacterId, cascadeDelete: true)
                .Index(t => t.Type)
                .Index(t => t.Domain)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.IdentifiedItem_ItemId)
                .Index(t => t.Lesson_LessonId)
                .Index(t => t.Schema_ItemSchemaId)
                .Index(t => t.Specialization_SpecializationId)
                .Index(t => t.Character_CharacterId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        Domain = c.Int(nullable: false),
                        Language = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false, precision: 0),
                        EndTime = c.DateTime(nullable: false, precision: 0),
                        Topic = c.String(unicode: false),
                        AdditionalInformation = c.String(unicode: false),
                        Comment = c.String(unicode: false),
                        IsArchived = c.Boolean(nullable: false),
                        Edition_EditionId = c.Int(nullable: false),
                        Teacher_CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId, cascadeDelete: true)
                .ForeignKey("dbo.Characters", t => t.Teacher_CharacterId, cascadeDelete: true)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Teacher_CharacterId);
            
            CreateTable(
                "dbo.SuccessCurves",
                c => new
                    {
                        SuccessCurveId = c.Int(nullable: false, identity: true),
                        Default = c.Boolean(nullable: false),
                        Min = c.Int(nullable: false),
                        Max = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SuccessCurveId);
            
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
                "dbo.Item_ItemSchema_Input",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemSchemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemId, t.ItemSchemaId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.ItemSchemas", t => t.ItemSchemaId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.ItemSchemaId);
            
            CreateTable(
                "dbo.Item_ItemSchema_Output",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemSchemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemId, t.ItemSchemaId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.ItemSchemas", t => t.ItemSchemaId, cascadeDelete: true)
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
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
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
                .ForeignKey("dbo.ItemSchemas", t => t.CharacterId, cascadeDelete: true)
                .Index(t => t.ItemSchemaId)
                .Index(t => t.CharacterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Accreditations", "ActiveCharacter_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Perks", "Character_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.LearningProgresses", "Character_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.character_schema", "CharacterId", "dbo.ItemSchemas");
            DropForeignKey("dbo.character_schema", "ItemSchemaId", "dbo.Characters");
            DropForeignKey("dbo.factions_quests", "QuestId", "dbo.Quests");
            DropForeignKey("dbo.factions_quests", "FactionId", "dbo.Factions");
            DropForeignKey("dbo.FactionCoordinations", "Faction_FactionId", "dbo.Factions");
            DropForeignKey("dbo.Factions", "Conglomerate_ConglomerateId", "dbo.Conglomerates");
            DropForeignKey("dbo.ConglomerateCoordinations", "Conglomerate_ConglomerateId", "dbo.Conglomerates");
            DropForeignKey("dbo.ConglomerateCoordinations", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quests", "Coordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.items_quests", "ItemId", "dbo.Items");
            DropForeignKey("dbo.items_quests", "QuestId", "dbo.Quests");
            DropForeignKey("dbo.Conditions", "IdentifiedItem_ItemId", "dbo.Items");
            DropForeignKey("dbo.Item_ItemSchema_Output", "ItemSchemaId", "dbo.ItemSchemas");
            DropForeignKey("dbo.Item_ItemSchema_Output", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Item_ItemSchema_Input", "ItemSchemaId", "dbo.ItemSchemas");
            DropForeignKey("dbo.Item_ItemSchema_Input", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Conditions", "UsedItemSchema_ItemSchemaId", "dbo.ItemSchemas");
            DropForeignKey("dbo.ItemSchemas", "SuccessRate_SuccessCurveId", "dbo.SuccessCurves");
            DropForeignKey("dbo.ItemSchemas", "ProductionTime_SuccessCurveId", "dbo.SuccessCurves");
            DropForeignKey("dbo.Conditions", "LearnedItemSchema_ItemSchemaId", "dbo.ItemSchemas");
            DropForeignKey("dbo.Conditions", "Specialization_SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.LearningProgresses", "Specialization_SpecializationId", "dbo.Specializations");
            DropForeignKey("dbo.LearningProgresses", "Schema_ItemSchemaId", "dbo.ItemSchemas");
            DropForeignKey("dbo.Lessons", "Teacher_CharacterId", "dbo.Characters");
            DropForeignKey("dbo.LearningProgresses", "Lesson_LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.LearningProgresses", "IdentifiedItem_ItemId", "dbo.Items");
            DropForeignKey("dbo.LearningProgresses", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.LearningProgresses", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConditionDomains", "ParentCondition_Id", "dbo.Conditions");
            DropForeignKey("dbo.FactionCoordinations", "FactionCoordinator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FactionCoordinations", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.Events", "MainOrganizer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Editions", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Accreditations", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.ConglomerateCoordinations", "ConglomerateCoordinator_Id", "dbo.AspNetUsers");
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
            DropIndex("dbo.Item_ItemSchema_Output", new[] { "ItemSchemaId" });
            DropIndex("dbo.Item_ItemSchema_Output", new[] { "ItemId" });
            DropIndex("dbo.Item_ItemSchema_Input", new[] { "ItemSchemaId" });
            DropIndex("dbo.Item_ItemSchema_Input", new[] { "ItemId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Languages", "Language_Abb");
            DropIndex("dbo.Languages", "LanguageName");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Lessons", new[] { "Teacher_CharacterId" });
            DropIndex("dbo.Lessons", new[] { "Edition_EditionId" });
            DropIndex("dbo.LearningProgresses", new[] { "Character_CharacterId" });
            DropIndex("dbo.LearningProgresses", new[] { "Specialization_SpecializationId" });
            DropIndex("dbo.LearningProgresses", new[] { "Schema_ItemSchemaId" });
            DropIndex("dbo.LearningProgresses", new[] { "Lesson_LessonId" });
            DropIndex("dbo.LearningProgresses", new[] { "IdentifiedItem_ItemId" });
            DropIndex("dbo.LearningProgresses", new[] { "Edition_EditionId" });
            DropIndex("dbo.LearningProgresses", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.LearningProgresses", new[] { "Domain" });
            DropIndex("dbo.LearningProgresses", new[] { "Type" });
            DropIndex("dbo.ConditionDomains", new[] { "ParentCondition_Id" });
            DropIndex("dbo.Conditions", new[] { "IdentifiedItem_ItemId" });
            DropIndex("dbo.Conditions", new[] { "UsedItemSchema_ItemSchemaId" });
            DropIndex("dbo.Conditions", new[] { "LearnedItemSchema_ItemSchemaId" });
            DropIndex("dbo.Conditions", new[] { "Specialization_SpecializationId" });
            DropIndex("dbo.ItemSchemas", new[] { "SuccessRate_SuccessCurveId" });
            DropIndex("dbo.ItemSchemas", new[] { "ProductionTime_SuccessCurveId" });
            DropIndex("dbo.Items", "Item_Name");
            DropIndex("dbo.Quests", new[] { "Coordinator_Id" });
            DropIndex("dbo.Events", new[] { "MainOrganizer_Id" });
            DropIndex("dbo.Events", "Event_Name");
            DropIndex("dbo.Editions", new[] { "Event_EventId" });
            DropIndex("dbo.FactionCoordinations", new[] { "Faction_FactionId" });
            DropIndex("dbo.FactionCoordinations", new[] { "FactionCoordinator_Id" });
            DropIndex("dbo.FactionCoordinations", new[] { "Edition_EditionId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Perks", new[] { "Character_CharacterId" });
            DropIndex("dbo.Perks", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ConglomerateCoordinations", new[] { "Conglomerate_ConglomerateId" });
            DropIndex("dbo.ConglomerateCoordinations", new[] { "Edition_EditionId" });
            DropIndex("dbo.ConglomerateCoordinations", new[] { "ConglomerateCoordinator_Id" });
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
            DropTable("dbo.Item_ItemSchema_Output");
            DropTable("dbo.Item_ItemSchema_Input");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Languages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.SuccessCurves");
            DropTable("dbo.Lessons");
            DropTable("dbo.LearningProgresses");
            DropTable("dbo.Specializations");
            DropTable("dbo.ConditionDomains");
            DropTable("dbo.Conditions");
            DropTable("dbo.ItemSchemas");
            DropTable("dbo.Items");
            DropTable("dbo.Quests");
            DropTable("dbo.Events");
            DropTable("dbo.Editions");
            DropTable("dbo.FactionCoordinations");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Perks");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ConglomerateCoordinations");
            DropTable("dbo.Conglomerates");
            DropTable("dbo.Factions");
            DropTable("dbo.Characters");
            DropTable("dbo.Accreditations");
        }
    }
}
