#region Copyright
// <copyright file="LuaIndex.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Enumeration of lua indexes</summary>
#endregion

using System;

namespace Sling.Scripting
{
    public enum LuaIndex : int
    {
        Registry = -1001000,
        Environment = -10001,
        Globals = -10002
    }
}
