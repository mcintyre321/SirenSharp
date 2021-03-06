using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SirenSharp
{
    /// <summary>
    /// Represents the Siren entity wrapper.
    /// </summary>
    public class SubEntity
    {
        public SubEntity()
        {
            this.Class = null;
            this.Properties = new Dictionary<string, JToken>();
            this.Entities = null;
            this.Links = null;
            this.Actions = null;
        }

        public IEnumerable<string> Rel { get; set; }

        /// <summary>
        /// Gets or sets the representation. 
        /// </summary>
        /// <remarks>
        /// Describes the nature of an entity's content based on the current representation. 
        /// Possible values are implementation-dependent and should be documented. 
        /// MUST be an array of strings. Optional.
        /// </remarks>
        public IEnumerable<string> Class { get; set; }

        /// <summary>
        /// Gets or sets the list of properties.
        /// </summary>
        /// <remarks>
        /// A set of key-value pairs that describe the state of an entity. Optional.
        /// </remarks>
        public Dictionary<string, JToken> Properties { get; set; }

        /// <summary>
        /// Gets or sets related entities.
        /// </summary>
        /// <remarks>
        /// A collection of related sub-entities. 
        /// If a sub-entity contains an href value, it should be treated as an embedded link. 
        /// Clients may choose to optimistically load embedded links. If no href value exists, 
        /// the sub-entity is an embedded entity representation that contains all the 
        /// characteristics of a typical entity. One difference is that a sub-entity 
        /// MUST contain a rel attribute to describe its relationship to the parent entity.
        /// In JSON Siren, this is represented as an array. Optional.
        /// </remarks>
        public IEnumerable<OneOf.OneOf<SubEntity, SubEntityLink>> Entities { get; set; }

        /// <summary>
        /// Gets or sets a list of links associated with the entity.
        /// </summary>
        /// <remarks>
        /// A collection of items that describe navigational links, distinct from entity relationships. 
        /// Link items should contain a rel attribute to describe the relationship and an href attribute 
        /// to point to the target URI. Entities should include a link rel to self. In JSON Siren, this is 
        /// represented as "links": [{ "rel": ["self"], "href": "http://api.x.io/orders/1234" }] Optional.
        /// </remarks>
        public IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Gets or sets a list of actions associated with the entity.
        /// </summary>
        /// <remarks>
        /// A collection of action objects, represented in JSON Siren as an array 
        /// such as { "actions": [{ ... }] }. See Actions. Optional
        /// </remarks>
        public IEnumerable<Action> Actions { get; set; }


    }
}