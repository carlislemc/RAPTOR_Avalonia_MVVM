using System;
using System.Collections;
using RAPTOR_Avalonia_MVVM.ViewModels;
using System.Collections.ObjectModel;

namespace raptor
{
	/// <summary>
	/// Summary description for Undo_Stack.
	/// </summary>
	public class Undo_Stack
	{
		public static int num_undo = 0;
		public static int num_redo = 0;
		public static ArrayList Undo_array = new ArrayList();
		public static ArrayList Redo_array = new ArrayList();
		const int Can_Undo=8;
		const int Can_Redo=9;
		const int No_Undo=14;
		const int No_Redo=15;

		public enum Action_Kind {Rename_Tab, Add_Tab, Delete_Tab, Change_Tab}

		public class Action 
		{
			public Action_Kind kind;
			public Subchart chart;
            //public Microsoft.Ink.Ink ink;
			public string old_name;
			public string new_name;
			public Component clone;
			public Action()
			{
			}
			public Action Clone()
			{
				Action result = (Action) this.MemberwiseClone();
				result.clone = this.clone.Clone();
				return result;
			}
		}

		public Undo_Stack()
		{
		}

		private static void Add_Undo_Action(Action new_action/*, Visual_Flow_Form form*/)
		{
			if(num_undo == 0){
				MainWindowViewModel.GetMainWindowViewModel().toggleUndoCommand = true;
			}
			num_undo++;
			/*form.undoButton.ImageIndex = Can_Undo;
			form.menuItemUndo.Enabled=true;*/
			if (num_undo-1 >= Undo_array.Count)
			{
				Undo_array.Add(new_action);
			}
			else
			{
				Undo_array[num_undo-1] = new_action;
			}
		}

		private static void Clear_Redo(/*Visual_Flow_Form form*/)
		{
			num_redo=0;
			//MainWindowViewModel.GetMainWindowViewModel().toggleRedoCommand = false;
			/*form.redoButton.ImageIndex = No_Redo;
			form.menuItemRedo.Enabled=false;*/
		}

		private static void Add_Redo_Action(Action new_action/*, Visual_Flow_Form form*/)
		{
			if(num_redo == 0){
				MainWindowViewModel.GetMainWindowViewModel().toggleRedoCommand = true;
			}
			num_redo++;
			/*form.redoButton.ImageIndex = Can_Redo;
			form.menuItemRedo.Enabled=true;*/
			if (num_redo-1 >= Redo_array.Count)
			{
				Redo_array.Add(new_action);
			}
			else
			{
				Redo_array[num_redo-1] = new_action;
			}
		}

		public static void Decrement_Undoable(/*Visual_Flow_Form form*/)
		{
			num_undo--;
			if (num_undo  < 1)
			{
				MainWindowViewModel.GetMainWindowViewModel().toggleUndoCommand = false;
				/*form.undoButton.ImageIndex = No_Undo;
				form.menuItemUndo.Enabled=false;
				form.modified = false;*/
			}
		}

		private static void Decrement_Redoable(/*Visual_Flow_Form form*/)
		{
			num_redo--;
			if (num_redo  < 1)
			{
				MainWindowViewModel.GetMainWindowViewModel().toggleRedoCommand = false;
				/*form.redoButton.ImageIndex = No_Redo;
				form.menuItemRedo.Enabled=false;
				form.modified = false;*/

				num_redo = 0;
			}
		}

		public static void Make_Add_Tab_Undoable(/*Visual_Flow_Form form, */Subchart chart)
		{
			Action new_action;
            ////form.modified = true;
            new_action = new Action();
			new_action.kind = Action_Kind.Add_Tab;
			new_action.chart = chart;
			Add_Undo_Action(new_action/*, form*/);
			Clear_Redo(/*form*/);
		}

		public static void Make_Delete_Tab_Undoable(/*Visual_Flow_Form form, */Subchart chart)
		{
			Action new_action;
            //form.modified = true;
            new_action = new Action();
			new_action.kind = Action_Kind.Delete_Tab;
			new_action.chart = chart;
			Add_Undo_Action(new_action/*, form*/);
			Clear_Redo(/*form*/);
		}

		public static void Make_Rename_Tab_Undoable(/*Visual_Flow_Form form,*/ Subchart chart, 
			string old_name, string new_name)
		{
			Action new_action;
            //form.modified = true;
			new_action = new Action();
			new_action.kind = Action_Kind.Rename_Tab;
			new_action.new_name = new_name;
			new_action.old_name = old_name;
			new_action.chart = chart;
			Add_Undo_Action(new_action/*, form*/);
			Clear_Redo(/*form*/);
		}

		public static void Make_Undoable(Subchart current/*Visual_Flow_Form form*/)
		{
			//Subchart current = form.selectedTabMaybeNull();
			//Subchart current = null;
			if (current==null)
            {
                return;
            }
			Action new_action;
            bool was_enabled;
			new_action = new Action();
			new_action.clone = current.Start.Clone();
			new_action.kind = Action_Kind.Change_Tab;
			new_action.chart = current;
            
            // was_enabled = new_action.chart.tab_overlay.Enabled;
            // new_action.chart.tab_overlay.Enabled = false;
            // new_action.chart.tab_overlay.Enabled = was_enabled;

            Add_Undo_Action(new_action/*, form*/);
			Clear_Redo(/*form*/);
		}

		public static void Undo_Action(Subchart current /*Visual_Flow_Form form*/)
		{
			//Subchart current = form.selectedTabMaybeNull();
			//Subchart current = null;
			if (num_undo > 0 && current!=null)
			{
				ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;
				Action this_action = ((Action) Undo_array[num_undo-1]);
				Action redo_action;
				redo_action = new Action();
				redo_action.kind = this_action.kind;
				redo_action.chart = this_action.chart;
				switch (this_action.kind)
				{
						
					case Action_Kind.Rename_Tab:
						redo_action.old_name = this_action.old_name;
						redo_action.new_name = this_action.new_name;
						Add_Redo_Action(redo_action/*,form*/);
						
						this_action.chart.Header=this_action.old_name;
						//form.Rename_Tab(this_action.new_name,this_action.old_name);
						//form.carlisle.SelectedTab=this_action.chart;
						break;
					case Action_Kind.Add_Tab:
						Add_Redo_Action(redo_action/*,form*/);
						tbs.Remove(this_action.chart);
						//form.carlisle.TabPages.Remove(this_action.chart);
						break;
					case Action_Kind.Delete_Tab:
						Add_Redo_Action(redo_action/*,form*/);
						tbs.Add(this_action.chart);
						//form.carlisle.TabPages.Add(this_action.chart);
						//form.carlisle.SelectedTab=this_action.chart;
						break;
					case Action_Kind.Change_Tab:
						redo_action.clone = this_action.chart.Start.Clone();
                        bool was_enabled = true;
                        /*if (!Component.BARTPE && !Component.VM && !Component.MONO)
                        {
                            was_enabled = this_action.chart.tab_overlay.Enabled;
                            this_action.chart.tab_overlay.Enabled = false;
                            redo_action.ink = this_action.chart.tab_overlay.Ink.Clone();
                        }*/
                        Add_Redo_Action(redo_action/*, form*/);

						this_action.chart.Start = (Oval)this_action.clone.Clone();
                        /*if (!Component.BARTPE && !Component.VM && !Component.MONO)
                        {
                            this_action.chart.tab_overlay.Ink = this_action.ink.Clone();
                            this_action.chart.tab_overlay.Enabled = was_enabled;
                        }*/
                        //this_action.chart.Start.scale = form.scale;
						//this_action.chart.Start.Scale(form.scale);
						//form.my_layout();
						//form.Current_Selection = current.Start.select(-1000,-1000);
						//this_action.chart.flow_panel.Invalidate();
                        /*(this_action.chart.Parent as System.Windows.Forms.TabControl).SelectedTab = this_action.chart;
                        if (this_action.chart.Parent != form.carlisle)
                        {
                            form.carlisle.SelectedTab = this_action.chart.Parent.Parent as System.Windows.Forms.TabPage;
                        }*/
                        //form.carlisle.SelectedTab=this_action.chart;
						break;
				}

				Decrement_Undoable(/*form*/);
			}
		}

		public static void Redo_Action(Subchart current/*Visual_Flow_Form form*/)
		{
			//Subchart current = form.selectedTabMaybeNull();
			//Subchart current = null;
			if (num_redo > 0 && current!=null)
			{
				ObservableCollection<Subchart> tbs = MainWindowViewModel.GetMainWindowViewModel().theTabs;
				Action this_action = ((Action) Redo_array[num_redo-1]);
				Action undo_action;
				undo_action = new Action();
				undo_action.kind = this_action.kind;
				undo_action.chart = this_action.chart;
				switch (this_action.kind)
				{
						
					case Action_Kind.Rename_Tab:
						undo_action.old_name = this_action.old_name;
						undo_action.new_name = this_action.new_name;
						Add_Undo_Action(undo_action/*,form*/);
						
						this_action.chart.Header=this_action.new_name;
						//form.Rename_Tab(this_action.old_name,this_action.new_name);
						//form.carlisle.SelectedTab=this_action.chart;
						break;
					case Action_Kind.Add_Tab:
						Add_Undo_Action(undo_action/*,form*/);
						tbs.Add(this_action.chart);
						//form.carlisle.TabPages.Add(this_action.chart);
						//form.carlisle.SelectedTab=this_action.chart;
						break;
					case Action_Kind.Delete_Tab:
						Add_Undo_Action(undo_action/*,form*/);
						tbs.Remove(this_action.chart);
						//form.carlisle.TabPages.Remove(this_action.chart);
						break;
					case Action_Kind.Change_Tab:
						undo_action.clone = this_action.chart.Start.Clone();
                        bool was_enabled = true;
                        /*if (!Component.BARTPE && !Component.VM && !Component.MONO)
                        {
                            was_enabled = this_action.chart.tab_overlay.Enabled;
                            this_action.chart.tab_overlay.Enabled = false;
                            undo_action.ink = this_action.chart.tab_overlay.Ink.Clone();
                        }*/
						Add_Undo_Action(undo_action/*,form*/);

						this_action.chart.Start = (Oval)this_action.clone.Clone();
                        /*if (!Component.BARTPE && !Component.VM && !Component.MONO)
                        {
                            this_action.chart.tab_overlay.Ink = this_action.ink.Clone();
                            this_action.chart.tab_overlay.Enabled = was_enabled;
                        }*/
                        /*this_action.chart.Start.scale = form.scale;
						this_action.chart.Start.Scale(form.scale);
						form.my_layout();
						form.Current_Selection = current.Start.select(-1000,-1000);
						this_action.chart.flow_panel.Invalidate();
                        (this_action.chart.Parent as System.Windows.Forms.TabControl).SelectedTab = this_action.chart;
                        if (this_action.chart.Parent != form.carlisle)
                        {
                            form.carlisle.SelectedTab = this_action.chart.Parent.Parent as System.Windows.Forms.TabPage;
                        }*/
                        //form.carlisle.SelectedTab=this_action.chart;
						break;
				}

				Decrement_Redoable(/*form*/);
			}
		}

		public static void Clear_Undo(/*Visual_Flow_Form form*/)
		{
			num_undo = 0;
			num_redo = 0;
			/*form.redoButton.ImageIndex = No_Redo;
			form.menuItemRedo.Enabled=false;
			form.undoButton.ImageIndex = No_Undo;
			form.menuItemUndo.Enabled=false;
			form.modified = false;*/
		}
	}
}
