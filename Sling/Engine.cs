#region Copyright
// <copyright file="Engine.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Engine model for API communication</summary>
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sling.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Sling
{
    public class Engine
    {
        #region Constants
        public const string ApiEndpoint = "http://slingit.herokuapp.com";
        #endregion

        #region Fields
        private IPlatform platform;
        private IConsole console;
        #endregion

        #region Properties        
        /// <summary>
        /// Gets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public IPlatform Platform {
            get {
                return platform;
            }
        }

        /// <summary>
        /// Gets a shared instance of the console interface.
        /// </summary>
        /// <value>The console interface.</value>
        public IConsole Console {
            get {
                return console;
            }
        }

        /// <summary>
        /// Gets a new instance of the scripting interface.
        /// </summary>
        /// <value>The scripting interface.</value>
        public IScripting Scripting {
            get {
                return new Lua(this);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Requests the specified request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="url">The request url.</param>
        /// <param name="name">The name.</param>
        /// <param name="data">The data.</param>
        /// <param name="callback">The callback.</param>
        public void Request(Method method, string url, string name, Dictionary<string, string> data, Action<bool> callback) {
            // concat path
            string path = ApiEndpoint + url;

            // build json data
            JObject jsonData = new JObject();

            foreach (KeyValuePair<string, string> kv in data)
                jsonData[kv.Key] = kv.Value;

            // build json
            JObject json = new JObject();
            json[name] = jsonData;

            string dataStr = json.ToString();

            // create
            HttpWebRequest req = HttpWebRequest.CreateHttp(path);
            req.Method = method.ToString().ToUpper();
            req.ContentType = "application/json";
            req.Headers["X-API-Version"] = "1.0.0";

            // get stream
            req.BeginGetRequestStream(new AsyncCallback(delegate(IAsyncResult res) {
                // get stream
                Stream stream = req.EndGetRequestStream(res);
                
                // get writer
                StreamWriter writer = new StreamWriter(stream);

                // auto flush
                writer.AutoFlush = true;

                // write json
                writer.Write(json.ToString(Formatting.None));

                // close writer
                writer.Dispose();

                // get response
                req.BeginGetResponse(new AsyncCallback(delegate(IAsyncResult res2) {
                    // get response
                    HttpWebResponse response = null;

                    try {
                        response = (HttpWebResponse)req.EndGetResponse(res2);
                    } catch (Exception ex) {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string read = reader.ReadToEnd();

                    response.Dispose();
                }), null);
            }), null);
        }

        /// <summary>
        /// Create a new Lua state through the current platform.
        /// </summary>
        /// <returns>Lua state.</returns>
        public Lua CreateLua() {
            return new Lua(this);
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        /// <param name="platform">The platform interface.</param>
        public Engine(IPlatform platform) {
            this.platform = platform;

            // initialize console
            this.console = platform.CreateConsole();
        }
        #endregion
    }

    public enum Method
    {
        Post,
        Get,
        Put,
        Delete
    }
}