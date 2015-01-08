using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sling.Scripting;
using Sling.Windows;
using System;

namespace Sling.Tests
{
    [TestClass]
    public class Scripting
    {
        #region Fields
        private IScripting scripting;
        #endregion

        #region Methods
        public void Setup() {
            Engine engine = new Engine(new Platform());
            this.scripting = engine.Scripting;
            engine = null;
        }
        #endregion

        #region Tests
        [TestMethod]
        public void Globals() {
            Setup();

            // set globals
            scripting.SetGlobal("varstr", "a string");
            scripting.SetGlobal("varbool", true);
            scripting.SetGlobal("varnum", 5.4);

            // check they exist
            Assert.AreEqual(scripting.GetGlobal("varstr"), "a string");
            Assert.AreEqual(scripting.GetGlobal("varbool"), true);
            Assert.AreEqual(scripting.GetGlobal("varnum"), 5.4);
            Assert.AreEqual(scripting.GetGlobal<bool>("varbool"), true);
            Assert.AreEqual(scripting.GetGlobal<string>("varstr"), "a string");
        }

        public void test() {
        }

        [TestMethod]
        public void Libraries() {
            Setup();

            // open libraries
            scripting.OpenLibraries();

            // check each library is available
            Assert.IsNotNull((LuaTable)scripting.GetGlobal("math"));
            Assert.IsNotNull((LuaTable)scripting.GetGlobal("string"));
            Assert.IsNotNull((LuaTable)scripting.GetGlobal("table"));
            Assert.IsNotNull((LuaTable)scripting.GetGlobal("bit32"));
        }
        #endregion
    }
}
