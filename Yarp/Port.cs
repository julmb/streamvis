using System;
using System.Runtime.InteropServices;

namespace Yarp
{
	public class Port : IDisposable
	{
		readonly IntPtr port;
		
		bool disposed = false;
		
		public Port(string name)
		{
			port = BufferedPort_Bottle_New();
			BufferedPort_Bottle_Open(port, name);
		}
		~Port()
		{
			Dispose();
		}
		
		public void Dispose()
		{
			if (!disposed)
			{
				disposed = true;
				
				BufferedPort_Bottle_Close(port);
				BufferedPort_Bottle_Dispose(port);
			}
		}
		public Bottle Read()
		{
			return new Bottle(BufferedPort_Bottle_Read(port));
		}
		
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr BufferedPort_Bottle_New();
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Dispose(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Open(IntPtr port, string name);
		[DllImport("Yarp.Wrapper")]
		static extern void BufferedPort_Bottle_Close(IntPtr port);
		[DllImport("Yarp.Wrapper")]
		static extern IntPtr BufferedPort_Bottle_Read(IntPtr port);
	}
}
