#region Copyright
// <copyright file="Message.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Message model</summary>
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Sling
{
    public class Message
    {
        #region Fields
        private static JsonSerializer serializer;

        private Guid id = new Guid();
        private string name = "";
        private string mediaType = "";
        private byte[] content = null;
        private string contentUrl = null;

        private Engine engine = null;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the message identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>The type of the media.</value>
        public string MediaType {
            get {
                return mediaType;
            }
            set {
                mediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public byte[] Content {
            get {
                return content;
            }
            set {
                content = value;
            }
        }

        /// <summary>
        /// Gets or sets the content URL.
        /// </summary>
        /// <value>The content URL.</value>
        public string ContentURL {
            get {
                return contentUrl;
            }
            set {
                contentUrl = value;
            }
        }
        #endregion

        #region Methods                
        /// <summary>
        /// Serializes the message.
        /// </summary>
        /// <returns></returns>
        internal JObject Serialize() {
            // create
            JObject obj = new JObject();

            // message
            obj["id"] = this.id.ToString();
            obj["name"] = this.name;
            obj["mediaType"] = this.mediaType;
            obj["content"] = this.content;
            obj["contentUrl"] = this.contentUrl;

            return obj;
        }

        /// <summary>
        /// Unserializes the message.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        internal void Unserialize(JObject obj) {
            // message
            this.id = new Guid((string)obj["id"]);
            this.name = (string)obj["name"];
            this.mediaType = (string)obj["media_type"];
            this.content = Utilities.Base64Decode((string)obj["content"]);
            this.contentUrl = (string)obj["contentUrl"];
        }
        #endregion

        #region Constructors           
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class and attaches it to an engine.
        /// </summary>
        public Message(Engine engine) {
            this.engine = engine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class with data and attaches it to an engine.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="content">The content.</param>
        /// <param name="contentUrl">The content URL.</param>
        public Message(Engine engine, Guid id, string name, string mediaType, byte[] content=null, string contentUrl = "") {
            this.engine = engine;
            this.id = id;
            this.name = name;
            this.mediaType = mediaType;
            this.content = content;
            this.contentUrl = contentUrl;
        }

        /// <summary>
        /// Initializes the <see cref="Message"/> class.
        /// </summary>
        static Message() {
            serializer = new JsonSerializer();
        }
        #endregion
    }
}
