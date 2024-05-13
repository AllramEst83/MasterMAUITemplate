namespace MasterTemplate.Interfaces
{
    /// <summary>
    /// Interface for managing application preferences.
    /// </summary>
    public interface IPreferencesService
    {
        /// <summary>
        /// Retrieves a preference value of the specified type from the storage.
        /// </summary>
        /// <typeparam name="T">The type of the preference value.</typeparam>
        /// <param name="key">The key associated with the preference value.</param>
        /// <returns>The preference value if found; otherwise, null.</returns>
        T? Get<T>(string key) where T : class;

        Guid? GetCheckAndGetNewGroup(string key);

        /// <summary>
        /// Sets a preference value for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the preference value.</typeparam>
        /// <param name="key">The key associated with the preference value.</param>
        /// <param name="value">The value to set for the preference.</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Removes the preference value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the preference value to remove.</param>
        void Remove(string key);

        /// <summary>
        /// Clears all stored preferences.
        /// </summary>
        void Clear();
    }
}
