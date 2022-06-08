using System;
using System.Collections.Generic;


namespace raptor
{
	/// <summary>
	/// Summary description for StopSign.
	/// </summary>
	public class StopSign
	{
		public StopSign()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static Avalonia.Controls.Shapes.Polygon Make_Path(
			int x, int y, int size)
		{
			Avalonia.Controls.Shapes.Polygon result = new Avalonia.Controls.Shapes.Polygon();
			result.Points = new List<Avalonia.Point>();
			result.Points.Add(new Avalonia.Point(x, y + size / 3));
			result.Points.Add(new Avalonia.Point(x + size / 3, y));
			result.Points.Add(new Avalonia.Point(x + 2 * size / 3, y));
			result.Points.Add(new Avalonia.Point(x + size, y + size / 3));
			result.Points.Add(new Avalonia.Point(x + size, y + 2 * size / 3));
			result.Points.Add(new Avalonia.Point(x + 2 * size / 3, y + size));
			result.Points.Add(new Avalonia.Point(x + size / 3, y + size));
			result.Points.Add(new Avalonia.Point(x, y + 2 * size / 3));


/*			result.StartFigure();
			result.AddLine(x,y+size/3,x+size/3,y);
			result.AddLine(x+size/3,y,x+2*size/3,y);
			result.AddLine(x+2*size/3,y,x+size,y+size/3);
			result.AddLine(x+size,y+size/3,x+size,y+2*size/3);
			result.AddLine(x+size,y+2*size/3,x+2*size/3,y+size);
			result.AddLine(x+2*size/3,y+size,x+size/3,y+size);
			result.AddLine(x+size/3,y+size,x,y+2*size/3);
			result.AddLine(x,y+2*size/3,x,y+size/3);*/
			return result;
		}

		public static void Draw(Avalonia.Media.DrawingContext gr,
			int x, int y, int size)
		{
			Avalonia.Controls.Shapes.Polygon gp = Make_Path(x,y,size);
			gp.Fill=(PensBrushes.redbrush);
			gr.DrawGeometry(gp.Fill,PensBrushes.black_pen,gp.DefiningGeometry);
		}
	}
}
