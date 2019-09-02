using System;
using ModCompendiumLibrary;
using ModCompendiumLibrary.Configuration;
using ModCompendiumLibrary.Logging;
using ModCompendiumLibrary.ModSystem;
using ModCompendiumLibrary.ModSystem.Builders;
using ModCompendiumLibrary.ModSystem.Builders.Utilities;
using ModCompendiumLibrary.ModSystem.Loaders;
using ModCompendiumLibrary.ModSystem.Mergers;
namespace GTKFrontend
{
    public partial class GCDialog : Gtk.Dialog
    {
        public String FilePicker(Gtk.FileChooserAction action)
        {
            Gtk.FileChooserDialog fileChooserDialog = new Gtk.FileChooserDialog("select output file", this, action, "open");
            fileChooserDialog.AddButton("open", Gtk.ResponseType.Accept);
            fileChooserDialog.AddButton("cancel", Gtk.ResponseType.Cancel);
            string output = "";
            if (fileChooserDialog.Run() == (int)Gtk.ResponseType.Accept)
            {
                output = fileChooserDialog.Filename;

            }
            fileChooserDialog.Destroy();
            return output;
        }
        private  GameConfig mConfig;
        public GCDialog(GameConfig config)
        {
            Build();

            mConfig = config;
            OutputPath.Text = mConfig.OutputDirectoryPath;
            Gtk.FileChooserButton fileChooserButton = new Gtk.FileChooserButton("select output directory", Gtk.FileChooserAction.SelectFolder);
            if (config.Game == Game.Persona3 || config.Game == Game.Persona4)
            {
                var p34Config = (Persona34GameConfig)config;

                ISOLabel.Visible = true;
                ISOLabel.TooltipText = "Path to an unmodified ISO of " + config.Game.ToString();
                ISOPath.Visible = true;
                if(p34Config.DvdRootOrIsoPath.Length == 0)
                    ISOPath.Text = nameof(p34Config.DvdRootOrIsoPath);
                else
                    ISOPath.Text = p34Config.DvdRootOrIsoPath;
                ISOPicker.Visible = true;

                // Dvd root directory text box
                /*
                TextBox dvdRootPathTextBox;
                {
                    dvdRootPathTextBox = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 20,
                        TextWrapping = TextWrapping.Wrap,
                        Width = 291,
                    };

                    dvdRootPathTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(Persona34GameConfig.DvdRootOrIsoPath)));

                    Grid.SetRow(dvdRootPathTextBox, 2);
                    Grid.SetColumn(dvdRootPathTextBox, 1);
                    ConfigPropertyGrid.Children.Add(dvdRootPathTextBox);
                }
                */

                // Dvd root directory text box button
                /*
                {
                    var dvdRootPathTextBoxButton = new Button()
                    {
                        Content = "...",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    };

                    dvdRootPathTextBoxButton.Click += (s, e) =>
                    {
                        var file = SelectFile(new CommonFileDialogFilter("ISO file", ".iso"));
                        if (file != null)
                        {
                            p34Config.DvdRootOrIsoPath = file;
                            dvdRootPathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    };

                    Grid.SetRow(dvdRootPathTextBoxButton, 2);
                    Grid.SetColumn(dvdRootPathTextBoxButton, 1);
                    ConfigPropertyGrid.Children.Add(dvdRootPathTextBoxButton);
                }
            */
            }


            else if (config.Game == Game.Persona5)
            {
                var ppConfig = (Persona5GameConfig)config;
            }
            else
            {
                var ppConfig = (PersonaPortableGameConfig)config;

                // Add extra row
                // ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());
                CPKLabel.Visible = true;
                CPKLabel.TooltipText = "Path to an unmodified CPK of " + config.Game.ToString();
                CPKPath.Visible = true;
                CPKPicker.Visible = true;



                // Cpk root directory path label
                /*
                {
                    var cpkRootPathLabel = new Label()
                    {
                        Content = "CPK Path",
                        ToolTip = "Path to an unmodified CPK of " + config.Game.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 25,
                        Width = 120
                    };

                    Grid.SetRow(cpkRootPathLabel, 2);
                    Grid.SetColumn(cpkRootPathLabel, 0);
                    ConfigPropertyGrid.Children.Add(cpkRootPathLabel);
                }


                // Cpk root directory text box
                TextBox cpkRootPathTextBox;
                {
                    cpkRootPathTextBox = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 20,
                        TextWrapping = TextWrapping.Wrap,
                        Width = 291,
                    };

                    cpkRootPathTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(PersonaPortableGameConfig.CpkRootOrPath)));

                    Grid.SetRow(cpkRootPathTextBox, 2);
                    Grid.SetColumn(cpkRootPathTextBox, 1);
                    ConfigPropertyGrid.Children.Add(cpkRootPathTextBox);
                }

                // Cpk root directory text box button
                {
                    var cpkRootPathTextBoxButton = new Button()
                    {
                        Content = "...",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    };

                    cpkRootPathTextBoxButton.Click += (s, e) =>
                    {
                        var file = SelectFile(new CommonFileDialogFilter("CPK file", ".cpk"));
                        if (file != null)
                        {
                            ppConfig.CpkRootOrPath = file;
                            cpkRootPathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    };

                    Grid.SetRow(cpkRootPathTextBoxButton, 2);
                    Grid.SetColumn(cpkRootPathTextBoxButton, 1);
                    ConfigPropertyGrid.Children.Add(cpkRootPathTextBoxButton);
                }
                */

                // Add extra row

                CPKCheckbox.Visible = true;

                /*
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());
                // Extraction checkbox label
                {
                    var cpkExtractLabel = new Label()
                    {
                        Content = "Use Extracted Files",
                        ToolTip = "Extract the contents of the CPK at the specified CPK Path",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Width = 120
                    };

                    Grid.SetRow(cpkExtractLabel, 4);
                    Grid.SetColumn(cpkExtractLabel, 0);
                    ConfigPropertyGrid.Children.Add(cpkExtractLabel);
                }

                // Cpk Extraction checkbox
                CheckBox cpkExtract;
                {
                    cpkExtract = new CheckBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                    };

                    cpkExtract.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(PersonaPortableGameConfig.Extract)));

                    Grid.SetRow(cpkExtract, 4);
                    Grid.SetColumn(cpkExtract, 1);
                    ConfigPropertyGrid.Children.Add(cpkExtract);
                }

            }
            */
                if (config.Game != Game.Persona3 && config.Game != Game.Persona4)
                {
                    /*
                    // Add extra row
                    ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                    // Compression checkbox label
                    {
                        var cpkCompressionLabel = new Label()
                        {
                            Content = "Use Compression",
                            ToolTip = "Check if CPK compression is required by " + config.Game.ToString(),
                            HorizontalContentAlignment = HorizontalAlignment.Center,
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 35,
                            Width = 120
                        };

                        Grid.SetRow(cpkCompressionLabel, 3);
                        Grid.SetColumn(cpkCompressionLabel, 0);
                        ConfigPropertyGrid.Children.Add(cpkCompressionLabel);
                    }

                    // Cpk compression checkbox
                    CheckBox cpkCompression;
                    {
                        cpkCompression = new CheckBox()
                        {
                            VerticalContentAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            Height = 35,
                        };

                        cpkCompression.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(PersonaPortableGameConfig.Compression)));

                        Grid.SetRow(cpkCompression, 3);
                        Grid.SetColumn(cpkCompression, 1);
                        ConfigPropertyGrid.Children.Add(cpkCompression);
                    }
                    */
                    CVMCheckbox.Visible = true;
                }
            }
        }
        protected void OnOK(object sender, EventArgs e)
        {
            this.Destroy();
        }
        protected void OnOutputPicker(object sender, EventArgs e)
        {
            String outputfolder = FilePicker(Gtk.FileChooserAction.SelectFolder);
            if(outputfolder.Length != 0) {
                mConfig.OutputDirectoryPath = outputfolder;
                OutputPath.Text = outputfolder;
            }
        }
        protected void OnISOPicker(object sender, EventArgs e)
        {
            var testconfig = (Persona34GameConfig)mConfig;
            mConfig = (Persona34GameConfig)mConfig;
            String isofile = FilePicker(Gtk.FileChooserAction.Open);
            if (isofile.Length != 0)
            {
                testconfig.DvdRootOrIsoPath = isofile;
                mConfig = testconfig;
                ISOPath.Text = isofile;
            }
        }
    }
}




