#region Copyright
// <copyright file="Lua.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Windows Lua interface/summary>
#endregion

using Sling.Scripting;
using System;
using System.Runtime.InteropServices;

namespace Sling.Windows
{
    public class Lua : ILua
    {
        #region Methods
        public IntPtr newstate() {
            return Lua.luaL_newstate();
        }

        public void close(IntPtr state) {
            Lua.lua_close(state);
        }

        public void error(IntPtr state, string message) {
            Lua.luaL_error(state, message);
        }

        public void openlibs(IntPtr state) {
            Lua.luaL_openlibs(state);
        }

        public void pushnumber(IntPtr state, double number) {
            Lua.lua_pushnumber(state, number);
        }

        public void pushstring(IntPtr state, string str) {
            Lua.lua_pushstring(state, str);
        }

        public void pushnil(IntPtr state) {
            Lua.lua_pushnil(state);
        }

        public void pushboolean(IntPtr state, bool boolean) {
            Lua.lua_pushboolean(state, boolean);
        }

        public void pushlstring(IntPtr state, string str, int size) {
            Lua.lua_pushlstring(state, str, size);
        }

        public void pushvalue(IntPtr state, int index) {
            Lua.lua_pushvalue(state, index);
        }

        public void pushcclosure(IntPtr state, LuaCMethod func, int n) {
            Lua.lua_pushcclosure(state, func, n);
        }

        public void pop(IntPtr state, int amount) {
            Lua.lua_settop(state, -(amount) - 1);
        }

        public void insert(IntPtr state, int newTop) {
            Lua.lua_insert(state, newTop);
        }

        public  void remove(IntPtr state, int index) {
            Lua.lua_remove(state, index);
        }

        public void replace(IntPtr state, int index) {
            Lua.lua_replace(state, index);
        }

        public void gettable(IntPtr state, int index) {
            Lua.lua_gettable(state, index);
        }

        public void settable(IntPtr state, int index) {
            Lua.lua_settable(state, index);
        }

        public void rawget(IntPtr state, int index) {
            Lua.lua_rawget(state, index);
        }

        public void rawgeti(IntPtr state, int tableIndex, int index) {
            Lua.lua_rawgeti(state, tableIndex, index);
        }

        public void rawset(IntPtr state, int index) {
            Lua.lua_rawset(state, index);
        }


        public void rawseti(IntPtr state, int tableIndex, int index) {
            Lua.lua_rawseti(state, tableIndex, index);
        }

        public void getmetatable(IntPtr state, string meta) {
            Lua.lua_getfield(state, (int)LuaIndex.Registry, meta);
        }

        public void setmetatable(IntPtr state, int objIndex) {
            Lua.lua_setmetatable(state, objIndex);
        }

        public int gettop(IntPtr state) {
            return Lua.lua_gettop(state);
        }

        public void settop(IntPtr state, int newTop) {
            Lua.lua_settop(state, newTop);
        }

        public void getfield(IntPtr state, int stackPos, string name) {
            Lua.lua_getfield(state, stackPos, name);
        }

        public void setfield(IntPtr state, int stackPos, string name) {
            Lua.lua_setfield(state, stackPos, name);
        }

        public void getglobal(IntPtr state, string name) {
            Lua.lua_getglobal(state, name);
        }

        public void setglobal(IntPtr state, string name) {
            Lua.lua_setglobal(state, name);
        }

        public LuaType type(IntPtr state, int index) {
            return Lua.lua_type(state, index);
        }

        public int equal(IntPtr state, int index1, int index2) {
            return Lua.lua_equal(state, index1, index2);
        }

        public bool isnil(IntPtr state, int index) {
            return (Lua.lua_type(state, index) == LuaType.Nil);
        }

        public bool isnumber(IntPtr state, int index) {
            return (Lua.lua_type(state, index) == LuaType.Number);
        }

        public bool isboolean(IntPtr state, int index) {
            return (Lua.lua_type(state, index) == LuaType.Boolean);
        }

        public bool isstring(IntPtr state, int index) {
            return (Lua.lua_type(state, index) == LuaType.String);
        }

        public int callk(IntPtr state, int args, int results) {
            return Lua.lua_callk(state, args, results);
        }

        public int pcallk(IntPtr state, int args, int results, int errorFunction) {
            return Lua.lua_pcallk(state, args, results, errorFunction);
        }

        public double tonumberx(IntPtr state, int index, IntPtr isnum) {
            return Lua.lua_tonumberx(state, index, isnum);
        }

        public bool toboolean(IntPtr state, int index) {
            return Lua.lua_toboolean(state, index);
        }

        public IntPtr tolstring(IntPtr state, int index, out int strLen) {
            return Lua.lua_tolstring(state, index, out strLen);
        }

        public string tostring(IntPtr state, int index) {
            int len;

            // get string
            IntPtr str = lua_tolstring(state, index, out len);

            // convert
            if (str != IntPtr.Zero)
                return Marshal.PtrToStringAnsi(str, len);
            else
                return null;
        }

        public int loadstring(IntPtr state, string chunk) {
            return Lua.luaL_loadstring(state, chunk);
        }

        public int ref_(IntPtr state, int registryIndex) {
            return Lua.luaL_ref(state, registryIndex);
        }

        public void getref(IntPtr state, int reference) {
            Lua.lua_rawgeti(state, (int)LuaIndex.Registry, reference);
        }

        public void unref(IntPtr state, int reference) {
            Lua.luaL_unref(state, (int)LuaIndex.Registry, reference);
        }

        public void createtable(IntPtr state, int numarr, int numrec) {
            Lua.lua_createtable(state, numarr, numrec);
        }
        #endregion

        #region Imports
        [DllImport("lua52.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern IntPtr luaL_newstate();

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_close(IntPtr state);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void luaL_error(IntPtr state, string message);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void luaL_openlibs(IntPtr state);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushnumber(IntPtr state, double number);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushnil(IntPtr state);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushboolean(IntPtr state, bool boolean);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushstring(IntPtr state, string str);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushlstring(IntPtr state, string str, int size);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushvalue(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_pushcclosure(IntPtr state, LuaCMethod func, int n);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_insert(IntPtr state, int newTop);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_remove(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_replace(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_gettable(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_settable(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_rawget(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_rawgeti(IntPtr state, int tableIndex, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_rawset(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_rawseti(IntPtr state, int tableIndex, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int lua_setmetatable(IntPtr state, int objIndex);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int lua_gettop(IntPtr state);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_settop(IntPtr state, int newTop);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_getfield(IntPtr state, int stackPos, string name);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_setfield(IntPtr state, int stackPos, string name);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_getglobal(IntPtr state, string name);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_setglobal(IntPtr state, string name);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern LuaType lua_type(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int lua_equal(IntPtr state, int index1, int index2);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int lua_callk(IntPtr state, int args, int results);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int lua_pcallk(IntPtr state, int args, int results, int errorFunction);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double lua_tonumberx(IntPtr state, int index, IntPtr isnum);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool lua_toboolean(IntPtr state, int index);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr lua_tolstring(IntPtr state, int index, out int strLen);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int luaL_loadstring(IntPtr state, string chunk);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int luaL_ref(IntPtr state, int registryIndex);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void luaL_unref(IntPtr state, int registryIndex, int reference);

        [DllImport("lua52.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void lua_createtable(IntPtr state, int narr, int nrec);
        #endregion
    }
}
