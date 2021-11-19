using System;
using IronVisor;
using NUnit.Framework;

namespace UnitTests {
	[NonParallelizable]
	public class VmTests {
		[Test]
		public void VmSetupTest() {
			var vm = IVm.Create();
			vm.Dispose();
		}

		[Test]
		public void VmDisposeRecreateTest() {
			using(var vm = IVm.Create()) {}
			using(var vm = IVm.Create()) {}
		}

		[Test]
		[Platform("MacOsX")]
		public void VmDoubleTest() {
			using var vm = IVm.Create();
			Assert.Throws<BusyException>(() => IVm.Create());
		}

		[Test]
		public void VmMapBasic() {
			using var vm = IVm.Create();
			var mem1 = vm.Map(0xDEAD0000, 0x10000, MemoryFlags.Read | MemoryFlags.Write);
			var span = mem1.AsSpan<uint>();
			span[0] = 0xDEADBEEF;
			Assert.AreEqual(span[0], 0xDEADBEEF);
			Assert.Throws<HvException>(() => vm.Map(0xDEAD0000, 0x10000, MemoryFlags.Read | MemoryFlags.Write)); 
			Assert.Throws<HvException>(() => vm.Map(0xDEAD0000, 0x20000, MemoryFlags.Read | MemoryFlags.Write)); 
			mem1.Dispose();
			using(var mem2 = vm.Map(0xDEAD0000, 0x100000, MemoryFlags.Read | MemoryFlags.Write)) {}
		}
	}
}