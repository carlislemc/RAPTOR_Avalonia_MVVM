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

			
			float f1 = Convert.ToSingle(rec.Left) + Convert.ToSingle(rec.Width / 2);
			float f2 = (float)rec.Top + (float)rec.Height / 2;
			Avalonia.Matrix m = Avalonia.Matrix.CreateTranslation(f1,f2);
			

			if (rec.Width < 2*radius)
			{
				// need extra room here for /|
				rec = rec.WithWidth(3*radius);
			}
			if (rec.Height < 2*radius)
			{
				rec = rec.WithHeight(2*radius);
			}

			l.StartPoint = new Avalonia.Point(-rec.Width / 2 , rec.Height / 2 - radius ).Transform(m);
			l.EndPoint = new Avalonia.Point(-rec.Width / 2 , -rec.Height / 2 + radius ).Transform(m);



			g.Children.Add(l);


			var s = new StreamGeometry();

			using (var gc = s.Open())
			{
				gc.BeginFigure(
					startPoint: new Avalonia.Point(-rec.Width / 2 + radius, -rec.Height / 2).Transform(m),
					isFilled: false);

				gc.ArcTo(
					point: new Avalonia.Point(-rec.Width / 2, -rec.Height / 2 + radius).Transform(m),
					size: new Avalonia.Size(radius, radius),
					rotationAngle: 0d,
					isLargeArc: false,
					sweepDirection: SweepDirection.CounterClockwise);
			}

			g.Children.Add(s);



			switch (corner)
            {
				case Corner.Upper_Left:
					l2.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 ).Transform(m);
					l2.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 - radius ).Transform(m);
					g.Children.Add(l2);

					l3.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 - radius ).Transform(m);
					l3.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , -rec.Height / 2 ).Transform(m);
					g.Children.Add(l3);

					l4.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , -rec.Height / 2 ).Transform(m);
					l4.EndPoint = new Avalonia.Point(rec.Width / 2 - radius, -rec.Height / 2).Transform(m);
					g.Children.Add(l4);

					break;
				case Corner.Upper_Right:
					l5.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 ).Transform(m);
					l5.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 - radius ).Transform(m);
					g.Children.Add(l5);

					l6.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 - radius ).Transform(m);
					l6.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , -rec.Height / 2 ).Transform(m);
					l6.StartPoint.Transform(m);
					l6.EndPoint.Transform(m);
					g.Children.Add(l6);

					l7.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , -rec.Height / 2 ).Transform(m);
					l7.EndPoint = new Avalonia.Point(rec.Width / 2 - radius , -rec.Height / 2 ).Transform(m);
					g.Children.Add(l7);

					break;
				case Corner.Lower_Left:
				case Corner.Lower_Right:
					l8.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , -rec.Height / 2 ).Transform(m);
					l8.EndPoint = new Avalonia.Point(rec.Width / 2 - radius , -rec.Height / 2 ).Transform(m);
					g.Children.Add(l8);

					break;
			}

			var s2 = new StreamGeometry();

			using (var gc = s2.Open())
			{
				gc.BeginFigure(
					startPoint: new Avalonia.Point((rec.Width / 2 - 2 * radius) + radius, -rec.Height / 2).Transform(m),
					isFilled: false);

				gc.ArcTo(
					point: new Avalonia.Point(rec.Width / 2 - 2 * radius + radius*2 , -rec.Height / 2 + radius).Transform(m),
					size: new Avalonia.Size(radius, radius),
					rotationAngle: 0d,
					isLargeArc: false,
					sweepDirection: SweepDirection.Clockwise);
			}

			g.Children.Add(s2);

			l9.StartPoint = new Avalonia.Point(rec.Width / 2 , -rec.Height / 2 + radius ).Transform(m);
            l9.EndPoint = new Avalonia.Point(rec.Width / 2 , +rec.Height / 2 - radius ).Transform(m);
			g.Children.Add(l9);


			var s3 = new StreamGeometry();

			using (var gc = s3.Open())
			{
				gc.BeginFigure(
					startPoint: new Avalonia.Point(rec.Width / 2 - 2 * radius + radius, rec.Height / 2 - 2 * radius + radius*2).Transform(m),
					isFilled: false);

				gc.ArcTo(
					point: new Avalonia.Point(rec.Width / 2 - 2 * radius +radius *2, rec.Height / 2 - 2 * radius + radius).Transform(m),
					size: new Avalonia.Size(radius, radius),
					rotationAngle: 0d,
					isLargeArc: false,
					sweepDirection: SweepDirection.CounterClockwise);
			}

			g.Children.Add(s3);

			switch (corner)
            {
				case Corner.Upper_Left:
				case Corner.Upper_Right:
					l10.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , rec.Height / 2 ).Transform(m);
					l10.EndPoint = new Avalonia.Point(rec.Width / 2 - radius , rec.Height / 2 ).Transform(m);
					g.Children.Add(l10);
					break;
				case Corner.Lower_Left:
					l11.StartPoint = new Avalonia.Point(rec.Width / 2 - radius , rec.Height / 2 ).Transform(m);
					l11.EndPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , rec.Height / 2 ).Transform(m);
					g.Children.Add(l11);

					l12.StartPoint = new Avalonia.Point(-rec.Width / 2 + 2 * radius , rec.Height / 2 ).Transform(m);
					l12.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius , rec.Height / 2 + radius ).Transform(m);
					g.Children.Add(l12);

					l13.StartPoint = new Avalonia.Point(-rec.Width / 2 + radius , rec.Height / 2 + radius ).Transform(m);
					l13.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius , rec.Height / 2 ).Transform(m);
					g.Children.Add(l13);

					break;
				case Corner.Lower_Right:
					l14.StartPoint = new Avalonia.Point(rec.Width / 2 - radius , rec.Height / 2 ).Transform(m);
					l14.EndPoint = new Avalonia.Point(rec.Width / 2 - radius , rec.Height / 2 + radius ).Transform(m);
					g.Children.Add(l14);

					l15.StartPoint = new Avalonia.Point(rec.Width / 2 - radius , rec.Height / 2 + radius ).Transform(m);
					l15.EndPoint = new Avalonia.Point(rec.Width / 2 - 2 * radius , rec.Height / 2 ).Transform(m);
					g.Children.Add(l15);

					l16.StartPoint = new Avalonia.Point(rec.Width / 2 - 2 * radius , rec.Height / 2 ).Transform(m);
					l16.EndPoint = new Avalonia.Point(-rec.Width / 2 + radius , rec.Height / 2 ).Transform(m);
					g.Children.Add(l16);

					break;
			}

			var s4 = new StreamGeometry();

			using (var gc = s4.Open())
			{
				gc.BeginFigure(
					startPoint: new Avalonia.Point(-rec.Width / 2 + radius, (int)rec.Height / 2 - 2 * radius + radius*2).Transform(m),
					isFilled: false);

				gc.ArcTo(
					point: new Avalonia.Point(-rec.Width / 2, (int)rec.Height / 2 - 2 * radius + radius).Transform(m),
					size: new Avalonia.Size(radius, radius),
					rotationAngle: 0d,
					isLargeArc: false,
					sweepDirection: SweepDirection.Clockwise);
			}

			g.Children.Add(s4);
			
            return g;
		}
	}
}
