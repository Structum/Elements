using System.Text;

namespace Structum.Elements.Security.Passwords
{
    /// <summary>
    ///     Provides a set of methods to generate passwords.
    /// </summary>
    /// <example>
    ///     The following code shows how to use the Password Generator:
    ///     <code>
    ///     Randomizer randomizer = new Randomizer();
    ///
    ///     // Generate a Password with the default configuration.
    ///     var pwdGenerator = new PasswordGenerator(randomizer);
    ///     string password = pwdGenerator.Generate();
    ///
    ///     Console.WriteLine("Generated Password: " + password);
    ///
    ///     // Generate a Password with a Minimum size of 10 and Maximum of 12.
    ///     var pwdGenerator = new PasswordGenerator(randomizer) {
    ///         MinimumSize = 10,
    ///         MaximumSize = 12
    ///     };
    ///     string password = pwdGenerator.Generate();
    ///
    ///     Console.WriteLine("Generated Password: " + password);
    ///     </code>
    /// </example>
    public class PasswordGenerator
    {
        /// <summary>
        ///     Gets or Sets the Randomizer.
        /// </summary>
        /// <value>Randomizer.</value>
        private readonly Randomizer _randomizer;

        /// <summary>
        ///     Default Minimum constant.
        /// </summary>
        private const int DefaultMinimum = 6;

        /// <summary>
        ///     Default Maximum constant.
        /// </summary>
        private const int DefaultMaximum = 20;

        /// <summary>
        ///     Gets or Sets the minimum password size. The default value is 6.
        /// </summary>
        /// <value>Minimum Password Size.</value>
        public int MinimumSize { get; set; }

        /// <summary>
        ///     Gets or Sets the maximum password size. The default value is 20.
        /// </summary>
        /// <value>Maximum Password Size.</value>
        public int MaximumSize { get; set; }

        /// <summary>
        ///     Gets or Sets the Allow Repeat Characters flag. Default is <c>false</c>
        /// </summary>
        /// <value>True to allow character repetition in the generated password.</value>
        public bool AllowRepeatCharacters { get; set; }

        /// <summary>
        ///     Gets or Sets the allow Consecutive Characters flag. Default is <c>false</c>
        /// </summary>
        /// <value>True to allow consecutive characters in the generated password.</value>
        public bool AllowConsecutiveCharacters { get; set; }

        /// <summary>
        ///     Initializes a new instance of the Password Generator class.
        /// </summary>
        /// <param name="randomizer">Randomizer instance. <see cref="Randomizer"/></param>
        public PasswordGenerator(Randomizer randomizer)
        {
            this.MinimumSize = DefaultMinimum;
            this.MaximumSize = DefaultMaximum;
            this.AllowConsecutiveCharacters = false;
            this.AllowRepeatCharacters = false;
            this._randomizer = randomizer;
        }

        /// <summary>
        ///     Generates a secure password.
        /// </summary>
        /// <returns>Secure Password.</returns>
        public string Generate()
        {
            int pwdLength = GetRandomPasswordLength(this.MinimumSize, this.MaximumSize);
            StringBuilder pwdBuffer = new StringBuilder(pwdLength+1);

            char lastCharacter = '\n';
            for (int i = 0; i < pwdLength; i++) {
                var nextCharacter = GetRandomCharacter();

                if (!this.AllowConsecutiveCharacters) {
                    while (lastCharacter == nextCharacter) {
                        nextCharacter = GetRandomCharacter();
                    }
                }

                if(!this.AllowRepeatCharacters) {
                    string temp = pwdBuffer.ToString();
                    int duplicateIndex = temp.IndexOf(nextCharacter);
                    while (-1 != duplicateIndex) {
                        nextCharacter = GetRandomCharacter();
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                pwdBuffer.Append(nextCharacter);
                lastCharacter = nextCharacter;
            }

            pwdBuffer.Append(this.GetRandomDigit());
            pwdBuffer.Append(this.GetRandomSpecialCharacter());

            return pwdBuffer.ToString();
        }

        /// <summary>
        ///     Returns a random character.
        /// </summary>
        /// <returns>Random Character.</returns>
        private char GetRandomCharacter()
        {
            return this._randomizer.GetRandomString(1, CharSetValues.Password)[0];
        }

		/// <summary>
		///     Returns a Random Digit.
		/// </summary>
		/// <returns>Random Digit.</returns>
		private char GetRandomDigit()
		{
            return this._randomizer.GetRandomInteger(0, 9).ToString()[0];
		}

		/// <summary>
		///     Returns a random special character.
		/// </summary>
		/// <returns>Random Special Character.</returns>
		private char GetRandomSpecialCharacter()
		{
            return this._randomizer.GetRandomString(1, CharSetValues.Specials)[0];
		}

        /// <summary>
        ///     Returns a random password size.
        /// </summary>
        /// <returns>Random integer representing the password size.</returns>
        private int GetRandomPasswordLength(int minSize, int maxSize)
        {
            return this._randomizer.GetRandomInteger(minSize, maxSize);
        }
    }
}
