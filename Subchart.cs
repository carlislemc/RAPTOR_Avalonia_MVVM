using Avalonia.Input;
using RAPTOR_Avalonia_MVVM.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Reactive;

namespace raptor
{
    public enum Subchart_Kinds { Subchart, Procedure, Function, UML };

    public class Subchart
    {
        public Oval Start, End;
        public string Text="Main";
        protected Subchart_Kinds kind = Subchart_Kinds.Subchart;

        // This has to be a property because it has a binding to the XAML 
        // (because reasons)
        public string Header
        {
            get { return this.Text; }
            set { Text = value;  }
        }
        public Subchart Content()
        {
            return this;
        }
        public ReactiveCommand<Unit, Unit> OpenCommand { get; set; }
        public ReactiveCommand<Unit, Unit> SaveCommand { get; set;  }
        private void initContextMenu()
        {
            OpenCommand = ReactiveCommand.Create(OnOpenCommand);
            SaveCommand = ReactiveCommand.Create(OnSaveCommand);
            //OpenRecentCommand = ReactiveCommand.Create<string>(OpenRecent);

            ContextMenuItemsFunction = new[]
            {
                new MenuItemViewModel { Header = "_Open...", Command = OpenCommand },
                new MenuItemViewModel { Header = "_Save", Command = SaveCommand },
                new MenuItemViewModel { Header = "-" }
            };
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

        public void OnSaveCommand() { }
        public void OnOpenCommand() { }

        public Subchart(string name)
        {
            initContextMenu();

            Text = name;
            End = new Oval(Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            End.Text = "end";
            Start = new Oval(End, Visual_Flow_Form.flow_height, Visual_Flow_Form.flow_width, "Oval");
            Start.Text = "start";
        }
        public void OnTapped(object sender, PointerEventArgs e)
        {
            this.Start.Text = "tapped";
        }

        private IReadOnlyList<MenuItemViewModel> menuItemListPrivate;
        public IReadOnlyList<MenuItemViewModel> ContextMenuItemsFunction
        {
            get
            {
                return menuItemListPrivate;
            }
            set
            {
                menuItemListPrivate = value;
            }
        }
        public void OnPasteCommand() { }
        public void OnCopyCommand() { }
        public virtual int num_params
        {
            get
            {
                return 0;
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
        public override int num_params
        {
            get
            {
                return ((Oval_Procedure)this.Start).Parameter_Count;
            }
        }
    }
}