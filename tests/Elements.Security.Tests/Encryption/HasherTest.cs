using Xunit;
using Structum.Elements.Security.Encryption;

namespace Structum.Elements.Security.Tests.Encryption
{
    /// <summary>
    ///     Provides a set of unit tests for the <see cref="Hasher"/> class.
    /// </summary>
    public class HasherTest
    {
        /// <summary>
        ///     Tests the Default configuration for the compute a hash.
        /// </summary>
        [Fact]
        public void DefaultComputeHashTest()
        {
            var hasher = new Hasher();
            string hash = hasher.ComputeHash("MyC0013ncrypti0nP@$$w0rd!");
            Assert.Equal(64, hash.Length);
        }

        /// <summary>
        ///     Tests the compute a hash using the MD5 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Md5ComputeHashTest()
        {
            const string testStr = "MyC0013ncrypti0nP@$$w0rd!";
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Md5
            };
            string hash = hasher.ComputeHash(testStr);
            Assert.Equal(32, hash.Length);
            Assert.Equal("eaf1f566d6062bfb339ce2bb2bc17d10", hash);

            hash = hasher.ComputeHash(testStr, true);
            Assert.Equal(24, hash.Length);
            Assert.Equal("6vH1ZtYGK/sznOK7K8F9EA==", hash);
        }

        /// <summary>
        ///     Tests the compute a hash using the SHA1 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Sha1ComputeHashTest()
        {
            const string testStr = "MyC0013ncrypti0nP@$$w0rd!";
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Sha1
            };

            string hash = hasher.ComputeHash(testStr);
            Assert.Equal(40, hash.Length);
            Assert.Equal("9ef250efe9e9ecf8fa1b2e5092291f184c3b12cc", hash);

            hash = hasher.ComputeHash(testStr, true);
            Assert.Equal(28, hash.Length);
            Assert.Equal("nvJQ7+np7Pj6Gy5QkikfGEw7Esw=", hash);
        }

        /// <summary>
        ///     Tests the compute a hash using the SHA256 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Sha256ComputeHashTest()
        {
            const string testStr = "MyC0013ncrypti0nP@$$w0rd!";
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Sha256
            };

            string hash = hasher.ComputeHash(testStr);
            Assert.Equal(64, hash.Length);
            Assert.Equal("44ee1da5c3fb087f652f7cc5057d9ad04f0a59e19a2c98be6a44852bcda74671", hash);

            hash = hasher.ComputeHash(testStr, true);
            Assert.Equal(44, hash.Length);
            Assert.Equal("RO4dpcP7CH9lL3zFBX2a0E8KWeGaLJi+akSFK82nRnE=", hash);
        }

        /// <summary>
        ///     Tests the compute a hash using the SHA512 Hashing Algorithm.
        /// </summary>
        [Fact]
        public void Sha512ComputeHashTest()
        {
            const string testStr = "MyC0013ncrypti0nP@$$w0rd!";
            var hasher = new Hasher {
                HashingAlgorithm = CryptographicHashAlgorithmType.Sha512
            };

            string hash = hasher.ComputeHash(testStr);
            Assert.Equal(128, hash.Length);
            Assert.Equal("d801a7ed6583291250e991fd09fd242dc7c135f0a36ed1339bda6861ea12b2624f4a158b6bb74649e7328c3dda15f65d6ae274a3e1c6b0208cf8be2381b914cb", hash);

            hash = hasher.ComputeHash(testStr, true);
            Assert.Equal(88, hash.Length);
            Assert.Equal("2AGn7WWDKRJQ6ZH9Cf0kLcfBNfCjbtEzm9poYeoSsmJPShWLa7dGSecyjD3aFfZdauJ0o+HGsCCM+L4jgbkUyw==", hash);
        }
    }
}