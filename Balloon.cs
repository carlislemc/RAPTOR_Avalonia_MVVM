using System;
using System.Drawing.Drawing2D;
using Avalonia.Collections;
using Avalonia.Media;


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
		
		public static Geometry Make_Path(
			Avalonia.Rect rec,
			Corner corner)
		{
			//Geometry result = new Geometry();
			PathGeometry result = new PathGeometry();

			LineGeometry l1 = new LineGeometry();
			LineGeometry l2 = new LineGeometry();
			LineGeometry l3 = new LineGeometry();
			LineGeometry l4 = new LineGeometry();
			LineGeometry l5 = new LineGeometry();
			LineGeometry l6 = new LineGeometry();
			LineGeometry l7 = new LineGeometry();
			LineGeometry l8 = new LineGeometry();
			LineGeometry l9 = new LineGeometry();
			LineGeometry l10 = new LineGeometry();
			LineGeometry l11 = new LineGeometry();
			LineGeometry l12 = new LineGeometry();
			LineGeometry l13 = new LineGeometry();
			LineGeometry l14 = new LineGeometry();
			LineGeometry l15 = new LineGeometry();
			LineGeometry l16 = new LineGeometry();

			GeometryGroup g = new GeometryGroup();

			if (rec.Width < 2*radius)
			{
				// need extra room here for /|
				rec = rec.WithWidth(3*radius);
			}
			if (rec.Height < 2*radius)
			{
				rec = rec.WithHeight(2*radius);
			}

			l1.StartPoint = new Avalonia.Point(-rec.Width / 2 + 50, rec.Height / 2 - radius + 50);
			l1.EndPoint = new Avalonia.Point(-rec.Width / 2 + 50, -rec.Height / 2 + radius + 50);
			g.Children.Add(l1);

			//Variable oasjdnfo = new Variable(corner + "", new numbers.value());

			switch (corner)
            {
				case Corner.Upper_Left:
					l2.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 + 50);
					l2.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					g.Children.Add(l2);

					l3.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					l3.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					g.Children.Add(l3);

					l4.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					l4.EndPoint = new Avalonia.Point(rec.Width / 2 - radius+50, -rec.Height / 2+50);
					g.Children.Add(l4);

					break;
				case Corner.Upper_Right:
					l5.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 + 50);
					l5.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					g.Children.Add(l5);

					l6.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					l6.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					g.Children.Add(l6);

					l7.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					l7.EndPoint = new Avalonia.Point(rec.Width / 2 - radius + 50, -rec.Height / 2 + 50);
					g.Children.Add(l7);

					break;
				case Corner.Lower_Left:
				case Corner.Lower_Right:
					Variable oubsoud = new Variable("here", new numbers.value());
					l8.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 + 50);
					l8.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					g.Children.Add(l8);

					l9.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius + 50, -rec.Height / 2 - radius + 50);
					l9.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					g.Children.Add(l9);

					l10.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius + 50, -rec.Height / 2 + 50);
					l10.EndPoint = new Avalonia.Point(rec.Width / 2 - radius + 50, -rec.Height / 2 + 50);
					g.Children.Add(l10);

					break;
			}

			


			

			//GraphicsPath g = new GraphicsPath();
			//g.AddLine()

			//result.StartFigure();
			//result.AddLine((int)-rec.Width/2,(int)rec.Height/2-radius,(int)-rec.Width/2,(int)-rec.Height/2+radius);
			//result.AddArc((int)-rec.Width/2,(int)-rec.Height/2,2*radius,2*radius,180,90);
			//switch (corner)
			//{
			//	case Corner.Upper_Left:
			//		result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)-rec.Width/2+radius,(int)-rec.Height/2-radius);
			//		result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2-radius,(int)-rec.Width/2+2*radius,(int)-rec.Height/2);
			//		result.AddLine((int)-rec.Width/2+2*radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2);
			//		break;
			//	case Corner.Upper_Right:
			//		result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)rec.Width/2-2*radius,(int)-rec.Height/2);
			//		result.AddLine((int)rec.Width/2-2*radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2-radius);
			//		result.AddLine((int)rec.Width/2-radius,(int)-rec.Height/2-radius,(int)rec.Width/2-radius,(int)-rec.Height/2);
			//		break;
			//	case Corner.Lower_Left|Corner.Lower_Right:
			//		result.AddLine((int)-rec.Width/2+radius,(int)-rec.Height/2,(int)rec.Width/2-radius,(int)-rec.Height/2);
			//		break;
			//}
			//result.AddArc((int)rec.Width/2-2*radius,(int)-rec.Height/2,2*radius,2*radius,270,90);
			//result.AddLine((int)rec.Width/2,(int)-rec.Height/2+radius,(int)rec.Width/2,(int)+rec.Height/2-radius);
			//result.AddArc((int)rec.Width/2-2*radius,(int)rec.Height/2-2*radius,2*radius,2*radius,
			//	0,90);
			//switch (corner)
			//{
			//	case Corner.Upper_Right|Corner.Upper_Left:
			//		result.AddLine((int)-rec.Width/2+radius,(int)rec.Height/2,(int)rec.Width/2-radius,(int)rec.Height/2);
			//		break;
			//	case Corner.Lower_Left:
			//		result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2,(int)-rec.Width/2+2*radius,(int)rec.Height/2);
			//		result.AddLine((int)-rec.Width/2+2*radius,(int)rec.Height/2,(int)-rec.Width/2+radius,(int)rec.Height/2+radius);
			//		result.AddLine((int)-rec.Width/2+radius,(int)rec.Height/2+radius,(int)-rec.Width/2+radius,(int)rec.Height/2);
			//		break;
			//	case Corner.Lower_Right:
			//		result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2,(int)rec.Width/2-radius,(int)rec.Height/2+radius);
			//		result.AddLine((int)rec.Width/2-radius,(int)rec.Height/2+radius,(int)rec.Width/2-2*radius,(int)rec.Height/2);
			//		result.AddLine((int)rec.Width/2-2*radius,(int)rec.Height/2,(int)-rec.Width/2+radius,(int)rec.Height/2);
			//		break;
			//}
			//result.AddArc((int)-rec.Width/2,(int)rec.Height/2-2*radius,2*radius,2*radius,90,90);
			//result.CloseFigure();
			//Matrix matrix = new Matrix();
			//float f1 = Convert.ToSingle(rec.Left) + Convert.ToSingle(rec.Width/2);
			//float f2 = (float)rec.Top + (float)rec.Height/2;
			//matrix.Translate(f1, f2);
			//result.Transform(matrix);

			return g;
		}
	}
}
