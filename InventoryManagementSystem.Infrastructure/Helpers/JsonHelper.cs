using InventoryManagementSystem.Infrastructure.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
                
namespace InventoryManagementSystem.Infrastucture.Helpers
{

    public static class JsonHelper
    {
        private static readonly JsonSerializerOptions _defaultOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            TypeInfoResolver = InventorySystemJsonContext.Instance
        };

        private static readonly JsonSerializerOptions _indentedOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            TypeInfoResolver = InventorySystemJsonContext.Instance
        };

        public static string Serialize<T>(T value, bool indented = false)
        {
            return JsonSerializer.Serialize(value, indented ? _indentedOptions : _defaultOptions);
        }

        public static T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _defaultOptions);
        }

        public static ValueTask<T?> DeserializeAsync<T>(Stream utf8Json, CancellationToken cancellationToken = default)
        {
            return JsonSerializer.DeserializeAsync<T>(utf8Json, _defaultOptions, cancellationToken);
        }
    }
}
