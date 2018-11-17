using System;
using System.Text;
using System.Security.Cryptography;

namespace Structum.Elements.Security.Encryption
{
    /// <summary>
    ///     Provides an easy to use hashing methods with a configurable hashing algorithm.
    /// </summary>
    /// <example>
    ///     The following code shows how to create a hasher using the default hashing Algorithm (Sha256):
    ///     <code>
    ///     var hasher = new Hasher();
    ///     string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
    ///
    ///     Console.WriteLine("This is the resulting hash: " + hash);
    ///     </code>
    ///     The following code demonstrates how to create a hasher with a different hashing algorithm:
    ///     <code>
    ///     var hasher = new Hasher();
    ///     string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!") {
    ///         HashAlgorithm = CryptographicHashAlgorithmType.Md5
    ///     };
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
        public CryptographicHashAlgorithmType HashingAlgorithm { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Hasher"/> class.
        /// </summary>
        public Hasher()
        {
            this.HashingAlgorithm = CryptographicHashAlgorithmType.Sha256;
        }

        /// <summary>
        ///     Computes a Hash for the selected text and returns it as a string. By default this method returns the hash string
        ///     representation, but it can alternatively return the hash as a base64 string which can be more compact.
        ///     For example using an MD5 Hash:
        ///     <ul>
        ///         <li>String Representation: eaf1f566d6062bfb339ce2bb2bc17d10 (32 chars)</li>
        ///         <li>Base64 Representation: 6vH1ZtYGK/sznOK7K8F9EA== (24 Chars)</li>
        ///     </ul>
        /// </summary>
        /// <param name="plainText">Plain Text.</param>
        /// <param name="returnBase64">Returns the hash contents represented in a base64 representation. (Optional: False default).</param>
        /// <exception cref="T:NotSupportedException">Thrown when the supplied Hashing Algorithm is not supported.</exception>
        /// <returns><c>String</c> containing the Hash.</returns>
        public string ComputeHash(string plainText, bool returnBase64=false)
        {
            using(HashAlgorithm hasher = CreateAlgorithm(this.HashingAlgorithm)) {
                byte[] originalBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] hashBytes = hasher.ComputeHash(originalBytes);

                hashBytes = hasher.ComputeHash(hashBytes);

                if (returnBase64) {
                    return Convert.ToBase64String(hashBytes);
                }

                StringBuilder result = new StringBuilder(hashBytes.Length*2);
                foreach(var b in hashBytes) {
                    result.Append(b.ToString("x2"));
                }

                return result.ToString();
            }
        }

        /// <summary>
        ///     Creates and returns the Hashing Algorithm instance.
        /// </summary>
        /// <param name="type">Algorithm Type.</param>
        /// <returns>Symmetric Algorithm Instance.</returns>
        private static HashAlgorithm CreateAlgorithm(CryptographicHashAlgorithmType type)
        {
            string algorithm = Enum.GetName(typeof(CryptographicHashAlgorithmType), type) ?? "SHA256";
            return HashAlgorithm.Create(algorithm);
        }
    }
}