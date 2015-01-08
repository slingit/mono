using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Sling.Scripting
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LuaCMethod(IntPtr state);

    public class LuaMethod
    {
        #region Fields
        private MethodInfo method;
        private Lua lua;
        private object instance;
        #endregion

        #region Methods
        /// <summary>
        /// Handles function callbacks.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        private int FunctionCallback(IntPtr state) {
            // number of arguments
            int count = lua.Provider.gettop(lua.State);

            // arguments
            object[] arguments = new object[count];
            ParameterInfo[] parameters = method.GetParameters();
            object[] parameterObjects = new object[parameters.Length];

            // pop all arguments
            for (int i = 0; i < count; i++) {
                arguments[count - (i + 1)] = this.lua.Pop(count - i);
            }
            
            // check arguments
            for (int i = 0; i < parameters.Length; i++)
			{
                if (i >= arguments.Length && parameters[i].DefaultValue == null) {
                    // arguments missing that have no default value
                    throw new InvalidOperationException("Lua function call missing parameter " + parameters[i].Name);
                } else if (i >= arguments.Length) {
                    // argument missing but default value found
                    parameterObjects[i] = parameters[i].DefaultValue;
                    continue;
                }

                parameterObjects[i] = arguments[i];
			}
            
            // invoke
            object result = method.Invoke(instance, parameterObjects);

            // results
            List<object> results = new List<object>();

            // check type
            if (result == null)
                results.Add(null);
            else
                results.Add(result);

            // push
            foreach (object obj in results)
                this.lua.Push(obj);

            return results.Count;
        }
        #endregion

        #region Constructors                        
        /// <summary>
        /// Initializes a new instance of the <see cref="LuaMethod"/> class.
        /// </summary>
        /// <param name="lua">The lua.</param>
        /// <param name="name">The name.</param>
        /// <param name="method">The method.</param>
        /// <param name="instance">The instance.</param>
        public LuaMethod(Lua lua, string name, MethodInfo method, object instance=null) {
            this.lua = lua;
            this.method = method;
            this.instance = instance;

            // register
            this.lua.SetGlobal(name, new LuaCMethod(FunctionCallback));
        }
        #endregion
    }
}
