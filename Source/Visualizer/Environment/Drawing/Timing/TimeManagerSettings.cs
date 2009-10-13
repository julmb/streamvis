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

using System.ComponentModel;
using Visualizer.Data;
using Visualizer.Drawing;

namespace Visualizer.Environment.Drawing.Timing
{
	// TODO: Add description for properties
	[TypeConverter(typeof(ExpandableObjectConverter))]
	abstract class TimeManagerSettings
	{
		readonly Diagram diagram;
		
		protected Diagram Diagram { get { return diagram; } }

		[DisplayName("Time")]
		public double Time
		{
			get { return diagram.TimeManager.Time.Seconds; }
			set { diagram.TimeManager.Time = new Time(value); }
		}
		[DisplayName("Width")]
		public double Width
		{
			get { return diagram.TimeManager.Width.Seconds; }
			set { diagram.TimeManager.Width = new Time(value); }
		}
		[DisplayName("Frozen")]
		public bool Frozen
		{
			get { return diagram.TimeManager.Frozen; }
			set { diagram.TimeManager.Frozen = value; }
		}

		protected TimeManagerSettings(Diagram diagram)
		{
			this.diagram = diagram;
		}
	}
}