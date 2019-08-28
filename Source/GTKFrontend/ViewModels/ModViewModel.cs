using System;
using Gtk;
using System.Linq;
using ModCompendiumLibrary.Configuration;
using ModCompendiumLibrary.ModSystem;

namespace GTKFrontend.ViewModels
{
    [Gtk.TreeNode(ListOnly = true)]
    public class ModViewModel :Gtk.TreeNode
    {
        private readonly Mod mMod;
        private readonly GameConfig mConfig;
        [Gtk.TreeNodeValue (Column=0)]
        public bool Enabled
        {
            get => mConfig.IsModEnabled( Id );
            set
            {
                if ( value )
                {
                    mConfig.EnableMod( Id );
                }
                else
                {
                    mConfig.DisableMod( Id );
                }
            }
        }
        [Gtk.TreeNodeValue (Column = 1)]
        public string Title
        {
            get => mMod.Title;
            set => mMod.Title = value;
        }

        [Gtk.TreeNodeValue(Column = 2)]
        public string Description
        {
            get => mMod.Description;
            set => mMod.Description = value;
        }

        [Gtk.TreeNodeValue(Column = 3)]
        public string Version
        {
            get => mMod.Version;
            set => mMod.Version = value;
        }

        [Gtk.TreeNodeValue(Column = 4)]
        public string Author
        {
            get => mMod.Author;
            set => mMod.Author = value;
        }

        [Gtk.TreeNodeValue(Column = 5)]
        public string Date
        {
            get => mMod.Date;
            set => mMod.Date = value;
        }

        [Gtk.TreeNodeValue(Column = 6)]
        public string Url
        {
            get => mMod.Url;
            set => mMod.Url = value;
        }

        [Gtk.TreeNodeValue(Column = 7)]
        public string UpdateUrl
        {
            get => mMod.UpdateUrl;
            set => mMod.UpdateUrl = value;
        }

        public Guid Id => mMod.Id;

        public ModViewModel( Mod model )
        {
            mMod = model;
            mConfig = ConfigStore.Get( model.Game );
        }

        public static explicit operator ModViewModel( Mod mod ) => new ModViewModel( mod );

        public static explicit operator Mod( ModViewModel viewModel ) => viewModel.mMod;
    }
}
