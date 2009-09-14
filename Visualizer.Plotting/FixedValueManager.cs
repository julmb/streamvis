using Extensions;

namespace Visualizer.Plotting
{
	public class FixedValueManager : ValueManager
	{
		readonly ValueRange range;

		public override ValueRange Range { get { return range; } }

		public FixedValueManager(double rangeLow, double rangeHigh)
		{
			this.range = new ValueRange(new Range<double>(rangeLow, rangeHigh));
		}
	}
}
