using Avalonia.Input;
using RAPTOR_Avalonia_MVVM.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using RAPTOR_Avalonia_MVVM.Views;
using numbers;
using Avalonia;
using System.Windows.Input;

namespace raptor
{
    public enum Subchart_Kinds { Subchart, Procedure, Function, UML };

    public class Subchart
    {
        public Oval Start, End;
        public string Text = "Main";
        public Subchart_Kinds kind = Subchart_Kinds.Subchart;
        public int positionX {
            get; set; 
        }

        public int positionXTapped{
            get; set;
        }
        public int positionYTapped{
            get; set;
        }

        private int privatePositionY;
        // This will be 0 at an insertable point and 1 otherwise
        // to trigger the appropriate context menu
        // set when the mouse moves
        private int contextMenuType = -1;
        public int positionY
        {
            get {
                return this.privatePositionY;
            } 
            set
            {
                this.privatePositionY = value;
                // if I can insert a symbol here, use the appropriate context menu
                if (this.Start.insert(null, this.positionX, this.positionY, 0))
                {
                    if (this.contextMenuType != 0)
                    {
                        this.ObservableMenuItems.Clear();
                        foreach (MenuItemViewModel j in this.OverArrowMenuItemsFunction)
                        {
                            this.ObservableMenuItems.Add(j);
                        }
                        this.contextMenuType = 0;
                    }
                }
                else
                {
                    Component t = findSelected(this);
                    if(t != null)
                    {
                        //if (this.contextMenuType != 1)
                        //{
                        if(t.GetType() == typeof(Oval)){
                            this.ObservableMenuItems.Clear();
                            foreach (MenuItemViewModel j in this.OvalMenuItemsFunction)
                            {
                                this.ObservableMenuItems.Add(j);
                            }
                            this.contextMenuType = 1;

                        }
                        else
                        {
                            this.ObservableMenuItems.Clear();
                            foreach (MenuItemViewModel j in this.OverSymbolMenuItemsFunction)
                            {
                                this.ObservableMenuItems.Add(j);
                            }
                            this.contextMenuType = 1;

                        }
                        //}
                    }
                    else
                    {
                        this.ObservableMenuItems.Clear();
                        foreach (MenuItemViewModel j in this.DisabledMenuItemsFunction)
                        {
                            this.ObservableMenuItems.Add(j);
                        }
                        this.contextMenuType = 1;
                    }
                    
                }
            }
        }

        // This has to be a property because it has a binding to the XAML 
        // (because reasons)
        public string Header
        {
            get { return this.Text; }
            set { Text = value; }
        }

        public Subchart Content()
        {
            return this;
        }
        public ReactiveCommand<Unit, Unit> PasteCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertAssignmentCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertCallCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertInputCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertOutputCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertSelectionCommand { get; set; }
        public ReactiveCommand<Unit, Unit> InsertLoopCommand { get; set; }
        public ReactiveCommand<Unit, Unit> EditCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CommentCommand { get; set; }
        public ReactiveCommand<Unit, Unit> ToggleBreakpointCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CutCommand { get; set; }
        public ReactiveCommand<Unit, Unit> CopyCommand { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; set; }

        private void initContextMenu()
        {
            PasteCommand = ReactiveCommand.Create(OnPasteCommand);
            InsertAssignmentCommand = ReactiveCommand.Create(OnInsertAssignmentCommand);
            InsertCallCommand = ReactiveCommand.Create(OnInsertCallCommand);
            InsertInputCommand = ReactiveCommand.Create(OnInsertInputCommand);
            InsertOutputCommand = ReactiveCommand.Create(OnInsertOutputCommand);
            InsertSelectionCommand = ReactiveCommand.Create(OnInsertSelectionCommand);
            InsertLoopCommand = ReactiveCommand.Create(OnInsertLoopCommand);
            EditCommand = ReactiveCommand.Create(OnEditCommand);
            CommentCommand = ReactiveCommand.Create(OnCommentCommand);
            ToggleBreakpointCommand = ReactiveCommand.Create(OnToggleBreakpointCommand);
            CutCommand = ReactiveCommand.Create(OnCutCommand);
            CopyCommand = ReactiveCommand.Create(OnCopyCommand);
            DeleteCommand = ReactiveCommand.Create(OnDeleteCommand);

            OverArrowMenuItemsFunction = new[]
            {
                new MenuItemViewModel { Header = "_Paste", Command = PasteCommand, IsEnabled=true },
                new MenuItemViewModel { Header = "Insert Assignment", Command = InsertAssignmentCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Insert Call", Command = InsertCallCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Insert Input", Command = InsertInputCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Insert Output", Command = InsertOutputCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Insert Selection", Command = InsertSelectionCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Insert Loop", Command = InsertLoopCommand, IsEnabled=true  }
            };


            OverSymbolMenuItemsFunction = new[]
            {
                new MenuItemViewModel { Header = "_Edit", Command = EditCommand, IsEnabled=true },
                new MenuItemViewModel { Header = "Comment", Command = CommentCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "Toggle Breakpoint", Command = ToggleBreakpointCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "C_ut", Command = CutCommand, IsEnabled=true },
                new MenuItemViewModel { Header = "C_opy", Command = CopyCommand, IsEnabled=true  },
                new MenuItemViewModel { Header = "_Delete", Command = DeleteCommand, IsEnabled=true  }
            };

            DisabledMenuItemsFunction = new[]
            {
                new MenuItemViewModel { Header = "_Edit", Command = EditCommand},
                new MenuItemViewModel { Header = "Comment", Command = CommentCommand},
                new MenuItemViewModel { Header = "Toggle Breakpoint", Command = ToggleBreakpointCommand},
                new MenuItemViewModel { Header = "C_ut", Command = CutCommand},
                new MenuItemViewModel { Header = "C_opy", Command = CopyCommand},
                new MenuItemViewModel { Header = "_Delete", Command = DeleteCommand}
            };

            OvalMenuItemsFunction = new[]
            {
                new MenuItemViewModel { Header = "_Edit", Command = EditCommand},
                new MenuItemViewModel { Header = "Comment", Command = CommentCommand, IsEnabled=true},
                new MenuItemViewModel { Header = "Toggle Breakpoint", Command = ToggleBreakpointCommand, IsEnabled=true},
                new MenuItemViewModel { Header = "C_ut", Command = CutCommand},
                new MenuItemViewModel { Header = "C_opy", Command = CopyCommand},
                new MenuItemViewModel { Header = "_Delete", Command = DeleteCommand}
            };

            this.ObservableMenuItems = new ObservableCollection<MenuItemViewModel>();

        }
        public Subchart()
        {
            initContextMenu();
            Text = "Main";
            End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            End.Text = "end";
            Start = new Oval(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            Start.Text = "start";
        }

        public void OnPasteCommand() { 
            MainWindowViewModel.GetMainWindowViewModel().OnPasteCommand();
        }
        public void OnInsertAssignmentCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new Rectangle(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width,
                "Rectangle",
                Rectangle.Kind_Of.Assignment), this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnInsertCallCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new Rectangle(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width,
                "Rectangle",
                Rectangle.Kind_Of.Call), this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnInsertInputCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new Parallelogram(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width,
                "Parallelogram",
                true), this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnInsertOutputCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new Parallelogram(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width,
                "Parallelogram",
                false), this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnInsertSelectionCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new IF_Control(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "IF_Control"),
                this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnInsertLoopCommand() {
            Undo_Stack.Make_Undoable(this);
            if (Start.insert(new Loop(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width,
                "Loop"), this.positionX, this.positionY, 0))
            {
                MainWindowViewModel.GetMainWindowViewModel().modified = true;
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnEditCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnEditCommand();
        }

        public Component findSelected(Subchart s)
        {
            Component ans = s.Start;
            ObservableCollection<int> loopPass = new ObservableCollection<int>() { 0 };
            ObservableCollection<int> selectionPass = new ObservableCollection<int>() { 0 };
            ObservableCollection<Component> p = new ObservableCollection<Component>();
            while (!ans.selected)
            {
                if (ans.Successor == null && p.Count == 0)
                {
                    return null;
                }

                else if (ans.Successor == null && p.Count != 0 && loopPass[loopPass.Count - 1] == 0 && selectionPass[selectionPass.Count - 1] == 0)
                {
                    ans = p[p.Count - 1];
                    p.RemoveAt(p.Count - 1);
                }

                else if (ans.Successor == null && p.Count != 0 && ans.GetType() != typeof(Loop) && ans.GetType() != typeof(IF_Control))
                {
                    ans = p[p.Count - 1];

                }

                if (ans.GetType() == typeof(Loop))
                {
                    Loop temp = (Loop)ans;
                    if (!p.Contains(temp))
                    {
                        loopPass.Add(1);
                        p.Add(temp);
                    }

                    if (loopPass[loopPass.Count - 1] == 1)
                    {
                        if (temp.before_Child != null)
                        {
                            ans = temp.before_Child;
                        }

                        loopPass[loopPass.Count - 1]++;
                    }
                    else if (loopPass[loopPass.Count - 1] == 2)
                    {
                        if (temp.after_Child != null)
                        {
                            ans = temp.after_Child;
                        }
                        loopPass[loopPass.Count - 1]++;
                    }
                    else if (loopPass[loopPass.Count - 1] == 3)
                    {

                        p.RemoveAt(p.Count - 1);
                        if (temp.Successor != null)
                        {
                            ans = temp.Successor;
                        }
                        else
                        {
                            ans = p[p.Count - 1];
                        }
                        loopPass.RemoveAt(loopPass.Count - 1);
                    }
                }

                else if (ans.GetType() == typeof(IF_Control))
                {
                    IF_Control temp = (IF_Control)ans;
                    if (!p.Contains(temp))
                    {
                        selectionPass.Add(1);
                        p.Add(temp);
                    }

                    if (selectionPass[selectionPass.Count - 1] == 1)
                    {
                        if (temp.left_Child != null)
                        {
                            ans = temp.left_Child;
                        }
                        selectionPass[selectionPass.Count - 1]++;
                    }
                    else if (selectionPass[selectionPass.Count - 1] == 2)
                    {
                        if (temp.right_Child != null)
                        {
                            ans = temp.right_Child;
                        }
                        selectionPass[selectionPass.Count - 1]++;
                    }
                    else if (selectionPass[selectionPass.Count - 1] == 3)
                    {
                        p.RemoveAt(p.Count - 1);
                        if (temp.Successor != null)
                        {
                            ans = temp.Successor;
                        }
                        else
                        {
                            ans = p[p.Count - 1];
                        }
                        selectionPass.RemoveAt(selectionPass.Count - 1);
                    }
                }

                else
                {
                    ans = ans.Successor;
                }

            }
            return ans;
        }

        public void OnCommentCommand(){
            Component ans = findSelected(this);
            ans.addComment();
        }
        public void OnToggleBreakpointCommand() {
            Component ans = findSelected(this);
            ans.Toggle_Breakpoint(ans.X, ans.Y);
        }
        public void OnCutCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnCutCommand();
        }
        public void OnCopyCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnCopyCommand();
        }
        public void OnDeleteCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnDeleteCommand();
        }

        public async void onAddSubchartCommand(){
            ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;
            AddSubchartDialog asc = new AddSubchartDialog(false);
            await asc.ShowDialog(MainWindow.topWindow);
            MainWindowViewModel.GetMainWindowViewModel().setViewTab = tbs.Count-1;
        }
        public async void onAddProcedureCommand(){
            ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;
            AddProcedureDialog avm = new AddProcedureDialog(false);
            await avm.ShowDialog(MainWindow.topWindow);
            MainWindowViewModel.GetMainWindowViewModel().setViewTab = tbs.Count-1;
        }
        public async void onModSubchartCommand(){
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();

            AddSubchartDialog asc = new AddSubchartDialog(true);
            await asc.ShowDialog(MainWindow.topWindow);
        }
        public async void onModProcedureCommand(){
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            Subchart modMe = mw.theTabs[mw.setViewTab];

            AddProcedureDialog avm = new AddProcedureDialog(true);
            await avm.ShowDialog(MainWindow.topWindow);
        }
        public async void onDelProcedureCommand(){
            MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();
            int spot = mw.setViewTab;
            mw.setViewTab = 0;
            mw.theTabs.RemoveAt(spot);

        }

        public Subchart(string name)
        {
            initContextMenu();

                Text = name;
                End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
                End.Text = "end";
                Start = new Oval(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
                Start.Text = "start";
        }


        private IList<MenuItemViewModel> OverArrowMenuItemsFunction;
        private IList<MenuItemViewModel> OverSymbolMenuItemsFunction;
        private IList<MenuItemViewModel> DisabledMenuItemsFunction;
        private IList<MenuItemViewModel> OvalMenuItemsFunction;


        public ReactiveCommand<Unit, Unit> AddSub { get; set; }
        public ReactiveCommand<Unit, Unit> AddProc { get; set; }
        public ReactiveCommand<Unit, Unit> ModSub { get; set; }
        public ReactiveCommand<Unit, Unit> ModProc { get; set; }
        public ReactiveCommand<Unit, Unit> DelProc { get; set; }
        private ObservableCollection<MenuItemViewModel> PrivateTabContextMenuItems = new ObservableCollection<MenuItemViewModel>(){
            new MenuItemViewModel() { Header = "Add Subchart"},
            new MenuItemViewModel() { Header = "Add Procedure"}

        };

        public int setViewTab {
            get{ return MainWindowViewModel.GetMainWindowViewModel().setViewTab; }
            set{ MainWindowViewModel.GetMainWindowViewModel().setViewTab = 1; }
        }
        public ObservableCollection<MenuItemViewModel> TabContextMenuItems
        {
            get
            {
                positionXTapped = positionX;
                positionYTapped = positionY;
                MainWindowViewModel mw = MainWindowViewModel.GetMainWindowViewModel();

                
                AddSub = ReactiveCommand.Create(onAddSubchartCommand);
                AddProc = ReactiveCommand.Create(onAddProcedureCommand);
                ModSub = ReactiveCommand.Create(onModSubchartCommand);
                ModProc = ReactiveCommand.Create(onModProcedureCommand);
                DelProc = ReactiveCommand.Create(onDelProcedureCommand);

                if (mw.setViewTab != 0)
                {
                    PrivateTabContextMenuItems.Clear();
                    PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Add Subchart",  Command = AddSub });
                    PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Add Procedure", Command = AddProc});
                    if (mw.theTabs[mw.setViewTab].Start.GetType() == typeof(Oval_Procedure))
                    {
                        PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Modify Procedure", Command = ModProc});
                        PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Delete Procedure", Command = DelProc});
                    }
                    else
                    {
                        PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Rename Subchart", Command = ModSub});
                        PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Delete Subchart", Command = DelProc});
                    }


                }
                else
                {
                    PrivateTabContextMenuItems.Clear();
                    PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Add Subchart", Command = AddSub });
                    PrivateTabContextMenuItems.Add(new MenuItemViewModel() { Header = "Add Procedure", Command = AddProc });
                }
                return PrivateTabContextMenuItems;
            }
        }

        private ObservableCollection<MenuItemViewModel> ObservableMenuItems;
        public ObservableCollection<MenuItemViewModel> ContextMenuItemsFunction
        {
            get
            {
                    positionXTapped = positionX;
                    positionYTapped = positionY;
                    return ObservableMenuItems;

            }
        }
        
        public int num_params;
        public virtual int Num_Params
        {
            get
            {
                return ((Oval_Procedure)this.Start).Parameter_Count;
            }
            set{
                ((Oval_Procedure)this.Start).Parameter_Count = value;
            }
        }

        public Subchart_Kinds Subchart_Kind
        {
            get
            {
                return kind;
            }
        }
    }

    public class Procedure_Chart : Subchart
    {
        public Procedure_Chart(string name, int parameter_count) 
        {
            Header = name;
            End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            End.Text = "end";
            Start = new Oval_Procedure(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval", parameter_count);
            Start.Text = "start";
        }
        public Procedure_Chart(string name, string[] incoming_params, bool[] ins, bool[] outs) 
        {
            Header = name;
            End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            End.Text = "end";
            Start = new Oval_Procedure(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval", incoming_params, ins, outs);
            //Start.Text = "start";
        }
        public override int Num_Params
        {
            get
            {
                return ((Oval_Procedure)this.Start).Parameter_Count;
            }
        }
    }
}