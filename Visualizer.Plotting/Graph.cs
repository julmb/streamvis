using System.Drawing;
using Graphics;
using OpenTK.Math;
using Visualizer.Data;
using Visualizer.Plotting.Data;
using Visualizer.Plotting.Timing;
using Visualizer.Plotting.Values;

namespace Visualizer.Plotting
{
	public class Graph
	{
		readonly Layouter layouter;
		readonly ValueManager valueManager;
		readonly SegmentManager segmentManager;
		readonly Drawer drawer;
		readonly DataManager dataManager;

		public bool IsDrawn { get; set; }
		public Color Color { get; set; }
		public DataManager DataManager { get { return dataManager; } }

		public Graph(Layouter layouter, ValueManager valueManager, SegmentManager segmentManager, Drawer drawer, DataManager dataManager)
		{
			this.layouter = layouter;
			this.valueManager = valueManager;
			this.segmentManager = segmentManager;
			this.drawer = drawer;
			this.dataManager = dataManager;

			IsDrawn = true;
		}

		public void Update()
		{
			dataManager.Update();
		}
		public void Draw()
		{
			if (IsDrawn)
			{
				ValueRange valueRange = valueManager.Range;

				foreach (DataSegment segment in segmentManager[this])
				{
					TimeRange timeRange = segment.TimeRange;

					float[] vertices = new float[segment.Entries.Length * 2];

					int position = 0;
					foreach (Entry entry in segment.Entries)
					{
						vertices[position++] = (float)entry.Time.Seconds;
						vertices[position++] = (float)entry.Value;
					}

					Matrix4 transformation = valueRange.Transformation * timeRange.Transformation * layouter.Transformation;

					drawer.DrawLineStrip(vertices, transformation, Color, 1);
				}
			}
		}

		// TODO: Reenable graph extension
		//IEnumerable<Entry> GetEntries(Time startTime, Time endTime)
		//{
		//    if (stream.Container.IsEmpty) yield break;

		//    int startIndex = stream.Container.GetIndex(start);
		//    int endIndex = stream.Container.GetIndex(end);

		//    if (plotter.ExtendGraphs)
		//    {
		//        double head;
		//        double tail;

		//        if (startIndex == endIndex)
		//        {
		//            if (endIndex == stream.Container.Count) head = tail = stream.Container[endIndex - 1].Value;
		//            else head = tail = stream.Container[startIndex].Value;
		//        }
		//        else
		//        {
		//            head = stream.Container[startIndex].Value;
		//            tail = stream.Container[endIndex - 1].Value;
		//        }

		//        yield return new Entry(start, head);
		//        foreach (Entry entry in stream.Container[startIndex, endIndex]) yield return entry;
		//        yield return new Entry(end, tail);
		//    }
		//    else
		//        foreach (Entry entry in stream.Container[startIndex, endIndex])
		//            yield return entry;
		//}
	}
}
