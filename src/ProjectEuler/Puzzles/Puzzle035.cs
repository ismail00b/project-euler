// Copyright (c) Martin Costello, 2015. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace MartinCostello.ProjectEuler.Puzzles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A class representing the solution to <c>https://projecteuler.net/problem=35</c>. This class cannot be inherited.
    /// </summary>
    internal sealed class Puzzle035 : Puzzle
    {
        /// <inheritdoc />
        public override string Question => "How many circular primes are there below the specified value?";

        /// <inheritdoc />
        protected override int MinimumArguments => 1;

        /// <summary>
        /// Returns all the rotations of the digits of the specified value.
        /// </summary>
        /// <param name="value">The value to get the rotations for.</param>
        /// <returns>
        /// An <see cref="IList{T}"/> containing the rotations of <paramref name="value"/>.
        /// </returns>
        internal static IList<long> GetRotations(int value)
        {
            var result = new List<long>()
            {
                value,
            };

            if (value < 12)
            {
                // The only rotation for values less than 12 is the value itself
                return result;
            }

            IList<int> digits = Maths.Digits(value);

            for (int i = 0; i < digits.Count - 1; i++)
            {
                var rotationDigits = new int[digits.Count];
                int index = 0;

                for (int j = i + 1; j < digits.Count; j++)
                {
                    rotationDigits[index++] = digits[j];
                }

                for (int j = 0; j < i + 1; j++)
                {
                    rotationDigits[index++] = digits[j];
                }

                double rotation = 0;

                for (int j = 0; j < rotationDigits.Length - 1; j++)
                {
                    rotation += rotationDigits[j] * Math.Pow(10, rotationDigits.Length - j - 1);
                }

                rotation += rotationDigits[rotationDigits.Length - 1];

                result.Add((long)rotation);
            }

            return result;
        }

        /// <inheritdoc />
        protected override int SolveCore(string[] args)
        {
            if (!TryParseInt32(args[0], out int maximum) || maximum < 2)
            {
                Console.WriteLine("The specified number is invalid.");
                return -1;
            }

            Answer = Maths.Primes(maximum)
                .AsParallel()
                .Select((p) => GetRotations(p))
                .Where((p) => p.All(Maths.IsPrime))
                .Count();

            return 0;
        }
    }
}
