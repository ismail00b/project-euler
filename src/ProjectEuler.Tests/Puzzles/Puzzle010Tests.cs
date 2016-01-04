﻿// Copyright (c) Martin Costello, 2015. All rights reserved.
// Licensed under the Apache 2.0 license. See the LICENSE file in the project root for full license information.

namespace MartinCostello.ProjectEuler.Puzzles
{
    using Xunit;

    /// <summary>
    /// A class containing tests for the <see cref="Puzzle010"/> class. This class cannot be inherited.
    /// </summary>
    public static class Puzzle010Tests
    {
        [Theory]
        [InlineData("10", 17L)]
        [InlineData("2000000", 142913828922L)]
        public static void Puzzle010_Returns_Correct_Solution(string value, long expected)
        {
            // Arrange
            string[] args = new[] { value };

            // Act and Assert
            Puzzles.AssertSolution<Puzzle010>(args, expected);
        }

        [Fact]
        public static void Puzzle010_Returns_Minus_One_If_Maximum_Value_Is_Invalid()
        {
            // Arrange
            string[] args = new[] { "a" };

            // Act and Assert
            Puzzles.AssertInvalid<Puzzle010>(args);
        }

        [Fact]
        public static void Puzzle010_Returns_Minus_One_If_Maximum_Value_Is_Too_Small()
        {
            // Arrange
            string[] args = new[] { "1" };

            // Act and Assert
            Puzzles.AssertInvalid<Puzzle010>(args);
        }
    }
}