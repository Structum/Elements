using System;
using System.Text;
using System.Security.Cryptography;

namespace Structum.Elements.Security
{
    /// <summary>
    ///     Provides a set of Randomization methods.
    /// </summary>
    /// <example>
    ///     The following code demonstrates how to use the Randomizer to create random strings, booleans, integers and doubles:
    ///     <code>
    ///     char[] charSet = new char[] {'A', 'B', 'C', 'D'};
    ///     using(Randomizer randomizer = new Randomizer()) {
    ///         string randomString = randomizer.GetRandomString(20, charSet);
    ///         bool randomBool = randomizer.GetRandomBoolean();
    ///         int randomInt = randomizer.GetRandomInteger(10, 100);
    ///         double randomDouble = randomizer.GetRandomDouble(0.5, 3.0);
    ///     }
    ///     </code>
    /// </example>
    public class Randomizer : IDisposable
    {
        /// <summary>
        ///     Gets the Random Number Generator instance.
        /// </summary>
        /// <value>Random Generator Instance.</value>
        private readonly RandomNumberGenerator _randomNumberGenerator = new RNGCryptoServiceProvider();

        /// <summary>
        ///     Returns a random boolean.
        /// </summary>
        /// <returns><c>bool</c></returns>
        public bool GetRandomBoolean()
        {
            byte[] randomByte = new byte[1];
            this._randomNumberGenerator.GetBytes(randomByte);

            return (randomByte[0] >= 128);
        }

        /// <summary>
        ///     Returns a random String.
        /// </summary>
        /// <param name="length">String Length.</param>
        /// <param name="characterSet">Character Set.</param>
        /// <returns><c>string</c></returns>
        public string GetRandomString(int length, char[] characterSet)
        {
            StringBuilder sb = new StringBuilder();

            for (int loop = 0; loop < length; loop++) {
                int index = this.GetRandomInteger(0, characterSet.GetLength(0) - 1);
                sb.Append(characterSet[index]);
            }
            string nonce = sb.ToString();

            return nonce;
        }

        /// <summary>
        ///     Returns a random integer value.
        /// </summary>
        /// <param name="min">Minimum Value.</param>
        /// <param name="max">Maximum Value.</param>
        /// <returns>Random Integer.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
        public int GetRandomInteger(int min, int max)
        {
            if (min >= max) {
                throw new ArgumentOutOfRangeException();
            }

            double range = (double)max - min;
            byte[] randomBytes = new byte[sizeof(int)];
            this._randomNumberGenerator.GetBytes(randomBytes);

            uint randomFactor = BitConverter.ToUInt32(randomBytes, 0);

            double divisor = (double) randomFactor / uint.MaxValue;
            int randomNumber = Convert.ToInt32(Math.Round(range * divisor) + min);

            return randomNumber;
        }

        /// <summary>
        ///     Returns a random double value.
        /// </summary>
        /// <param name="min">Minimum Value.</param>
        /// <param name="max">Minimum Value.</param>
        /// <returns>Random Double.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
        public double GetRandomDouble(double min, double max)
        {
            if (min >= max) {
                throw new ArgumentOutOfRangeException();
            }

            double factor = max - min;
            double random = (double) this.GetRandomInteger(0, int.MaxValue) / int.MaxValue;
            return random * factor + min;
        }

        /// <summary>
        ///     Releases all resource used by the <see cref="Randomizer"/> object.
        /// </summary>
        public void Dispose()
        {
            this._randomNumberGenerator?.Dispose();
        }
    }
}
