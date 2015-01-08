#region Copyright
// <copyright file="IConsole.cs" company="Sling">
// Copyright (c) 2015 All Rights Reserved
// </copyright>
// <author>Alan Doherty</author>
// <summary>Debugging console interface</summary>
#endregion

using System;

namespace Sling
{
    public interface IConsole
    {
        void Write(string str);
        void WriteLine();
        void WriteLine(string str);
    }
}
