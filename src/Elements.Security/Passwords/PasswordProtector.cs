using System;
using Structum.Elements.Security.Encryption;

namespace Structum.Elements.Security.Passwords
{
    /// <summary>
    ///     Provides a set of methods to protect passwords using iterative stretching. Once a password
    ///     is protected it is returned as <see cref="ProtectedPassword"/> class.
    /// </summary>
    /// <remarks>
    ///     Iterative Stretching works by iteratively adding the salt and hashing the results certain number of times.
    ///     This is controlled by the Iteration Count.
    /// </remarks>
    /// <example>
    ///     The following code shows how to use the Password Protector with a random salt and random iteration count:
    ///     <code>
    ///     string passwordToProtect = "MyC0013ncrypti0nP@$$w0rd!";
    ///     using(var protector = new PasswordProtector()) {
    ///         ProtectedPassword protectedPassword = protector.GetProtectedPassword(passwordToProtect);
    ///
    ///         Console.WriteLine("Protected Password Text: " + protectedPassword.Password);
    ///         Console.WriteLine("Protected Password Salt: " + protectedPassword.Salt);
    ///         Console.WriteLine("Protected Password Iteration Count: " + protectedPassword.IterationCount);
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
        private readonly Hasher _hasher = new Hasher("SHA-256");

        /// <summary>
        ///     Initializes a new instance of the <see cref="PasswordProtector"/> class.
        /// </summary>
        public PasswordProtector()
        {
            this._passwordGenerator = new PasswordGenerator(this._randomizer);
        }

        /// <summary>
        /// Gets the protected password.
        /// </summary>
        /// <param name="passwordToProtect">Password to Protect.</param>
        /// <returns>Protected Password.</returns>
        public ProtectedPassword GetProtectedPassword(string passwordToProtect)
        {
            string salt = CreateRandomSalt();
            int iterationCount = CreateRandomIterationNumber();

            return GetProtectedPassword(passwordToProtect, salt, iterationCount);
        }

        /// <summary>
        ///     Gets the protected password.
        /// </summary>
        /// <param name="passwordToProtect">Password to Protect.</param>
        /// <param name="salt">Password Salt.</param>
        /// <param name="iterationCount">Iteration count.</param>
        /// <returns>Protected Password.</returns>
        public ProtectedPassword GetProtectedPassword(string passwordToProtect, string salt, int iterationCount)
        {
            if(salt == null) {
                salt = this._passwordGenerator.Generate();
            }

            string protectedPassword = passwordToProtect + salt;
            for (int iteration = 0; iteration < iterationCount; iteration++) {
                protectedPassword = this._hasher.Hash(protectedPassword);
            }

            return new ProtectedPassword(protectedPassword, salt, iterationCount);
        }

        /// <summary>
        ///     Releases all resource used by the <see cref="PasswordProtector"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="PasswordProtector"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="PasswordProtector"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="PasswordProtector"/> so the garbage collector can reclaim the memory
        /// that the <see cref="PasswordProtector"/> was occupying.</remarks>
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
