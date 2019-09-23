using System;
namespace GTKFrontend
{
    public partial class NewModDialog : Gtk.Dialog
    {
        public string ModTitle => titleentry.Text;

        public string Description => descriptionentry.Text;

        public string Version => versionentry.Text;

        public string Author => authorentry.Text;

        public string Url => urlentry.Text;
        public bool creation = false;
        public string UpdateUrl => updateurlentry.Text;
        public NewModDialog()
        {
            this.Build();
            buttonOk.Sensitive = false;
        }


        protected void ontitletextchange(object sender, EventArgs e)
        {
            buttonOk.Sensitive = titleentry.Text.Length != 0;
        }
    }
}
