using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Structum.Elements.Security.Tests
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="Randomizer"/> class.
    /// </summary>
    public class RandomizerTest : IDisposable
    {
        /// <summary>
        ///     Internal Randomizer.
        /// </summary>
        private readonly Randomizer _randomizer = new Randomizer();

        /// <summary>
        ///     Iteration count.
        /// </summary>
        private const int Iterations = 100;

        /// <summary>
        ///     Tests the Random String functionality.
        /// </summary>
        [Fact]
        public void RandomStringTest()
        {
            const int maxLen = 5;
            char[] charSet = CharSetValues.Alphanumerics;

            string[] prevGen = new string[Iterations];
            for (int i = 0; i < Iterations; i++) {
                string currGen = this._randomizer.GetRandomString(maxLen, charSet);
                Assert.True(currGen.Length <= maxLen);
                Assert.True(currGen.All(c => charSet.Contains(c)));
                Assert.DoesNotContain(currGen, prevGen);
                prevGen[i] = currGen;
            }
        }

        /// <summary>
        ///     Tests the Random Integer functionality.
        /// </summary>
        [Fact]
        public void RandomIntegerTest()
        {
            const int min = 1;
            const int max = 300;

            // Generate a 100 Random Numbers.
            int[] prevGen = new int[Iterations];
            for (int i = 0; i < Iterations; i++) {
                int currGen = this._randomizer.GetRandomInteger(min, max);
                Assert.True(currGen <= max);
                Assert.True(currGen >= min);
                prevGen[i] = currGen;
            }

            // Find repetitions.
            var dict = new Dictionary<int, int>();
            foreach (int gen in prevGen) {
                if (dict.ContainsKey(gen)) {
                    dict[gen]++;
                } else {
                    dict.Add(gen, 1);
                }
            }

            // Make sure the number is not repeated more than 3 times.
            foreach (int gen in dict.Keys) {
                Assert.True(dict[gen] <= 3, $"{gen} {dict[gen]} ");
            }
        }

        /// <summary>
        ///     Tests the Random Double functionality.
        /// </summary>
        [Fact]
        public void RandomDoubleTest()
        {
            const int min = 1;
            const int max = 300;

            // Generate a 100 Random Numbers.
            double[] prevGen = new double[Iterations];
            for (int i = 0; i < Iterations; i++) {
                double currGen = this._randomizer.GetRandomDouble(min, max);
                Assert.True(currGen <= max);
                Assert.True(currGen >= min);
                Assert.DoesNotContain(currGen, prevGen);
                prevGen[i] = currGen;
            }

            // Find repetitions.
            var dict = new Dictionary<double, int>();
            foreach (double gen in prevGen) {
                if (dict.ContainsKey(gen)) {
                    dict[gen]++;
                } else {
                    dict.Add(gen, 1);
                }
            }

            // Make sure the number is not repeated more than 3 times.
            foreach (double gen in dict.Keys) {
                Assert.True(dict[gen] <= 3, $"{gen} {dict[gen]} ");
            }
        }

        /// <summary>
        ///     Tests the Random Bool functionality.
        /// </summary>
        [Fact]
        public void RandomBoolTest()
        {
            int countTrues = 0;
            int countFalses = 0;
            for (int i = 0; i < Iterations; i++) {
                bool currGen = this._randomizer.GetRandomBoolean();
                if (currGen) {
                    countTrues++;
                } else {
                    countFalses++;
                }
            }

            int x = (countTrues / Iterations) * 100;
            Assert.True(x < 60);
            int y = (countFalses / Iterations) * 100;
            Assert.True(y < 60);
        }

        /// <summary>
        ///     Disposes the tests resources.
        /// </summary>
        public void Dispose()
        {
            this._randomizer?.Dispose();
        }
    }
}