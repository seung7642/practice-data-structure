﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataStructure
{
    internal static class HashHelpers
    {
        // 1000보다 작은 소수(prime)들 (실제로는 더 큰 수까지 사용되지만 예제이므로 1000 이하의 숫자만 사용한다)
        private static readonly int[] _primes = new int[] {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919
        };

        public static int PRIME_FACTOR = 4;
        public static decimal RESIZE_FACTOR = 1.25M; // 소수점 있는 decimal임을 명시하기 위해 M을 써준다.

        public static int GetPrime(int min)
        {
            for (int index = 0; index < _primes.Length; ++index)
            {
                int prime = _primes[index];
                if (prime >= min)
                    return prime;
            }
            return min;
        }
    }
}
