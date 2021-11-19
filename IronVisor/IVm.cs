using System;
using System.Runtime.InteropServices;

namespace IronVisor; 

public interface IVm : IDisposable {
	public static IVm Create() {
		if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			return new HvfVm();
		if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			return new KvmVm();
		throw new NotImplementedException();
	}
	
	BoundMemory Map(ulong guestPhysAddr, ulong size, MemoryFlags flags);
	void Map(IntPtr hostAddress, ulong guestPhysAddr, ulong size, MemoryFlags flags);
	void Unmap(ulong guestPhysAddr, ulong size);
	void Protect(ulong guestPhysAddr, ulong size, MemoryFlags flags);
	IVcpu CreateVcpu();
}