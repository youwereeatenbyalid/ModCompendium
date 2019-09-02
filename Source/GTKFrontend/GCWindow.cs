using System;
namespace GTKFrontend
{
    public partial class GCWindow : Gtk.Window
    {
        public GCWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
