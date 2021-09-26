using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Primes
{
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
		public static bool ParallelIsPrime(uint x)
		{
			if(x < 2) return false;
			if (x == 2 || x == 3) return true;
			if(x % 2 == 0 || x % 3 == 0)  return false;
			uint upperBound = (uint)Math.Sqrt(x) + 1;

			IEnumerable<int> SteppedList() {
				for(int i = 5; i < upperBound; i +=6) {
					yield return i;
				}
			}
			var result = true;
			Parallel.ForEach(SteppedList(), (int i, ParallelLoopState state) => {
				if(x % i == 0 || x % (i + 2) == 0) {
					result = false;
					state.Break();
				}
			});
			return result;
		}
		public static bool LinqIsPrime(uint x)
		{
			if(x < 2) return false;
			return Enumerable.Range(2, (int)(x - 2))
				.All(y => x % y != 0);
		}

		public static void VerifyPrimeMethods()
		{
			IsCorrect(Primes.SlowIsPrime);
			IsCorrect(Primes.IsPrime);
			IsCorrect(Primes.FastIsPrime);
			IsCorrect(Primes.ParallelIsPrime);
			IsCorrect(Primes.LinqIsPrime);
		}

		public static void IsCorrect(Func<uint, bool> x)
		{
			var primeTruths = new Dictionary<uint,bool> {
				{0,false},{1,false},{2,true},{3,true},{4,false},{5,true},{6,false},
				{7,true},{8,false},{9,false},{10,false},{11,true},{12,false},{13,true},
				{15,false},{17,true},{442,false},{443,true},{8166,false},{8167,true}
			};

			foreach(var primeTruth in primeTruths) {
				if(x(primeTruth.Key) != primeTruth.Value)
					throw new Exception($"Incorrect validity for prime {primeTruth.Key}");
			}
		}
	}
}