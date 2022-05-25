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
                    if (this.contextMenuType != 1)
                    {
                        this.ObservableMenuItems.Clear();
                        foreach (MenuItemViewModel j in this.OverSymbolMenuItemsFunction)
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
                new MenuItemViewModel { Header = "_Paste", Command = PasteCommand },
                new MenuItemViewModel { Header = "Insert Assignment", Command = InsertAssignmentCommand },
                new MenuItemViewModel { Header = "Insert Call", Command = InsertCallCommand },
                new MenuItemViewModel { Header = "Insert Input", Command = InsertInputCommand },
                new MenuItemViewModel { Header = "Insert Output", Command = InsertOutputCommand },
                new MenuItemViewModel { Header = "Insert Selection", Command = InsertSelectionCommand },
                new MenuItemViewModel { Header = "Insert Loop", Command = InsertLoopCommand }
            };

            OverSymbolMenuItemsFunction = new[]
{
                new MenuItemViewModel { Header = "_Edit", Command = EditCommand },
                new MenuItemViewModel { Header = "Comment", Command = CommentCommand },
                new MenuItemViewModel { Header = "Toggle Breakpoint", Command = ToggleBreakpointCommand },
                new MenuItemViewModel { Header = "C_ut", Command = CutCommand },
                new MenuItemViewModel { Header = "C_opy", Command = CopyCommand },
                new MenuItemViewModel { Header = "_Delete", Command = DeleteCommand }
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
            }
            else
            {
                Undo_Stack.Decrement_Undoable();
            }
        }
        public void OnEditCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnEditCommand();
        }

        public void OnCommentCommand(){
            CommentBox cb = new CommentBox();
            cb.setText();
        }
        public void OnToggleBreakpointCommand() { }
        public void OnCutCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnCutCommand();
        }
        public void OnCopyCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnCopyCommand();
        }
        public void OnDeleteCommand() {
            MainWindowViewModel.GetMainWindowViewModel().OnDeleteCommand();
        }

        public void onAddSubchartCommand(){
            AddSubchartDialog asc = new AddSubchartDialog();
            asc.ShowDialog(MainWindow.topWindow);
        }
        public void onAddProcedureCommand(){
            AddProcedureDialog avm = new AddProcedureDialog();
            avm.ShowDialog(MainWindow.topWindow);
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
        private ObservableCollection<MenuItemViewModel> ObservableMenuItems;
        public ObservableCollection<MenuItemViewModel> ContextMenuItemsFunction
        {
            get
            {

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