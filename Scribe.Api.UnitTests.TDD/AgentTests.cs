using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Scribe.Api.Library;
using Scribe.Api.Library.Models;
using Scribe.Api.Library.Settings;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Scribe.Api.UnitTests.TDD
{
    [TestClass]
    public class AgentTests
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
        public string Get_ListOfAgentsTest()
        {
            string response = api.Get_ListOfAgents(orgID, null, null, null, null);
            Assert.IsNotNull(response);
            return response;
        }

        [TestMethod()]
        public string Post_CaptureAgentLogsTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            string agentId = oAgent.Where(a => a.Name == Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User)).Select(a => a.Id).Single();

            string response = api.Post_CaptureAgentLogs(orgID, agentId);
            Assert.IsNotNull(response);
            return response;
        }

        [TestMethod()]
        public void Get_ReturnAgentLogsTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            string agentId = oAgent.Where(a => a.Name == Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User)).Select(a => a.Id).Single();

            string logData = JObject.Parse(Post_CaptureAgentLogsTest())["logId"].ToString();
            string response = api.Get_ReturnAgentLogs(orgID, agentId, logData);
            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void Post_RestartAnAgentTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            string agentId = oAgent.Where(a => a.Name == Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User)).Select(a => a.Id).Single();

            string response = api.Post_RestartAnAgent(orgID, agentId, true);
            Assert.IsNotNull(response);
            Assert.AreEqual("NO CONTENT", response);
        }

        [TestMethod()]
        public void Delete_DeleteAnAgentTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            string agentId = oAgent.Where(a => a.Name == "Cloud Agent").Select(a => a.Id).Single();
            string response = api.Delete_DeleteAnAgent(orgID, agentId);
            Assert.AreEqual("NO CONTENT", response);
        }

        [TestMethod()]
        public void Get_ReturnInfoAboutAgentTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            string agentId = oAgent.Where(a => a.Name == Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User)).Select(a => a.Id).Single();

            string response = api.Get_ReturnInfoAboutAgent(orgID, agentId);
            Assert.IsNotNull(response);
            AgentModel oReturnedAgent = JsonConvert.DeserializeObject<AgentModel>(response);
            Assert.AreEqual(oAgent[0].Id, oReturnedAgent.Id);
        }

        [TestMethod()]
        public void Put_ModifyAgentTest()
        {
            string agentResponse = Get_ListOfAgentsTest();
            List<AgentModel> oAgent = JsonConvert.DeserializeObject<List<AgentModel>>(agentResponse);
            AgentModel oOnPremAgent = oAgent.Where(a => a.Name == Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User)).Single();

            oOnPremAgent.Name = "OnPremise Agent";
            string response = api.Put_ModifyAgent(orgID, oOnPremAgent.Id, oOnPremAgent);
            Assert.IsNotNull(response);
            oOnPremAgent.Name = Environment.GetEnvironmentVariable("ScribeOnPremiseAgentName", EnvironmentVariableTarget.User);
            string reset = api.Put_ModifyAgent(orgID, oOnPremAgent.Id, oOnPremAgent);
            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void Post_ProvisionCloudAgentTest()
        {
            string response = api.Post_ProvisionCloudAgent(orgID);
            Assert.AreEqual("NO CONTENT", response);
        }

        [TestMethod()]
        public void Post_ReturnOn_PremiseInstallInfoTest()
        {
            string response = api.Post_ReturnOn_PremiseInstallInfo(orgID);
            Assert.IsNotNull(response);
        }
    }
}