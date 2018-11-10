using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scribe.Api.Library;
using Scribe.Api.Library.Settings;
using System;

namespace Scribe.Api.UnitTests.TDD
{
    [TestClass()]
    public class ConnectionsTests
    {
        #region Private Varibles
        private readonly int orgID = int.Parse(Environment.GetEnvironmentVariable("ScribeOrgSandbox", EnvironmentVariableTarget.User));

        private readonly ScribeApiLibrary api = new ScribeApiLibrary
        {
            UserName = Environment.GetEnvironmentVariable("ScribeUser", EnvironmentVariableTarget.User),
            Password = Environment.GetEnvironmentVariable("ScribePassword", EnvironmentVariableTarget.User),
            BaseUrl = GeneralSettings.baseURLSandbox
        };
        #endregion

        [TestMethod()]
        public void Get_ListOfSupportedConnectionsTest()
        {

        }

        [TestMethod()]
        public void Post_CreateConnectionTest()
        {

        }

        [TestMethod()]
        public void Delete_DeleteConnectionTest()
        {

        }

        [TestMethod()]
        public void Get_ReturnInfoAboutConnectionTest()
        {

        }

        [TestMethod()]
        public void Put_ModifyConnectionTest()
        {

        }

        [TestMethod()]
        public void Get_ListOfActionsTest()
        {

        }

        [TestMethod()]
        public void Get_ListOfEntitiesTest()
        {

        }

        [TestMethod()]
        public void Get_InfoAboutEntityTest()
        {

        }

        [TestMethod()]
        public void Get_ListOfFieldsTest()
        {

        }

        [TestMethod()]
        public void Get_ListOfRelationshipsTest()
        {

        }

        [TestMethod()]
        public void Get_ListOfEntityNamesTest()
        {

        }

        [TestMethod()]
        public void Post_RestMetadataTest()
        {

        }

        [TestMethod()]
        public void Get_SupportedActionsTest()
        {

        }

        [TestMethod()]
        public void Post_TestExistingConnectionTest()
        {

        }

        [TestMethod()]
        public void Post_TestConnectionPropertiesTest()
        {

        }

        [TestMethod()]
        public void Get_StatusOfConnectionTest()
        {

        }
    }
}