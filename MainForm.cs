using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;
using ASAServerTool.Models;
using ASAServerTool.UI;

namespace ASAServerTool
{
    public class MainForm : Form
    {
        private AppSettings settings;
        private string configDir = "Config";
        private string currentConfigPath = "Config\\ASA_Config.xml";
        private UIManager uiManager;
        
        private Timer backupTimer;
        private Process serverProcess;

        public MainForm()
        {
            this.Text = "方舟飞升 (ASA) 开服工具 - 终极版";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 确保 Config 文件夹存在
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }

            uiManager = new UIManager(this, BtnBrowse_Click, BtnBrowseBackup_Click, BtnSave_Click, BtnLoad_Click, BtnStart_Click);
            uiManager.InitializeComponents();

            backupTimer = new Timer();
            backupTimer.Tick += BackupTimer_Tick;

            LoadSettings(currentConfigPath);
        }

        private void BtnBrowse_Click()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Executable Files|*.exe|All Files|*.*";
            ofd.Title = "选择 ArkAscendedServer.exe";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                uiManager.txtPath.Text = ofd.FileName;
            }
        }

        private void BtnBrowseBackup_Click()
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.Description = "选择自动备份的保存目录";
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    uiManager.txtBackupPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void LoadSettings(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                    using (StreamReader reader = new StreamReader(path))
                    {
                        settings = (AppSettings)serializer.Deserialize(reader);
                    }
                    currentConfigPath = path;
                }
                catch
                {
                    settings = new AppSettings();
                }
            }
            else
            {
                settings = new AppSettings();
            }

            uiManager.BindSettingsToUI(settings);
            uiManager.Log("配置加载完成: " + Path.GetFileName(path));
        }

        private void BtnLoad_Click()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Path.GetFullPath(configDir);
            ofd.Filter = "XML Configuration Files|*.xml|All Files|*.*";
            ofd.Title = "选择要加载的配置文件";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadSettings(ofd.FileName);
            }
        }

        private void BtnSave_Click()
        {
            uiManager.PopulateSettingsFromUI(settings);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Path.GetFullPath(configDir);
            sfd.Filter = "XML Configuration Files|*.xml|All Files|*.*";
            sfd.Title = "保存配置文件";
            sfd.FileName = Path.GetFileName(currentConfigPath);
            sfd.DefaultExt = "xml";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveSettingsTo(sfd.FileName);
            }
        }

        private void SaveSettingsTo(string path)
        {
            uiManager.PopulateSettingsFromUI(settings);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
                using (StreamWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, settings);
                }
                currentConfigPath = path;
                uiManager.Log("配置已成功保存至: " + Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                uiManager.Log("保存配置失败: " + ex.Message);
            }
        }

        private void BtnStart_Click()
        {
            // 启动前自动静默保存到当前配置路径，避免丢失修改
            SaveSettingsTo(currentConfigPath);

            if (string.IsNullOrEmpty(settings.ServerPath) || !File.Exists(settings.ServerPath))
            {
                MessageBox.Show("请指定正确的服务端可执行文件路径 (例如 ArkAscendedServer.exe)", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string args = string.Format("{0}?listen?SessionName=\"{1}\"", settings.MapName, settings.ServerName);

            if (!string.IsNullOrEmpty(settings.ServerPassword))
                args += string.Format("?ServerPassword=\"{0}\"", settings.ServerPassword);

            args += string.Format("?QueryPort={0}", settings.QueryPort);

            // World Settings
            args += string.Format("?XPMultiplier={0}?TamingSpeedMultiplier={1}?HarvestAmountMultiplier={2}?DifficultyOffset={3}", 
                settings.XPMultiplier, settings.TamingSpeedMultiplier, settings.HarvestAmountMultiplier, settings.DifficultyOffset);

            args += string.Format("?MatingIntervalMultiplier={0}?EggHatchSpeedMultiplier={1}?BabyMatureSpeedMultiplier={2}?DinoCountMultiplier={3}",
                settings.MatingIntervalMultiplier, settings.EggHatchSpeedMultiplier, settings.BabyMatureSpeedMultiplier, settings.DinoCountMultiplier);

            // Advanced Breeding
            args += string.Format("?BabyCuddleIntervalMultiplier={0}?BabyCuddleGracePeriodMultiplier={1}?BabyCuddleLoseImprintQualitySpeedMultiplier={2}?BabyImprintingStatScaleMultiplier={3}?BabyFoodConsumptionSpeedMultiplier={4}",
                settings.BabyCuddleIntervalMultiplier, settings.BabyCuddleGracePeriodMultiplier, settings.BabyCuddleLoseImprintQualitySpeedMultiplier, settings.BabyImprintingStatScaleMultiplier, settings.BabyFoodConsumptionSpeedMultiplier);

            // Player & Dino Stats
            args += string.Format("?PlayerCharacterWaterDrainMultiplier={0}?PlayerCharacterFoodDrainMultiplier={1}?PlayerCharacterStaminaDrainMultiplier={2}?PlayerCharacterHealthRecoveryMultiplier={3}",
                settings.PlayerCharacterWaterDrainMultiplier, settings.PlayerCharacterFoodDrainMultiplier, settings.PlayerCharacterStaminaDrainMultiplier, settings.PlayerCharacterHealthRecoveryMultiplier);
            args += string.Format("?DinoCharacterFoodDrainMultiplier={0}?DinoCharacterStaminaDrainMultiplier={1}?DinoCharacterHealthRecoveryMultiplier={2}",
                settings.DinoCharacterFoodDrainMultiplier, settings.DinoCharacterStaminaDrainMultiplier, settings.DinoCharacterHealthRecoveryMultiplier);

            // Rules & Options
            args += string.Format("?AllowCaveBuildingPvE={0}?bAllowFlyerCarryPvE={1}?bDisableStructurePlacementCollision={2}?EnableCryoSicknessPVE={3}",
                settings.AllowCaveBuildingPvE, settings.AllowFlyerCarryPvE, settings.DisableStructurePlacementCollision, settings.EnableCryoSicknessPVE);
            args += string.Format("?ShowFloatingDamageText={0}?AllowThirdPersonPlayer={1}?ServerCrosshair={2}?ShowMapPlayerLocation={3}",
                settings.ShowFloatingDamageText, settings.AllowThirdPersonPlayer, settings.ServerCrosshair, settings.ShowMapPlayerLocation);
            args += string.Format("?MaxPersonalTamedDinos={0}", settings.MaxPersonalTamedDinos);

            // Day/Night & Resources
            args += string.Format("?DayTimeSpeedScale={0}?NightTimeSpeedScale={1}?ResourceNoReplenishRadiusPlayers={2}?ResourcesRespawnPeriodMultiplier={3}",
                settings.DayTimeSpeedScale, settings.NightTimeSpeedScale, settings.ResourceNoReplenishRadiusPlayers, settings.ResourcesRespawnPeriodMultiplier);

            // Additional Gameplay Multipliers
            args += string.Format("?CropGrowthSpeedMultiplier={0}?PoopIntervalMultiplier={1}?LayEggIntervalMultiplier={2}?GlobalSpoilingTimeMultiplier={3}?GlobalItemDecompositionTimeMultiplier={4}?GlobalCorpseDecompositionTimeMultiplier={5}",
                settings.CropGrowthSpeedMultiplier, settings.PoopIntervalMultiplier, settings.LayEggIntervalMultiplier, settings.GlobalSpoilingTimeMultiplier, settings.GlobalItemDecompositionTimeMultiplier, settings.GlobalCorpseDecompositionTimeMultiplier);

            // Additional Rules
            args += string.Format("?bEnableProximityChat={0}?bDisableDinoDecayPvE={1}?AllowAnyoneBabyImprintCuddle={2}?PreventOfflinePvP={3}?bUseCorpseLocator={4}?DisableWeatherFog={5}",
                settings.EnableProximityChat, settings.DisableDinoDecayPvE, settings.AllowAnyoneBabyImprintCuddle, settings.PreventOfflinePvP, settings.UseCorpseLocator, settings.DisableWeatherFog);

            // PvP / PvE
            if (settings.DisablePvE == false)
                args += "?ServerPVE=True";

            // RCON
            if (settings.EnableRCON)
            {
                args += "?RCONEnabled=True?RCONPort=" + settings.RCONPort;
            }

            // 根据官方文档，管理员密码必须是最后一个带问号的参数
            if (!string.IsNullOrEmpty(settings.AdminPassword))
                args += string.Format("?ServerAdminPassword=\"{0}\"", settings.AdminPassword);

            // ASA 要求使用 -port 而不是 ?Port，并且建议用 -WinLiveMaxPlayers 控制人数
            args += string.Format(" -port={0} -WinLiveMaxPlayers={1}", settings.Port, settings.MaxPlayers);

            if (settings.UseForceRespawnDinos)
                args += " -ForceRespawnDinos";

            if (settings.Crossplay)
                args += " -crossplay";

            if (!string.IsNullOrEmpty(settings.Mods))
                args += string.Format(" -mods={0}", settings.Mods);

            if (!settings.BattleEye)
                args += " -NoBattlEye";

            if (!string.IsNullOrEmpty(settings.ExtraArgs))
                args += " " + settings.ExtraArgs;

            args += " -server -log";

            uiManager.Log("启动参数: " + args);

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = settings.ServerPath;
                psi.Arguments = args;
                psi.UseShellExecute = true;

                serverProcess = Process.Start(psi);
                uiManager.Log("服务器已启动!");

                if (settings.EnableAutoBackup)
                {
                    backupTimer.Interval = settings.AutoBackupIntervalMinutes * 60 * 1000;
                    backupTimer.Start();
                    uiManager.Log(string.Format("自动备份已开启，间隔: {0} 分钟。", settings.AutoBackupIntervalMinutes));
                }
            }
            catch (Exception ex)
            {
                uiManager.Log("启动服务器失败: " + ex.Message);
                MessageBox.Show("启动失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackupTimer_Tick(object sender, EventArgs e)
        {
            if (serverProcess == null || serverProcess.HasExited)
            {
                backupTimer.Stop();
                uiManager.Log("服务器未运行，自动备份已停止。");
                return;
            }

            try
            {
                // Ark Ascended Saved Path calculation
                string serverDir = Path.GetDirectoryName(settings.ServerPath);
                // Go up from ShooterGame/Binaries/Win64 to ShooterGame
                string shooterGameDir = Path.GetFullPath(Path.Combine(serverDir, "..", ".."));
                string savedDir = Path.Combine(shooterGameDir, "Saved", "SavedArks", settings.MapName);

                if (Directory.Exists(savedDir))
                {
                    string backupFolder = string.IsNullOrWhiteSpace(settings.CustomBackupPath) 
                        ? Path.Combine(shooterGameDir, "Saved", "AutoBackups")
                        : settings.CustomBackupPath;

                    if (!Directory.Exists(backupFolder))
                        Directory.CreateDirectory(backupFolder);

                    string backupFileName = string.Format("Backup_{0}_{1}.zip", settings.MapName, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    string backupPath = Path.Combine(backupFolder, backupFileName);

                    // A simple copy for the .ark file instead of full zip to avoid heavy dependency
                    // Ideally we backup the main map .ark file
                    string mapFile = Path.Combine(savedDir, settings.MapName + ".ark");
                    if (File.Exists(mapFile))
                    {
                        string targetMapFile = Path.Combine(backupFolder, string.Format("Backup_{0}_{1}.ark", settings.MapName, DateTime.Now.ToString("yyyyMMdd_HHmmss")));
                        File.Copy(mapFile, targetMapFile, true);
                        uiManager.Log(string.Format("自动备份完成: {0}", targetMapFile));
                    }
                    else
                    {
                        uiManager.Log(string.Format("自动备份警告: 找不到地图存档文件 {0}", mapFile));
                    }
                }
            }
            catch (Exception ex)
            {
                uiManager.Log("自动备份失败: " + ex.Message);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
