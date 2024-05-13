using MasterTemplate.Interfaces;
using System.Text.Json;

namespace MasterTemplate.Services
{
    /// <summary>
    /// Service for managing application preferences using Xamarin Essentials Preferences API.
    /// </summary>
    public class PreferencesService : IPreferencesService
    {
        public Guid? GetCheckAndGetNewGroup(string key)
        {
            // Check if the key exists
            if (!Preferences.Default.ContainsKey(key))
            {
                return null;
            }

            // Get the GUID as a serialized JSON string
            var jsonString = Preferences.Default.Get<string>(key, null);

            // If for some reason it's still null, return null
            if (string.IsNullOrEmpty(jsonString)) return null;

            // Deserialize the JSON string back to Guid
            try
            {
                var guidValue = JsonSerializer.Deserialize<Guid>(jsonString);
                return guidValue;
            }
            catch (JsonException)
            {
                // Handle or log the error as needed if deserialization fails
                return null;
            }
        }



        /// <summary>
        /// Retrieves a preference value of the specified type associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the preference value.</typeparam>
        /// <param name="key">The key associated with the preference value.</param>
        /// <returns>The preference value if found; otherwise, null.</returns>
        public T? Get<T>(string key) where T : class
        {
            try
            {
                var jsonString = Preferences.Default.Get<string?>(key, null);
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
                throw new InvalidOperationException($"Unable to retrieve item from preferences for key {key}.", ex);
            }
        }

        /// <summary>
        /// Sets a preference value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the preference value.</typeparam>
        /// <param name="key">The key associated with the preference value.</param>
        /// <param name="value">The value to set for the preference.</param>
        public void Set<T>(string key, T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value), "Cannot store null value in preferences.");

            try
            {
                var jsonString = JsonSerializer.Serialize(value);
                Preferences.Default.Set(key, jsonString);
            }
            catch (JsonException jsonEx)
            {
                // Specific handling for JSON serialization issues
                throw new InvalidOperationException($"Error serializing item for key {key}.", jsonEx);
            }
            catch (Exception ex)
            {
                // Generic exception handling
                throw new InvalidOperationException($"Unable to set item in preferences for key {key}.", ex);
            }
        }

        /// <summary>
        /// Removes the preference value associated with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the preference value to remove.</param>
        public void Remove(string key)
        {
            try
            {
                Preferences.Default.Remove(key);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to remove item from preferences for key {key}.", ex);
            }
        }

        /// <summary>
        /// Clears all stored preferences.
        /// </summary>
        public void Clear()
        {
            try
            {
                Preferences.Default.Clear();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to clear preferences.", ex);
            }
        }
    }
}
