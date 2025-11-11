using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models
{
    public abstract class MongoDocument
    {
        [BsonId]
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }
    }
}
