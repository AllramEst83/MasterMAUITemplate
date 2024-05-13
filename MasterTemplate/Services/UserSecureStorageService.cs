using MasterTemplate.Interfaces;
using System.Text.Json;

namespace MasterTemplate.Services
{
    /// <summary>
    /// Service for securely storing and retrieving user data.
    /// </summary>
    public class UserSecureStorageService : IUserSecureStorageService
    {
        private readonly ISecureStorage _secureStorage;

        /// <summary>
        /// Initializes a new instance of the UserSecureStorageService class.
        /// </summary>
        /// <param name="secureStorage">The secure storage provider.</param>
        public UserSecureStorageService(ISecureStorage secureStorage)
        {
            _secureStorage = secureStorage;
        }

        /// <summary>
        /// Asynchronously checks if the user has data stored securely with the specified key.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the user has data stored securely with the specified key; otherwise, false.</returns>
        public async Task<bool> UserHasSecureDataAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or whitespace.", nameof(key));
            }

            try
            {
                var data = await _secureStorage.GetAsync(key);
                return !string.IsNullOrEmpty(data);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Asynchronously retrieves data of the specified type associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        /// <returns>The retrieved data if found; otherwise, null.</returns>
        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            try
            {
                var jsonString = await _secureStorage.GetAsync(key);
                if (string.IsNullOrEmpty(jsonString)) return null;

                var result = JsonSerializer.Deserialize<T>(jsonString);
                if (result == null)
                {
                    throw new InvalidOperationException($"Deserialization of {key} returned null.");
                }
                return result;
            }
            catch (JsonException jsonEx)
            {
                // Specific handling for JSON serialization issues
                throw new InvalidOperationException($"Error deserializing item for key {key}.", jsonEx);
            }
            catch (Exception ex)
            {
                // Generic exception handling
                throw new InvalidOperationException($"Unable to retrieve item from secure storage for key {key}.", ex);
            }
        }

        /// <summary>
        /// Asynchronously stores data associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to store.</typeparam>
        /// <param name="key">The key associated with the data.</param>
        /// <param name="value">The data to store.</param>
        public async Task SetAsync<T>(string key, T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Cannot store null value in secure storage.");

            try
            {
                var jsonString = JsonSerializer.Serialize(value);
                await _secureStorage.SetAsync(key, jsonString);
            }
            catch (JsonException jsonEx)
            {
                // Specific handling for JSON serialization issues
                throw new InvalidOperationException($"Error serializing item for key {key}.", jsonEx);
            }
            catch (Exception ex)
            {
                // Generic exception handling
                throw new InvalidOperationException($"Unable to set item in secure storage for key {key}.", ex);
            }
        }

        /// <summary>
        /// Removes the data associated with the specified key from the secure storage.
        /// </summary>
        /// <param name="key">The key associated with the data to remove.</param>
        /// <returns>True if the data was successfully removed; otherwise, false.</returns>
        public bool RemoveAsync(string key)
        {
            return SecureStorage.Default.Remove(key);
        }

        /// <summary>
        /// Clears all data stored in the secure storage.
        /// </summary>
        public void ClearAsync()
        {
            SecureStorage.Default.RemoveAll();
        }
    }
}
