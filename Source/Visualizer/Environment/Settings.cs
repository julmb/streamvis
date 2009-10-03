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
using Visualizer.Drawing;

namespace Visualizer.Environment
{
	class Settings
	{
		readonly DiagramSettings diagram;
		readonly GraphSettingsSettings graphSettings;

		[Description("Contains settings concerning the Diagram.")]
		[DisplayName("Diagram")]
		public DiagramSettings Diagram { get { return diagram; } }
		[Description("Contains settings concerning the Graphs.")]
		[DisplayName("Graph Settings")]
		public GraphSettingsSettings GraphSettings { get { return graphSettings; } }

		public Settings(Diagram diagram, GraphSettings graphSettings)
		{
			this.diagram = new DiagramSettings(diagram);
			this.graphSettings = new GraphSettingsSettings(graphSettings);
		}
	}
}
