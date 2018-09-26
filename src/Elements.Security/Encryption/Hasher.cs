using System;
using System.Text;
using System.Security.Cryptography;

namespace Structum.Elements.Security.Encryption
{
    /// <summary>
    ///     Provides an easy to use hashing methods with a configurable hashing algorithm.
    /// </summary>
    /// <example>
    ///     The following code shows how to create a hasher using SHA-256 Algorithm:
    ///     <code>
    ///     var hasher = new Hasher("SHA-256");
    ///     string hash = hasher.Hash("MyC0013ncrypti0nP@$$w0rd!");
    ///
    ///     Console.WriteLine("This is the resulting hash: " + hash);
    ///     </code>
    /// </example>
    public class Hasher
    {
        /// <summary>
        ///     Gets or Sets the Hashing Algorithm.
        /// </summary>
        /// <value>Name of the Hash Algorithm.</value>
        private string HashAlgorithmName { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hasher"/> class.
        /// </summary>
        /// <param name="hashAlgorithm">Hash algorithm.</param>
        public Hasher(string hashAlgorithm)
        {
            this.HashAlgorithmName = hashAlgorithm;
        }

        /// <summary>
        ///     Creates a Hash for the selected plain text
        /// </summary>
        /// <param name="plainText">Plain Text.</param>
        /// <exception cref="T:NotSupportedException">Thrown when the supplied Hashing Algorithm is not supported.</exception>
        /// <returns><c>String</c> containing the Hash.</returns>
        public string Hash(string plainText)
        {
            using(HashAlgorithm hasher = HashAlgorithm.Create(this.HashAlgorithmName)) {

                if (hasher == null) {
                    throw new NotSupportedException(this.HashAlgorithmName);
                }

                byte[] originalBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] hashBytes = hasher.ComputeHash(originalBytes);

                hashBytes = hasher.ComputeHash(hashBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}