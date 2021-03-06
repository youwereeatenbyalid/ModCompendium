
// This file has been generated by the GUI designer. Do not modify.
namespace GTKFrontend
{
	public partial class GCDialog
	{
		private global::Gtk.Table table1;

		private global::Gtk.CheckButton CPKCheckbox;

		private global::Gtk.Label CPKLabel;

		private global::Gtk.Entry CPKPath;

		private global::Gtk.Button CPKPicker;

		private global::Gtk.CheckButton CVMCheckbox;

		private global::Gtk.Label ISOLabel;

		private global::Gtk.Entry ISOPath;

		private global::Gtk.Button ISOPicker;

		private global::Gtk.Label OutputLabel;

		private global::Gtk.Entry OutputPath;

		private global::Gtk.Button OutputPicker;

		private global::Gtk.Label WinePrefixLabel;

		private global::Gtk.Entry WinePrefixPath;

		private global::Gtk.Button WinePrefixPicker;

		private global::Gtk.Button buttonOk;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget GTKFrontend.GCDialog
			this.Name = "GTKFrontend.GCDialog";
			this.Title = global::Mono.Unix.Catalog.GetString("Selected Game Config");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Internal child GTKFrontend.GCDialog.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(6)), ((uint)(3)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.CPKCheckbox = new global::Gtk.CheckButton();
			this.CPKCheckbox.CanFocus = true;
			this.CPKCheckbox.Name = "CPKCheckbox";
			this.CPKCheckbox.Label = global::Mono.Unix.Catalog.GetString("Extract CPK");
			this.CPKCheckbox.DrawIndicator = true;
			this.CPKCheckbox.UseUnderline = true;
			this.table1.Add(this.CPKCheckbox);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.CPKCheckbox]));
			w2.TopAttach = ((uint)(4));
			w2.BottomAttach = ((uint)(5));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.CPKLabel = new global::Gtk.Label();
			this.CPKLabel.Name = "CPKLabel";
			this.CPKLabel.LabelProp = global::Mono.Unix.Catalog.GetString("CPK File");
			this.table1.Add(this.CPKLabel);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.CPKLabel]));
			w3.TopAttach = ((uint)(2));
			w3.BottomAttach = ((uint)(3));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.CPKPath = new global::Gtk.Entry();
			this.CPKPath.CanFocus = true;
			this.CPKPath.Name = "CPKPath";
			this.CPKPath.IsEditable = true;
			this.CPKPath.InvisibleChar = '•';
			this.table1.Add(this.CPKPath);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.CPKPath]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.CPKPicker = new global::Gtk.Button();
			this.CPKPicker.CanFocus = true;
			this.CPKPicker.Name = "CPKPicker";
			this.CPKPicker.UseStock = true;
			this.CPKPicker.UseUnderline = true;
			this.CPKPicker.Label = "gtk-open";
			this.table1.Add(this.CPKPicker);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.CPKPicker]));
			w5.TopAttach = ((uint)(2));
			w5.BottomAttach = ((uint)(3));
			w5.LeftAttach = ((uint)(2));
			w5.RightAttach = ((uint)(3));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.CVMCheckbox = new global::Gtk.CheckButton();
			this.CVMCheckbox.CanFocus = true;
			this.CVMCheckbox.Name = "CVMCheckbox";
			this.CVMCheckbox.Label = global::Mono.Unix.Catalog.GetString("Compress CVM");
			this.CVMCheckbox.DrawIndicator = true;
			this.CVMCheckbox.UseUnderline = true;
			this.table1.Add(this.CVMCheckbox);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.CVMCheckbox]));
			w6.TopAttach = ((uint)(5));
			w6.BottomAttach = ((uint)(6));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ISOLabel = new global::Gtk.Label();
			this.ISOLabel.Name = "ISOLabel";
			this.ISOLabel.LabelProp = global::Mono.Unix.Catalog.GetString("ISO File");
			this.table1.Add(this.ISOLabel);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.ISOLabel]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ISOPath = new global::Gtk.Entry();
			this.ISOPath.CanFocus = true;
			this.ISOPath.Name = "ISOPath";
			this.ISOPath.IsEditable = true;
			this.ISOPath.InvisibleChar = '•';
			this.table1.Add(this.ISOPath);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.ISOPath]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ISOPicker = new global::Gtk.Button();
			this.ISOPicker.CanFocus = true;
			this.ISOPicker.Name = "ISOPicker";
			this.ISOPicker.UseStock = true;
			this.ISOPicker.UseUnderline = true;
			this.ISOPicker.Label = "gtk-open";
			this.table1.Add(this.ISOPicker);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.ISOPicker]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.LeftAttach = ((uint)(2));
			w9.RightAttach = ((uint)(3));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.OutputLabel = new global::Gtk.Label();
			this.OutputLabel.Name = "OutputLabel";
			this.OutputLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Output Directory");
			this.table1.Add(this.OutputLabel);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.OutputLabel]));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.OutputPath = new global::Gtk.Entry();
			this.OutputPath.CanFocus = true;
			this.OutputPath.Name = "OutputPath";
			this.OutputPath.IsEditable = true;
			this.OutputPath.InvisibleChar = '•';
			this.table1.Add(this.OutputPath);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.OutputPath]));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.OutputPicker = new global::Gtk.Button();
			this.OutputPicker.CanFocus = true;
			this.OutputPicker.Name = "OutputPicker";
			this.OutputPicker.UseStock = true;
			this.OutputPicker.UseUnderline = true;
			this.OutputPicker.Label = "gtk-open";
			this.table1.Add(this.OutputPicker);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.OutputPicker]));
			w12.LeftAttach = ((uint)(2));
			w12.RightAttach = ((uint)(3));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.WinePrefixLabel = new global::Gtk.Label();
			this.WinePrefixLabel.Name = "WinePrefixLabel";
			this.WinePrefixLabel.LabelProp = global::Mono.Unix.Catalog.GetString("Wine Prefix Location");
			this.table1.Add(this.WinePrefixLabel);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.WinePrefixLabel]));
			w13.TopAttach = ((uint)(3));
			w13.BottomAttach = ((uint)(4));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.WinePrefixPath = new global::Gtk.Entry();
			this.WinePrefixPath.CanFocus = true;
			this.WinePrefixPath.Name = "WinePrefixPath";
			this.WinePrefixPath.IsEditable = true;
			this.WinePrefixPath.InvisibleChar = '•';
			this.table1.Add(this.WinePrefixPath);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.WinePrefixPath]));
			w14.TopAttach = ((uint)(3));
			w14.BottomAttach = ((uint)(4));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.WinePrefixPicker = new global::Gtk.Button();
			this.WinePrefixPicker.CanFocus = true;
			this.WinePrefixPicker.Name = "WinePrefixPicker";
			this.WinePrefixPicker.UseStock = true;
			this.WinePrefixPicker.UseUnderline = true;
			this.WinePrefixPicker.Label = "gtk-open";
			this.table1.Add(this.WinePrefixPicker);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.WinePrefixPicker]));
			w15.TopAttach = ((uint)(3));
			w15.BottomAttach = ((uint)(4));
			w15.LeftAttach = ((uint)(2));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add(this.table1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(w1[this.table1]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Internal child GTKFrontend.GCDialog.ActionArea
			global::Gtk.HButtonBox w17 = this.ActionArea;
			w17.Name = "dialog1_ActionArea";
			w17.Spacing = 10;
			w17.BorderWidth = ((uint)(5));
			w17.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget(this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w18 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w17[this.buttonOk]));
			w18.Expand = false;
			w18.Fill = false;
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 400;
			this.DefaultHeight = 300;
			this.CPKCheckbox.Hide();
			this.CPKLabel.Hide();
			this.CPKPath.Hide();
			this.CPKPicker.Hide();
			this.CVMCheckbox.Hide();
			this.ISOLabel.Hide();
			this.ISOPath.Hide();
			this.ISOPicker.Hide();
			this.WinePrefixLabel.Hide();
			this.WinePrefixPath.Hide();
			this.WinePrefixPicker.Hide();
			this.Show();
			this.Response += new global::Gtk.ResponseHandler(this.OnOK);
			this.OutputPicker.Clicked += new global::System.EventHandler(this.OnOutputPicker);
			this.ISOPicker.Clicked += new global::System.EventHandler(this.OnISOPicker);
		}
	}
}
