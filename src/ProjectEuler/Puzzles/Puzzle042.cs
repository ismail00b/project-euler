﻿// Copyright (c) Martin Costello, 2015. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace MartinCostello.ProjectEuler.Puzzles
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// A class representing the solution to <c>https://projecteuler.net/problem=42</c>. This class cannot be inherited.
    /// </summary>
    internal sealed class Puzzle042 : Puzzle
    {
        /// <inheritdoc />
        public override string Question => "How many are triangle words are in the word data?";

        /// <summary>
        /// Returns the score of the specified word.
        /// </summary>
        /// <param name="word">The word to get the score for.</param>
        /// <returns>
        /// The score for the word specified by <paramref name="word"/>.
        /// </returns>
        internal static int GetScore(string word)
        {
            return word
                .Select((p) => p - 'A' + 1)
                .Sum();
        }

        /// <summary>
        /// Returns the triangle number at the specified position in the sequence of triangle numbers.
        /// </summary>
        /// <param name="n">The position in the sequence to get the triangle number of.</param>
        /// <returns>
        /// The triangle number in the sequence at the position specified by <paramref name="n"/>.
        /// </returns>
        internal static int TriangleNumber(int n) => (int)(0.5 * n * (n + 1));

        /// <summary>
        /// Reads the words from the data for the puzzle.
        /// </summary>
        /// <returns>
        /// An <see cref="IList{T}"/> that returns the words associated with the puzzle.
        /// </returns>
        internal IList<string> ReadWords()
        {
            string rawWords;

            using (Stream stream = ReadResource())
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    rawWords = reader.ReadToEnd();
                }
            }

            string[] split = rawWords.Split(',');
            char[] quote = new[] { '\"' };

            IList<string> words = new List<string>();

            foreach (string word in split)
            {
                words.Add(word.Trim(quote));
            }

            return words;
        }

        /// <inheritdoc />
        protected override int SolveCore(string[] args)
        {
            IList<string> words = ReadWords();

            int maximumLength = words.Max((p) => p.Length);
            int maximumScore = GetScore(new string('Z', maximumLength));

            IList<int> triangleNumbers = new List<int>();

            for (int n = 1; n <= maximumScore; n++)
            {
                int triangleNumber = TriangleNumber(n);

                if (triangleNumber > maximumScore)
                {
                    break;
                }

                triangleNumbers.Add(triangleNumber);
            }

            int count = 0;

            foreach (string word in words)
            {
                int score = GetScore(word);

                if (triangleNumbers.Contains(score))
                {
                    count++;
                }
            }

            Answer = count;

            return 0;
        }
    }
}
