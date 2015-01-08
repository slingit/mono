#region Copyright
// <copyright file="Device.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Device model</summary>
#endregion

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Sling
{
    public class Device
    {
        #region Fields
        private Guid id = new Guid();
        private Guid secret = new Guid();

        private Engine engine = null;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid ID {
            get {
                return id;
            }
            set {
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public Guid Secret {
            get {
                return secret;
            }
            set {
                secret = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates this device on the server.
        /// </summary>
        public void Create() {
            // data
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["id"] = id.ToString();
            data["secret"] = secret.ToString();

            // request
            engine.Request(Method.Post, "/devices", "devices", data, new Action<bool>(delegate(bool success) {
                System.Diagnostics.Debug.WriteLine("test: " + success.ToString());
            }));
        }

        /// <summary>
        /// Serializes the device.
        /// </summary>
        /// <returns></returns>
        internal JObject Serialize() {
            // create
            JObject obj = new JObject();

            // dev
            obj["id"] = this.id.ToString();
            obj["secret"] = this.secret.ToString();

            return obj;
        }

        /// <summary>
        /// Unserializes the device.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <returns></returns>
        internal void Unserialize(JObject obj) {
            // device
            this.id = new Guid((string)obj["id"]);
            this.secret = new Guid((string)obj["secret"]);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class, generates data and attaches it to an engine.
        /// </summary>
        /// <param name="engine">The engine.</param>
        public Device(Engine engine) {
            this.engine = engine;
            this.id = Guid.NewGuid();
            this.secret = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class with data and attaches it to an engine.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="secret">The secret.</param>
        public Device(Engine engine, Guid id, Guid secret = new Guid()) {
            this.engine = engine;
            this.id = id;
            this.secret = secret;
        }
        #endregion
    }
}
