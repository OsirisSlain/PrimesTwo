using System;

namespace Primes
{
	class Program
	{
		//Test update
		static void Main(string[] args)
		{
			for(uint i = 1; i <= 1000; i++) {
				if(Primes.IsPrime(i)) Console.WriteLine(i);
			}
		}
	}

	class Primes
	{
		public static bool IsPrime(uint x)
		{
			if(x < 2) return false;
			uint upperBound = (uint)Math.Sqrt(x) + 1;
			for(uint i = 2; i < upperBound; i++) {
				if(x % i == 0) return false;
			}
			return true;
		}
	}
}