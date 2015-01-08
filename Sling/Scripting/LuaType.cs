#region Copyright
// <copyright file="LuaType.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Enumeration of lua types</summary>
#endregion

using System;

namespace Sling.Scripting
{
    public enum LuaType
    {
        None = -1,
        Nil = 0,
        Number = 3,
        String = 4,
        Boolean = 1,
        Table = 5,
        Function = 6,
        UserData = 7,
        LightUserData = 2
    }
}
