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
        public ComboBox cmbMap;
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

        // Stats
        public NumericUpDown numPlayerWaterDrain;
        public NumericUpDown numPlayerFoodDrain;
        public NumericUpDown numPlayerStaminaDrain;
        public NumericUpDown numPlayerHealthRecovery;

        public NumericUpDown numDinoFoodDrain;
        public NumericUpDown numDinoStaminaDrain;
        public NumericUpDown numDinoHealthRecovery;

        // Advanced Breeding
        public NumericUpDown numBabyCuddleInterval;
        public NumericUpDown numBabyCuddleGrace;
        public NumericUpDown numBabyCuddleLoseImprint;
        public NumericUpDown numBabyImprintScale;
        public NumericUpDown numBabyFoodConsume;

        // Rules
        public CheckBox chkAllowCaveBuilding;
        public CheckBox chkAllowFlyerCarry;
        public CheckBox chkDisableStructurePlacementCollision;
        public CheckBox chkEnableCryoSickness;
        public CheckBox chkShowFloatingDamage;
        public CheckBox chkAllowThirdPerson;
        public CheckBox chkServerCrosshair;
        public CheckBox chkShowMapLocation;
        public NumericUpDown numMaxTamedDinos;

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
            TabPage tabStats = new TabPage("玩家与恐龙");
            TabPage tabRules = new TabPage("游戏选项");
            TabPage tabBackup = new TabPage("自动备份");
            TabPage tabAdvanced = new TabPage("高级与网络");
            TabPage tabHelp = new TabPage("连接教程");

            tabControl.TabPages.Add(tabBasic);
            tabControl.TabPages.Add(tabWorld);
            tabControl.TabPages.Add(tabStats);
            tabControl.TabPages.Add(tabRules);
            tabControl.TabPages.Add(tabBackup);
            tabControl.TabPages.Add(tabAdvanced);
            tabControl.TabPages.Add(tabHelp);
            parentForm.Controls.Add(tabControl);

            BuildBasicTab(tabBasic, lblWidth, txtWidth, rowHeight);
            BuildWorldTab(tabWorld, lblWidth, txtWidth, rowHeight);
            BuildStatsTab(tabStats, lblWidth, txtWidth, rowHeight);
            BuildRulesTab(tabRules, lblWidth, txtWidth, rowHeight);
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
            cmbMap = new ComboBox { Left = 140, Top = y, Width = txtWidth, DropDownStyle = ComboBoxStyle.DropDown };
            cmbMap.Items.AddRange(new object[] {
                "TheIsland_WP",
                "ScorchedEarth_WP",
                "TheCenter_WP",
                "Aberration_WP",
                "Extinction_WP"
            });
            tab.Controls.Add(cmbMap);
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

        private void BuildStatsTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            Panel scrollPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tab.Controls.Add(scrollPanel);
            int y = 10;
            int hintLeft = 250;
            int hintWidth = 280;

            scrollPanel.Controls.Add(new Label { Text = "玩家水分消耗:", Left = 20, Top = y, Width = lblWidth });
            numPlayerWaterDrain = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numPlayerWaterDrain);
            scrollPanel.Controls.Add(new Label { Text = "(越小口渴越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "玩家食物消耗:", Left = 20, Top = y, Width = lblWidth });
            numPlayerFoodDrain = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numPlayerFoodDrain);
            scrollPanel.Controls.Add(new Label { Text = "(越小饥饿越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "玩家耐力消耗:", Left = 20, Top = y, Width = lblWidth });
            numPlayerStaminaDrain = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numPlayerStaminaDrain);
            scrollPanel.Controls.Add(new Label { Text = "(越小耐力掉得越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "玩家生命恢复:", Left = 20, Top = y, Width = lblWidth });
            numPlayerHealthRecovery = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numPlayerHealthRecovery);
            scrollPanel.Controls.Add(new Label { Text = "(越大回血越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "恐龙食物消耗:", Left = 20, Top = y, Width = lblWidth });
            numDinoFoodDrain = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numDinoFoodDrain);
            scrollPanel.Controls.Add(new Label { Text = "(越小恐龙饥饿越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "恐龙耐力消耗:", Left = 20, Top = y, Width = lblWidth });
            numDinoStaminaDrain = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numDinoStaminaDrain);
            scrollPanel.Controls.Add(new Label { Text = "(越小恐龙耐力掉得越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "恐龙生命恢复:", Left = 20, Top = y, Width = lblWidth });
            numDinoHealthRecovery = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 10m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numDinoHealthRecovery);
            scrollPanel.Controls.Add(new Label { Text = "(越大恐龙回血越快)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "--- 留痕高级设置 ---", Left = 20, Top = y, Width = 200, ForeColor = Color.Blue });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "留痕间隔倍率:", Left = 20, Top = y, Width = lblWidth });
            numBabyCuddleInterval = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 100m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numBabyCuddleInterval);
            scrollPanel.Controls.Add(new Label { Text = "(越小越频繁要求留痕)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "留痕宽限期倍率:", Left = 20, Top = y, Width = lblWidth });
            numBabyCuddleGrace = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 100m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numBabyCuddleGrace);
            scrollPanel.Controls.Add(new Label { Text = "(越大允许你迟到的时间越久)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "留痕掉分速度倍率:", Left = 20, Top = y, Width = lblWidth });
            numBabyCuddleLoseImprint = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 100m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numBabyCuddleLoseImprint);
            scrollPanel.Controls.Add(new Label { Text = "(越小错过留痕掉的分越少)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "单次留痕加成倍率:", Left = 20, Top = y, Width = lblWidth });
            numBabyImprintScale = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 1000m, DecimalPlaces = 2, Increment = 0.5m, Value = 1.0m };
            scrollPanel.Controls.Add(numBabyImprintScale);
            scrollPanel.Controls.Add(new Label { Text = "(越大每次给的留痕度越多)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "幼龙食物消耗:", Left = 20, Top = y, Width = lblWidth });
            numBabyFoodConsume = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 0.01m, Maximum = 100m, DecimalPlaces = 2, Increment = 0.1m, Value = 1.0m };
            scrollPanel.Controls.Add(numBabyFoodConsume);
            scrollPanel.Controls.Add(new Label { Text = "(越小宝宝饿得越慢)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
        }

        private void BuildRulesTab(TabPage tab, int lblWidth, int txtWidth, int rowHeight)
        {
            Panel scrollPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            tab.Controls.Add(scrollPanel);
            int y = 10;
            int hintLeft = 250;
            int hintWidth = 280;

            chkAllowCaveBuilding = new CheckBox { Text = "允许在神器矿洞内建筑", Left = 20, Top = y, Width = 220, Checked = false };
            scrollPanel.Controls.Add(chkAllowCaveBuilding);
            scrollPanel.Controls.Add(new Label { Text = "(仅在PvE模式下生效)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkAllowFlyerCarry = new CheckBox { Text = "允许飞行龙抓人/野龙", Left = 20, Top = y, Width = 220, Checked = false };
            scrollPanel.Controls.Add(chkAllowFlyerCarry);
            scrollPanel.Controls.Add(new Label { Text = "(PvE模式默认关闭，开启后可抓野龙)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkDisableStructurePlacementCollision = new CheckBox { Text = "允许建筑穿模", Left = 20, Top = y, Width = 220, Checked = false };
            scrollPanel.Controls.Add(chkDisableStructurePlacementCollision);
            scrollPanel.Controls.Add(new Label { Text = "(可将建筑卡入地形/石头中)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkEnableCryoSickness = new CheckBox { Text = "启用低温症", Left = 20, Top = y, Width = 220, Checked = true };
            scrollPanel.Controls.Add(chkEnableCryoSickness);
            scrollPanel.Controls.Add(new Label { Text = "(关闭后丢出冷冻舱恐龙不会眩晕)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkShowFloatingDamage = new CheckBox { Text = "显示浮动伤害数字", Left = 20, Top = y, Width = 220, Checked = false };
            scrollPanel.Controls.Add(chkShowFloatingDamage);
            scrollPanel.Controls.Add(new Label { Text = "(攻击时头上冒出的伤害值)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkAllowThirdPerson = new CheckBox { Text = "允许第三人称视角", Left = 20, Top = y, Width = 220, Checked = true };
            scrollPanel.Controls.Add(chkAllowThirdPerson);
            scrollPanel.Controls.Add(new Label { Text = "(允许玩家滚轮切换到第三人称)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkServerCrosshair = new CheckBox { Text = "显示服务器准星", Left = 20, Top = y, Width = 220, Checked = true };
            scrollPanel.Controls.Add(chkServerCrosshair);
            scrollPanel.Controls.Add(new Label { Text = "(屏幕中间的十字准星)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            chkShowMapLocation = new CheckBox { Text = "在地图上显示玩家位置", Left = 20, Top = y, Width = 220, Checked = true };
            scrollPanel.Controls.Add(chkShowMapLocation);
            scrollPanel.Controls.Add(new Label { Text = "(按M打开地图能看到自己的大头针)", Left = hintLeft, Top = y+4, Width = hintWidth, ForeColor = Color.Gray });
            y += rowHeight;

            scrollPanel.Controls.Add(new Label { Text = "部落最大恐龙数:", Left = 20, Top = y, Width = lblWidth });
            numMaxTamedDinos = new NumericUpDown { Left = 140, Top = y, Width = 100, Minimum = 1, Maximum = 10000, Value = 500 };
            scrollPanel.Controls.Add(numMaxTamedDinos);
            scrollPanel.Controls.Add(new Label { Text = "(每个部落允许拥有的最大恐龙数量)", Left = hintLeft, Top = y+2, Width = hintWidth, ForeColor = Color.Gray });
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
            cmbMap.Text = settings.MapName;
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

            numPlayerWaterDrain.Value = settings.PlayerCharacterWaterDrainMultiplier > 0 ? settings.PlayerCharacterWaterDrainMultiplier : 1.0m;
            numPlayerFoodDrain.Value = settings.PlayerCharacterFoodDrainMultiplier > 0 ? settings.PlayerCharacterFoodDrainMultiplier : 1.0m;
            numPlayerStaminaDrain.Value = settings.PlayerCharacterStaminaDrainMultiplier > 0 ? settings.PlayerCharacterStaminaDrainMultiplier : 1.0m;
            numPlayerHealthRecovery.Value = settings.PlayerCharacterHealthRecoveryMultiplier > 0 ? settings.PlayerCharacterHealthRecoveryMultiplier : 1.0m;

            numDinoFoodDrain.Value = settings.DinoCharacterFoodDrainMultiplier > 0 ? settings.DinoCharacterFoodDrainMultiplier : 1.0m;
            numDinoStaminaDrain.Value = settings.DinoCharacterStaminaDrainMultiplier > 0 ? settings.DinoCharacterStaminaDrainMultiplier : 1.0m;
            numDinoHealthRecovery.Value = settings.DinoCharacterHealthRecoveryMultiplier > 0 ? settings.DinoCharacterHealthRecoveryMultiplier : 1.0m;

            numBabyCuddleInterval.Value = settings.BabyCuddleIntervalMultiplier > 0 ? settings.BabyCuddleIntervalMultiplier : 1.0m;
            numBabyCuddleGrace.Value = settings.BabyCuddleGracePeriodMultiplier > 0 ? settings.BabyCuddleGracePeriodMultiplier : 1.0m;
            numBabyCuddleLoseImprint.Value = settings.BabyCuddleLoseImprintQualitySpeedMultiplier > 0 ? settings.BabyCuddleLoseImprintQualitySpeedMultiplier : 1.0m;
            numBabyImprintScale.Value = settings.BabyImprintingStatScaleMultiplier > 0 ? settings.BabyImprintingStatScaleMultiplier : 1.0m;
            numBabyFoodConsume.Value = settings.BabyFoodConsumptionSpeedMultiplier > 0 ? settings.BabyFoodConsumptionSpeedMultiplier : 1.0m;

            chkAllowCaveBuilding.Checked = settings.AllowCaveBuildingPvE;
            chkAllowFlyerCarry.Checked = settings.AllowFlyerCarryPvE;
            chkDisableStructurePlacementCollision.Checked = settings.DisableStructurePlacementCollision;
            chkEnableCryoSickness.Checked = settings.EnableCryoSicknessPVE;
            chkShowFloatingDamage.Checked = settings.ShowFloatingDamageText;
            chkAllowThirdPerson.Checked = settings.AllowThirdPersonPlayer;
            chkServerCrosshair.Checked = settings.ServerCrosshair;
            chkShowMapLocation.Checked = settings.ShowMapPlayerLocation;
            numMaxTamedDinos.Value = settings.MaxPersonalTamedDinos > 0 ? settings.MaxPersonalTamedDinos : 500;

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
            settings.MapName = cmbMap.Text;
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

            settings.PlayerCharacterWaterDrainMultiplier = numPlayerWaterDrain.Value;
            settings.PlayerCharacterFoodDrainMultiplier = numPlayerFoodDrain.Value;
            settings.PlayerCharacterStaminaDrainMultiplier = numPlayerStaminaDrain.Value;
            settings.PlayerCharacterHealthRecoveryMultiplier = numPlayerHealthRecovery.Value;

            settings.DinoCharacterFoodDrainMultiplier = numDinoFoodDrain.Value;
            settings.DinoCharacterStaminaDrainMultiplier = numDinoStaminaDrain.Value;
            settings.DinoCharacterHealthRecoveryMultiplier = numDinoHealthRecovery.Value;

            settings.BabyCuddleIntervalMultiplier = numBabyCuddleInterval.Value;
            settings.BabyCuddleGracePeriodMultiplier = numBabyCuddleGrace.Value;
            settings.BabyCuddleLoseImprintQualitySpeedMultiplier = numBabyCuddleLoseImprint.Value;
            settings.BabyImprintingStatScaleMultiplier = numBabyImprintScale.Value;
            settings.BabyFoodConsumptionSpeedMultiplier = numBabyFoodConsume.Value;

            settings.AllowCaveBuildingPvE = chkAllowCaveBuilding.Checked;
            settings.AllowFlyerCarryPvE = chkAllowFlyerCarry.Checked;
            settings.DisableStructurePlacementCollision = chkDisableStructurePlacementCollision.Checked;
            settings.EnableCryoSicknessPVE = chkEnableCryoSickness.Checked;
            settings.ShowFloatingDamageText = chkShowFloatingDamage.Checked;
            settings.AllowThirdPersonPlayer = chkAllowThirdPerson.Checked;
            settings.ServerCrosshair = chkServerCrosshair.Checked;
            settings.ShowMapPlayerLocation = chkShowMapLocation.Checked;
            settings.MaxPersonalTamedDinos = (int)numMaxTamedDinos.Value;

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
