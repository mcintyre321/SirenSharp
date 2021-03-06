﻿using System.Linq;
using Newtonsoft.Json.Linq;
using OneOf;

namespace SirenSharp.Mvc
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class ApiContent<T> : HttpContent where T : IHypermediaEntity
    {
        private readonly byte[] content;
        private readonly int offset;
        private readonly int count;

        public ApiContent(T entity)
        {
            HypermediaEntity = new Entity
            {
                Actions = entity.GetSirenActions(),
                Class = entity.GetSirenClasses(),
                Entities = entity.GetSirenSubEntities().Cast<OneOf<SubEntity, SubEntityLink>>(),
                Links = entity.GetSirenLinks(),
                Properties = entity.GetType().GetProperties().ToDictionary(pi => pi.Name, pi => JToken.FromObject(pi.GetValue(this)))
            };

            string _jsonObject = JsonConvert.SerializeObject(
                        HypermediaEntity,
                        new JsonSerializerSettings
                        {
                            ContractResolver =
                                new Newtonsoft.Json.Serialization.
                                CamelCasePropertyNamesContractResolver(),
                            Formatting = Formatting.Indented,
                            NullValueHandling = NullValueHandling.Ignore
                        });

            this.content = Encoding.UTF8.GetBytes(_jsonObject);
            this.count = this.content.Length;
            this.offset = 0;

            this.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        public Entity HypermediaEntity { get; private set; }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return Task.Factory.FromAsync(stream.BeginWrite, stream.EndWrite, this.content, this.offset, this.count, null);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = this.count;
            return true;
        }
    }
}
