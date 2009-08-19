using System;
using System.Collections.Generic;
using System.Drawing;
using Graphics;

namespace Visualizer.Plotting
{
	public class Plotter : IComponent, IUpdateable, IDrawable
	{
		readonly IEnumerable<Graph> graphs;
		readonly Drawer drawer;
		readonly TimeManager timeManager;
		readonly ValueManager valueManager;
		readonly Layouter layouter;
		// TODO: Is this even used?
		readonly int resolution;
		readonly int intervalsX;
		readonly int intervalsY;
		readonly Color color;

		public bool IsDrawn { get; set; }
		public TimeManager TimeManager { get { return timeManager; } }
		public ValueManager ValueManager { get { return valueManager; } }
		public Layouter Layouter { get { return layouter; } }
		public int Resolution { get { return resolution; } }
		public bool ExtendGraphs { get; set; }
		public bool Frozen { get; set; }

		public Plotter(IEnumerable<Graph> graphs, Drawer drawer, TimeManager timeManager, ValueManager valueManager, Layouter layouter, int resolution, int intervalsX, int intervalsY, Color color)
		{
			this.graphs = graphs;
			this.drawer = drawer;
			this.timeManager = timeManager;
			this.valueManager = valueManager;
			this.layouter = layouter;
			this.resolution = resolution;
			this.intervalsX = intervalsX;
			this.intervalsY = intervalsY;
			this.color = color;

			IsDrawn = true;
		}

		public void Update()
		{
			if (!Frozen) timeManager.Update();
			valueManager.Update();
			layouter.Update();

			foreach (Graph graph in graphs) graph.Update();
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				foreach (Graph graph in graphs) graph.Draw();

				Range<TimeSpan> timeRange = timeManager.Range;
				Range<double> valueRange = valueManager.Range;

				DrawAxisX(timeRange, valueRange);
				DrawAxisY(timeRange, valueRange);
			}
		}

		void DrawAxisX(Range<TimeSpan> timeRange, Range<double> valueRange)
		{
			PointF start = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(0));
			PointF end = layouter.TransformGraph(timeRange.Map(1), valueRange.Map(0));

			start.Y += 5;
			end.Y += 5;
			drawer.DrawLine(start, end, color, 1);

			TimeSpan width = timeRange.End.Value - timeRange.Start.Value;
			TimeSpan interval = new TimeSpan(width.Ticks / intervalsX);
			TimeSpan offset = interval - Modulo(timeRange.Start.Value, interval);

			if (width > TimeSpan.Zero)
				for (int i = 0; i < intervalsX + 1; i++)
				{
					TimeSpan time = offset + new TimeSpan(i * interval.Ticks);
					PointF position = layouter.TransformGraph(timeRange.Map((float)((double)time.Ticks / (double)width.Ticks)), valueRange.Map(0));
					position.Y += 5;
					drawer.DrawLine(new PointF(position.X, position.Y + 5), position, color, 1);
					drawer.DrawNumber((timeRange.Start.Value + time).TotalSeconds, new PointF(position.X, position.Y + 7), color, TextAlignment.Center);
				}
		}
		void DrawAxisY(Range<TimeSpan> timeRange, Range<double> valueRange)
		{
			PointF start = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(0));
			PointF end = layouter.TransformGraph(timeRange.Map(0), valueRange.Map(1));

			drawer.DrawLine(start, end, color, 1);

			double height = valueRange.End.Value - valueRange.Start.Value;
			double interval = height / intervalsY;

			if (height > 0)
				for (int i = 0; i < intervalsY + 1; i++)
				{
					double value = i * interval;
					PointF position = layouter.TransformGraph(timeRange.Map(0), valueRange.Map((float)(value / height)));
					drawer.DrawLine(new PointF(position.X - 5, position.Y), position, color, 1);
					drawer.DrawNumber(valueRange.Start.Value + value, new PointF(position.X - 7, position.Y - 5), color, TextAlignment.Far);
				}
		}

		static TimeSpan Modulo(TimeSpan a, TimeSpan b)
		{
			TimeSpan rest = new TimeSpan(a.Ticks % b.Ticks);
			if (rest <= TimeSpan.Zero) rest += b;
			return rest;
		}
	}
}
