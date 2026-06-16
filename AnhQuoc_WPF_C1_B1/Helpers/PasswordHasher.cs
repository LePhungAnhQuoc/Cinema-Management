using System;
using System.Security.Cryptography;

public static class PasswordHasher
{
    private const int SaltSize = 16; // 128-bit
    private const int KeySize = 32;  // 256-bit
    private const int Iterations = 100000; // High iteration count to slow down attackers

    /// <summary>
    /// Creates a secure, salted hash from a plain text password.
    /// </summary>
    public static string HashPassword(string password)
    {
        // 1. Generate a unique salt
        using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] salt = algorithm.Salt;
            byte[] key = algorithm.GetBytes(KeySize);

            // 2. Combine salt and key into one array for easy database storage
            byte[] hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

            // 3. Convert to Base64 string to store in a standard text column (e.g., VARCHAR)
            return Convert.ToBase64String(hashBytes);
        }
    }

    /// <summary>
    /// Verifies an incoming password against the stored database hash.
    /// </summary>
    public static bool VerifyPassword(string password, string storedHash)
    {
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // 1. Extract the salt from the stored hash
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // 2. Compute the hash of the incoming password using the extracted salt
        using (var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] key = algorithm.GetBytes(KeySize);

            // 3. Compare the results byte-by-byte
            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[i + SaltSize] != key[i])
                    return false; // Password mismatch
            }
        }
        return true;
    }
}