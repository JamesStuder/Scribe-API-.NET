using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scribe.Api.Library;
using Scribe.Api.Library.Settings;
using System;

namespace Scribe.Api.UnitTests.TDD
{
    [TestClass()]
    public class CommandsTests
    {
        #region Private Varibles
        private readonly int orgID = int.Parse(Environment.GetEnvironmentVariable("ScribeOrgSandbox", EnvironmentVariableTarget.User));

        private ScribeApiLibrary api = new ScribeApiLibrary
        {
            UserName = Environment.GetEnvironmentVariable("ScribeUser", EnvironmentVariableTarget.User),
            Password = Environment.GetEnvironmentVariable("ScribePassword", EnvironmentVariableTarget.User),
            BaseUrl = GeneralSettings.baseURLSandbox
        };
        #endregion

        [TestMethod()]
        public void Deprecated_Post_StartSolutionTest()
        {

        }

        [TestMethod()]
        public void Deprecated_Post_StopSolutionTest()
        {

        }
    }
}