using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ModCompendiumCLICore.Configs;
using ModCompendiumCLICore.ViewModels;
using ModCompendiumLibrary;
using ModCompendiumLibrary.Configuration;
using ModCompendiumLibrary.Logging;
using ModCompendiumLibrary.ModSystem;
using ModCompendiumLibrary.ModSystem.Builders;
using ModCompendiumLibrary.ModSystem.Builders.Utilities;
using ModCompendiumLibrary.ModSystem.Loaders;
using ModCompendiumLibrary.ModSystem.Mergers;
using ModCompendiumLibrary.VirtualFileSystem;

namespace ModCompendiumCLI
{
 
       public class Initializer {
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
        public Game SelectedGame { get; private set; }
        //list of all active mods(?)
        public List<ModViewModel> Mods { get; private set; }
        //loads the GameConfig from the ModCompendiumLibrary
        public GameConfig GameConfig { get; private set; }
        public MainWindowConfig Config { get; private set; }


        public Initializer()
        {
            Console.WriteLine("Yo you wanna learn how to do a fuckin infinite?");
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine($"Mod Compendium {version.Major}.{version.Minor}.{version.Revision}");
            Config = ConfigStore.Get<MainWindowConfig>();
            Console.WriteLine("Currently modding: " + Config.SelectedGame);
        }
        public void RefreshMods()
        {
            Mods = ModDatabase.Get(SelectedGame)
                              .OrderBy(x => GameConfig.GetModPriority(x.Id))
                              .Select(x => new ModViewModel(x))
                              .ToList();
        }

        public void RefreshModDatabase()
        {
            ModDatabase.Initialize();
            RefreshMods();
        }

        public bool UpdateGameConfigEnabledMods()
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

        private void UpdateWindowConfigModOrder()
        {
            for (var i = 0; i < Mods.Count; i++)
            {
                var mod = Mods[i];
                GameConfig.SetModPriority(mod.Id, i);
            }
        }

        private void UpdateConfigChangesAndSave()
        {
            UpdateGameConfigEnabledMods();
            UpdateWindowConfig();
            ConfigStore.Save();
        }

        private void UpdateWindowConfig()
        {
            UpdateWindowConfigModOrder();
            Config.SelectedGame = SelectedGame;
        }
        private static void RunModScripts(List<Mod> enabledMods, string scriptFileName)
        {
            foreach (var enabledMod in enabledMods)
            {
                var scriptFilePath = Path.Combine(enabledMod.BaseDirectory, scriptFileName);
                if (File.Exists(scriptFilePath))
                {
                    try
                    {
                        var info = new ProcessStartInfo(Path.GetFullPath(scriptFilePath));
                        info.WorkingDirectory = Path.GetFullPath(enabledMod.BaseDirectory);

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
            var scriptFilePath = Path.Combine(Directory.GetCurrentDirectory(), scriptFileName);
            if (File.Exists(scriptFilePath))
            {
                try
                {
                    var info = new ProcessStartInfo(Path.GetFullPath(scriptFilePath));
                    info.WorkingDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());

                    var process = Process.Start(info);
                    process?.WaitForExit();
                }
                catch (Exception)
                {
                }
            }
        }
    }


    internal class Program
    {


        private static void Main(string[] args)
        {
            Log.MessageBroadcasted += (s, e) =>
            {
                var currentColor = Console.ForegroundColor;

                switch (e.Severity)
                {
                    case Severity.Trace:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case Severity.Info:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case Severity.Warning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case Severity.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case Severity.Fatal:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                }

                Console.WriteLine($"{e.Channel.Name}: {e.Severity}: {e.Message}");

                Console.ForegroundColor = currentColor;

            };
            Initializer test = new Initializer();
        }

    }
}

