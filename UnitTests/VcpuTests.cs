using System;
using IronVisor;
using NUnit.Framework;

namespace UnitTests {
	[NonParallelizable]
	public class VcpuTests {
		[Test]
		public void VcpuSetupTest() {
			using var vm = IVm.Create();
			var vcpu = vm.CreateVcpu();
			vcpu.Dispose();
		}
		
		[Test]
		public void VcpuNonDestroyedTest() {
			var vm = IVm.Create();
			var vcpu = vm.CreateVcpu();
			Assert.Throws<HvException>(() => vm.Dispose());
			vcpu.Dispose();
			vm.Dispose();
		}

		[Test]
		public void VcpuRegisterAccessTest() {
			using var vm = IVm.Create();
			using var vcpu = vm.CreateVcpu();
			vcpu[Reg.X0] = 0xDEADBEEF;
			vcpu.X[1] = 0xCAFEBABE;
			Assert.AreEqual(vcpu[Reg.X0], 0xDEADBEEF);
			Assert.AreEqual(vcpu[Reg.X1], 0xCAFEBABE);
			Assert.AreEqual(vcpu.X[0], 0xDEADBEEF);
			Assert.AreEqual(vcpu.X[1], 0xCAFEBABE);
		}

		[Test]
		public void VcpuExecTest() {
			using var vm = IVm.Create();
			using var vcpu = vm.CreateVcpu();
			vcpu.X[0] = 0x1000;
			vcpu.X[1] = 0x1337;

			var mb = vm.Map(0x10000, 0x4000, MemoryFlags.Exec | MemoryFlags.Read);
			var cm = mb.AsSpan<uint>();
			cm[0] = 0x8b010002; // add x2, x0, x1
			cm[1] = 0xd4000081; // svc 4

			vcpu.PC = 0x10000;
			vcpu.Run();

			Assert.AreEqual(0x2337, vcpu.X[2]);
		}
	}
}