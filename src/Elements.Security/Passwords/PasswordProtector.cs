using System;
using Structum.Elements.Security.Encryption;

namespace Structum.Elements.Security.Passwords
{
    /// <summary>
    ///     Provides a set of methods to protect passwords using iterative stretching. Once a password
    ///     is protected it is returned as <see cref="ProtectedPassword"/> class.
    /// </summary>
    /// <remarks>
    ///     Iterative Stretching works by iteratively adding a salt to a password and then hashing the results a certain number of times.
    ///     The <see cref="Protect"/> method provides the best solution when setting iteration count and salt parameters with a null value,
    ///     which will generate a random salt and iteration count. The resulting <see cref="ProtectedPassword"/> instance will contain the
    ///     Salt and Iteration Count used to protect the password. The results of <see cref="Protect"/> method can then be stored in a database
    ///     to use for comparison later. It is necessary to store the full Protected Password in the database, otherwise there is no way to compare.
    ///     <br />
    ///     To compare a plain text password, we just need to use <see cref="CompareProtectedPassword"/> method which expects the plain text password and
    ///     the stored protected password as parameters.
    ///     <br />
    ///     The default <see cref="CryptographicHashAlgorithmType"/> is <see cref="CryptographicHashAlgorithmType.Sha256"/>.
    /// </remarks>
    /// <example>
    ///     The following code shows how to use the Password Protector with a random salt and random iteration count:
    ///     <code>
    ///     string passwordToProtect = "MyC0013ncrypti0nP@$$w0rd!";
    ///     using(var protector = new PasswordProtector()) {
    ///         ProtectedPassword protectedPassword = protector.Protect(passwordToProtect);
    ///
    ///         Console.WriteLine("Protected Password Text: " + protectedPassword.Password);
    ///         Console.WriteLine("Protected Password Salt: " + protectedPassword.Salt);
    ///         Console.WriteLine("Protected Password Iteration Count: " + protectedPassword.IterationCount);
    ///     }
    ///     </code>
    ///     The following code demonstrates how to compare a plain text password to the stored Protect Password:
    ///     <code>
    ///     using(var protector = new PasswordProtector()) {
    ///         if(protector.CompareProtectedPassword("MyC0013ncrypti0nP@$$w0rd!", protectedPassword)) {
    ///             Console.WriteLine("Passwords Match!");
    ///         } else {
    ///             Console.WriteLine("Passwords not match...");
    ///         }
    ///     }
    ///     </code>
    /// </example>
    public class PasswordProtector : IDisposable
    {
        /// <summary>
        ///     Gets or Sets the Randomizer.
        /// </summary>
        /// <value>Randomizer</value>
        private readonly Randomizer _randomizer = new Randomizer();

        /// <summary>
        ///     Gets or Sets the Password Generator.
        /// </summary>
        /// <value>Password Generator.</value>
        private readonly PasswordGenerator _passwordGenerator;

        /// <summary>
        ///     Gets or Sets teh Hasher.
        /// </summary>
        /// <value>Hasher.</value>
        private readonly Hasher _hasher = new Hasher();

        /// <summary>
        ///     Initializes a new instance of the <see cref="PasswordProtector"/> class.
        /// </summary>
        public PasswordProtector()
        {
            this._passwordGenerator = new PasswordGenerator(this._randomizer);
        }

        /// <summary>
        ///     Protects the selected password using a defined salt and iteration count.
        /// </summary>
        /// <param name="passwordToProtect">Password to Protect.</param>
        /// <param name="iterationCount">Iteration count. Set to null to generate a random iteration count.</param>
        /// <param name="salt">Password Salt to use. Set to null to generate a random salt.</param>
        /// <returns>Protected Password.</returns>
        public ProtectedPassword Protect(string passwordToProtect, int? iterationCount = null, string salt = null)
        {
            if (iterationCount == null) {
                iterationCount = CreateRandomIterationNumber();
            }

            if(salt == null) {
                salt = CreateRandomSalt();
            }

            string protectedPassword = passwordToProtect + salt;
            for (int iteration = 0; iteration < iterationCount; iteration++) {
                protectedPassword = this._hasher.ComputeHash(protectedPassword, true);
            }

            return new ProtectedPassword(protectedPassword, salt, iterationCount.Value);
        }

        /// <summary>
        ///     Compares the selected plain text password to the stored Protected Password.
        ///     Returns <c>true</c> if the plain text password matches the stored protected password, <c>false</c> otherwise.
        /// </summary>
        /// <param name="passwordToCompare">Plain Text Password.</param>
        /// <param name="protectedPassword">Stored Protected Password.</param>
        /// <returns><c>true</c> if the plain text password matches the stored protected password, <c>false</c> otherwise.</returns>
        public bool CompareProtectedPassword(string passwordToCompare, ProtectedPassword protectedPassword)
        {
            ProtectedPassword protectedPasswordToCompare = this.Protect(passwordToCompare, protectedPassword.IterationCount, protectedPassword.Salt);
            return protectedPasswordToCompare.Password.Equals(protectedPassword.Password);
        }

        /// <summary>
        ///     Releases all resource used by the <see cref="PasswordProtector"/> object.
        /// </summary>
        public void Dispose()
        {
            this._randomizer?.Dispose();
        }

        /// <summary>
        ///     Creates a Random Salt.
        /// </summary>
        /// <returns>Random Salt.</returns>
        private string CreateRandomSalt()
        {
            return this._passwordGenerator.Generate();
        }

        /// <summary>
        ///     Creates a Random Iteration Number.
        /// </summary>
        /// <returns>Random Iteration Number.</returns>
        private int CreateRandomIterationNumber()
        {
            return this._randomizer.GetRandomInteger(500, 1024);
        }
    }
}
