using System;
using System.Drawing;
using System.Windows.Forms;
using ASAServerTool.Models;

namespace ASAServerTool.UI
{
    public class UIManager
    {
        public TextBox txtPath;
        public Button btnBrowse;
        public TextBox txtMap;
        public TextBox txtName;
        public TextBox txtPass;
        public TextBox txtAdminPass;
        public NumericUpDown numMaxPlayers;
        public TextBox txtPort;
        public TextBox txtQueryPort;
        public TextBox txtMods;

        public NumericUpDown numXP;
        public NumericUpDown numTaming;
        public NumericUpDown numHarvest;
        public NumericUpDown numDifficulty;
        public NumericUpDown numMating;
        public NumericUpDown numHatching;
        public NumericUpDown numMature;
        public NumericUpDown numDinoCount;
        
        public NumericUpDown numDayTime;
        public NumericUpDown numNightTime;
        public NumericUpDown numResourceRadius;
        public NumericUpDown numResourceRespawn;

        public CheckBox chkPvE;
        public CheckBox chkBattleEye;
        public CheckBox chkCrossplay;
        public CheckBox chkRCON;
        public TextBox txtRCONPort;
        public CheckBox chkForceRespawn;
        public TextBox txtExtraArgs;
        
        public CheckBox chkAutoBackup;
        public NumericUpDown numBackupInterval;
        public TextBox txtBackupPath;
        public Button btnBrowseBackup;

        public Button btnStart;
        public Button btnSave;
        public Button btnLoad;
        public TextBox txtLog;
        public TabControl tabControl;

        private Form parentForm;
        private Action browseAction;
        private Action browseBackupAction;
        private Action saveAction;
        private Action loadAction;
        private Action startAction;

        public UIManager(Form parent, Action onBrowse, Action onBrowseBackup, Action onSave, Action onLoad, Action onStart)
        {
            parentForm = parent;
            browseAction = onBrowse;
            browseBackupAction = onBrowseBackup;
            saveAction = onSave;
            loadAction = onLoad;
            startAction = onStart;
        }

        public void InitializeComponents()
        {
            int lblWidth = 120;
            int txtWidth = 350;
            int rowHeight = 35;

            tabControl = new TabControl { Left = 10, Top = 10, Width = 560, Height = 450 };
            
            TabPage tabBasic = new TabPage("基础设置");
            TabPage tabWorld = new TabPage("世界规则");
            TabPage tabBackup = new TabPage("自动备份");
            TabPage tabAdvanced = new TabPage("高级与网络");
            TabPage tabHelp = new TabPage("连接教程");

            tabControl.TabPages.Add(tabBasic);
            tabControl.TabPages.Add(tabWorld);
            tabControl.TabPages.Add(tabBackup);
            tabControl.TabPages.Add(tabAdvanced);
            tabControl.TabPages.Add(tabHelp);
            parentForm.Controls.Add(tabControl);

            BuildBasicTab(tabBasic, lblWidth, txtWidth, rowHeight);
            BuildWorldTab(tabWorld, lblWidth, txtWidth, rowHeight);
            BuildBackupTab(tabBackup, lblWidth, txtWidth, rowHeight);
            BuildAdvancedTab(tabAdvanced, lblWidth, txtWidth, rowHeight);
            BuildHelpTab(tabHelp);
            BuildBottomControls();
        }

        private void BuildBasicTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            int y = 20;

            tab.Controls.Add(new Label { Text = "服务端路径:", Left = 20, Top = y, Width = lblWidth });
            txtPath = new TextBox { Left = 140, Top = y, Width = txtWidth - 40 };
            tab.Controls.Add(txtPath);
            btnBrowse = new Button { Text = "...", Left = 140 + txtWidth - 30, Top = y - 2, Width = 30 };
            btnBrowse.Click += (s, e) => { if (browseAction != null) browseAction(); };
            tab.Controls.Add(btnBrowse);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "地图名称:", Left = 20, Top = y, Width = lblWidth });
            txtMap = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtMap);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "服务器名称:", Left = 20, Top = y, Width = lblWidth });
            txtName = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtName);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "服务器密码:", Left = 20, Top = y, Width = lblWidth });
            txtPass = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtPass);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "管理员密码:", Left = 20, Top = y, Width = lblWidth });
            txtAdminPass = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtAdminPass);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "最大玩家数:", Left = 20, Top = y, Width = lblWidth });
            numMaxPlayers = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 1, Maximum = 200, Value = 70 };
            tab.Controls.Add(numMaxPlayers);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "游戏端口:", Left = 20, Top = y, Width = lblWidth });
            txtPort = new TextBox { Left = 140, Top = y, Width = 100 };
            tab.Controls.Add(txtPort);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "查询端口:", Left = 20, Top = y, Width = lblWidth });
            txtQueryPort = new TextBox { Left = 140, Top = y, Width = 100 };
            tab.Controls.Add(txtQueryPort);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "Mod ID(逗号分隔):", Left = 20, Top = y, Width = lblWidth });
            txtMods = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtMods);
        }

        private void BuildWorldTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            // 添加一个滚动面板以容纳较多的规则和说明
            Panel scrollPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tab.Controls.Add(scrollPanel);

            int y = 10;
            int hintLeft = 250;
            int hintWidth = 280;

            scrollPanel.Controls.Add(new Label { Text = "经验倍率:", Left = 20, Top = y, Width = lblWidth });
            numXP = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numXP);
            scrollPanel.Controls.Add(new Label { Text = "(越大升级越快, 1.0为官服标准)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "驯服倍率:", Left = 20, Top = y, Width = lblWidth });
            numTaming = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numTaming);
            scrollPanel.Controls.Add(new Label { Text = "(越大驯服越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "采集倍率:", Left = 20, Top = y, Width = lblWidth });
            numHarvest = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numHarvest);
            scrollPanel.Controls.Add(new Label { Text = "(越大单次采集资源越多)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "游戏难度:", Left = 20, Top = y, Width = lblWidth });
            numDifficulty = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numDifficulty);
            scrollPanel.Controls.Add(new Label { Text = "(影响野生龙等级, 1.0通常对应150级满级)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "交配间隔倍率:", Left = 20, Top = y, Width = lblWidth });
            numMating = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 1000m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numMating);
            scrollPanel.Controls.Add(new Label { Text = "(越小交配冷却时间越短!)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.IndianRed });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "孵化速度倍率:", Left = 20, Top = y, Width = lblWidth });
            numHatching = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numHatching);
            scrollPanel.Controls.Add(new Label { Text = "(越大蛋孵化越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "宝宝成长倍率:", Left = 20, Top = y, Width = lblWidth });
            numMature = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 1000m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numMature);
            scrollPanel.Controls.Add(new Label { Text = "(越大幼龙长大越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "恐龙刷新数量:", Left = 20, Top = y, Width = lblWidth });
            numDinoCount = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 10m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numDinoCount);
            scrollPanel.Controls.Add(new Label { Text = "(全图野生龙总数, 谨慎调高以免卡顿)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            // Day/Night & Resources
            scrollPanel.Controls.Add(new Label { Text = "白天时间流逝速度:", Left = 20, Top = y, Width = lblWidth });
            numDayTime = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 10m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numDayTime);
            scrollPanel.Controls.Add(new Label { Text = "(越小白天持续越久!)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.IndianRed });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "夜晚时间流逝速度:", Left = 20, Top = y, Width = lblWidth });
            numNightTime = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 10m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numNightTime);
            scrollPanel.Controls.Add(new Label { Text = "(越大黑夜过得越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "资源刷新周期:", Left = 20, Top = y, Width = lblWidth });
            numResourceRespawn = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numResourceRespawn);
            scrollPanel.Controls.Add(new Label { Text = "(越小树木/石头长出来越快!)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.IndianRed });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "阻挡资源半径:", Left = 20, Top = y, Width = lblWidth });
            numResourceRadius = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.1m, Maximum = 10m, DecimalPlaces = 1, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numResourceRadius);
            scrollPanel.Controls.Add(new Label { Text = "(越小资源离家越近)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
        }

        private void BuildBackupTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            int y = 20;

            chkAutoBackup = new CheckBox { Text = "启用自动备份存档 (仅在此工具运行时生效)", Left = 20, Top = y, Width = txtWidth, Checked = false };
            tab.Controls.Add(chkAutoBackup);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "自动备份间隔(分钟):", Left = 20, Top = y, Width = lblWidth });
            numBackupInterval = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 1, Maximum = 1440, Value = 60 };
            tab.Controls.Add(numBackupInterval);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "自定义备份目录:", Left = 20, Top = y, Width = lblWidth });
            txtBackupPath = new TextBox { Left = 140, Top = y, Width = txtWidth - 40 };
            tab.Controls.Add(txtBackupPath);
            btnBrowseBackup = new Button { Text = "...", Left = 140 + txtWidth - 30, Top = y - 2, Width = 30 };
            btnBrowseBackup.Click += (s, e) => { if (browseBackupAction != null) browseBackupAction(); };
            tab.Controls.Add(btnBrowseBackup);
            y += rowHeight;
            
            Label lblHint = new Label { 
                Text = "提示：如果留空，默认备份至 ShooterGame/Saved/AutoBackups", 
                Left = 20, Top = y, Width = 500, ForeColor = Color.Gray 
            };
            tab.Controls.Add(lblHint);
        }

        private void BuildAdvancedTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            int y = 20;

            chkPvE = new CheckBox { Text = "启用 PvE 模式 (否则为 PvP)", Left = 20, Top = y, Width = txtWidth, Checked = false };
            tab.Controls.Add(chkPvE);
            y += rowHeight;

            chkBattleEye = new CheckBox { Text = "启用 BattleEye 反作弊", Left = 20, Top = y, Width = txtWidth, Checked = true };
            tab.Controls.Add(chkBattleEye);
            y += rowHeight;

            chkCrossplay = new CheckBox { Text = "启用全平台跨平台联机 (Crossplay)", Left = 20, Top = y, Width = txtWidth, Checked = true };
            tab.Controls.Add(chkCrossplay);
            y += rowHeight;

            chkRCON = new CheckBox { Text = "启用 RCON 远程管理", Left = 20, Top = y, Width = 150, Checked = true };
            tab.Controls.Add(chkRCON);
            tab.Controls.Add(new Label { Text = "端口:", Left = 180, Top = y+4, Width = 40 });
            txtRCONPort = new TextBox { Left = 220, Top = y, Width = 80 };
            tab.Controls.Add(txtRCONPort);
            y += rowHeight;

            chkForceRespawn = new CheckBox { Text = "启动时强制刷新野生恐龙 (-ForceRespawnDinos)", Left = 20, Top = y, Width = 400, Checked = false };
            tab.Controls.Add(chkForceRespawn);
            y += rowHeight;

            tab.Controls.Add(new Label { Text = "额外启动参数:", Left = 20, Top = y, Width = lblWidth });
            txtExtraArgs = new TextBox { Left = 140, Top = y, Width = txtWidth };
            tab.Controls.Add(txtExtraArgs);
        }

        private void BuildHelpTab(TabPage tab)
        {
            string helpText = 
                "【方法一：游戏内直接搜索 (最常用/推荐)】\r\n" +
                "1. 打开《方舟：生存飞升》游戏，点击主菜单的“加入游戏” (Join Game)。\r\n" +
                "2. 在顶部过滤栏中，游戏模式 (Game Mode) 选择“非官方” (Unofficial)。\r\n" +
                "3. 如果您设置了密码，请务必勾选右侧的“显示密码服务器” (Show Password Protected)。\r\n" +
                "4. 在右上角的搜索框中，输入您在工具里填写的“服务器名称”。\r\n" +
                "5. 点击刷新，找到您的服务器后点击加入，输入密码即可。\r\n" +
                "注：服务器刚启动完毕时，可能需要等待 1-3 分钟才能在列表中刷出来。\r\n\r\n" +
                "【方法二：通过控制台命令直连 (仅限 PC/Steam)】\r\n" +
                "1. 打开游戏，进入主界面。\r\n" +
                "2. 按下键盘上的 ~ 键（在 ESC 下面）打开控制台输入框。\r\n" +
                "3. 输入直连代码：\r\n" +
                "   - 本机游玩：open 127.0.0.1:7777\r\n" +
                "   - 局域网游玩：open 局域网IP:7777 (例如 open 192.168.1.100:7777)\r\n" +
                "   - 外网游玩：open 公网IP:7777\r\n" +
                "注：如果有密码，可在IP后加参数，例如：open 127.0.0.1:7777?password=您的密码\r\n\r\n" +
                "【⚠️ 重要的排错提醒：如果朋友搜不到/连不上】\r\n" +
                "如果您自己能进，但朋友进不来，99% 是端口映射（防火墙）的问题：\r\n" +
                "1. 云服务器开服：必须去云控制台“安全组”中，放行 7777 和 27015 的 UDP 协议。\r\n" +
                "2. 家用电脑开服：需要进入路由器后台，将 7777 和 27015 (UDP) 映射到您电脑的局域网 IP 上。\r\n" +
                "   同时别忘了在 Windows Defender 防火墙中放行这两个端口的入站规则。";

            TextBox txtHelp = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = helpText,
                Font = new Font("Microsoft YaHei", 10),
                BackColor = Color.White
            };
            tab.Controls.Add(txtHelp);
        }

        private void BuildBottomControls()
        {
            int y = 470;

            btnLoad = new Button { Text = "加载配置", Left = 30, Top = y, Width = 90, Height = 35 };
            btnLoad.Click += (s, e) => { if (loadAction != null) loadAction(); };
            parentForm.Controls.Add(btnLoad);

            btnSave = new Button { Text = "保存配置", Left = 130, Top = y, Width = 90, Height = 35 };
            btnSave.Click += (s, e) => { if (saveAction != null) saveAction(); };
            parentForm.Controls.Add(btnSave);

            btnStart = new Button { Text = "启动服务器", Left = 240, Top = y, Width = 180, Height = 35, BackColor = Color.LightGreen };
            btnStart.Click += (s, e) => { if (startAction != null) startAction(); };
            parentForm.Controls.Add(btnStart);
            y += 45;

            txtLog = new TextBox { Left = 10, Top = y, Width = 560, Height = 130, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical };
            parentForm.Controls.Add(txtLog);
        }

        public void BindSettingsToUI(AppSettings settings)
        {
            txtPath.Text = settings.ServerPath;
            txtMap.Text = settings.MapName;
            txtName.Text = settings.ServerName;
            txtPass.Text = settings.ServerPassword;
            txtAdminPass.Text = settings.AdminPassword;
            numMaxPlayers.Value = settings.MaxPlayers > 0 ? settings.MaxPlayers : 70;
            
            numXP.Value = settings.XPMultiplier > 0 ? settings.XPMultiplier : 1.0m;
            numTaming.Value = settings.TamingSpeedMultiplier > 0 ? settings.TamingSpeedMultiplier : 1.0m;
            numHarvest.Value = settings.HarvestAmountMultiplier > 0 ? settings.HarvestAmountMultiplier : 1.0m;
            numDifficulty.Value = settings.DifficultyOffset > 0 ? settings.DifficultyOffset : 1.0m;
            numMating.Value = settings.MatingIntervalMultiplier > 0 ? settings.MatingIntervalMultiplier : 1.0m;
            numHatching.Value = settings.EggHatchSpeedMultiplier > 0 ? settings.EggHatchSpeedMultiplier : 1.0m;
            numMature.Value = settings.BabyMatureSpeedMultiplier > 0 ? settings.BabyMatureSpeedMultiplier : 1.0m;
            numDinoCount.Value = settings.DinoCountMultiplier > 0 ? settings.DinoCountMultiplier : 1.0m;
            
            numDayTime.Value = settings.DayTimeSpeedScale > 0 ? settings.DayTimeSpeedScale : 1.0m;
            numNightTime.Value = settings.NightTimeSpeedScale > 0 ? settings.NightTimeSpeedScale : 1.0m;
            numResourceRadius.Value = settings.ResourceNoReplenishRadiusPlayers > 0 ? settings.ResourceNoReplenishRadiusPlayers : 1.0m;
            numResourceRespawn.Value = settings.ResourcesRespawnPeriodMultiplier > 0 ? settings.ResourcesRespawnPeriodMultiplier : 1.0m;

            chkAutoBackup.Checked = settings.EnableAutoBackup;
            numBackupInterval.Value = settings.AutoBackupIntervalMinutes > 0 ? settings.AutoBackupIntervalMinutes : 60;
            txtBackupPath.Text = settings.CustomBackupPath;

            txtPort.Text = settings.Port;
            txtQueryPort.Text = settings.QueryPort;
            txtMods.Text = settings.Mods;

            chkPvE.Checked = !settings.DisablePvE;
            chkBattleEye.Checked = settings.BattleEye;
            chkCrossplay.Checked = settings.Crossplay;
            chkRCON.Checked = settings.EnableRCON;
            txtRCONPort.Text = settings.RCONPort;
            chkForceRespawn.Checked = settings.UseForceRespawnDinos;
            txtExtraArgs.Text = settings.ExtraArgs;
        }

        public void PopulateSettingsFromUI(AppSettings settings)
        {
            settings.ServerPath = txtPath.Text;
            settings.MapName = txtMap.Text;
            settings.ServerName = txtName.Text;
            settings.ServerPassword = txtPass.Text;
            settings.AdminPassword = txtAdminPass.Text;
            settings.MaxPlayers = (int)numMaxPlayers.Value;
            
            settings.XPMultiplier = numXP.Value;
            settings.TamingSpeedMultiplier = numTaming.Value;
            settings.HarvestAmountMultiplier = numHarvest.Value;
            settings.DifficultyOffset = numDifficulty.Value;
            settings.MatingIntervalMultiplier = numMating.Value;
            settings.EggHatchSpeedMultiplier = numHatching.Value;
            settings.BabyMatureSpeedMultiplier = numMature.Value;
            settings.DinoCountMultiplier = numDinoCount.Value;
            
            settings.DayTimeSpeedScale = numDayTime.Value;
            settings.NightTimeSpeedScale = numNightTime.Value;
            settings.ResourceNoReplenishRadiusPlayers = numResourceRadius.Value;
            settings.ResourcesRespawnPeriodMultiplier = numResourceRespawn.Value;

            settings.EnableAutoBackup = chkAutoBackup.Checked;
            settings.AutoBackupIntervalMinutes = (int)numBackupInterval.Value;
            settings.CustomBackupPath = txtBackupPath.Text;

            settings.Port = txtPort.Text;
            settings.QueryPort = txtQueryPort.Text;
            settings.Mods = txtMods.Text;
            
            settings.DisablePvE = !chkPvE.Checked;
            settings.BattleEye = chkBattleEye.Checked;
            settings.Crossplay = chkCrossplay.Checked;
            settings.EnableRCON = chkRCON.Checked;
            settings.RCONPort = txtRCONPort.Text;
            settings.UseForceRespawnDinos = chkForceRespawn.Checked;
            settings.ExtraArgs = txtExtraArgs.Text;
        }

        public void Log(string msg)
        {
            txtLog.AppendText(string.Format("[{0}] {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), msg));
        }
    }
}
