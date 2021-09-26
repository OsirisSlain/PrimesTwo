using System;
using System.Diagnostics;

namespace Primes
{
	class Program
	{
		static void Main(string[] args)
		{
			Tests.IsCorrect(Primes.EasyIsPrime, Primes.IsPrime, 1000);
			Tests.IsCorrect(Primes.EasyIsPrime, Primes.FastIsPrime, 1000);
			for(uint i = 1; i <= 1000; i++) {
				if(Primes.EasyIsPrime(i)) Console.WriteLine(i);
			}
			var easyResult = Tests.Benchmark(Primes.EasyIsPrime, 1000000);
			Console.WriteLine("easyResult Result: " + easyResult.Milliseconds);
			var normalResult = Tests.Benchmark(Primes.IsPrime, 1000000);
			Console.WriteLine("normal Result: " + normalResult.Milliseconds);
			var fastResult = Tests.Benchmark(Primes.FastIsPrime, 1000000);
			Console.WriteLine("fast Result: " + fastResult.Milliseconds);
		}
	}

	class Primes
	{
		public static bool EasyIsPrime(uint x)
		{
			if (x < 2) return false;
			if (x == 2) return true;
			if (x % 2 == 0) return false;
			for (uint i = 3; i < x; i += 2)
			{
				if (x % i == 0) return false;
			}
			return true;
		}
		public static bool IsPrime(uint x)
		{
			if(x < 2) return false;
			if (x == 2) return true;
			if(x % 2 == 0) return false;
			uint upperBound = (uint)Math.Sqrt(x) + 1;
			for(uint i = 3; i < upperBound; i+=2) {
				if(x % i == 0) return false;
			}
			return true;
		}
		public static bool FastIsPrime(uint x)
		{
			if(x < 2) return false;
			if (x == 2 || x == 3) return true;
			if(x % 2 == 0 || x % 3 == 0)  return false;
			uint upperBound = (uint)Math.Sqrt(x) + 1;
			for(uint i = 5; i < upperBound; i+=6) {
				if(x % i == 0 || x % (i + 2) == 0) return false;
			}
			return true;
		}
	}
	
	class Tests
	{
		public static TimeSpan Benchmark(
			Func<uint, bool> test,
			uint count)
		{
			var timer = new Stopwatch();
			timer.Start();
			for(uint i = 0; i < count; i++) test(i);
			timer.Stop();
			return timer.Elapsed;
		}

		public static bool IsCorrect(
			Func<uint, bool> x,
			Func<uint, bool> y,
			uint count)
		{
			for(uint i = 0; i < count; i++) {
				if(x(i) != y(i)) throw new Exception("Functions mismatch at " + i);
			}
			return true;
		}
	}
}