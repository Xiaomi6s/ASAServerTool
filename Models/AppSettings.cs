using System;

namespace ASAServerTool.Models
{
    [Serializable]
    public class AppSettings
    {
        public string ServerPath { get; set; }
        public string MapName { get; set; }
        public string ServerName { get; set; }
        public string ServerPassword { get; set; }
        public string AdminPassword { get; set; }
        public int MaxPlayers { get; set; }
        public string Port { get; set; }
        public string QueryPort { get; set; }
        public string Mods { get; set; }
        public bool BattleEye { get; set; }
        public bool Crossplay { get; set; }
        public string ExtraArgs { get; set; }

        public decimal XPMultiplier { get; set; }
        public decimal TamingSpeedMultiplier { get; set; }
        public decimal HarvestAmountMultiplier { get; set; }
        public decimal DifficultyOffset { get; set; }
        
        // Breeding & Advanced World Settings
        public decimal MatingIntervalMultiplier { get; set; }
        public decimal EggHatchSpeedMultiplier { get; set; }
        public decimal BabyMatureSpeedMultiplier { get; set; }
        public decimal DinoCountMultiplier { get; set; }

        // Day/Night & Resources
        public decimal DayTimeSpeedScale { get; set; }
        public decimal NightTimeSpeedScale { get; set; }
        public decimal ResourceNoReplenishRadiusPlayers { get; set; }
        public decimal ResourcesRespawnPeriodMultiplier { get; set; }

        // Player Stats Multipliers
        public decimal PlayerCharacterWaterDrainMultiplier { get; set; }
        public decimal PlayerCharacterFoodDrainMultiplier { get; set; }
        public decimal PlayerCharacterStaminaDrainMultiplier { get; set; }
        public decimal PlayerCharacterHealthRecoveryMultiplier { get; set; }

        // Dino Stats Multipliers
        public decimal DinoCharacterFoodDrainMultiplier { get; set; }
        public decimal DinoCharacterStaminaDrainMultiplier { get; set; }
        public decimal DinoCharacterHealthRecoveryMultiplier { get; set; }

        // Advanced Breeding
        public decimal BabyCuddleIntervalMultiplier { get; set; }
        public decimal BabyCuddleGracePeriodMultiplier { get; set; }
        public decimal BabyCuddleLoseImprintQualitySpeedMultiplier { get; set; }
        public decimal BabyImprintingStatScaleMultiplier { get; set; }
        public decimal BabyFoodConsumptionSpeedMultiplier { get; set; }

        // Rules & Options
        public bool AllowCaveBuildingPvE { get; set; }
        public bool AllowFlyerCarryPvE { get; set; }
        public bool DisableStructurePlacementCollision { get; set; }
        public bool EnableCryoSicknessPVE { get; set; }
        public bool ShowFloatingDamageText { get; set; }
        public bool AllowThirdPersonPlayer { get; set; }
        public bool ServerCrosshair { get; set; }
        public bool ShowMapPlayerLocation { get; set; }
        public int MaxPersonalTamedDinos { get; set; }

        // Environment & Networking Settings
        public bool EnableRCON { get; set; }
        public string RCONPort { get; set; }
        public bool UseForceRespawnDinos { get; set; }
        public bool DisablePvE { get; set; } // If true, Server is PvP

        // Additional Gameplay Multipliers
        public decimal CropGrowthSpeedMultiplier { get; set; }
        public decimal PoopIntervalMultiplier { get; set; }
        public decimal LayEggIntervalMultiplier { get; set; }
        public decimal GlobalSpoilingTimeMultiplier { get; set; }
        public decimal GlobalItemDecompositionTimeMultiplier { get; set; }
        public decimal GlobalCorpseDecompositionTimeMultiplier { get; set; }

        // Additional Rules
        public bool EnableProximityChat { get; set; }
        public bool DisableDinoDecayPvE { get; set; }
        public bool AllowAnyoneBabyImprintCuddle { get; set; }
        public bool PreventOfflinePvP { get; set; }
        public bool UseCorpseLocator { get; set; }
        public bool DisableWeatherFog { get; set; }

        // Auto Backup
        public bool EnableAutoBackup { get; set; }
        public int AutoBackupIntervalMinutes { get; set; }
        public string CustomBackupPath { get; set; }

        public AppSettings()
        {
            ServerPath = "";
            MapName = "TheIsland_WP";
            ServerName = "My ASA Server";
            ServerPassword = "";
            AdminPassword = "";
            MaxPlayers = 70;
            Port = "7777";
            QueryPort = "27015";
            Mods = "";
            BattleEye = true;
            Crossplay = true;
            ExtraArgs = "";
            
            // Default Multipliers
            XPMultiplier = 1.0m;
            TamingSpeedMultiplier = 1.0m;
            HarvestAmountMultiplier = 1.0m;
            DifficultyOffset = 1.0m;
            MatingIntervalMultiplier = 1.0m;
            EggHatchSpeedMultiplier = 1.0m;
            BabyMatureSpeedMultiplier = 1.0m;
            DinoCountMultiplier = 1.0m;
            
            DayTimeSpeedScale = 1.0m;
            NightTimeSpeedScale = 1.0m;
            ResourceNoReplenishRadiusPlayers = 1.0m;
            ResourcesRespawnPeriodMultiplier = 1.0m;

            // Stats Defaults
            PlayerCharacterWaterDrainMultiplier = 1.0m;
            PlayerCharacterFoodDrainMultiplier = 1.0m;
            PlayerCharacterStaminaDrainMultiplier = 1.0m;
            PlayerCharacterHealthRecoveryMultiplier = 1.0m;

            DinoCharacterFoodDrainMultiplier = 1.0m;
            DinoCharacterStaminaDrainMultiplier = 1.0m;
            DinoCharacterHealthRecoveryMultiplier = 1.0m;

            BabyCuddleIntervalMultiplier = 1.0m;
            BabyCuddleGracePeriodMultiplier = 1.0m;
            BabyCuddleLoseImprintQualitySpeedMultiplier = 1.0m;
            BabyImprintingStatScaleMultiplier = 1.0m;
            BabyFoodConsumptionSpeedMultiplier = 1.0m;

            AllowCaveBuildingPvE = false;
            AllowFlyerCarryPvE = false;
            DisableStructurePlacementCollision = false;
            EnableCryoSicknessPVE = true;
            ShowFloatingDamageText = false;
            AllowThirdPersonPlayer = true;
            ServerCrosshair = true;
            ShowMapPlayerLocation = true;
            MaxPersonalTamedDinos = 500;

            // Additional default values
            CropGrowthSpeedMultiplier = 1.0m;
            PoopIntervalMultiplier = 1.0m;
            LayEggIntervalMultiplier = 1.0m;
            GlobalSpoilingTimeMultiplier = 1.0m;
            GlobalItemDecompositionTimeMultiplier = 1.0m;
            GlobalCorpseDecompositionTimeMultiplier = 1.0m;

            EnableProximityChat = false;
            DisableDinoDecayPvE = false;
            AllowAnyoneBabyImprintCuddle = false;
            PreventOfflinePvP = false;
            UseCorpseLocator = true;
            DisableWeatherFog = false;

            // Network/Env defaults
            EnableRCON = true;
            RCONPort = "27020";
            UseForceRespawnDinos = false;
            DisablePvE = true; // PvP by default

            // Auto Backup
            EnableAutoBackup = false;
            AutoBackupIntervalMinutes = 60;
            CustomBackupPath = "";
        }
    }
}
