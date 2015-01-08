using System;

namespace Sling.Scripting
{
    public class LuaTable : IScriptingTable, IDisposable
    {
        #region Fields
        private int reference;

        private Lua lua;
        #endregion

        #region Methods
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            // unreference
            lua.Provider.unref(lua.State, this.reference);
        }

        /// <summary>
        /// Gets a field from the table.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Value.</returns>
        public object GetField(string name) {
            // get
            lua.Provider.rawgeti(lua.State, (int)LuaIndex.Registry, this.reference);

            // get field
            lua.Push(name);
            lua.Provider.gettable(lua.State, -2);
            return lua.Pop(-1);
        }

        /// <summary>
        /// Gets the field from the table and converts it to the specified type.
        /// </summary>
        /// <typeparam name="T">Type/typeparam>
        /// <param name="index">The index.</param>
        /// <returns>Value.</returns>
        public T GetField<T>(string index) {
            return (T)GetField(index);
        }

        /// <summary>
        /// Gets a field from the table.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Value.</returns>
        public object GetField(int index) {
            // get
            lua.Provider.rawgeti(lua.State, (int)LuaIndex.Registry, this.reference);

            // get field
            lua.Push(index);
            lua.Provider.gettable(lua.State, -2);
            return lua.Pop(-1);
        }

        /// <summary>
        /// Gets the field from the table and converts it to the specified type.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="index">The index.</param>
        /// <returns>Value.</returns>
        public T GetField<T>(int index) {
            return (T)GetField(index);
        }

        /// <summary>
        /// Sets a field in the table.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="o">The object.</param>
        public void SetField(string name, object o) {
            // get
            lua.Provider.rawgeti(lua.State, (int)LuaIndex.Registry, this.reference);

            // set field
            lua.Push(name);
            lua.Push(o);
            lua.Provider.settable(lua.State, -3);
        }

        /// <summary>
        /// Sets the field in the table.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="o">The object.</param>
        public void SetField(int index, object o) {
            // get
            lua.Provider.rawgeti(lua.State, (int)LuaIndex.Registry, this.reference);

            // set field
            lua.Push(index);
            lua.Push(o);
            lua.Provider.settable(lua.State, -3);
        }

        /// <summary>
        /// Pushes the table onto the stack.
        /// </summary>
        internal void Push() {
            // push
            lua.Provider.rawgeti(lua.State, (int)LuaIndex.Registry, this.reference);
        }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="LuaTable"/> class.
        /// </summary>
        /// <param name="lua">The lua.</param>
        /// <param name="create">if set to <c>true</c> [create].</param>
        /// <param name="pop">if set to <c>true</c> [remove from stack].</param>
        public LuaTable(Lua lua, bool create=false, bool pop=true) {
            // create table
            if (create)
                lua.Provider.createtable(lua.State, 0, 0);

            // reference
            this.reference = lua.Provider.ref_(lua.State, (int)LuaIndex.Registry);
            this.lua = lua;
        }
        #endregion

        #region Indexers        
        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> at the specified index.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="index">The index.</param>
        /// <returns>Value.</returns>
        public object this[string index] {
            get {
                return GetField(index);
            }
            set {
                SetField(index, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> at the specified index.
        /// </summary>
        /// <value>The <see cref="System.Object"/>.</value>
        /// <param name="index">The index.</param>
        /// <returns>Value.</returns>
        public object this[int index] {
            get {
                return GetField(index);
            }
            set {
                SetField(index, value);
            }
        }
        #endregion
    }
}
