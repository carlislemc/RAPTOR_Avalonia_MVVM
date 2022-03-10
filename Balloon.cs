using System;



namespace raptor
{
	/// <summary>
	/// Summary description for Balloon.
	/// </summary>
	public class Balloon
	{
		public static int radius = 10;

		public Balloon()
		{
		}

		public enum Corner {Upper_Left, Lower_Left, Upper_Right, Lower_Right};
		/*
		public static GraphicsPath Make_Path(
			Avalonia.Rect rec,
			Corner corner)
		{
			GraphicsPath result = new GraphicsPath();

			if (rec.Width < 2*radius)
			{
				// need extra room here for /|
				rec.Width = 3*radius;
			}
			if (rec.Height < 2*radius)
			{
				rec.Height = 2*radius;
			}
			result.StartFigure();
			result.AddLine(-rec.Width/2,rec.Height/2-radius,-rec.Width/2,-rec.Height/2+radius);
			result.AddArc(-rec.Width/2,-rec.Height/2,2*radius,2*radius,180,90);
			switch (corner)
			{
				case Corner.Upper_Left:
					result.AddLine(-rec.Width/2+radius,-rec.Height/2,-rec.Width/2+radius,-rec.Height/2-radius);
					result.AddLine(-rec.Width/2+radius,-rec.Height/2-radius,-rec.Width/2+2*radius,-rec.Height/2);
					result.AddLine(-rec.Width/2+2*radius,-rec.Height/2,rec.Width/2-radius,-rec.Height/2);
					break;
				case Corner.Upper_Right:
					result.AddLine(-rec.Width/2+radius,-rec.Height/2,rec.Width/2-2*radius,-rec.Height/2);
					result.AddLine(rec.Width/2-2*radius,-rec.Height/2,rec.Width/2-radius,-rec.Height/2-radius);
					result.AddLine(rec.Width/2-radius,-rec.Height/2-radius,rec.Width/2-radius,-rec.Height/2);
					break;
				case Corner.Lower_Left|Corner.Lower_Right:
					result.AddLine(-rec.Width/2+radius,-rec.Height/2,rec.Width/2-radius,-rec.Height/2);
					break;
			}
			result.AddArc(rec.Width/2-2*radius,-rec.Height/2,2*radius,2*radius,270,90);
			result.AddLine(rec.Width/2,-rec.Height/2+radius,rec.Width/2,+rec.Height/2-radius);
			result.AddArc(rec.Width/2-2*radius,rec.Height/2-2*radius,2*radius,2*radius,
				0,90);
			switch (corner)
			{
				case Corner.Upper_Right|Corner.Upper_Left:
					result.AddLine(-rec.Width/2+radius,rec.Height/2,rec.Width/2-radius,rec.Height/2);
					break;
				case Corner.Lower_Left:
					result.AddLine(rec.Width/2-radius,rec.Height/2,-rec.Width/2+2*radius,rec.Height/2);
					result.AddLine(-rec.Width/2+2*radius,rec.Height/2,-rec.Width/2+radius,rec.Height/2+radius);
					result.AddLine(-rec.Width/2+radius,rec.Height/2+radius,-rec.Width/2+radius,rec.Height/2);
					break;
				case Corner.Lower_Right:
					result.AddLine(rec.Width/2-radius,rec.Height/2,rec.Width/2-radius,rec.Height/2+radius);
					result.AddLine(rec.Width/2-radius,rec.Height/2+radius,rec.Width/2-2*radius,rec.Height/2);
					result.AddLine(rec.Width/2-2*radius,rec.Height/2,-rec.Width/2+radius,rec.Height/2);
					break;
			}
			result.AddArc(-rec.Width/2,rec.Height/2-2*radius,2*radius,2*radius,90,90);
			result.CloseFigure();
			Matrix matrix = new Matrix();
			matrix.Translate(rec.Left+rec.Width/2,rec.Top+rec.Height/2);
			result.Transform(matrix);
			return result;
		}*/
	}
}
