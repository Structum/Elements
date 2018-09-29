using System;
using Xunit;
using Structum.Elements.Security.Encryption;

namespace Structum.Elements.Security.Tests.Encryption
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="Encryptor"/> class.
    /// </summary>
    public class EncryptorTest
    {
        /// <summary>
        ///     Tests the Default configuration for the Encryption and Decryption functionality.
        /// </summary>
        [Fact]
        public void DefaultEncryptionDecryptionTest()
        {
            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf");

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Tests the Encryption and Decryption functionality using the AES Symmetric Algorithm.
        /// </summary>
        [Fact]
        public void AesEncryptionDecryptionTest()
        {
            SymmetricAlgorithmType expectedAlgorithm = SymmetricAlgorithmType.Aes;

            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf") {
                EncryptionAlgorithm = expectedAlgorithm,
                CharacterEncoding = "utf-8"
            };

            Assert.Equal(expectedAlgorithm, encryptor.EncryptionAlgorithm);

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Tests the Encryption and Decryption functionality using the DES Symmetric Algorithm.
        /// </summary>
        [Fact]
        public void DesEncryptionDecryptionTest()
        {
            SymmetricAlgorithmType expectedAlgorithm = SymmetricAlgorithmType.Des;

            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf") {
                EncryptionAlgorithm = expectedAlgorithm,
                CharacterEncoding = "utf-8"
            };

            Assert.Equal(expectedAlgorithm, encryptor.EncryptionAlgorithm);

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Tests the Encryption and Decryption functionality using the RC2 Symmetric Algorithm.
        /// </summary>
        [Fact]
        public void Rc2EncryptionDecryptionTest()
        {
            SymmetricAlgorithmType expectedAlgorithm = SymmetricAlgorithmType.Rc2;

            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf") {
                EncryptionAlgorithm = expectedAlgorithm,
                CharacterEncoding = "utf-8"
            };

            Assert.Equal(expectedAlgorithm, encryptor.EncryptionAlgorithm);

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Tests the Encryption and Decryption functionality using the Triple-DES Symmetric Algorithm.
        /// </summary>
        [Fact]
        public void TripleDesEncryptionDecryptionTest()
        {
            SymmetricAlgorithmType expectedAlgorithm = SymmetricAlgorithmType.TripleDes;

            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf") {
                EncryptionAlgorithm = expectedAlgorithm,
                CharacterEncoding = "utf-8"
            };

            Assert.Equal(expectedAlgorithm, encryptor.EncryptionAlgorithm);

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Tests the Encryption and Decryption functionality using the Rijndael Symmetric Algorithm.
        /// </summary>
        [Fact]
        public void RijndaelEncryptionDecryptionTest()
        {
            SymmetricAlgorithmType expectedAlgorithm = SymmetricAlgorithmType.Rijndael;

            // Create an encryptor with the default configuration.
            var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf") {
                EncryptionAlgorithm = expectedAlgorithm,
                CharacterEncoding = "utf-8"
            };

            Assert.Equal(expectedAlgorithm, encryptor.EncryptionAlgorithm);

            RunEncryptionDecryptionTest(encryptor);
        }

        /// <summary>
        ///     Runs the Encryption/Decryption tests using a predetermined <see cref="Encryptor"/> instance.
        /// </summary>
        /// <param name="encryptor">Encryptor instance to use.</param>
        private void RunEncryptionDecryptionTest(Encryptor encryptor)
        {
            if (encryptor == null) {
                throw new ArgumentNullException(nameof(encryptor));
            }

            const string expectedSecret = "My Dirty Little Secret";

            // Encrypt the text.
            string encryptedText = encryptor.Encrypt(expectedSecret);
            Assert.False(string.IsNullOrEmpty(encryptedText));

            // Decrypt the Cipher Text.
            string decryptedValue = encryptor.Decrypt(encryptedText);
            Assert.Equal(expectedSecret, decryptedValue);
        }
    }
}