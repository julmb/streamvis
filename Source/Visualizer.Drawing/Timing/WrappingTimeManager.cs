// Copyright © Julian Brunner 2009

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

using System.Collections.Generic;
using Utility;
using Visualizer.Data;

namespace Visualizer.Drawing.Timing
{
	public class WrappingTimeManager : TimeManager
	{
		TimeRange range;
		IEnumerable<TimeRange> graphRanges;

		public double GapLength { get; set; }
		public override TimeRange Range { get { return range; } }
		public override IEnumerable<TimeRange> GraphRanges { get { return graphRanges; } }

		public WrappingTimeManager(Timer timer)
			: base(timer)
		{
			GapLength = 0.2;
		}

		public override void Update()
		{
			base.Update();

			double intervals = Time / Width;
			int wholeIntervals = (int)intervals;
			double fractionalIntervals = intervals - wholeIntervals;

			Time startTime = Time - (1 - GapLength) * Width;
			double startPosition = (fractionalIntervals + GapLength) % 1;
			Time endTime = Time;
			double endPosition = startPosition + (1 - GapLength);

			range = new TimeRange(new Range<Time>((wholeIntervals + 0) * Width, (wholeIntervals + 1) * Width));

			if (startTime >= wholeIntervals * Width)
				graphRanges = new TimeRange[]
				{
					new TimeRange(new Range<Time>(startTime, endTime), new Range<double>(startPosition,endPosition))
				};
			else
				graphRanges = new TimeRange[]
				{
					new TimeRange(new Range<Time>(startTime, wholeIntervals * Width), new Range<double>(startPosition, 1)),
					new TimeRange(new Range<Time>(wholeIntervals * Width, endTime), new Range<double>(0, endPosition - 1))
				};
		}
	}
}
