using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IronVisor; 

public class KvmVm : IVm {
	readonly WrappedFD KvmFd = new(open("/dev/kvm", 2));

	public KvmVm() {
		var version = ioctl_KVM_GET_API_VERSION(KvmFd, KvmIoctl.KVM_GET_API_VERSION);
		if(version != 12)
			throw new Exception($"Unsupported KVM API version {version}!");
	}

	~KvmVm() {
		Dispose();
	}
	
	public void Dispose() {
		KvmFd.Dispose();
	}

	public BoundMemory Map(ulong guestPhysAddr, ulong size, MemoryFlags flags) {
		throw new NotImplementedException();
	}

	public void Map(IntPtr hostAddress, ulong guestPhysAddr, ulong size, MemoryFlags flags) {
		throw new NotImplementedException();
	}

	public void Unmap(ulong guestPhysAddr, ulong size) {
		throw new NotImplementedException();
	}

	public void Protect(ulong guestPhysAddr, ulong size, MemoryFlags flags) {
		throw new NotImplementedException();
	}

	public IVcpu CreateVcpu() {
		throw new NotImplementedException();
	}
	
	[DllImport("libc", CharSet = CharSet.Ansi)]
	static extern int open(string filename, int flags);

	[DllImport("libc", EntryPoint = "ioctl")]
	static extern int ioctl_KVM_GET_API_VERSION(int fd, ulong req, ulong _ = 0);

	[DllImport("libc", EntryPoint = "ioctl")]
	static extern int ioctl_KVM_CREATE_VM (int fd, ulong req, ulong _ = 0);
}