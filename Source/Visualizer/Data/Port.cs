// Copyright © Julian Brunner 2009 - 2010

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

using System;
using System.Linq;
using Utility.Utilities;
using System.Collections.Generic;

namespace Data
{
	public abstract class Port
	{
		readonly string name;

		public string Name { get { return name; } }
		public IEnumerable<Path> ValidPaths { get; private set; }

		protected Port(string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			this.name = name;
		}

		public abstract Packet Read();
		public abstract void AbortWait();

		protected void Initialize()
		{
			Packet firstValid = EnumerableUtility.Consume<Packet>(Read).First(packet => packet != null && !(packet is InvalidPacket));
			ValidPaths = firstValid.ValidPaths;
		}
	}
}
