#region Copyright
// <copyright file="ILua.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Lua scripting interface</summary>
#endregion

using System;

namespace Sling.Scripting
{
    public interface ILua
    {
        IntPtr newstate();
        void close(IntPtr state);
        void error(IntPtr state, string message);

        void openlibs(IntPtr state);

        void pushnumber(IntPtr state, double number);
        void pushstring(IntPtr state, string str);
        void pushnil(IntPtr state);
        void pushboolean(IntPtr state, bool boolean);
        void pushvalue(IntPtr state, int index);
        void pushcclosure(IntPtr state, LuaCMethod func, int n);
        void pop(IntPtr state, int amount);

        void insert(IntPtr state, int newTop);
        void remove(IntPtr state, int index);
        void replace(IntPtr state, int index);
        void gettable(IntPtr state, int index);
        void settable(IntPtr state, int index);
        void rawget(IntPtr state, int index);
        void rawgeti(IntPtr state, int tableIndex, int index);
        void rawset(IntPtr state, int index);
        void rawseti(IntPtr state, int tableIndex, int index);
        void getmetatable(IntPtr state, string meta);
        void setmetatable(IntPtr state, int objIndex);
        int gettop(IntPtr state);
        void settop(IntPtr state, int newTop);
        void getfield(IntPtr state, int stackPos, string name);
        void setfield(IntPtr state, int stackPos, string name);
        void getglobal(IntPtr state, string name);
        void setglobal(IntPtr state, string name);

        LuaType type(IntPtr state, int index);
        int equal(IntPtr state, int index1, int index2);

        bool isnil(IntPtr state, int index);
        bool isnumber(IntPtr state, int index);
        bool isboolean(IntPtr state, int index);
        bool isstring(IntPtr state, int index);

        int callk(IntPtr state, int args, int results);
        int pcallk(IntPtr state, int args, int results, int errorFunction);

        double tonumberx(IntPtr state, int index, IntPtr isnum);
        bool toboolean(IntPtr state, int index);
        IntPtr tolstring(IntPtr state, int index, out int strLen);
        string tostring(IntPtr state, int index);

        int loadstring(IntPtr state, string chunk);

        int ref_(IntPtr state, int registryIndex);
        void getref(IntPtr state, int reference);
        void unref(IntPtr state, int reference);

        void createtable(IntPtr state, int narr, int nrec);
    }
}
