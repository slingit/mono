using System;
using System.Reflection;

namespace Sling.Scripting
{
    public interface IScripting
    {
        IntPtr State { get; }
        IScriptingTable Table { get; }
        object[] Stack { get; }

        void OpenLibraries();
        void Execute(string chunk);

        object[] CallGlobal(string name, params object[] arguments);
        object GetGlobal(string name);
        T GetGlobal<T>(string name);
        void SetGlobal(string name, object value);

        void RegisterFunction(string name, MethodInfo method, object instance);
    }
}
