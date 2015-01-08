#region Copyright
// <copyright file="Lua.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Lua scripting engine</summary>
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Sling.Scripting
{
    public class Lua : IDisposable, IScripting
    {
        #region Fields
        private IntPtr state;
        private ILua provider;
        private List<LuaMethod> methods;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the state pointer.
        /// </summary>
        /// <value>The state.</value>
        public IntPtr State {
            get {
                return state;
            }
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public ILua Provider {
            get {
                return provider;
            }
        }

        /// <summary>
        /// Gets a new instance of the table interface.
        /// </summary>
        /// <value>The table interface.</value>
        public IScriptingTable Table {
            get {
                return new LuaTable(this);
            }
        }

        /// <summary>
        /// Gets the stack.
        /// </summary>
        /// <value>The stack.</value>
        public object[] Stack {
            get {
                // get top
                int top = this.provider.gettop(this.state);

                // stack
                object[] stack = new object[top];

                // loop
                for (int i = 1; i <= top; i++) {  /* repeat for each level */
                    stack[i - 1] = Pop(i, false);
                }

                return stack;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Executes the chunk.
        /// </summary>
        /// <param name="chunk">The chunk.</param>
        public void Execute(string chunk) {
            // load string
            int resultLoad = this.provider.loadstring(this.state, chunk);

            // check
            if (resultLoad != 0) {
                // get error and pop
                string error = this.provider.tostring(this.state, -1);
                this.provider.pop(this.state, 1);

                // throw
                throw new Exception("A Lua error occured while loading a chunk: " + error);
            }

            // execute
            int resultCall = this.provider.pcallk(this.state, 0, 0, 0);

            if (resultCall != 0) {
                // get error and pop
                string error = this.provider.tostring(this.state, -1);
                this.provider.pop(this.state, 1);

                // throw
                throw new Exception("A Lua error occured while calling a chunk: " + error);
            }
        }

        /// <summary>
        /// Pops an object from the stack.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="pop">if set to <c>true</c> [remove item from stack].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException">No handler for Lua type  + type.ToString()</exception>
        internal object Pop(int index, bool pop=true) {
            // get type
            LuaType type = provider.type(state, index);

            // find type
            switch (type) {
                case LuaType.Number:
                    double d = provider.tonumberx(this.state, index, IntPtr.Zero);

                    // pop
                    if (pop)
                        provider.pop(this.state, 1);
                    return d;
                case LuaType.String:
                    string str = provider.tostring(this.state, index);

                    // pop
                    if (pop)
                        provider.pop(this.state, 1);
                    return str;
                case LuaType.Boolean:
                    bool boolean = provider.toboolean(this.state, index);

                    // pop
                    if (pop)
                        provider.pop(this.state, 1);
                    return boolean;
                case LuaType.Nil:
                    // pop
                    if (pop)
                        provider.pop(this.state, 1);
                    return null;
                case LuaType.Table:
                    return new LuaTable(this, false, pop);
                default:
                    throw new NotImplementedException("No handler for Lua type " + type.ToString());
            }
        }

        /// <summary>
        /// Pops an object from the stack and converts it to the specified object.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal T Pop<T>(int index) {
            return (T)Pop(index);
        }
        
        /// <summary>
        /// Pushes an object on to the stack.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="o">The object.</param>
        internal void Push(object o) {
            // find type
            if (o is double || o is int || o is float || o is short || o is long || o is uint || o is ushort || o is ulong || o is decimal)
                this.provider.pushnumber(this.state, (double)o);
            else if (o is string)
                this.provider.pushstring(this.state, (string)o);
            else if (o is Boolean)
                this.provider.pushboolean(this.state, (bool)o);
            else if (o == null)
                this.provider.pushnil(this.state);
            else if (o is LuaTable)
                ((LuaTable)o).Push();
            else if (o is LuaCMethod)
                this.provider.pushcclosure(this.state, (LuaCMethod)o, 0);
            else
                throw new NotImplementedException("No handler for .Net type " + o.GetType().ToString());
        }

        /// <summary>
        /// Opens optional libraries.
        /// </summary>
        /// <param name="library">The library.</param>
        public void OpenLibraries() {
            this.provider.openlibs(this.state);

            // remove unwanted libraries
            SetGlobal("debug", null);
            SetGlobal("os", null);
            SetGlobal("io", null);
            SetGlobal("debug", null);
            SetGlobal("package", null);
            SetGlobal("coroutine", null);
            SetGlobal("module", null);
        }

        /// <summary>
        /// Calls a global function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        public object[] CallGlobal(string name, params object[] arguments) {
            // get global
            this.provider.getglobal(this.state, name);

            // push arguments
            foreach (object arg in arguments)
                Push(arg);

            // call
            this.provider.pcallk(this.state, arguments.Length, -1, 0);

            // get result count
            int resultsCount = this.provider.gettop(this.state);

            if (resultsCount == 0)
                return new object[] { };

            // get results
            object[] results = new object[resultsCount];

            for (int i = 0; i < resultsCount; i++)
                results[i] = Pop(((-1) - (resultsCount - 1)) + i);

            return results;
        }

        /// <summary>
        /// Gets a global variable.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Value.</returns>
        public object GetGlobal(string name) {
            // get global
            this.provider.getglobal(this.state, name);

            // get value and pop
            object val = Pop(-1);
            this.provider.pop(this.state, 1);

            return val;
        }

        /// <summary>
        /// Gets the global and converts it to the specified object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public T GetGlobal<T>(string name) {
            return (T)GetGlobal(name);
        }

        /// <summary>
        /// Sets a global variable.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="val">The value.</param>
        public void SetGlobal(string name, object val) {
            // set value
            Push(val);

            // set global
            this.provider.setglobal(this.state, name);
        }

        /// <summary>
        /// Registers a method to a Lua function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="method">The method.</param>
        /// <param name="instance">The instance.</param>
        public void RegisterFunction(string name, MethodInfo method, object instance=null) {
            // create method
            LuaMethod methodLua = new LuaMethod(this, name, method, instance);

            // add
            methods.Add(methodLua);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            // close state
            provider.close(state);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Lua"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        public Lua(Engine engine) {
            this.provider = engine.Platform.CreateLua();
            this.state = provider.newstate();
            this.methods = new List<LuaMethod>();
        }
        #endregion
    }
}
