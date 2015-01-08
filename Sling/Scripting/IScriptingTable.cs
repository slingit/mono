using System;

namespace Sling.Scripting
{
    public interface IScriptingTable
    {
        void SetField(string name, object o);
        object GetField(string name);
        object GetField(int index);

        object this[string index] { get; set; }
        object this[int index] { get; set; }
    }
}
