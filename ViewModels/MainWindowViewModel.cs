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
using parse_tree;
using System.Timers;
using System.Threading;
using RAPTOR_Avalonia_MVVM.Views;

using Avalonia.Markup.Xaml;
using RAPTOR_Avalonia_MVVM.ViewModels;
using System.Collections;
using Avalonia.Threading;
using System.Diagnostics;


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
            this.theTabs[this.activeTab].Start.setText(this.theTabs[this.activeTab].positionX,this.theTabs[this.activeTab].positionY);
        }
        public void OnCutCommand()
        {
            Component cutReturn = this.theTabs[this.activeTab].Start.cut();
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
            this.theTabs[this.activeTab].Start.delete();

        }

        public async void OnPasteCommand() { 
            int x_position = this.theTabs[this.activeTab].positionXTapped;
            int y_position = this.theTabs[this.activeTab].positionYTapped;

            try
            {
                Object thing = await ClipboardMultiplatform.GetDataObjectAsync();
                if (thing != null)
                {
                    Component obj = ((Clipboard_Data)thing).symbols;
                    Undo_Stack.Make_Undoable(this.theTabs[this.activeTab]);
                    Component the_clone = obj.Clone();
                    this.theTabs[this.activeTab].Start.insert(the_clone, x_position, y_position, 0);
                }
            }
            catch
            {

            }
            
            
        }
        public void OnCopyCommand() {
            Component copyReturn = this.theTabs[this.activeTab].Start.copy();
            if (copyReturn!=null)
            {
                Clipboard_Data cd = new Clipboard_Data(
                copyReturn,
                this.file_guid, this.log.Clone());
                ClipboardMultiplatform.SetDataObject(cd, true);
            }
        }
        public void OnSaveCommand() {

            FileSave_Click();

        }

        private void FileSave_Click()
        {
            if (fileName == "" || fileName == null)
            {
                this.OnSaveAsCommand();
            }
            else
            {
                this.Perform_Save(this.fileName, false);
            }
        }

        private bool Save_Error = false;
        private void Perform_Save(string name, bool is_autosave)
        {
            Stream stream;
            string prefix;

            if (is_autosave)
            {
                prefix = "Error during autosave:";
            }
            else
            {
                prefix = "Error during save:";
            }

            try
            {
                stream = File.Open(name, FileMode.Create);
            }
            catch
            {
                if (File.Exists(name) &&
                    (File.GetAttributes(name) & FileAttributes.ReadOnly) > 0)
                {
                    MessageBoxClass.Show(
                        prefix + '\n' +
                        name + " is a read-only file",
                        "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBoxClass.Show(
                        prefix + '\n' +
                        "Unable to create file: " +
                        name, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Save_Error = true;
                return;
            }

            try
            {

                BinaryFormatter bformatter = new BinaryFormatter();
                //bformatter.Serialize(stream, 
                bformatter.Serialize(stream, Component.current_serialization_version);
                // USMA_mode new in file version 13
                bformatter.Serialize(stream, Component.reverse_loop_logic);

                bformatter.Serialize(stream, theTabs.Count);

                for (int i = mainIndex; i < theTabs.Count; i++)
                {
                    bformatter.Serialize(stream, this.theTabs[i].Text);
                    // subchart kind is new in file version 14
                    bformatter.Serialize(stream, ((Subchart)this.theTabs[i]).Subchart_Kind);
                    if (((Subchart)this.theTabs[i]) is Procedure_Chart)
                    {
                        bformatter.Serialize(stream, ((Procedure_Chart)this.theTabs[i]).num_params);
                    }
                }

                for (int i = mainIndex; i < this.theTabs.Count; i++)
                {
                    bformatter.Serialize(stream, ((Subchart)this.theTabs[i]).Start);
                    // new in version 17
                    byte[] output;
                    if (!Component.BARTPE && !Component.VM && !Component.MONO)
                    {
                        //output = ((Subchart)this.theTabs[i]).tab_overlay.Ink.Save();
                        output = new byte[1];
                    }
                    else
                    {
                        output = new byte[1];
                    }
                    bformatter.Serialize(stream, output);
                }

                if (!is_autosave)
                {
                    this.log.Record_Save();
                }
                else
                {
                    this.log.Record_Autosave();
                }

                bformatter.Serialize(stream, this.log);
                bformatter.Serialize(stream, Component.compiled_flowchart);
                bformatter.Serialize(stream, this.file_guid);
                stream.Close();
                this.Save_Error = false;
                if (!is_autosave)
                {
                    this.modified = false;
                }


            }
            catch (System.Exception exc)
            {
                MessageBoxClass.Show(
                    prefix + '\n' +
                    "Please report to Martin.Carlisle@usafa.edu" + '\n' +
                    "Meantime, try undo then save (keep doing undo until success)" + '\n' +
                    "Or open an autosave file: " + this.fileName + ".[0-9]" + '\n' +
                    "Use Alt-PrtSc and paste into email" + '\n' +
                    exc.Message + '\n' +
                    exc.StackTrace, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Save_Error = true;
            }






        }

        public async void OnSaveAsCommand() {

            string dialog_fileName;
            string oldName = this.fileName;

            if (!Component.BARTPE)
            {
                SaveFileDialog fileChooser = new SaveFileDialog();

                List<FileDialogFilter> Filters = new List<FileDialogFilter>();
                FileDialogFilter filter = new FileDialogFilter();
                List<string> extension = new List<string>();
                extension.Add("rap");
                filter.Extensions = extension;
                filter.Name = "Raptor Files";
                Filters.Add(filter);
                fileChooser.Filters = Filters;

                fileChooser.DefaultExtension = "rap";

                string ans = await fileChooser.ShowAsync(MainWindow.topWindow);

                if(ans == null || ans == "")
                {
                    return;
                }

                this.fileName = ans;
                this.FileSave_Click();

            }

        }
        
        public Component currentActiveComponent;
        public Component activeComponent{
            get{return currentActiveComponent;}
            set{currentActiveComponent = value;}
        }

        public Component currentParentComponent;
        public Component parentComponent{
            get{return currentParentComponent;}
            set{currentParentComponent = value;}
        }

        public int currentInLoop = 0;
        public int inLoop{
            get{return currentInLoop;}
            set{currentInLoop = value;}
        }

        public int currentInSelection = 0;
        public int inSelection{
            get{return currentInSelection;}
            set{currentInSelection = value;}
        }

        public int symbolCount = 0;
        public int decreaseScope = 0;
        public int decreaseSub = 0;
        public bool isSub = false;

        public ObservableCollection<Component> parentCount = new ObservableCollection<Component>();

        public ObservableCollection<string> activeScopes = new ObservableCollection<string>() {"main"};

        private bool getParent = false;
        private void goToNextComponent(){
            try
            {
                symbolCount++;
                if (parentCount.Count != 0 && parentComponent == null)
                {
                    parentComponent = parentCount[parentCount.Count - 1];
                    parentCount.RemoveAt(parentCount.Count - 1);
                }
                if (activeComponent.Successor == null && parentComponent != null)
                {
                    bool removeMe = true;
                    if (inSelection != 0 || inLoop != 0)
                    {
                        activeComponent.running = false;
                        if (activeComponent.Successor == null)
                        {
                            while (activeComponent.parent.GetType() != typeof(Loop) && activeComponent.parent.Successor == null)
                            {
                                activeComponent = activeComponent.parent;
                                inSelection--;
                            }
                            if (activeComponent.parent.GetType() == typeof(Loop))
                            {
                                Loop tempLoop = (Loop)activeComponent.parent;
                                if (!activeComponent.is_beforeChild && tempLoop.before_Child != null)
                                {
                                    activeComponent = tempLoop.before_Child;
                                    parentComponent = tempLoop;
                                    //parentCount.Add(tempLoop);
                                    removeMe = true;
                                    //inLoop++;
                                }
                                else
                                {
                                    activeComponent = activeComponent.parent;
                                    if (!parentCount.Contains(activeComponent))
                                    {
                                        parentCount.Add(activeComponent);
                                    }
                                    inLoop--;

                                }

                            }
                            else if (activeComponent.parent.GetType() == typeof(IF_Control))
                            {
                                activeComponent = activeComponent.parent.Successor;
                                inSelection--;
                            }
                        }
                        activeComponent.running = true;
                        if (removeMe)
                        {
                            parentCount.RemoveAt(parentCount.Count - 1);
                        }

                    }
                    else
                    {
                        if (isSub)
                        {
                            decreaseSub--;
                            if (activeTabs.Count > 1)
                            {
                                activeTab = activeTabs[activeTabs.Count - 2];
                                activeTabs.RemoveAt(activeTabs.Count - 1);
                            }
                            else
                            {
                                activeTab = 0;
                            }
                            isSub = false;
                        }
                        else if (decreaseScope != 0)
                        {
                            Runtime.Decrease_Scope();
                            decreaseScope--;
                            if (activeTabs.Count > 1)
                            {
                                activeTab = activeTabs[activeTabs.Count - 2];
                                activeTabs.RemoveAt(activeTabs.Count - 1);
                            }
                            else
                            {
                                activeTab = 0;
                            }
                            activeScopes.RemoveAt(activeScopes.Count - 1);
                        }
                        parentComponent.running = false;
                        activeComponent.running = false;
                        if (parentCount.Count != 0)
                        {
                            parentComponent = parentCount[parentCount.Count - 1];
                            parentCount.RemoveAt(parentCount.Count - 1);
                        }
                        parentComponent.running = false;
                        activeComponent = parentComponent.Successor;
                        activeComponent.running = true;

                    }

                }
                else
                {
                    activeComponent.running = false;

                    if (activeComponent.Successor != null && activeComponent.Successor.GetType() == typeof(Loop))
                    {
                        Loop tempComponent = (Loop)activeComponent.Successor;
                        if (tempComponent.before_Child != null)
                        {
                            parentComponent = activeComponent;
                            parentCount.Add(parentComponent);
                            activeComponent = tempComponent.before_Child;
                            inLoop++;
                        }
                        else
                        {
                            activeComponent = activeComponent.Successor;
                        }
                    }
                    else
                    {
                        activeComponent = activeComponent.Successor;
                    }
                    activeComponent.running = true;
                }

                if (activeComponent.break_now())
                {
                    if (myTimer != null)
                    {
                        OnPauseCommand();
                    }
                }
                if (activeComponent.selected)
                {
                    activeComponent.selected = false;
                }
            }
            catch(Exception e)
            {
                if(myTimer != null)
                {
                    myTimer.Stop();
                }
                Dispatcher.UIThread.Post(() => postDialog("--- Run Halted! ---\n"+e.Message, true), DispatcherPriority.Background);
            }
        }

        private bool varFound(string s){
            return Runtime.getAnyVariable(s, activeScopes[activeScopes.Count-1]);
        }

        public ObservableCollection<string> getParamNames(string s){
            
            // string of the form func_name(param1, param2, param3)
            ;
            ObservableCollection<string> ans = new ObservableCollection<string>();
            s = s.Replace(" ", "");
            string[] temp = s.Split("(")[1].Split(",");
            for(int i = 0; i < temp.Length; i++){
                ans.Add(temp[i].Replace(")", ""));
            }

            return ans;

        }

        public async void postDialog(string text, bool isEnd)
        {
            MasterConsoleViewModel mc = MasterConsoleViewModel.MC;
            mc.Text += text;
            MainWindow.masterConsole.Activate();
            if (isEnd)
            {
                symbolCount = 0;
            }
            
        }

        public async void OnNextCommand() {
            if(activeComponent == null){
                startRun();
                activeComponent = this.mainSubchart().Start;
                activeComponent.running = true;
            } else {
                if((activeComponent.Successor == null && activeTab==0 && inLoop == 0 && inSelection == 0) || activeComponent.GetType() == typeof(Oval) && activeComponent.Successor == null && parentComponent == null){
                    symbolCount++;
          
                    Dispatcher.UIThread.Post(() => postDialog("--- Run Complete! " + symbolCount + " Symbols Evaluated ---\n",true), DispatcherPriority.Background);
                    
                    
                    activeComponent.running = false;
                    activeComponent = null;
                    if(myTimer != null){
                        myTimer.Stop();
                        myTimer = null;
                    }
                    parentCount.Clear();
                    parentComponent = null;
                    decreaseScope = 0;
                    activeTab = 0;
                    return;
                } else if(activeComponent.GetType() == typeof(Oval) && activeComponent.Successor == null && parentComponent != null) { 

                    Subchart activeSubchart = theTabs[activeTab];
                    symbolCount++;
                    activeComponent.running = false;

                    if(activeSubchart.Start.GetType() != typeof(Oval_Procedure))
                    {
                        isSub = true;
                        goToNextComponent();
                        setViewTab = activeTab;
                        return;
                    }

                    Oval_Procedure tempStart = (Oval_Procedure)activeSubchart.Start;
                    ObservableCollection<Object> outVals = new ObservableCollection<Object>();

                    for(int i = 0; i < tempStart.param_names.Length; i++){
                        if(tempStart.param_is_output[i]){
                            Variable tempVar = Runtime.Lookup_Variable(tempStart.param_names[i]);
                            if(tempVar.Kind == Runtime.Variable_Kind.Value){
                                numbers.value outVal = Runtime.getVariable(tempStart.param_names[i]);
                                outVals.Add(outVal);
                            } else if(tempVar.Kind == Runtime.Variable_Kind.One_D_Array){
                                numbers.value[] outVal = Runtime.getValueArray(tempStart.param_names[i]);
                                outVals.Add(outVal);
                            }else if(tempVar.Kind == Runtime.Variable_Kind.Two_D_Array){
                                numbers.value[][] outVal = Runtime.get2DValueArray(tempStart.param_names[i]);
                                outVals.Add(outVal);
                            }
                            
                        }
                    }

                    //string[] textStr = parentComponent.text_str.Split("(")[1].Split(","); // wont work for array[3,5] 

                    //ObservableCollection<string> textStr = getParamNames(parentComponent.text_str);
                    goToNextComponent();
                    setViewTab = activeTab;
                    
                    string[] textStr = parentComponent.text_str.Split("(")[1].Split(",");

                    int spot = 0;
                    for(int i = 0; i < outVals.Count; i++){
                        for(int k = spot; k < tempStart.param_names.Length; k++){
                            if(tempStart.param_is_output[k]){
                                textStr[k] = textStr[k].Replace(")", "");
                                Variable tempVar = Runtime.Lookup_Variable(textStr[k]);
                                if(tempVar == null){
                                    throw new Exception("Variable " + textStr[k] + " not found!");
                                }
                                if(tempVar.Kind == Runtime.Variable_Kind.Value){
                                    Runtime.setVariable(textStr[k], (numbers.value)outVals[i]);
                                    spot = k+1;
                                    break;
                                }
                                else if(tempVar.Kind == Runtime.Variable_Kind.One_D_Array){
                                    numbers.value[] arr = (numbers.value[])outVals[i];
                                    for(int n = 0; n < arr.Length; n++){
                                        Runtime.setArrayElement(textStr[k], n+1 , arr[n]);
                                        spot = k+1; 
                                    }
                                    break;
                                }else if(tempVar.Kind == Runtime.Variable_Kind.Two_D_Array){
                                    numbers.value[][] arr = (numbers.value[][])outVals[i];
                                    for(int r = 0; r < arr.Length; r++){
                                        for(int c = 0; c < arr[r].Length; c++){
                                            Runtime.set2DArrayElement(textStr[k], r+1, c+1, arr[r][c]);
                                            spot = k+1;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    return;
                }
                else if(activeComponent.GetType() == typeof(Oval)){
                    goToNextComponent();
                    return;
                }
                else if(activeComponent.GetType() == typeof(Rectangle)){
                    Rectangle temp = (Rectangle)activeComponent;
                    if(temp.kind == Rectangle.Kind_Of.Assignment){
                        string str = temp.text_str;
                        if(temp.text_str == ""){
                            throw new Exception("Assignment not instantiated");
                        }
                        Lexer l = new Lexer(str);
                        if(temp.parse_tree != null){
                            Expr_Assignment ea = (Expr_Assignment)temp.parse_tree;
                            ea.Execute(l);
                            if(decreaseScope != 0 && !varFound(l.Get_Text(0, str.IndexOf(":")))){
                                //decreaseScope--;
                                Variable tempVar = theVariables[theVariables.Count-1];
                                theVariables.RemoveAt(theVariables.Count-1);
                                theVariables.Insert(1, tempVar);
                            }
                        }
                    } else {
                        string str = temp.text_str;
                        if(temp.text_str == ""){
                            throw new Exception("Call not instantiated");
                        }
                        Lexer l = new Lexer(str);
                        if(temp.parse_tree != null){
                            Procedure_Call ea = (Procedure_Call)temp.parse_tree;
                            ea.Execute(l);
                        }
                    }
                    goToNextComponent();
                } else if(activeComponent.GetType() == typeof(IF_Control)){
                    IF_Control temp = (IF_Control)activeComponent;
                    string str = temp.text_str;
                    if(temp.text_str == ""){
                        throw new Exception("Selection not instantiated");
                    }
                    Lexer l = new Lexer(str);
                    if(temp.parse_tree != null){
                        Boolean_Expression r = (Boolean_Expression)temp.parse_tree;
                        bool rel = r.Execute(l);
                        parentComponent = temp;
                        if(!parentCount.Contains(parentComponent)){
                            parentCount.Add(parentComponent);
                            inSelection++;
                        }
                        if(rel){
                            if(temp.left_Child != null){
                                activeComponent.running = false;
                                activeComponent = temp.left_Child;
                                if (activeComponent.break_now())
                                {
                                    if (myTimer != null)
                                    {
                                        OnPauseCommand();
                                    }
                                }
                                activeComponent.running = true;
                            }
                        } else{
                            if(temp.right_Child != null){
                                activeComponent.running = false;
                                activeComponent = temp.right_Child;
                                if (activeComponent.break_now())
                                {
                                    if (myTimer != null)
                                    {
                                        OnPauseCommand();
                                    }
                                }
                                activeComponent.running = true;
                            }
                        }
                    }
                } else if(activeComponent.GetType() == typeof(Loop)){
                    Loop temp = (Loop)activeComponent;
                    string str = temp.text_str;
                    if(temp.text_str == ""){
                        throw new Exception("Loop not instantiated");
                    }
                    Lexer l = new Lexer(str);
                    if(temp.parse_tree != null){
                        Boolean_Expression r = (Boolean_Expression)temp.parse_tree;
                        bool rel = r.Execute(l);
                        parentComponent = temp;
                        if (!parentCount.Contains(parentComponent))
                        {
                            parentCount.Add(parentComponent);
                        }
                        inLoop++;
                        if (rel){
                            if(inLoop > 1){
                                getParent = true;
                            }
                            inLoop--;
                            goToNextComponent();
                            parentCount.RemoveAt(parentCount.Count-1);
                        } else {
                            if(temp.after_Child != null){
                                activeComponent.running = false;
                                activeComponent = temp.after_Child;
                                if (activeComponent.break_now())
                                {
                                    if (myTimer != null)
                                    {
                                        OnPauseCommand();
                                    }
                                }
                                activeComponent.running = true;
                            }
                        }
                    }
                } else if(activeComponent.GetType() == typeof(Parallelogram)){
                    Parallelogram temp = (Parallelogram)activeComponent;
                    if(temp.is_input){
                        string str = temp.text_str;
                        if(str == ""){
                            throw new Exception("Input not instantiated");
                        }
                        if(temp.parse_tree != null){
                            Input inp = (Input)temp.parse_tree;

                            await Dispatcher.UIThread.InvokeAsync(async () => {
                                if(myTimer != null){
                                    myTimer.Stop();
                                }
                                UserInputDialog uid = new UserInputDialog(temp);
                                await uid.ShowDialog(MainWindow.topWindow);
                                Lexer l = new Lexer(temp.assign);
                                Syntax_Result r = temp.result;
                                Expr_Assignment ex = (Expr_Assignment)r.tree;
                                numbers.value v = ex.Execute(l);
                                inp.Execute(l, v);
                                 if(myTimer != null){
                                    myTimer.Start();
                                }
                            });
                        }
                    } else{
                        string str = temp.text_str;
                        if(str == ""){
                            throw new Exception("Output not instantiated");
                        }
                        Lexer l = new Lexer(str);
                        if(temp.parse_tree != null){
                            Output op = (Output)temp.parse_tree;
                            numbers.value v = op.Execute(l);
                            string outputAns = numbers.Numbers.msstring_view_image(v).Replace("\"", "");
                            if(temp.new_line){
                                outputAns += "\n";
                            }

                            Dispatcher.UIThread.Post(() => postDialog(outputAns, false), DispatcherPriority.Background);
                            //MainWindow.masterConsole.Activate();

                        }

                    }
                    goToNextComponent();

                }

                
                
            }           
            
         }
        public void OnPauseCommand() {
            MasterConsoleViewModel mc = MasterConsoleViewModel.MC;
            mc.Text += "Run Paused!\n";
            myTimer.Stop();
        }

        public void Create_Flow_graphx()
        {
            Oval End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            if (!Component.USMA_mode)
            {
                End.Text = "End";
            }
            else
            {
                End.Text = "Stop";
            }

            this.mainSubchart().Start = new Oval(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            this.mainSubchart().Start.Text = "Start";
            this.mainSubchart().Start.scale = this.scale;
            this.mainSubchart().Start.Scale(this.scale);

            this.Clear_Undo();
            this.modified = false;

            //flow_panel.Invalidate();
        }

        private async Task Save_Before_Losing()
        {
            //DialogResult dr = DialogResult.No;
            //if (this.modified)
            //{
            //    string msg = "Choosing this option will delete the current flow chart!" + '\n' + "Do you want to save first?";
            //    dr = MessageBox.Show(msg,
            //        "Open New Chart", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //}

            //if (dr == DialogResult.Cancel)
            //{
            //    return false;
            //}
            //if (dr == DialogResult.Yes)
            //{
            //    this.FileSave_Click(sender, e);

            //    while (this.Save_Error)
            //    {
            //        dr = MessageBox.Show("Save failed-- try again?",
            //            "Open New Chart",
            //            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //        if (dr == DialogResult.Yes)
            //        {
            //            this.SaveAs_Click(sender, e);
            //        }
            //        else if (dr == DialogResult.Cancel)
            //        {
            //            return false;
            //        }
            //        else
            //        {
            //            this.Save_Error = false;
            //        }
            //    }
            //}
            //return true;

            

            if (this.modified)
            {
               
                string msg = "Choosing this option will delete the current flow chart!" + '\n' + "Do you want to save first?";

                await Dispatcher.UIThread.InvokeAsync(async () => {

                    await MessageBoxClass.Show(msg, "Open New Chart", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                    if (buttonAnswer == ButtonResult.Yes)
                    {
                        OnSaveCommand();
                        checkSave = false;
                    }
                    else if (buttonAnswer == ButtonResult.No)
                    {
                        checkSave = false;
                    }
                    else
                    {
                        checkSave = false;
                    }
                });

            }
            else
            {
                checkSave = true;
            }
            
        }

        public ButtonResult buttonAnswer = new ButtonResult();
        public bool checkSave = true;

        public async void OnNewCommand()
        {
            //var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
            //    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
            //    {
            //        ButtonDefinitions = ButtonEnum.OkAbort,
            //        ContentTitle = "Title",
            //        ContentMessage = "Message",
            //        Icon = Icon.Plus,
            //        Style = Style.Windows
            //    });
            //msBoxStandardWindow.Show();

            await Dispatcher.UIThread.InvokeAsync(async () => { await Save_Before_Losing(); });

            if (checkSave)
            {
                return;
            }
            Clear_Subcharts();
            Create_Flow_graphx();
            Runtime.Clear_Variables();
            Component.compiled_flowchart = false;
            this.runningState = false;
            this.mainSubchart().Start.scale = this.scale;
            this.mainSubchart().Start.Scale(this.scale);
            //mainSubchart().flow_panel.Invalidate();

            Undo_Stack.Clear_Undo();
            this.Text = My_Title + "- Untitled";
  
            this.modified = false;
            this.fileName = null;
            this.file_guid = System.Guid.NewGuid();
            MasterConsoleViewModel.MC.clear_txt();
            this.Update_View_Variables();

        }
        public void OnResetExecuteCommand() {
            OnResetCommand();
            OnExecuteCommand();
        }

        public int executeSpeed = 70;
        public int setSpeed{
            get{return executeSpeed;}
            set{
                this.RaiseAndSetIfChanged(ref executeSpeed, value);
                if(myTimer != null){
                    myTimer.Interval = 1000 * (1.01 - (double)setSpeed/100);
                }
            }
        }

        private System.Timers.Timer myTimer;
        public void OnExecuteCommand() {
            if(myTimer == null){
                
                myTimer = new System.Timers.Timer(1000 * (1.01 - (double)setSpeed/100));
                myTimer.Elapsed += new System.Timers.ElapsedEventHandler(stepper);
            }else{
                MasterConsoleViewModel mc = MasterConsoleViewModel.MC;
                mc.Text += "Run Resumed!\n";
            }
            myTimer.Start();
        }

        private Thread InstanceCaller;
        private void stepper(Object source, ElapsedEventArgs e)
        {

            OnNextCommand();
            // if (InstanceCaller != null && InstanceCaller.IsAlive)
			// {
			// 	return;
			// }
			// try 
			// {
			// 	InstanceCaller = new Thread(new ThreadStart(this.OnNextCommand));
            //     InstanceCaller.SetApartmentState(ApartmentState.MTA);
            //     InstanceCaller.Priority = ThreadPriority.BelowNormal;
			// 	InstanceCaller.Start();
			// }
			// catch (System.Exception exc)
			// {
			// 	Console.WriteLine(exc.Message);
			// }

        }


        public void OnStepCommand() {
            OnNextCommand();
        }

        public void OnResetCommand() {
            symbolCount = 0;
            if(myTimer != null){
                myTimer.Stop();
                myTimer = null;
            }
            this.theVariables.Clear();
            if(this.activeComponent == null){
                return;
            }
            if(this.activeComponent.running){
                this.activeComponent.running = false;
                this.activeComponent = null;
            }
        }

        public void startRun() {
            symbolCount = 0;
            this.theVariables.Clear();
            if(this.activeComponent == null){
                return;
            }
            if(this.activeComponent.running){
                this.activeComponent.running = false;
                this.activeComponent = null;
            }
            parentComponent = null;
            parentCount.Clear();
            activeTab = 0;
            activeTabs.Clear();
            activeTabs.Add(0);
            decreaseScope = 0;

        }

        public bool isUndoable = false;
        public bool toggleUndoCommand{
            get{ return isUndoable; }
            set{ this.RaiseAndSetIfChanged(ref isUndoable, value); }
        }
        public void OnUndoCommand() {
            Undo_Stack.Undo_Action(this.mainSubchart());
        }

        public bool isRedoable = false;
        public bool toggleRedoCommand{
            get{ return isRedoable; }
            set{ this.RaiseAndSetIfChanged(ref isRedoable, value); }
        }

        // need active tab so we know where to undo and redo changes.
        public int activeTab = 0;
        public ObservableCollection<int> activeTabs = new ObservableCollection<int>() {0};

        public int setActiveTab{
            get{return activeTab;}
            set{ this.RaiseAndSetIfChanged(ref activeTab, value); }
        }

        public int viewTab = 0;
        public int setViewTab{
            get{return viewTab; }
            set{this.RaiseAndSetIfChanged(ref viewTab, value); }
        }
        public void OnRedoCommand() {
            Undo_Stack.Redo_Action(this.mainSubchart());
        }
        public void OnClearBreakpointsCommand()
        {
            foreach(Subchart s in theTabs)
            {
                s.Start.Clear_Breakpoints();
            }
          
        }
        public void OnAboutCommand()
        {
            AboutDialog ad = new AboutDialog();
            ad.ShowDialog(MainWindow.topWindow);
        }
        public void OnShowLogCommand() {
            if(log != null)
            {
                Dispatcher.UIThread.Post(() => postDialog(log.Display(file_guid, true) +"\n", false), DispatcherPriority.Background);
                
            }
            else
            {
                Dispatcher.UIThread.Post(() => postDialog("Log unavailable", false), DispatcherPriority.Background);
            }

        }
        public void OnCountSymbolsCommand() {
            int count = 0;
            foreach(Subchart s in theTabs)
            {
                Component temp = s.Start;
                while(temp != null)
                {
                    count++;
                    if(temp.GetType() == typeof(Loop))
                    {
                        Loop tempLoop = (Loop)temp;
                        Component temp2;
                        if(tempLoop.before_Child != null)
                        {
                            temp2 = tempLoop.before_Child;
                            while(temp2 != null)
                            {
                                count++;
                                temp2 = temp2.Successor;
                            }
                        }
                        if (tempLoop.after_Child != null)
                        {
                            temp2 = tempLoop.after_Child;
                            while (temp2 != null)
                            {
                                count++;
                                temp2 = temp2.Successor;
                            }
                        }
                    }
                    if (temp.GetType() == typeof(IF_Control))
                    {
                        IF_Control tempIF = (IF_Control)temp;
                        Component temp2;
                        if (tempIF.left_Child != null)
                        {
                            temp2 = tempIF.left_Child;
                            while (temp2 != null)
                            {
                                count++;
                                temp2 = temp2.Successor;
                            }
                        }
                        if (tempIF.right_Child != null)
                        {
                            temp2 = tempIF.right_Child;
                            while (temp2 != null)
                            {
                                count++;
                                temp2 = temp2.Successor;
                            }
                        }
                    }
                    temp = temp.Successor;
                }
            }

            Dispatcher.UIThread.Post(() => postDialog("The total number of symbols is: " + count, false), DispatcherPriority.Background);

        }

        public void OnGeneralHelpCommand()
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://raptor.martincarlisle.com/docs",
                UseShellExecute = true
            });
        }

    }
}
