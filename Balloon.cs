using System;
using System.Drawing.Drawing2D;


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
		
		public static GraphicsPath Make_Path(
			Avalonia.Rect rec,
			Corner corner)
		{
			GraphicsPath result = new GraphicsPath();

			if (rec.Width < 2*radius)
			{
				// need extra room here for /|
				rec = rec.WithWidth(3*radius);
			}
			if (rec.Height < 2*radius)
			{
				rec = rec.WithHeight(2*radius);
			}
			result.StartFigure();
			result.AddLine((int)-rec.Width/2,(int)rec.Height/2-radius,(int)-rec.Width/2,(int)-rec.Height/2+radius);
			result.AddArc((int)-rec.Width/2,(int)-rec.Height/2,2*radius,2*radius,180,90);
			switch (corner)
			{
				case Corner.Upper_Left:
					result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)-rec.Width/2+radius,(int)-rec.Height/2-radius);
					result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2-radius,(int)-rec.Width/2+2*radius,(int)-rec.Height/2);
					result.AddLine((int)-rec.Width/2+2*radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2);
					break;
				case Corner.Upper_Right:
					result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)rec.Width/2-2*radius,(int)-rec.Height/2);
					result.AddLine((int)rec.Width/2-2*radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2-radius);
					result.AddLine((int)rec.Width/2-radius,(int)-rec.Height/2-radius,(int)rec.Width/2-radius,(int)-rec.Height/2);
					break;
				case Corner.Lower_Left|Corner.Lower_Right:
					result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2);
					break;
			}
			result.AddArc((int)rec.Width/2-2*radius,(int)-rec.Height/2,2*radius,2*radius,270,90);
			result.AddLine((int)rec.Width/2,(int)-rec.Height/2+radius,(int)rec.Width/2,(int)+rec.Height/2-radius);
			result.AddArc((int)rec.Width/2-2*radius,(int)rec.Height/2-2*radius,2*radius,2*radius,
				0,90);
			switch (corner)
			{
				case Corner.Upper_Right|Corner.Upper_Left:
					result.AddLine((int)-rec.Width/2+radius,(int)rec.Height/2,(int)rec.Width/2-radius,(int)rec.Height/2);
					break;
				case Corner.Lower_Left:
					result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2,(int)-rec.Width/2+2*radius,(int)rec.Height/2);
					result.AddLine((int)-rec.Width/2+2*radius,(int)rec.Height/2,(int)-rec.Width/2+radius,(int)rec.Height/2+radius);
					result.AddLine((int)-rec.Width/2+radius,(int)rec.Height/2+radius,(int)-rec.Width/2+radius,(int)rec.Height/2);
					break;
				case Corner.Lower_Right:
					result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2,(int)rec.Width/2-radius,(int)rec.Height/2+radius);
					result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2+radius,(int)rec.Width/2-2*radius,(int)rec.Height/2);
					result.AddLine((int)rec.Width/2-2*radius,(int)rec.Height/2,(int)-rec.Width/2+radius,(int)rec.Height/2);
					break;
			}
			result.AddArc((int)-rec.Width/2,(int)rec.Height/2-2*radius,2*radius,2*radius,90,90);
			result.CloseFigure();
			Matrix matrix = new Matrix();
			float f1 = Convert.ToSingle(rec.Left) + Convert.ToSingle(rec.Width/2);
			float f2 = (float)rec.Top + (float)rec.Height/2;
			matrix.Translate(f1, f2);
			result.Transform(matrix);
			return result;
		}
	}
}
