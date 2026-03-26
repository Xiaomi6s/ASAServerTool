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

        // Environment & Networking Settings
        public bool EnableRCON { get; set; }
        public string RCONPort { get; set; }
        public bool UseForceRespawnDinos { get; set; }
        public bool DisablePvE { get; set; } // If true, Server is PvP

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
