using System.Collections;
using Dexla.Common.Types;
using Dexla.Common.Types.Configuration;
using Microsoft.Extensions.Configuration;

namespace Dexla.Common.Utilities
{
    public static class UtilityExtensions
    {
        public static string GetId()
        {
            return Guid.NewGuid().ToString("N");
        }

        private const char CompositeIdDelimiter = '|';

        public static string GetCompositeId(params string[] values)
        {
            return string.Join(CompositeIdDelimiter, values);
        }

        public static string[] SplitCompositeId(string value)
        {
            return value.Split(CompositeIdDelimiter);
        }

        public static IEnumerable<string> GetSupportedMediaTypes()
        {
            List<string> mediaTypes = GetPreferredMediaTypes().ToList();
            mediaTypes.Add("application/graphql");
            mediaTypes.Add("application/vnd.oai.openapi");
            mediaTypes.Add("application/vnd.oai.openapi+json");
            mediaTypes.Add(MediaTypes.EventStream);
            mediaTypes.Add(MediaTypes.OctetStream);
            mediaTypes.Add("text/plain");

            return mediaTypes;
        }

        public static string[] GetPreferredMediaTypes()
        {
            return new[]
            {
                MediaTypes.ApplicationJson, MediaTypes.ApplicationFormUrlEncoded, "multipart/form-data",
                "application/graphql"
            };
        }

        public static bool InList<TType>(this TType value, params TType[] values)
        {
            return ((IList)values).Contains(value);
        }

        public static TReturnType GetSettingByName<TReturnType>(
            this SettingCollection<TReturnType> settings,
            string name)
        {
            bool? result = settings.Settings.TryGetValue(name, out TReturnType? value);

            if (result == false || value == null)
                throw new ArgumentException($"{nameof(TReturnType)}:Settings:{name} " +
                                            $"section missing from appSettings", name);

            return value;
        }

        public static PublicApiSetting GetSettingByName(this PublicApiSettings settings, string name)
        {
            return settings.GetSettingByName<PublicApiSetting>(name);
        }

        public static string ToBaseUri(this PublicApiSetting setting)
        {
            List<string> parts = new()
            {
                setting.BaseUrl.RemoveLeadingAndTrailing("/"),
                setting.Version.RemoveLeadingAndTrailing("/")
            };
            parts.RemoveAll(string.IsNullOrWhiteSpace);
            return string.Join("/", parts);
        }

        private static string RemoveLeadingAndTrailing(this string fromValue, string removeValue)
        {
            if (fromValue.EndsWith(removeValue)) fromValue = fromValue.Remove(fromValue.Length - 1, 1);
            if (fromValue.StartsWith(removeValue)) fromValue = fromValue[1..];
            return fromValue;
        }

        public static T GetConfigurationSection<T>(IConfiguration configuration)
        {
            string className = typeof(T).Name;
            IConfigurationSection configurationSection = configuration.GetSection(className);
            return configurationSection.Get<T>()!;
        }

        public static T GetRandomItem<T>(List<T> list, Random random)
        {
            int index = random.Next(list.Count);
            return list[index];
        }

        public static IEnumerable<string> GetInternalUrls()
        {
            return
            [
                "www.dexla.io",
                "www.dexla.ai",
                "dev-app.dexla.ai",
                "beta.dexla.ai",
                "app.dexla.ai",
            ];
        }
    }
}