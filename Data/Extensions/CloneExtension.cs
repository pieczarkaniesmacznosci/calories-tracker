using Newtonsoft.Json;

namespace Data.Extensions
{
    public static class CloneExtension
    {
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}

    