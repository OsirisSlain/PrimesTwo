using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Primes
{
	class Program
	{
		static void Main(string[] args)
		{
			Primes.VerifyPrimeMethods();

			Console.WriteLine("Primes from 1 to 100:");
			var primes = new List<uint>();
			for(uint i = 1; i <= 100; i++) {
				if(Primes.FastIsPrime(i)) primes.Add(i);
			}
			Console.WriteLine(string.Join(", ", primes) + Environment.NewLine);

			const uint benchCount = 100000;
			Console.WriteLine($"Benchmarking results for 0 to {benchCount}");
			Benchmark("Slow", Primes.SlowIsPrime, benchCount);
			Benchmark("Normal", Primes.IsPrime, benchCount);
			Benchmark("Fast", Primes.FastIsPrime, benchCount);
			Benchmark("Linq", Primes.LinqIsPrime, benchCount);
			Benchmark("Parallel", Primes.ParallelIsPrime, benchCount);
		}
		public static TimeSpan Benchmark(
			string name,
			Func<uint, bool> test,
			uint count)
		{
			var timer = new Stopwatch();
			timer.Start();
			for(uint i = 0; i < count; i++) test(i);
			timer.Stop();
			Console.WriteLine($"{name}: {timer.ElapsedMilliseconds}ms");
			return timer.Elapsed;
		}
	}
}