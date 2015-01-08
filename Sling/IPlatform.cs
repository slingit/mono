#region Copyright
// <copyright file="IPlatform.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Cross-platform interface</summary>
#endregion

using Sling.Networking;
using Sling.Scripting;
using System;

namespace Sling
{
    public interface IPlatform
    {
        string Name { get; }

        ILua CreateLua();
        ISocket CreateSocket();
        IConsole CreateConsole();
    }
}
