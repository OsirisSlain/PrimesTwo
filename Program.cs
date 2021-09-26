using System;
using System.Diagnostics;
using System.Linq;

namespace Primes
{
	class Program
	{
		static void Main(string[] args)
		{
			uint slowCount = 1000;
			Tests.IsCorrect(Primes.SlowIsPrime, Primes.LinqIsPrime, slowCount);
			Tests.IsCorrect(Primes.LinqIsPrime, Primes.FastIsPrime, slowCount);
			Tests.IsCorrect(Primes.FastIsPrime, Primes.IsPrime, slowCount);

			for(uint i = 1; i <= slowCount; i++) {
				if(Primes.FastIsPrime(i)) Console.WriteLine(i);
			}

			const uint benchCount = 50000;
			var easyResult = Tests.Benchmark(Primes.SlowIsPrime, benchCount);
			Console.WriteLine("easyResult Result: " + easyResult.Milliseconds);
			var normalResult = Tests.Benchmark(Primes.IsPrime, benchCount);
			Console.WriteLine("normal Result: " + normalResult.Milliseconds);
			var fastResult = Tests.Benchmark(Primes.FastIsPrime, benchCount);
			Console.WriteLine("fast Result: " + fastResult.Milliseconds);
			var oddResult = Tests.Benchmark(Primes.LinqIsPrime, benchCount);
			Console.WriteLine("odd Result: " + oddResult.Milliseconds);
		}
	}

	class Primes
	{
		public static bool SlowIsPrime(uint x)
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
		public static bool LinqIsPrime(uint x)
		{
			var xx = (int)x;
			if(x < 2) return false;
			return Enumerable.Range(2, xx - 2)
				.Where(y => xx % y == 0).Count() == 0;
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