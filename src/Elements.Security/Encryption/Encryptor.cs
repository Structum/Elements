using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Structum.Elements.Security.Encryption
{
    /// <summary>
    ///     Provides an easy to use encryption and decryption methods
    ///     with configurable Encryption Algorithm and Character Encoding.
    /// </summary>
    /// <remarks>
    ///     The encryptor uses a <see cref="Password"/> and a <see cref="Salt"/> to encrypt/decrypt. Internally
    ///     these 2 are used to create an secrete key which in turn is used to perform the encryption and decryption.
    /// </remarks>
    /// <example>
    ///     The following code shows how to create an encryptor with the Rijndael and utf-16 Algorithm
    ///     and encrypt a text:
    ///     <code>
    ///     // Create an encryptor with the default configuration.
    ///     var encryptor = new Encryptor("MyC0013ncrypti0nP@$$w0rd!", "RDBST2345asertf");
    ///
    ///     // Encrypt the text.
    ///     string encryptedText = encryptor.Encrypt("My Dirty Little Secret");
    ///     Console.WriteLine("This is the encrypted value:" + encryptedText);
    ///
    ///     // Decrypt the Cipher Text.
    ///     string decryptedValue = encryptor.Decrypt(encryptedText);
    ///     Console.WriteLine("This is the decrypted text" + decryptedValue);
    ///     </code>
    /// </example>
    public class Encryptor
    {
        /// <summary>
        ///     Gets or Sets the Encryption Algorithm. The default value is "Rijndael".
        ///     Please see <see cref="SymmetricAlgorithmType"/> for all the available values.
        /// </summary>
        /// <value>Name of Encryption Algorithm.</value>
        public SymmetricAlgorithmType EncryptionAlgorithm { get; set; }

        /// <summary>
        ///     Gets or Sets the Character Encoding to use. The default value "utf-16"
        /// </summary>
        /// <value>Character Encoding.</value>
        public string CharacterEncoding { get; set; }

        /// <summary>
        ///     Sets the Password used to encrypt/decrypt.
        /// </summary>
        /// <value>Password used by the encryptor.</value>
        private string Password { get; set; }

        /// <summary>
        ///     Sets the Salt.
        /// </summary>
        /// <value>Salt used by the encryptor.</value>
        private string Salt { get; set; }

        /// <summary>
        ///     Secrete key.
        /// </summary>
        private byte[] _secreteKeyBytes;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Encryptor"/> class.
        /// </summary>
        /// <param name="password">Password used for encryption/decryption.</param>
        /// <param name="salt">Salt used for encryption/decryption.</param>
        /// <exception cref="T:ArgumentNullException">Thrown when <paramref name="password"/> or <paramref name="salt"/> is empty.</exception>
        public Encryptor(string password, string salt)
        {
            if (string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException(nameof(password));
            }

            if (string.IsNullOrEmpty(salt)) {
                throw new ArgumentNullException(nameof(salt));
            }

            this.EncryptionAlgorithm = SymmetricAlgorithmType.Rijndael;
            this.CharacterEncoding = "utf-16";
            this.Password = password;
            this.Salt = salt;
        }

        /// <summary>
        ///     Encrypts the provided plain text.
        /// </summary>
        /// <param name="plainText">Plain Text to Encrypt.</param>
        /// <returns><c>String</c> containing the cipher text.</returns>
        public string Encrypt(string plainText)
        {
            SymmetricAlgorithm symmetricAlgorithm = CreateAlgorithm(this.EncryptionAlgorithm);
            symmetricAlgorithm.GenerateIV();
            byte[] iv = symmetricAlgorithm.IV;

            ICryptoTransform encryptor = symmetricAlgorithm.CreateEncryptor(this.CreateSecretKeyBytes(), iv);
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            string result;
            try {
                Encoding textConverter = Encoding.GetEncoding(this.CharacterEncoding);
                byte[] plaintextBytes = textConverter.GetBytes(plainText);

                csEncrypt.Write(plaintextBytes, 0, plaintextBytes.Length);
                csEncrypt.FlushFinalBlock();

                byte[] encryptedBytes = msEncrypt.ToArray();
                byte[] encryptedBytesPlusIv = Combine(iv, encryptedBytes);

                result = Convert.ToBase64String(encryptedBytesPlusIv);
            } finally {
                symmetricAlgorithm.Clear();
                msEncrypt.Close();
                csEncrypt.Close();
            }

            return result;
        }

        /// <summary>
        ///     Decrypts the selected cipher text.
        /// </summary>
        /// <param name="cipherText">Cipher Text to Decrypt.</param>
        /// <returns><c>String</c> containing the plain text.</returns>
        public string Decrypt(string cipherText)
        {
            SymmetricAlgorithm symmetricAlgorithm = CreateAlgorithm(this.EncryptionAlgorithm);
            Encoding textConverter = Encoding.GetEncoding(this.CharacterEncoding);

            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            int ivLength = symmetricAlgorithm.IV.Length;
            byte[] ivBytes = new byte[ivLength];
            Array.Copy(cipherTextBytes, ivBytes, ivLength);

            int onlyCipherTextLength = cipherTextBytes.Length - ivLength;
            byte[] onlyCipherTextBytes = new byte[onlyCipherTextLength];
            Array.Copy(cipherTextBytes, ivLength, onlyCipherTextBytes, 0, onlyCipherTextLength);

            ICryptoTransform decryptor = symmetricAlgorithm.CreateDecryptor(this.CreateSecretKeyBytes(), ivBytes);

            MemoryStream msDecrypt = new MemoryStream(onlyCipherTextBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            string result;
            try {
                byte[] plaintextBytes = new byte[onlyCipherTextLength];
                int decryptedBytes = csDecrypt.Read(plaintextBytes, 0, onlyCipherTextLength);

                string plaintext = textConverter.GetString(plaintextBytes, 0, decryptedBytes);

                result = plaintext;

            } finally {
                symmetricAlgorithm.Clear();
                msDecrypt.Close();
                csDecrypt.Close();
            }

            return result;
        }

        /// <summary>
        ///     Creates and returns the Secret Key.
        /// </summary>
        /// <returns><c>byte[]</c> containing the secret encryption key.</returns>
        private byte[] CreateSecretKeyBytes()
        {
            if (this._secreteKeyBytes != null) {
                return this._secreteKeyBytes;
            }

            SymmetricAlgorithm symmetricAlgorithm = CreateAlgorithm(this.EncryptionAlgorithm);

            byte[] saltBytes = Encoding.ASCII.GetBytes(this.Salt);
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(this.Password, saltBytes);

            this._secreteKeyBytes = rfc2898.GetBytes(symmetricAlgorithm.KeySize / 8);

            return this._secreteKeyBytes;
        }

        /// <summary>
        ///     Combines the two byte arrays.
        /// </summary>
        /// <param name="a">Byte Array A.</param>
        /// <param name="b">Byte Array B.</param>
        /// <returns>Combined byte array. <c>byte[]</c></returns>
        private static byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        /// <summary>
        ///     Creates and returns the Algorithm instance.
        /// </summary>
        /// <param name="type">Algorithm Type.</param>
        /// <returns>Symmetric Algorithm Instance.</returns>
        private static SymmetricAlgorithm CreateAlgorithm(SymmetricAlgorithmType type)
        {
            string algorithm = Enum.GetName(typeof(SymmetricAlgorithmType), type) ?? "Rijndael";
            return SymmetricAlgorithm.Create(algorithm);
        }
    }
}
