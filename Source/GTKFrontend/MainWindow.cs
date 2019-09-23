using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Gtk;
using GTKFrontend.ViewModels;
using GTKFrontend.Configs;
using ModCompendiumLibrary;
using ModCompendiumLibrary.Configuration;
using ModCompendiumLibrary.Logging;
using ModCompendiumLibrary.ModSystem;
using ModCompendiumLibrary.ModSystem.Builders;
using ModCompendiumLibrary.ModSystem.Builders.Utilities;
using ModCompendiumLibrary.ModSystem.Loaders;
using ModCompendiumLibrary.ModSystem.Mergers;
public partial class MainWindow : Gtk.Window
{

    private static readonly string[] sGameNames =
        {
            "Persona 3",
            "Persona 3 Portable",
            "Persona 4",
            "Persona 4 Golden",
            "Persona 4 Dancing All Night",
            "Persona 5",
            "Persona 3 Dancing Moon Night",
            "Persona 5 Dancing Star Night",
            "Persona Q",
            "Persona Q2",
            "Catherine Full Body"
        };
    //specifies the game being currently edited.
    public Game SelectedGame { get; private set; }
    //list of all active mods(?)
    public List<ModViewModel> Mods { get; private set; }
    //loads the GameConfig from the ModCompendiumLibrary
    public GameConfig GameConfig { get; private set; }
    public MainWindowConfig Config { get; private set; }
    //retrieves the modviewmodel of the selected item in the datagrid
    public ModViewModel SelectedModViewModel => (ModViewModel)ModGrid.NodeSelection.SelectedNode;
    //and then retrieves the mod from that view model?
    public Mod SelectedMod => (Mod)SelectedModViewModel;
    private void InitializeGameComboBox()
    {
        GameSelect.Active = Math.Max(0, (int)Config.SelectedGame - 1);
    }
    /// <summary>
    /// Refreshes the mods.
    /// Clears the nodestore,
    /// Sets up the modviewmodels from the moddatabase
    /// adds them to a list and the nodestore
    /// </summary>
    private void RefreshMods()
    {
        ModGrid.NodeStore.Clear();
        Mods = ModDatabase.Get(SelectedGame)
                          .OrderBy(x => GameConfig.GetModPriority(x.Id))
                          .Select(x => new ModViewModel(x))
                          .ToList();
        Mods.ForEach(x => ModGrid.NodeStore.AddNode(x));

    }

    private void RefreshModDatabase()
    {
        ModDatabase.Initialize();
        RefreshMods();
    }
    /// <summary>
    /// Updates the game config enabled mods.
    /// </summary>
    /// <returns><c>true</c>, if game config enabled mods was updated, <c>false</c> otherwise.</returns>
    private bool UpdateGameConfigEnabledMods()
    {
        var enabledMods = Mods.Where(x => x.Enabled)
                              .Select(x => x.Id)
                              .ToList();

        GameConfig.ClearEnabledMods();

        if (enabledMods.Count == 0)
            return false;

        enabledMods.ForEach(GameConfig.EnableMod);

        return true;
    }
    /// <summary>
    /// Updates the window config mod order.
    /// </summary>
    private void UpdateWindowConfigModOrder()
    {
        for (var i = 0; i < Mods.Count; i++)
        {
            var mod = Mods[i];
            GameConfig.SetModPriority(mod.Id, i);
        }
    }
    /// <summary>
    /// Updates the config changes and saves them to the config store.
    /// </summary>
    private void UpdateConfigChangesAndSave()
    {
        UpdateGameConfigEnabledMods();
        UpdateWindowConfig();
        ConfigStore.Save();
    }

    //Updates the Window Config
    private void UpdateWindowConfig()
    {
        UpdateWindowConfigModOrder();
        Config.SelectedGame = SelectedGame;
    }

    /// <summary>
    /// Initializes the mod grid.
    /// Sets the nodestore up to store Mod View Models, 
    /// Then adds the appropriate columns to the ModGrid.
    /// </summary>
    private void InitializeModGrid() {
        ModGrid.NodeSelection.Changed += onModSelection;
        NodeStore modStore = new NodeStore(typeof(ModViewModel));
        ModGrid.NodeStore = modStore;
        //this line currently works around a bug in mono 5. Should be able to remove it once mono 6 is publicly used.
        typeof(NodeView).GetField("store", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ModGrid, modStore);

        CellRendererToggle modcheckbox = new CellRendererToggle();
        modcheckbox.Activatable = true;
        modcheckbox.Toggled += delegate (object o, Gtk.ToggledArgs args) {
            var node = modStore.GetNode(new Gtk.TreePath(args.Path)) as ModViewModel;
            node.Enabled = !node.Enabled;
        };
        //modcheckbox.
        TreeViewColumn modEnabled = new TreeViewColumn("Enabled", modcheckbox, "active", 0);
        //modEnabled.AddAttribute(modcheckbox, "active", 0);
        ModGrid.AppendColumn(modEnabled);
        //modEnabled.SetCellDataFunc(modcheckbox, "activatable", true,);
        ModGrid.AppendColumn("title", new Gtk.CellRendererText(), "text", 1);
        ModGrid.AppendColumn("description", new Gtk.CellRendererText(), "text", 2);
        ModGrid.AppendColumn("version", new Gtk.CellRendererText(), "text", 3);
        ModGrid.AppendColumn("author", new Gtk.CellRendererText(), "text", 4);
        ModGrid.AppendColumn("date", new Gtk.CellRendererText(), "text", 5);
        ModGrid.AppendColumn("url", new Gtk.CellRendererText(), "text", 6);
        ModGrid.AppendColumn("updateurl", new Gtk.CellRendererText(), "text", 7);
       // ModGrid.AppendColumn("test", new Gtk.CellRendererToggle();
        ModGrid.ShowAll();
    }

    /// <summary>
    /// Initializes the log.
    /// </summary>
    private void InitializeLog()
    {
        Log.MessageBroadcasted += Log_MessageBroadcasted;
    }
 

    /// <summary>
    /// Logs the message broadcasted.
    /// Tracks the severity of the log message and assigns it an appropriate
    /// Indicator and Color.
    /// Appends the log message to the TextView.
    /// It should be coloring it, but it isn't right now.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">E.</param>
    private void Log_MessageBroadcasted(object sender, MessageBroadcastedEventArgs e)
    {
        if (e.Severity == Severity.Trace)
            return;

               string severityIndicator;
        ConsoleColor color;
        TextTag logcolor = new TextTag("color");
               switch (e.Severity)
               {
                   case Severity.Trace:
                       color = ConsoleColor.Blue;
                logcolor.Foreground = "blue";
                severityIndicator = "T";
                       break;
                   case Severity.Warning:
                       color = ConsoleColor.Yellow;
                logcolor.Foreground = "yellow";
                       severityIndicator = "!";
                       break;
                   case Severity.Error:
                       color = ConsoleColor.Red;
                logcolor.Foreground = "red";
                severityIndicator = "E";
                       break;
                   case Severity.Fatal:
                       color = ConsoleColor.Magenta;
                       severityIndicator = "F";
                       break;

                   default:
                       color = ConsoleColor.White;
                logcolor.Foreground = "green";
                severityIndicator = "I";
                       break;
               }
        TextIter logend = LogView.Buffer.EndIter;
        LogView.Buffer.Insert(ref logend, $"[{e.Channel.Name}]:{severityIndicator}: {e.Message}\n");
      //  LogView.Buffer.ApplyTag(logcolor, logend, LogView.Buffer.EndIter);
        Console.ForegroundColor = color;
        Console.WriteLine($"[{e.Channel.Name}]:{severityIndicator}: {e.Message}\n");
    }



    private static void RunModScripts(List<Mod> enabledMods, string scriptFileName)
    {
        foreach (var enabledMod in enabledMods)
        {
            var scriptFilePath = System.IO.Path.Combine(enabledMod.BaseDirectory, scriptFileName);
            if (File.Exists(scriptFilePath))
            {
                try
                {
                    var info = new ProcessStartInfo(System.IO.Path.GetFullPath(scriptFilePath));
                    info.WorkingDirectory = System.IO.Path.GetFullPath(enabledMod.BaseDirectory);

                    var process = Process.Start(info);
                    process?.WaitForExit();
                }
                catch (Exception)
                {
                }
            }
        }
    }

    private static void RunPostBuildScript(string scriptFileName)
    {
        var scriptFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), scriptFileName);
        if (File.Exists(scriptFilePath))
        {
            try
            {
                var info = new ProcessStartInfo(System.IO.Path.GetFullPath(scriptFilePath));
                info.WorkingDirectory = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());

                var process = Process.Start(info);
                process?.WaitForExit();
            }
            catch (Exception)
            {
            }
        }
    }
    /// <summary>
    /// Runs when the Main Window Initializes.
    /// </summary>
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {

        Build();
        this.Deleted.Sensitive = false;
        InitializeLog();
        //gets the Assembly version
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        //appends it to mod compendium in the window title.
        Title = $"Mod Compendium {version.Major}.{version.Minor}.{version.Revision}";
        //retrieves the the main window configuration from the config store.
        Config = ConfigStore.Get<MainWindowConfig>();
        //retrieves the gameconfig previously in use from the combo box
        //which is set from the Main Window (somehow)
        SelectedGame = (Game)(GameSelect.Active + 1);
        //retrieves the gameconfig previously in use.
        GameConfig = ConfigStore.Get(SelectedGame);
        //starts the ModGrid.
        InitializeModGrid();
        //Refreshes the mod database to collect the correct mods
        RefreshModDatabase();
        //starts the combobox (shouldn't it be already be initialized?)
        InitializeGameComboBox();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnSettings(object sender, EventArgs e)

    {
        Gtk.Dialog testdialog = new GTKFrontend.GCDialog(GameConfig);
        testdialog.Show();
    }

    protected void OnRefresh(object sender, EventArgs e)
    {
        // Save
        UpdateConfigChangesAndSave();

        // Reload
        ConfigStore.Load();
        RefreshModDatabase();
    }

    protected void OnGameChange(object sender, EventArgs e)
    {
        SelectedGame = (Game)(GameSelect.Active + 1);
        GameConfig = ConfigStore.Get(SelectedGame);
        RefreshMods();
        Console.WriteLine("Now Modding:" + SelectedGame);
    }

    protected void OnNewMod(object sender, EventArgs e)
    {

    }

    protected void OnDeleteMod(object sender, EventArgs e)
    {
        DeleteSelectedMod();
    }

    protected void WindowClose(object o, DeleteEventArgs args)
    {
        UpdateConfigChangesAndSave();
        Environment.Exit(0);
    }



    protected void BuildButtonClick(object sender, EventArgs e)
        {
        MessageDialog delegateddialog;
        if (string.IsNullOrWhiteSpace(GameConfig.OutputDirectoryPath))
        {
            Console.WriteLine("Please specify an output directory in the settings.");
            delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "Please specify an output directory in the settings.");
            delegateddialog.Run();
            delegateddialog.Destroy();
            //MessageBox.Show(this, "Please specify an output directory in the settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (Mods.Count == 0)
        {

            Console.WriteLine("No mods are available.");
            delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "No mods are available.");
            delegateddialog.Run();
            delegateddialog.Destroy();
            //MessageBox.Show(this, "No mods are available.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (!UpdateGameConfigEnabledMods())
        {
            Console.WriteLine("No mods are enabled.");
            delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "No mods are enabled.");
            delegateddialog.Run();
            delegateddialog.Destroy();
            // MessageBox.Show(this, "No mods are enabled.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }


        var task = Task.Factory.StartNew(() =>
        {
            var enabledMods = GameConfig.ModConfigs.Where(x => x.Enabled)
                                        .OrderBy(x => x.Priority)
                                        .Select(x => x.ModId)
                                        .Select(x => ModDatabase.Get(x))
                                        .ToList();

            Log.General.Info("Building mods:");
            foreach (var enabledMod in enabledMods)
                Log.General.Info($"\t{enabledMod.Title}");

            // Run prebuild scripts
            RunModScripts(enabledMods, "prebuild.bat");

            var merger = new TopToBottomModMerger();
            var merged = merger.Merge(enabledMods);

            // Todo
            var builder = ModBuilderManager.GetCompatibleModBuilders(SelectedGame).First().Create();
            if (UltraISOUtility.Available)
            {
                if (SelectedGame == Game.Persona3)
                    builder = new Persona3IsoModBuilder();
                else if (SelectedGame == Game.Persona4)
                    builder = new Persona4IsoModBuilder();
            }

            Log.General.Info($"Output path: {GameConfig.OutputDirectoryPath}");

#if !DEBUG
                try
#endif
            {
                builder.Build(merged, GameConfig.OutputDirectoryPath);
            }
#if !DEBUG
                catch ( InvalidConfigException exception )
                {
                Gtk.Application.Invoke(delegate
                {
                    delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, $"SelectedGame configuration is invalid.\n{exception.Message}");
                    delegateddialog.Run();
                    delegateddialog.Destroy();
                });
                    return false;
                }
                catch ( MissingFileException exception )
                {
                Gtk.Application.Invoke(delegate
                {
                    delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, $"A file is missing:\n{exception.Message}");
                    delegateddialog.Run();
                    delegateddialog.Destroy();
                });

                 
                    return false;
                }
                catch ( Exception exception )
                {
                Gtk.Application.Invoke(delegate
                {
                    delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, $"Unhandled exception occured while building:\n{exception.Message}\n{exception.StackTrace}");
                    delegateddialog.Run();
                    delegateddialog.Destroy();
                });


#if DEBUG
                    throw;
#endif

#pragma warning disable 162
                    return false;
#pragma warning restore 162
                }
#endif

            return true;
        }, TaskCreationOptions.AttachedToParent);

        task.ContinueWith((t) =>
        {
            if (t.Result)
                {
                Gtk.Application.Invoke(delegate {
                    delegateddialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Done building!");
                    delegateddialog.Run();
                    delegateddialog.Destroy();
                });

                RunPostBuildScript("postbuild.bat");
                }
            
        });}





    [GLib.ConnectBeforeAttribute]
    protected void onModGridContext(object o, ButtonPressEventArgs args)
    {
        if (args.Event.Button == 3 && ModGrid.NodeSelection.SelectedNode != null)
        {
            Menu m = new Menu();
            MenuItem opendirectory = new MenuItem("Open Directory");
            MenuItem deletemod = new MenuItem("Delete");
            deletemod.ButtonPressEvent += OnDeleteModButtonPressed;
            opendirectory.ButtonPressEvent += OnOpenDirectoryClick;
            m.Add(opendirectory);
            m.Add(deletemod);
            m.ShowAll();
            m.Popup();
        }

    }
    protected void OnDeleteModButtonPressed(object o, ButtonPressEventArgs args) 
    {
        DeleteSelectedMod();
    }

    private void DeleteSelectedMod()
    {
        Gtk.MessageDialog deletewarning = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Warning, ButtonsType.OkCancel, "Are you sure you want to delete this mod? The data will be lost forever.");
        if (deletewarning.Run() == (int)ResponseType.Ok)
        {
            Log.General.Warning($"Deleting mod directory: {SelectedMod.BaseDirectory}");
            Directory.Delete(SelectedMod.BaseDirectory, true);
            RefreshModDatabase();
        }
        deletewarning.Destroy();
    }

    protected void OnOpenDirectoryClick(object sender, ButtonPressEventArgs e)
    {
        Process.Start(SelectedMod.BaseDirectory);
    }

    protected void newmodclick(object sender, EventArgs e)
    {
        GTKFrontend.NewModDialog newMod = new GTKFrontend.NewModDialog();
        if (newMod.Run() != (int)ResponseType.Ok) 
        {
            newMod.Destroy(); 
            return; 
        }

        // Get unique directory
        string modPath = System.IO.Path.Combine(ModDatabase.ModDirectory, SelectedGame.ToString(), newMod.ModTitle);
        if (Directory.Exists(modPath))
        {
            var newModPath = modPath;
            int i = 0;

            while (Directory.Exists(newModPath))
                newModPath = modPath + "_" + i++;

            modPath = newModPath;
        }

        // Build mod
        var mod = new ModBuilder()
            .SetGame(SelectedGame)
            .SetTitle(newMod.ModTitle)
            .SetDescription(newMod.Description)
            .SetVersion(newMod.Version)
            .SetDate(DateTime.UtcNow.ToShortDateString())
            .SetAuthor(newMod.Author)
            .SetUrl(newMod.Url)
            .SetUpdateUrl(newMod.UpdateUrl)
            .SetBaseDirectoryPath(modPath)
            .Build();

        // Do actual saving
        var modLoader = new XmlModLoader();
        modLoader.Save(mod);

        // Reload
        RefreshModDatabase();
    }

    protected void onModSelection(object o, EventArgs args)
    {
        this.Deleted.Sensitive = (ModGrid.NodeSelection.SelectedNode != null);

    }
}
