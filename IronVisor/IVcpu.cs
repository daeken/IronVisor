using System;
using System.Numerics;

namespace IronVisor; 

public interface IVcpu : IDisposable {
	ulong this[Reg reg] { get; set; }
	Vector4 this[FpReg reg] { get; set; }
	ulong this[SysReg reg] { get; set; }
	ulong PC { get; set; }
	ulong SP { get; set; }
	ulong FPCR { get; set; }
	ulong FPSR { get; set; }
	ulong CPSR { get; set; }
	
	bool TrapDebugExceptions { get; set; }
	bool TrapDebugRegisterAccesses { get; set; }
	bool IrqPending { get; set; }
	bool FiqPending { get; set; }
	
	Indexer<int, ulong> X { get; }
	Indexer<int, Vector4> Q { get; }

	void Run();
	void ForceExit();
}