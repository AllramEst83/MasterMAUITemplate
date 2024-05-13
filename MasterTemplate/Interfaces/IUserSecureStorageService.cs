namespace MasterTemplate.Interfaces
{
    /// <summary>
    /// Interface for securely storing and retrieving user data.
    /// </summary>
    public interface IUserSecureStorageService
    {
        /// <summary>
        /// Asynchronously retrieves the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <returns>The retrieved value if it exists; otherwise, null.</returns>
        Task<T?> GetAsync<T>(string key) where T : class;

        /// <summary>
        /// Asynchronously sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the value to set.</typeparam>
        /// <param name="key">The key associated with the value.</param>
        /// <param name="value">The value to set.</param>
        Task SetAsync<T>(string key, T value);

        /// <summary>
        /// Asynchronously removes the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the value to remove.</param>
        /// <returns>True if the value was successfully removed; otherwise, false.</returns>
        bool RemoveAsync(string key);

        /// <summary>
        /// Asynchronously clears all stored user data.
        /// </summary>
        void ClearAsync();

        /// <summary>
        /// Asynchronously checks if the user has data stored securely with the specified key.
        /// </summary>
        /// <param name="key">The key to check for.</param>
        /// <returns>True if the user has data stored securely with the specified key; otherwise, false.</returns>
        Task<bool> UserHasSecureDataAsync(string key);
    }
}
