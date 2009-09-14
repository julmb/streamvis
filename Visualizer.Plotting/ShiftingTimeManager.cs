using System;
using System.Collections.Generic;
using Visualizer.Data;

namespace Visualizer.Plotting
{
	public class ShiftingTimeManager : TimeManager
	{
		readonly double shiftLength;

		_Range<Time> range;
		_Range<Time> graphRange;

		public override _Range<Time> Range { get { return range; } }
		public override IEnumerable<_Range<Time>> GraphRanges { get { yield return graphRange; } }

		public ShiftingTimeManager(Timer timer, Time width, double shiftLength)
			: base(timer, width)
		{
			this.shiftLength = shiftLength;
		}

		public override void Update()
		{
			base.Update();

			Time interval = Width * shiftLength;

			double intervals = Time / interval;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = interval * (wholeIntervals + 1) - Width;
			float startPosition = 0;
			Time endTime = Time;
			float endPosition = (float)((1 - shiftLength) + shiftLength * fractionalIntervals);

			graphRange = new _Range<Time>
			(
				new Marker<Time>(startTime, startPosition),
				new Marker<Time>(endTime, endPosition)
			);

			range = new _Range<Time>
			(
				new Marker<Time>(startTime, 0),
				new Marker<Time>(startTime + Width, 1)
			);
		}
	}
}
