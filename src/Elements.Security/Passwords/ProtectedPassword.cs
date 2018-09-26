namespace Structum.Elements.Security.Passwords
{
    /// <summary>
    ///     Defines a protected password class which is used to contain the information for a
    ///     protected password. This is usually created using the <see cref="PasswordProtector"/>.
    /// </summary>
    /// <example>
    ///     <code>
    ///         ProtectedPassword protectedPassword = new ProtectedPassword(protectedPasswordText, "SDFR$%", 100);
    ///
    ///         Console.WriteLine("Protected Password Text: " + protectedPassword.Password);
    ///         Console.WriteLine("Protected Password Salt: " + protectedPassword.Salt);
    ///         Console.WriteLine("Protected Password Iteration Count: " + protectedPassword.IterationCount);
    ///     </code>
    /// </example>
    public class ProtectedPassword
    {
        /// <summary>
        ///     Gets the Protected Password.
        /// </summary>
        /// <value>Protected Password.</value>
        public string Password { get; private set; }

        /// <summary>
        ///     Gets the Random Salt.
        /// </summary>
        /// <value>Random Salt.</value>
        public string Salt { get; private set; }

        /// <summary>
        ///     Gets the Iteration Count.
        /// </summary>
        /// <value>Iteration Count.</value>
        public int IterationCount { get; private set; }

        /// <summary>
        ///     Initializes a new instance of the Protected Password class.
        /// </summary>
        /// <param name="password">Protected Password.</param>
        /// <param name="salt">Random Salt.</param>
        /// <param name="iterationCount">Iteration Count.</param>
        public ProtectedPassword(string password, string salt, int iterationCount)
        {
            this.Password = password;
            this.Salt = salt;
            this.IterationCount = iterationCount;
        }

        /// <summary>
        ///     Returns true if the provided object has the same values as the current object.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns><c>bool</c></returns>
        public override bool Equals(object obj)
        {
            ProtectedPassword pwd = obj as ProtectedPassword;
            if (pwd == null) {
                return false;
            }

            return (this.Password == pwd.Password && this.Salt == pwd.Salt && this.IterationCount == pwd.IterationCount);
        }

        /// <summary>
        ///     Returns the Hash Code of the current instance.
        /// </summary>
        /// <returns><c>Int</c> representing the current instance's hash code.</returns>
        public override int GetHashCode()
        {
            return this.Password.GetHashCode() ^ this.Salt.GetHashCode() ^ this.IterationCount.GetHashCode();
        }
    }
}