using System;
using System.Drawing.Drawing2D;
using Avalonia.Collections;
using Avalonia.Media;
using System.Collections.Generic;


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

			LineGeometry l = new LineGeometry();
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

			GraphicsPath aa = new GraphicsPath();

			
			float f1 = Convert.ToSingle(rec.Left) + Convert.ToSingle(rec.Width / 2);
			float f2 = (float)rec.Top + (float)rec.Height / 2;
			Avalonia.Vector v = new Avalonia.Vector(f1, f2);
			Avalonia.Matrix m = Avalonia.Matrix.CreateTranslation(v);
			

			if (rec.Width < 2*radius)
			{
				// need extra room here for /|
				rec = rec.WithWidth(3*radius);
			}
			if (rec.Height < 2*radius)
			{
				rec = rec.WithHeight(2*radius);
			}

			l.StartPoint = new Avalonia.Point(-rec.Width / 2 /*+100*/, rec.Height / 2 - radius /*+100*/);
			l.EndPoint = new Avalonia.Point(-rec.Width / 2 /*+100*/, -rec.Height / 2 + radius /*+100*/);
			l.StartPoint.Transform(m);
			l.EndPoint.Transform(m);

			g.Children.Add(l);


			EllipseGeometry a2 = new EllipseGeometry();

			a2.Center = new Avalonia.Point(-rec.Width / 2 /*+100*/ + radius, -rec.Height / 2 /*+100*/ + radius);
			a2.RadiusX = radius;
			a2.RadiusY = radius;
			
			g.Children.Add(a2);


			switch (corner)
            {
				case Corner.Upper_Left:
					l2.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 /*+100*/);
					l2.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 - radius /*+100*/);
					l2.StartPoint.Transform(m);
					l2.EndPoint.Transform(m);
					g.Children.Add(l2);

					l3.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 - radius /*+100*/);
					l3.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, -rec.Height / 2 /*+100*/);
					l3.StartPoint.Transform(m);
					l3.EndPoint.Transform(m);
					g.Children.Add(l3);

					l4.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, -rec.Height / 2 /*+100*/);
					l4.EndPoint = new Avalonia.Point(rec.Width / 2 - radius/*+100*/, -rec.Height / 2/*+100*/);
					l4.StartPoint.Transform(m);
					l4.EndPoint.Transform(m);
					g.Children.Add(l4);

					break;
				case Corner.Upper_Right:
					l5.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 /*+100*/);
					l5.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 - radius /*+100*/);
					l5.StartPoint.Transform(m);
					l5.EndPoint.Transform(m);
					g.Children.Add(l5);

					l6.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 - radius /*+100*/);
					l6.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, -rec.Height / 2 /*+100*/);
					l6.StartPoint.Transform(m);
					l6.EndPoint.Transform(m);
					g.Children.Add(l6);

					l7.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, -rec.Height / 2 /*+100*/);
					l7.EndPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, -rec.Height / 2 /*+100*/);
					l7.StartPoint.Transform(m);
					l7.EndPoint.Transform(m);
					g.Children.Add(l7);

					break;
				case Corner.Lower_Left:
				case Corner.Lower_Right:
					l8.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, -rec.Height / 2 /*+100*/);
					l8.EndPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, -rec.Height / 2 /*+100*/);
					l8.StartPoint.Transform(m);
					l8.EndPoint.Transform(m);
					g.Children.Add(l8);

					break;
			}

			EllipseGeometry a3 = new EllipseGeometry();

			a3.Center = new Avalonia.Point(rec.Width / 2 - 2 * radius /*+100*/ + radius, -rec.Height / 2 /*+100*/ + radius);
			a3.RadiusX = radius;
			a3.RadiusY = radius;
			g.Children.Add(a3);

			l9.StartPoint = new Avalonia.Point(rec.Width / 2 /*+100*/, -rec.Height / 2 + radius /*+100*/);
            l9.EndPoint = new Avalonia.Point(rec.Width / 2 /*+100*/, +rec.Height / 2 - radius /*+100*/);
			l9.StartPoint.Transform(m);
			l9.EndPoint.Transform(m);
			g.Children.Add(l9);

			EllipseGeometry a4 = new EllipseGeometry();

			a4.Center = new Avalonia.Point(rec.Width / 2 - 2 * radius /*+100*/ + radius, rec.Height / 2 - 2 * radius /*+100*/ + radius);
			a4.RadiusX = radius;
			a4.RadiusY = radius;
			g.Children.Add(a4);

			switch (corner)
            {
				case Corner.Upper_Left:
				case Corner.Upper_Right:
					l10.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, rec.Height / 2 /*+100*/);
					l10.EndPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, rec.Height / 2 /*+100*/);
					l10.StartPoint.Transform(m);
					l10.EndPoint.Transform(m);
					g.Children.Add(l10);
					break;
				case Corner.Lower_Left:
					l11.StartPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, rec.Height / 2 /*+100*/);
					l11.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, rec.Height / 2 /*+100*/);
					l11.StartPoint.Transform(m);
					l11.EndPoint.Transform(m);
					g.Children.Add(l11);

					l12.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius /*+100*/, rec.Height / 2 /*+100*/);
					l12.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, rec.Height / 2 + radius /*+100*/);
					l12.StartPoint.Transform(m);
					l12.EndPoint.Transform(m);
					g.Children.Add(l12);

					l13.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, rec.Height / 2 + radius /*+100*/);
					l13.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, rec.Height / 2 /*+100*/);
					l13.StartPoint.Transform(m);
					l13.EndPoint.Transform(m);
					g.Children.Add(l13);

					break;
				case Corner.Lower_Right:
					l14.StartPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, rec.Height / 2 /*+100*/);
					l14.EndPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, rec.Height / 2 + radius /*+100*/);
					l14.StartPoint.Transform(m);
					l14.EndPoint.Transform(m);
					g.Children.Add(l14);

					l15.StartPoint = new Avalonia.Point(rec.Width / 2 - radius /*+100*/, rec.Height / 2 + radius /*+100*/);
					l15.EndPoint = new Avalonia.Point(rec.Width / 2 - 2 * radius /*+100*/, rec.Height / 2 /*+100*/);
					l15.StartPoint.Transform(m);
					l15.EndPoint.Transform(m);
					g.Children.Add(l15);

					l16.StartPoint = new Avalonia.Point(rec.Width / 2 - 2 * radius /*+100*/, rec.Height / 2 /*+100*/);
					l16.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius /*+100*/, rec.Height / 2 /*+100*/);
					l16.StartPoint.Transform(m);
					l16.EndPoint.Transform(m);
					g.Children.Add(l16);

					break;
			}

			EllipseGeometry a5 = new EllipseGeometry();

			a5.Center = new Avalonia.Point(-rec.Width / 2 /*+100*/ + radius, (int)rec.Height / 2 - 2 * radius /*+100*/ +radius);
			a5.RadiusX = radius;
			a5.RadiusY = radius;
			g.Children.Add(a5);


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

            

            return g;
		}
	}
}
