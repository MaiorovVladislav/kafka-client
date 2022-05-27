using System.Text.Json.Serialization;

namespace KafkaClient
{
    /// <summary>
    /// CloudEvents Specification JSON Schema
    /// </summary>
    public class CloudEvent
    {
        /// <summary>
        /// The event payload.
        /// </summary>
        [JsonPropertyName("data")]
        public string? Data { get; set; }

        /// <summary>
        /// Content type of the data value. Must adhere to RFC 2046 format.
        /// </summary>
        [JsonPropertyName("data-content-type")]
        public string? DataContentType { get; set; }

        /// <summary>
        /// Identifies the schema that data adheres to.
        /// </summary>
        [JsonPropertyName("data-schema")]
        public string? DataSchema { get; set; }

        /// <summary>
        /// Identifies the event.
        /// </summary>
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        /// <summary>
        /// Identifies the context in which an event happened.
        /// </summary>
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        /// <summary>
        /// The version of the CloudEvents specification which the event uses.
        /// </summary>
        [JsonPropertyName("spec-version")]
        public string? SpecVersion { get; set; }

        /// <summary>
        /// Describes the subject of the event in the context of the event producer (identified by
        /// source).
        /// </summary>
        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        /// <summary>
        /// Timestamp of when the occurrence happened. Must adhere to RFC 3339.
        /// </summary>
        [JsonPropertyName("time")]
        public string? Time { get; set; }

        /// <summary>
        /// TracerId the event.
        /// </summary>
        [JsonPropertyName("traceparent")]
        public string? TraceParent { get; set; }

        /// <summary>
        /// List of key-value pairs
        /// </summary>
        [JsonPropertyName("tracestate")]
        public List<object>? TraceState { get; set; }

        /// <summary>
        /// Describes the type of event related to the originating occurrence.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
