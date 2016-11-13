﻿// Copyright (c) Martin Costello, 2015. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace MartinCostello.ProjectEuler.Puzzles
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class representing the solution to <c>https://projecteuler.net/problem=49</c>. This class cannot be inherited.
    /// </summary>
    internal sealed class Puzzle049 : Puzzle
    {
        /// <inheritdoc />
        public override string Question => "What 12-digit number do you form by concatenating the three terms in the puzzle sequence other than for 1487?";

        /// <inheritdoc />
        protected override int SolveCore(string[] args)
        {
            var primes = Maths.Primes(10000)
                .SkipWhile((p) => p <= 1487)
                .Select((p) => (long)p)
                .ToList();

            IList<long> factors = null;

            for (int i = 0; i < primes.Count && factors == null; i++)
            {
                for (int j = i + 1; j < primes.Count; j++)
                {
                    long a = primes[i];
                    long b = primes[j];
                    long c = b + b - a;

                    if (primes.Contains(c))
                    {
                        if (ArePermutations(a, b) &&
                            ArePermutations(a, c))
                        {
                            factors = new[] { a, b, c };
                        }
                    }
                }
            }

            Answer = Maths.FromDigits(factors.SelectMany(Maths.Digits).ToList());

            return 0;
        }

        /// <summary>
        /// Returns whether the digits of the specified values are permutations of each other.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>
        /// <see langword="true"/> if the values are permutations of each others' digits; otherwise <see langword="false"/>.
        /// </returns>
        private static bool ArePermutations(long x, long y)
        {
            int[] digitsX = Maths.Digits(x).OrderBy((p) => p).ToArray();
            int[] digitsY = Maths.Digits(y).OrderBy((p) => p).ToArray();

            for (int i = 0; i < digitsX.Length; i++)
            {
                if (digitsX[i] != digitsY[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
