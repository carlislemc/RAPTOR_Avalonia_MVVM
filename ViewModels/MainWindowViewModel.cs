using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.DTO;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using raptor;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Avalonia.Input;
using ReactiveUI;
using System.Reactive;
using interpreter;
using numbers;

namespace RAPTOR_Avalonia_MVVM.ViewModels
{



    public class MainWindowViewModel : ViewModelBase
    {
        private string My_Title = "Raptor";

        public void Clear_Undo()
        {
            Undo_Stack.Clear_Undo(/*this*/);
        }

        public int mainIndex
        {
            get
            {
                if (Component.Current_Mode == Mode.Expert)
                    return 1;
                else
                    return 0;
            }
        }
        public float scale = 1.0f;
        public Component? clipboard;
        public logging_info? log = new logging_info();
        public bool modified = false;
        public bool runningState = false;
        public string Text = "sdfasdf";
        private System.Guid file_guid_back = System.Guid.NewGuid();
        public Component? Current_Selection = null;
        public System.Guid file_guid
        {
            get
            {
                return file_guid_back;
            }
            set
            {
                file_guid_back = value;
                /*if (Component.BARTPE)
                {
                    this.MC.Text = file_guid_back.ToString().Substring(0, 8) + ": Console";
                }*/
            }
        }
        private string? fileName;

        private ObservableCollection<Subchart>? privateTheTabs;
        public ObservableCollection<Subchart>? theTabs {
            get
            {
                return privateTheTabs;
            }
            set
            {
                privateTheTabs = value;
            }
        }
        private ObservableCollection<Variable> privateTheVariables;
        public ObservableCollection<Variable> theVariables
        {
            get
            {
                return privateTheVariables;
            }
            set
            {
                privateTheVariables = value;
            }
        }
        public void Update_View_Variables()
        {

        }
        public Subchart mainSubchart()
        {
            return this.theTabs[0];
        }
        
        public static List<Subchart> FillTabs(){
            Subchart main_subchart = new Subchart("main");
            return new List<Subchart>(){
                main_subchart
            };
        }
        public static List<Variable> FillWatch()
        {
            //Fill league data here...
            return new List<Variable>()
            {
            };

        }
        public int x = 0;
        /*public ObservableCollection<MenuItem> ContextMenuItemsFunction()
        {
            return new ObservableCollection<MenuItem> { new MenuItem() { Header = "Hello" }, new MenuItem() { Header = "World" } };
        }*/

        public bool OnClosingCommand()
        {
            if (x==0)
            {
                x++;
                return true;
            }
            else
            {
                return false;
            }
        }
        private static MainWindowViewModel theMainWindowViewModel;
        public static MainWindowViewModel GetMainWindowViewModel()
        {
            return theMainWindowViewModel;
        }
        public MainWindowViewModel()
        {
            theMainWindowViewModel = this;
            theVariables = new ObservableCollection<Variable>(FillWatch());
            //Subchart main_subchart = new Subchart("main");
            theTabs = new ObservableCollection<Subchart>(FillTabs());

            /*
            GetWindow().Closing += (s, e) =>
            {
                if (x < 5)
                {
                    e.Cancel = true;
                    x++;
                }
            }; */
        }
        public string Greeting => "Welcome to Avalonia!";
        private Window GetWindow()
        {
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime)
            {
                return desktopLifetime.MainWindow;
            }
            return null;
        }
        public void Clear_Subcharts()
        {
            this.theTabs.Clear();
            this.theTabs.Add(new Subchart());
        }
        private void Load_File(string dialog_fileName)
        {

            Stream stream;
            FileAttributes attr;
            try
            {
                stream = File.Open(dialog_fileName, FileMode.Open,
                        FileAccess.Read);
                attr = System.IO.File.GetAttributes(dialog_fileName);
            }
            catch
            {
                MessageBoxClass.Show("Unable to open file: " + dialog_fileName);
                return;
            }
            BinaryFormatter bformatter = new BinaryFormatter();
            try
            {
                try
                {
                    Component.warned_about_newer_version = false;
                    Component.warned_about_error = false;
                    this.Clear_Subcharts();
                    try
                    {
                        // starting with version 11, we put the version number first
                        // WARNING!  If you change this, you'll have to change
                        // this similar code in MasterConsole that does 
                        // extract_times
                        Component.last_incoming_serialization_version = (int)bformatter.Deserialize(stream);
                        bool incoming_reverse_loop_logic;
                        if (Component.last_incoming_serialization_version >= 13)
                        {
                            incoming_reverse_loop_logic = (bool)bformatter.Deserialize(stream);
                        }
                        else
                        {
                            incoming_reverse_loop_logic = false;
                        }

                        // read in number of pages
                        int num_pages = (int)bformatter.Deserialize(stream);
                        for (int i = 0; i < num_pages; i++)
                        {
                            Subchart_Kinds incoming_kind;
                            string name = (string)bformatter.Deserialize(stream);
                            if (Component.last_incoming_serialization_version >= 14)
                            {
                                object o = bformatter.Deserialize(stream);
                                incoming_kind = (Subchart_Kinds)o;
                            }
                            else
                            {
                                incoming_kind = Subchart_Kinds.Subchart;
                            }
                            if (i == 0 && incoming_kind != Subchart_Kinds.UML &&
                                Component.Current_Mode == Mode.Expert)
                            {
                                MessageBoxClass.Show("Changing to Intermediate Mode");
                                //this.menuIntermediate_Click(null, null);
                            }
                            if (incoming_kind != Subchart_Kinds.Subchart)
                            {
                                if (Component.Current_Mode != Mode.Expert &&
                                    incoming_kind == Subchart_Kinds.UML)
                                {
                                    MessageBoxClass.Show("Can't open OO RAPTOR file");
                                    //MessageBox.Show("Changing to Object-Oriented Mode");
                                    //this.menuObjectiveMode_Click(null, null);
                                    throw new Exception("unimplemented");
                                }
                                if (Component.Current_Mode == Mode.Novice)
                                {
                                    MessageBoxClass.Show("Changing to Intermediate Mode");
                                    //this.menuIntermediate_Click(null, null);
                                }
                            }
                            // I moved these down lower in case the mode was changed by
                            // reading in this flowchart (which calls new and clears filename)
                            this.fileName = dialog_fileName;
                            //Plugins.Load_Plugins(this.fileName);

                            if (i > mainIndex)
                            {
                                int param_count = 0;
                                switch (incoming_kind)
                                {
                                    case Subchart_Kinds.Function:
                                        param_count = (int)bformatter.Deserialize(stream);
                                        //this.theTabs.Add()
                                        this.theTabs.Add(new Procedure_Chart(name,
                                            param_count));
                                        break;
                                    case Subchart_Kinds.Procedure:
                                        if (Component.last_incoming_serialization_version >= 15)
                                        {
                                            param_count = (int)bformatter.Deserialize(stream);
                                        }
                                        this.theTabs.Add(new Procedure_Chart(name,
                                            param_count));
                                        break;
                                    case Subchart_Kinds.Subchart:
                                        this.theTabs.Add(new Subchart(name));
                                        break;
                                }
                            }
                        }

                        Component.negate_loops = false;
                        /*if (Component.Current_Mode == Mode.Expert)
                        {
                            NClass.Core.BinarySerializationHelper.diagram =
                                (this.theTabs[0].Controls[0] as UMLDiagram).diagram;
                            (this.theTabs[0].Controls[0] as UMLDiagram).project.LoadBinary(
                                bformatter, stream);
                        }
                        else */if (incoming_reverse_loop_logic != Component.reverse_loop_logic)
                        {
                            Component.negate_loops = true;
                        }
                        for (int i = mainIndex; i < num_pages; i++)
                        {
                            object o = bformatter.Deserialize(stream);
                            ((Subchart)this.theTabs[i]).Start = (Oval)o;
                            ((Subchart)this.theTabs[i]).Start.scale = this.scale;
                            ((Subchart)this.theTabs[i]).Start.Scale(this.scale);
                            if (Component.last_incoming_serialization_version >= 17)
                            {
                                byte[] ink = (byte[])bformatter.Deserialize(stream);
                                /*if (!Component.BARTPE && !Component.MONO && ink.Length > 1)
                                {
                                    bool was_enabled = ((Subchart)this.theTabs[i]).tab_overlay.Enabled;
                                    ((Subchart)this.theTabs[i]).tab_overlay.Enabled = false;
                                    ((Subchart)this.theTabs[i]).tab_overlay.Ink = new Microsoft.Ink.Ink();
                                    ((Subchart)this.theTabs[i]).tab_overlay.Ink.Load(ink);
                                    ((Subchart)this.theTabs[i]).tab_overlay.Enabled = was_enabled;
                                    ((Subchart)this.theTabs[i]).scale_ink(this.scale);
                                }
                                else if (((Subchart)this.theTabs[i]).tab_overlay != null)
                                {
                                    bool was_enabled = ((Subchart)this.theTabs[i]).tab_overlay.Enabled;
                                    ((Subchart)this.theTabs[i]).tab_overlay.Enabled = false;
                                    ((Subchart)this.theTabs[i]).tab_overlay.Ink = new Microsoft.Ink.Ink();
                                    ((Subchart)this.theTabs[i]).tab_overlay.Enabled = was_enabled;
                                    ((Subchart)this.theTabs[i]).scale_ink(this.scale);
                                }*/
                            }
                            this.Current_Selection = ((Subchart)this.theTabs[i]).Start.select(-1000, -1000);
                        }
                        //this.carlisle.SelectedTab = this.mainSubchart();
                    }
                    catch (System.Exception)
                    {
                        // previous to version 11, there is just one tab page
                        // moved this way down here for very old files (previous to version 11)
                        this.fileName = dialog_fileName;
                        //Plugins.Load_Plugins(this.fileName);
                        stream.Seek(0, SeekOrigin.Begin);
                        this.mainSubchart().Start = (Oval)bformatter.Deserialize(stream);
                        Component.last_incoming_serialization_version =
                           this.mainSubchart().Start.incoming_serialization_version;
                    }

                    // load all of the subcharts based on what the UML Diagram created for tabs
                    /*if (Component.Current_Mode == Mode.Expert)
                    {
                        for (int i = mainIndex + 1; i < this.theTabs.Count; i++)
                        {
                            ClassTabPage ctp = this.theTabs[i] as ClassTabPage;
                            for (int j = 0; j < ctp.tabControl1.TabPages.Count; j++)
                            {
                                Subchart sc = ctp.tabControl1.TabPages[j] as Subchart;
                                sc.Start = (Oval)bformatter.Deserialize(stream);
                                sc.Start.scale = this.scale;
                                sc.Start.Scale(this.scale);
                                byte[] ink = (byte[])bformatter.Deserialize(stream);
                                if (!Component.BARTPE && ink.Length > 1)
                                {
                                    bool was_enabled = sc.tab_overlay.Enabled;
                                    sc.tab_overlay.Enabled = false;
                                    sc.tab_overlay.Ink = new Microsoft.Ink.Ink();
                                    sc.tab_overlay.Ink.Load(ink);
                                    sc.tab_overlay.Enabled = was_enabled;
                                    sc.scale_ink(this.scale);
                                }
                                else if (sc.tab_overlay != null)
                                {
                                    bool was_enabled = sc.tab_overlay.Enabled;
                                    sc.tab_overlay.Enabled = false;
                                    sc.tab_overlay.Ink = new Microsoft.Ink.Ink();
                                    sc.tab_overlay.Enabled = was_enabled;
                                    sc.scale_ink(this.scale);
                                }
                                this.Current_Selection = sc.Start.select(-1000, -1000);
                            }
                        }
                    }*/
                    if (Component.last_incoming_serialization_version >= 4)
                    {
                        this.log = (logging_info)bformatter.Deserialize(stream);
                    }
                    else
                    {
                        this.log.Clear();
                    }
                    if (Component.last_incoming_serialization_version >= 6)
                    {
                        Component.compiled_flowchart = (bool)bformatter.Deserialize(stream);
                    }
                    else
                    {
                        Component.compiled_flowchart = false;
                    }
                    if (Component.last_incoming_serialization_version >= 8)
                    {
                        this.file_guid = (System.Guid)bformatter.Deserialize(stream);
                    }
                    else
                    {
                        this.file_guid = System.Guid.NewGuid();
                    }
                    if (Component.compiled_flowchart)
                    {
                        MessageBoxClass.Show("Compiled flowchart not supported");
                        //MessageBox.Show("Changing to Object-Oriented Mode");
                        //this.menuObjectiveMode_Click(null, null);
                        throw new Exception("unimplemented");
                        /*Registry_Settings.Ignore_Updates = true;
                        this.trackBar1.Value = this.trackBar1.Maximum;
                        this.trackBar1_Scroll(null, null);
                        if (this.menuViewVariables.Checked)
                        {
                            this.menuViewVariables_Click(null, null);
                        }
                        Registry_Settings.Ignore_Updates = false;

                        Compile_Helpers.Compile_Flowchart(this.theTabs);*/
                    }
                    if (Component.Current_Mode != Mode.Expert)
                    {
                        /*for (int i = mainIndex; i < this.carlisle.TabCount; i++)
                        {
                            ((Subchart)this.theTabs[i]).flow_panel.Invalidate();
                        }*/
                    }
                    else
                    {/*
                        ((Subchart)this.theTabs[mainIndex]).flow_panel.Invalidate();
                        for (int i = mainIndex + 1; i < this.carlisle.TabCount; i++)
                        {
                            for (int j = 0; j < (this.theTabs[i] as ClassTabPage).tabControl1.TabCount; j++)
                            {
                                Subchart sc = (this.theTabs[i] as ClassTabPage).tabControl1.TabPages[j] as Subchart;
                                sc.flow_panel.Invalidate();
                            }
                        }*/
                    }
                    this.log.Record_Open();
                    stream.Close();
                }
                catch
                {
                    /*if (command_line_run)
                    {
                        stream.Close();
                        return;
                    }*/
                    MessageBoxClass.Show("Invalid File-not a flowchart, aborting",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.OnNewCommand();
                    try
                    {
                        stream.Close();
                    }
                    catch
                    {
                    }
                    return;
                }
                this.Update_View_Variables();

                Environment.CurrentDirectory = Path.GetDirectoryName(dialog_fileName);
                Runtime.Clear_Variables();

                this.runningState = false;
                //MRU.Add_To_MRU_Registry(this.fileName);
                this.Text = My_Title + " - " +
                    Path.GetFileName(this.fileName);
                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    this.Text = this.Text + " [Read-Only]";
                }
                this.modified = false;


                this.mainSubchart().Start.scale = this.scale;
                this.mainSubchart().Start.Scale(this.scale);
                this.Current_Selection = this.mainSubchart().Start.select(-1000, -1000);
                this.Clear_Undo();


                /*if (this.menuAllText.Checked)
                {
                    this.menuAllText_Click(null, null);
                }
                else if (this.menuTruncated.Checked)
                {
                    this.menuTruncated_Click(null, null);
                }
                else
                {
                    this.menuNoText_Click(null, null);
                }*/
                //Component.view_comments = this.menuViewComments.Checked;
                // can only Invalidate the flow_panel if it has had its handle created (i.e. not in /run mode)
                /*if (flow_panel.IsHandleCreated)
                {
                    flow_panel.Invalidate();
                }*/
                RAPTOR_Avalonia_MVVM.ViewModels.MasterConsoleViewModel.MC.clear_txt();
            }
            catch (System.Exception e)
            {
                MessageBoxClass.Show(e.Message + "\n" + e.StackTrace + "\n" + "Invalid Filename:" + dialog_fileName, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void OnOpenCommand()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters.Add(new FileDialogFilter() { Name = "RAPTOR", Extensions = { "rap" } });
            dialog.Filters.Add(new FileDialogFilter() { Name = "All Files", Extensions = { "*" } });
            dialog.AllowMultiple = false;
            if (Avalonia.Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                string[] result = await dialog.ShowAsync(desktop.MainWindow);
                if (result != null)
                {
                    Load_File(result[0]);
                    /*var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                        .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Opening",
                            ContentMessage = result[0],
                            Icon = Icon.Plus,
                            Style = Style.Windows
                        });

                    ButtonResult br= await msBoxStandardWindow.ShowDialog(desktop.MainWindow);*/
                }
            }

        }
        public void OnEditCommand()
        {
            this.mainSubchart().Start.setText(this.mainSubchart().positionX,this.mainSubchart().positionY);
        }
        public void OnCutCommand()
        {
            Component cutReturn = this.mainSubchart().Start.cut();
            if (cutReturn!=null)
            {
                Clipboard_Data cd = new Clipboard_Data(
                cutReturn,
                this.file_guid, this.log.Clone());
                ClipboardMultiplatform.SetDataObject(cd, true);
            }
        }
        public void OnExitCommand()
        {
            GetWindow().Close();
        }
        public void OnDeleteCommand()
        {

        }

        public void OnPasteCommand() { }
        public void OnCopyCommand() { }
        public void OnSaveCommand() { }
        public void OnSaveAsCommand() { }
        public void OnNextCommand() { }
        public void OnPauseCommand() {
         }

        public void OnNewCommand()
        {
            var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.OkAbort,
                    ContentTitle = "Title",
                    ContentMessage = "Message",
                    Icon = Icon.Plus,
                    Style = Style.Windows
                });
            msBoxStandardWindow.Show();
        }
        public void OnResetExecuteCommand() { }
        public void OnExecuteCommand() { 
        }

        public void OnStepCommand() {
        }

        public void OnResetCommand() { }
        public void OnUndoCommand() {
            Undo_Stack.Undo_Action(this.mainSubchart());
        }
        public void OnRedoCommand() {
            Undo_Stack.Redo_Action(this.mainSubchart());
        }
        public void OnClearBreakpointsCommand()
        {

        }
        public void OnAboutCommand()
        {

        }
        public void OnShowLogCommand() { }
        public void OnCountSymbolsCommand() { }

    }
}
