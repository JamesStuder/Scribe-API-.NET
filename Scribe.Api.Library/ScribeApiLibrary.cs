/*******************************************************************************************************************
 * Method Setup:                                                                                                   *
 * 1) Validate inputs (not done on int, since a number has to be put in for an inut unless allowed to be nullable. *
 * 2) Populate path dictionary.  This is used to place the items in the path of the URL, in the correct order.     *
 * 3) Populate query dictionary.  This is used to place the items in the query part of the url, after the ?.       *
 * 4) Build the URL that will be used to make the call.                                                            *
 * 5) Make the web request.                                                                                        *
 * 6) Return the string response (JSON or Non-JSON).                                                                *
 *******************************************************************************************************************/
using System;
using System.Collections.Generic;
using Scribe.Api.Library.Models;
using Scribe.Api.Library.Services;
using Scribe.Api.Library.Settings;

namespace Scribe.Api.Library
{
    public class ScribeApiLibrary
    {
        #region Private variables
        //Service used to dynamically build url
        private UrlBuilderService _urlBuilderService = new UrlBuilderService();
        //Service used to make the web call
        private WebServiceDAO _WebServiceDAO = new WebServiceDAO();
        //Service to parse model to json
        private JsonService _jsonService = new JsonService();
        //Dictionary to store value that will be placed in the query part of the url. (After "?")
        private Dictionary<string, object> _oQuery = new Dictionary<string, object>();
        //Dictionary to store values that will be placed in the path part of the url. (Before "?")
        private Dictionary<int, string> _oPath = new Dictionary<int, string>();
        //Reusable varible for storing the returned url that was built.
        private string _url = "";
        #endregion

        #region Public variables
        //This region contains the information that the end user needs to create.  This way the web requests can be made.
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseUrl { get; set; }
        #endregion

        #region Agents
        /// <summary>
        /// Return a list of Agents
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectorId">ID of a Connector installed in an Agent.</param>
        /// <param name="name">Name of an Agent.</param>
        /// <param name="offset">The number of Agents to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Agents. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfAgents(int orgId, string connectorId, string name, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (connectorId != null && connectorId != string.Empty) { _oQuery.Add(nameof(connectorId), connectorId); }
            if (name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.GET_Agents_GetAgents, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Capture Agent Logs
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="agentId">ID of the Agent.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_CaptureAgentLogs(int orgId, string agentId)
        {
            _oQuery = new Dictionary<string, object>();
            if (agentId == null || agentId == string.Empty) throw new NullReferenceException("Agent ID can't be null.", new Exception("Post_CaptureAgentLogs"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.POST_Agents_CaptureAgentLogs, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return Agent Logs
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="agentId">ID of the Agent.</param>
        /// <param name="logId">ID of the log request.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ReturnAgentLogs(int orgId, string agentId, string logId)
        {
            _oQuery = new Dictionary<string, object>();
            if (agentId == null || agentId == string.Empty) throw new NullReferenceException("Agent ID can't be null.", new Exception("Get_ReturnAgentLogs"));
            if (logId == null || logId == string.Empty) throw new NullReferenceException("Log ID can't be null.", new Exception("Get_ReturnAgentLogs"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                {2, agentId },
                {3, logId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.GET_Agents_GetAgentLogs, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Restart an Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="agentId">ID of the Agent.</param>
        /// <param name="restartNow">Set to true to restart now, interrupting running Solutions or false to wait until Solutions finish. Default is false.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_RestartAnAgent(int orgId, string agentId, bool? restartNow)
        {
            _oQuery = new Dictionary<string, object>();
            if (agentId == null || agentId == string.Empty) throw new NullReferenceException("Agent ID can't be null.", new Exception("Post_RestartAnAgent"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, agentId.ToString() }
            };

            if (restartNow != null) { _oQuery.Add(nameof(restartNow), restartNow); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.POST_Agents_RestartAgent, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Delete an Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="id">ID of the Agent.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_DeleteAnAgent(int orgId, string id)
        {
            _oQuery = new Dictionary<string, object>();
            if (id == null || id == string.Empty) throw new NullReferenceException("Agent ID can't be null.", new Exception("Delete_DeleteAnAgent"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (id != null && id != string.Empty) { _oQuery.Add(nameof(id), id); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.DELETE_Agents_DeleteAgent, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about an Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="id">ID of the Agent.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnInfoAboutAgent(int orgId, string id)
        {
            _oQuery = new Dictionary<string, object>();
            if (id == null || id == string.Empty) throw new NullReferenceException("ID can't be null.", new Exception("Get_ReturnInfoAboutAgent"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                {2, id }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.GET_Agents_GetAgent, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify an Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="id">ID of the Agent.</param>
        /// <param name="model">Model for the Agent. Required: Name</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyAgent(int orgId, string id, AgentModel model)
        {
            _oQuery = new Dictionary<string, object>();
            if (id == null || id == string.Empty) throw new NullReferenceException("ID can't be null.", new Exception("Put_ModifyAgent"));
            if (model.Name == null || model.Name == string.Empty) throw new NullReferenceException("Name can't be null.", new Exception("Put_ModifyAgent, AgentModel Name Property"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (id != null && id != string.Empty) { _oQuery.Add(nameof(id), id); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.PUT_Agents_ModifyAgent, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Provision a Cloud Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ProvisionCloudAgent(int orgId)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.POST_Agents_ProvisionCloudAgent, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return installation information for an On-Premise Agent
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>JSON String</returns>
        public string Post_ReturnOn_PremiseInstallInfo(int orgId)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlAgentsSettings.POST_Agents_ProvisionOnPremiseAgent, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }
        #endregion

        #region Commands

        /// <summary>
        /// Start a Solution (deprecated) - This command has been deprecated. Use the Solutions POST start command, instead.
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Deprecated_Post_StartSolution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("ID can't be null.", new Exception("Put_ModifyAgent"));
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlCommandsSettings.POST_Commands_Start, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Stop a Solution (deprecated) - This command has been deprecated. Use the Solutions POST stop command, instead.
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Deprecated_Post_StopSolution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("ID can't be null.", new Exception("Put_ModifyAgent"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlCommandsSettings.POST_Commands_Stop, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }
        #endregion

        #region Connections
        /// <summary>
        /// Return a list of supported Connections
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="name">Name of the Connection.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <param name="expand">Setting to return Connection properties. Default value is true.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfSupportedConnections(int orgId, string name, int? offset, int? limit, bool? expand, string agentId)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()}
            };

            if (name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }
            if (expand != null) { _oQuery.Add(nameof(expand), expand); }
            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetConnections, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="model">Model for the Connection. Required: Name, ConnectorId, and Color (in hexadecimal ARGB format)</param>
        /// <returns>JSON String</returns>
        public string Post_CreateConnection(int orgId, ConnectionModel model)
        {
            if (model.Name == null || model.Name == string.Empty) throw new NullReferenceException("Name can't be null.", new Exception("Post_CreateConnection, ConnectionModel Name Property"));
            if (model.ConnectorId == null || model.ConnectorId == string.Empty) throw new NullReferenceException("ConnectorId can't be null.", new Exception("Post_CreateConnection, ConnectionModel ConnectorId Property"));
            if (model.Color == null || model.Color == string.Empty) throw new NullReferenceException("Color can't be null.", new Exception("Post_CreateConnection, ConnectionModel Color Property"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.POST_Connections_CreateConnection, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_DeleteConnection(int orgId, string connectionId)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Delete_DeleteConnection"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.DELETE_Connections_DeleteConnection, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnInfoAboutConnection(int orgId, string connectionId)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ReturnInfoAboutConnection"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetConnection, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="model">Model for the Connection.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyConnection(int orgId, string connectionId, ConnectionModel model)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Put_ModifyConnection"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.PUT_Connections_ModifyConnection, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return a list of Actions for a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfActions(int orgId, string connectionId, string agentId, int? offset, int? limit)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ListOfActions"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetConnectionActions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Entities for a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="expand">eturn the Actions with the Entity. Default is false.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfEntities(int orgId, string connectionId, string agentId, bool? expand, int? offset, int? limit)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ListOfEntities"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (expand != null) { _oQuery.Add(nameof(expand), expand); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.PUT_Connections_ModifyConnection, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about an Entity
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="entityIdOrName">ID or name of the Entity.</param>
        /// <param name="expand">eturn the Actions with the Entity. Default is false.</param>
        /// <returns>JSON String</returns>
        public string Get_InfoAboutEntity(int orgId, string connectionId, string entityIdOrName, bool? expand)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_InfoAboutEntity"));
            if (entityIdOrName == null || entityIdOrName == string.Empty) throw new NullReferenceException("Entity Id or Name can't be null.", new Exception("Get_InfoAboutEntity"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                 {2, connectionId},
                {3, entityIdOrName}
            };

            if (expand != null) { _oQuery.Add(nameof(expand), expand); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetEntity, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Fields for an Entity
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="entityIdOrName">ID or name of the Entity.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfFields(int orgId, string connectionId, string entityIdOrName, string agentId, int? offset, int? limit)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ListOfEntities"));
            if (entityIdOrName == null || entityIdOrName == string.Empty) throw new NullReferenceException("Entity Id or Name can't be null.", new Exception("Get_InfoAboutEntity"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId},
                {3, entityIdOrName}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetEntityFields, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Relationships for an Entity
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="entityIdOrName">ID or name of the Entity.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfRelationships(int orgId, string connectionId, string entityIdOrName, string agentId, int? offset, int? limit)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ListOfEntities"));
            if (entityIdOrName == null || entityIdOrName == string.Empty) throw new NullReferenceException("Entity Id or Name can't be null.", new Exception("Get_InfoAboutEntity"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId},
                {3, entityIdOrName}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetEntityRelationships, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Entity names for a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="offset">Number of Connections to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connections. Default value is 100.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ListOfEntityNames(int orgId, string connectionId, string agentId, int? offset, int? limit)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_ListOfEntities"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetEntityNamesForConnection, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Reset the metadata for a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_RestMetadata(int orgId, string connectionId)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Post_RestMetadata"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.POST_Connections_ResetMetadata, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return a list of supported Actions for a Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="mapType">Map type.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <returns>JSON String</returns>
        public string Get_SupportedActions(int orgId, string connectionId, string mapType, string agentId)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Get_SupportedActions"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            if (mapType != null && mapType != string.Empty) { _oQuery.Add(nameof(mapType), mapType); }
            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.GET_Connections_GetConnectionActions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Test an existing Connection
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="connectionId">ID of the Connection.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <returns>JSON String</returns>
        public string Post_TestExistingConnection(int orgId, string connectionId, string agentId)
        {
            if (connectionId == null || connectionId == string.Empty) throw new NullReferenceException("ConnectionId can't be null.", new Exception("Post_TestExistingConnection"));
            if (agentId == null || agentId == string.Empty) throw new NullReferenceException("Agent Id can't be null.", new Exception("Post_TestExistingConnection"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectionId}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.POST_Connection_Test, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Test Connection properties
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="agentId">Filter for Connections supported by this Agent.</param>
        /// <param name="model">Model of the unsaved Connection being tested. Id is not required.</param>
        /// <returns>SJON String</returns>
        public string Post_TestConnectionProperties(int orgId, string agentId, ConnectionModel model)
        {
            if (agentId == null || agentId == string.Empty) throw new NullReferenceException("Agent Id can't be null.", new Exception("Post_TestConnectionProperties"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.POST_Connections_Test, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return the status of a Connection test command
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="commandId">ID of the command returned by a Connection's POST Test call.</param>
        /// <returns>JSON String</returns>
        public string Get_StatusOfConnection(int orgId, string commandId)
        {
            if (commandId == null || commandId == string.Empty) throw new NullReferenceException("Command Id can't be null.", new Exception("Get_StatusOfConnection"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, commandId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectionsSettings.POST_Connections_Test, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Connectors
        /// <summary>
        /// Return a list of connectors
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="name">Name of the Connector.</param>
        /// <param name="offset">Number of Connectors to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Connectors. Default value is 100.</param>
        /// <param name="AgentId">	Filter for installed Connectors supported by this Agent.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfConnectors(int orgId, string name, int? offset, int? limit, string AgentId)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }
            if (AgentId != null && AgentId != string.Empty) { _oQuery.Add(nameof(AgentId), AgentId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.GET_Connectors_GetByOrg, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Setup call for a Connection to be created
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="connectorId">ID of the Connection. - Required</param>
        /// <param name="properties">Model for the properties the Connector uses for preconnect.</param>
        /// <param name="agentId">ID of the Agent to use.</param>
        /// <param name="isOauth">For hybrid UI, change to support OAuth.</param>
        /// <returns>JSON String</returns>
        public string Post_SetupCallToCreateConnection(int orgId, string connectorId, PropertiesModel properties, string agentId, bool? isOauth)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Post_SetupCallToCreateConnection"));
            if (properties == null) throw new NullReferenceException("Properties can't be null.", new Exception("Post_SetupCallToCreateConnection"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                {1, orgId.ToString()},
                {2, connectorId}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (isOauth != null) { _oQuery.Add(nameof(isOauth), isOauth); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.POST_Connectors_Preconnect, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(properties));
        }

        /// <summary>
        /// Return the status of a preconnect command
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="connectorId">ID of the Connection. - Required</param>
        /// <param name="commandId">ID of the command returned by a Connection's POST Test call.</param>
        /// <returns>JSON String</returns>
        public string Get_StatusOfPreconnectCommand(int orgId, string connectorId, string commandId)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Get_StatusOfPreconnectCommand"));
            if (commandId == null || commandId == string.Empty) throw new NullReferenceException("Command Id can't be null.", new Exception("Get_StatusOfPreconnectCommand"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, connectorId },
                { 3, commandId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.GET_Connectors_GetPreconnectResults, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return version information for a Connector.
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="connectorId">ID of the Connection. - Required</param>
        /// <returns>JSON String</returns>
        public string Get_ConnectorVersion(int orgId, string connectorId)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Get_ConnectorVersion"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, connectorId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.GET_Connectors_GetConnectorVersion, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify Connector version Locked/Unlocked state
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="connectorId">ID of the Connection. - Required</param>
        /// <param name="updateVersion">Version of the connector to lock/unlock. - Required</param>
        /// <param name="isLocked">Set to true to lock or false to unlock.	</param>
        /// <returns>Non-JSON String</returns>
        public string Put_ModifyConnectorLockState(int orgId, string connectorId, string updateVersion, bool? isLocked)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Put_ModifyConnectorLockState"));
            if (updateVersion == null || updateVersion == string.Empty) throw new NullReferenceException("Update Version can't be null.", new Exception("Put_ModifyConnectorLockState"));
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                {1, orgId.ToString()},
                {2, connectorId}
            };

            if (updateVersion != null && updateVersion != string.Empty) { _oQuery.Add(nameof(updateVersion), updateVersion); }
            if (isLocked != null) { _oQuery.Add(nameof(isLocked), isLocked); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.POST_Connectors_Preconnect, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Return information about a Connector
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="id">ID of the Connector. - Required</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutConnector(int orgId, string id)
        {
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Get_InformationAboutConnector"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, id }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.GET_Connectors_InstGetById, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Uninstall a Marketplace Connector from all Agents
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="id">ID of the Connector. - Required</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_UninstallConnector(int orgId, string id)
        {
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Delete_UninstallConnector"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, id }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.DELETE_Connectors_UninstallConnector, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Install a Connector to all Agents
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="id">ID of the Connector. - Required</param>
        /// <returns>Non-JSON String</returns>
        public string Post_InstallConnector(int orgId, string id)
        {
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Post_InstallConnector"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, id }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.POST_Connectors_InstallConnector, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return UI options for a Connector
        /// </summary>
        /// <param name="orgId">ID of the Organization. - Required</param>
        /// <param name="id">ID of the Connector. - Required</param>
        /// <param name="agentId">Return the UI from this Agent.</param>
        /// <param name="version">Version of the Connector to return, if not the latest.</param>
        /// <returns>JSON String</returns>
        public string Get_UIOptionsForConnector(int orgId, string id, string agentId, string version)
        {
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Get_UIOptionsForConnector"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                {1, orgId.ToString()},
                {2, id}
            };

            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (version != null && version != string.Empty) { _oQuery.Add(nameof(version), version); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlConnectorsSettings.GET_Connectors_getConnectoruioptions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Customers
        /// <summary>
        /// Return a list of Customer Organizations with access to a Managed Connector
        /// </summary>
        /// <param name="orgId">ID of the Managing Organization.</param>
        /// <param name="connectorId">ID of the Managed Connector.</param>
        /// <param name="offset">Number of Customer Organizations to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Customer Organizations. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfCustomers(int orgId, string connectorId, int? offset, int? limit)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Get_ListOfCustomers"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectorId}
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlCustomersSettings.GET_Customers_GetConnectorCustomers, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Delete a Customer Organization's access to a Managed Connector
        /// </summary>
        /// <param name="orgId">ID of the Managing Organization.</param>
        /// <param name="connectorId">ID of the Managed Connector.</param>
        /// <param name="id">ID of the Customer Organization.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_CustomerAccess(int orgId, string connectorId, int id)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Delete_CustomerAccess"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectorId},
                {3, id.ToString()}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlCustomersSettings.DELETE_Customers_RemoveCustomer, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Grant a Customer Organization access to a Managed Connector
        /// </summary>
        /// <param name="orgId">ID of the Managing Organization.</param>
        /// <param name="connectorId">ID of the Managed Connector.</param>
        /// <param name="id">ID of the Customer Organization.</param>
        /// <returns></returns>
        public string Post_GrantCustomerAccess(int orgId, string connectorId, int id)
        {
            if (connectorId == null || connectorId == string.Empty) throw new NullReferenceException("Connector Id can't be null.", new Exception("Post_GrantCustomerAccess"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, connectorId},
                {3, id.ToString()}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlCustomersSettings.DELETE_Customers_RemoveCustomer, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }
        #endregion

        #region Errors
        /// <summary>
        /// Return a list of Errors from a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="historyId">ID of the Solution execution.</param>
        /// <param name="offset">Number of Errors to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Errors. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfErrors(int orgId, string solutionId, int historyId, int? offset, int? limit)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_ListOfErrors"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, historyId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlErrorsSettings.GET_Errors_GetErrors, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about an Error from a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="historyId">ID of the Solution execution.</param>
        /// <param name="id">ID of the Error.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutAnError(int orgId, string solutionId, string historyId, string id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_InformationAboutAnError"));
            if (historyId == null || historyId == string.Empty) throw new NullReferenceException("History Id can't be null.", new Exception("Get_InformationAboutAnError"));
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Get_InformationAboutAnError"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, historyId.ToString() },
                { 4, id }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlErrorsSettings.GET_Errors_GetErrors, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about an Error from a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="historyId">ID of the Solution execution.</param>
        /// <param name="id">ID of the Error.</param>
        /// <param name="mark">Set or clear the reprocess flag for failed records. Default is true.</param>
        /// <returns>Nono JSON String</returns>
        public string Post_MarkErrorForReprocessing(int orgId, string solutionId, int historyId, string id, bool? mark)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Post_MarkErrorForReprocessing"));
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Post_MarkErrorForReprocessing"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, historyId.ToString() },
                { 4, id }
            };

            if (mark != null) { _oQuery.Add(nameof(mark), mark); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlErrorsSettings.POST_Errors_MarkForReprocess, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }
        #endregion

        #region Failed Records
        /// <summary>
        /// Return information about the failed source record for an Error from a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="historyId">ID of the Solution execution.</param>
        /// <param name="id">ID of the Error.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_InformationAboutFailedRecord(int orgId, string solutionId, int historyId, string id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_InformationAboutFailedRecord"));
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Get_InformationAboutFailedRecord"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, historyId.ToString()},
                {4, id}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlFailedRecordsSettings.GET_FailedRecords_GetFailedRecords, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region History
        /// <summary>
        /// Return a list of Solution executions
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="laterThanDate">Filter for Solution executions for this date and later.</param>
        /// <param name="result">Filter based on the result of the execution.	</param>
        /// <param name="executionHistoryColumnSort">Sort returned data set by selected item.	</param>
        /// <param name="sortOrder">Sort ascending or descending. Default is ascending.</param>
        /// <param name="offset">Number of Solution executions to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Solution executions. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_SoltuionExecutions(int orgId, string solutionId, string laterThanDate, string result, string executionHistoryColumnSort, string sortOrder, int? offset, int? limit)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_SoltuionExecutions"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId }
            };

            if (laterThanDate != null && laterThanDate != string.Empty) { _oQuery.Add(nameof(laterThanDate), laterThanDate); }
            if (result != null && result != string.Empty) { _oQuery.Add(nameof(result), result); }
            if (executionHistoryColumnSort != null && executionHistoryColumnSort != string.Empty) { _oQuery.Add(nameof(executionHistoryColumnSort), executionHistoryColumnSort); }
            if (sortOrder != null && sortOrder != string.Empty) { _oQuery.Add(nameof(sortOrder), sortOrder); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.GET_History_GetHistorys, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutSolutionExecution(int orgId, string solutionId, int id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_InformationAboutSolutionExecution"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, id.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.GET_History_GetHistory, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Export Execution History for a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ExportExecutionHistory(int orgId, string solutionId, int id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Post_ExportExecutionHistory"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, id.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.POST_History_ExportHistory, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return exported Execution History
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <param name="exportId">ID of the export request.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ExportedExecutionHistory(int orgId, string solutionId, int id, int exportId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Get_ExportedExecutionHistory"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                { 3, id.ToString() },
                { 4, exportId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.GET_History_RetrieveExportedHistory, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Mark all Errors from a Solution execution for reprocessing
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <param name="mark">Set or clear the reprocess flag for failed records. Default is true.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_MarkAllErrorsForReprocessing(int orgId, string solutionId, int id, bool? mark)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Post_MarkAllErrorsForReprocessing"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                {3, id.ToString() }
            };

            if (mark != null) { _oQuery.Add(nameof(mark), mark); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.POST_History_MarkForReprocess, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Reprocess all marked Errors from a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ReprocessAllMarkedErrors(int orgId, string solutionId, int id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Post_ReprocessAllMarkedErrors"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                {3, id.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.POST_History_ReprocessHistory, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return statistics for a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="id">ID of the Solution execution.</param>
        /// <returns>JSON String</returns>
        public string Get_SolutionExecutionStatistics(int orgId, string solutionId, int id)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("Solution Id can't be null.", new Exception("Post_ReprocessAllMarkedErrors"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, solutionId },
                {3, id.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlHistorySettings.GET_History_GetHistoryStatistics, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Invited Users
        /// <summary>
        /// Delete an invited User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="email">Email of the invited User.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_InvitedUser(int orgId, string email)
        {
            if (email == null || email == string.Empty) throw new NullReferenceException("Email can't be null.", new Exception("Delete_InvitedUser"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
            };

            if (email != null && email != string.Empty) { _oQuery.Add(nameof(email), email); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlInvitedUsersSettings.DELETE_InvitedUsers_Delete, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return a list of invited Users
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfInvitedUsers(int orgId)
        {
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlInvitedUsersSettings.DELETE_InvitedUsers_Delete, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Invite a User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="model">Model for the invited User. Required: Email, Status, and Role</param>
        /// <returns>JSON String</returns>
        public string Post_InviteAUser(int orgId, InvitedUsersModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null.", new Exception("Post_InviteAUser"));
            if (model.Email == null || model.Email == string.Empty) throw new NullReferenceException("Email in model can't be null.", new Exception("Post_InviteAUser"));
            if (model.StatusType == null || model.StatusType == string.Empty) throw new NullReferenceException("StatusType in model can't be null.", new Exception("Post_InviteAUser"));
            if (model.RoleType == null || model.RoleType == string.Empty) throw new NullReferenceException("RoleType in model can't be null.", new Exception("Post_InviteAUser"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlInvitedUsersSettings.DELETE_InvitedUsers_Delete, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return information about an invited User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="email">Email of the invited User.</param>
        /// <returns>JSON String</returns>
        public string Get_InfoAboutInvitedUser(int orgId, string email)
        {
            if (email == null || email == string.Empty) throw new NullReferenceException("email can't be null.", new Exception("Get_InfoAboutInvitedUser"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                {2, email }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlInvitedUsersSettings.GET_InvitedUsers_Get, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Modify an invited User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="email">Email of the invited User.</param>
        /// <param name="model">Model for the invited User. Required: Email, Status, and Role</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyInvitedUser(int orgId, string email, InvitedUsersModel model)
        {
            if (email == null || email == string.Empty) throw new NullReferenceException("Email can't be null.", new Exception("Put_ModifyInvitedUser"));
            if (model == null) throw new NullReferenceException("model can't be null.", new Exception("Put_ModifyInvitedUser"));
            if (model.Email == null || model.Email == string.Empty) throw new NullReferenceException("Email in model can't be null.", new Exception("Put_ModifyInvitedUser"));
            if (model.StatusType == null || model.StatusType == string.Empty) throw new NullReferenceException("StatusType in model can't be null.", new Exception("Put_ModifyInvitedUser"));
            if (model.RoleType == null || model.RoleType == string.Empty) throw new NullReferenceException("RoleType in model can't be null.", new Exception("Put_ModifyInvitedUser"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                {2, email }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlInvitedUsersSettings.GET_InvitedUsers_Get, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }
        #endregion

        #region Lookup Tables
        /// <summary>
        /// Return a list of Lookup Tables
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="includeLookupTableValues">	Set to true to return Lookup Table Values. Default value is false.</param>
        /// <param name="offset">Number of Lookup Tables to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Lookup Tables. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListofLookupTables(int orgId, bool? includeLookupTableValues, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() }
            };

            if (includeLookupTableValues != null) { _oQuery.Add(nameof(includeLookupTableValues), includeLookupTableValues); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTables, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization</param>
        /// <param name="model">Model for the Lookup Table.</param>
        /// <returns>JSON String</returns>
        public string Post_CreateLookupTable(int orgId, LookupTablesModel model)
        {
            if (model == null) throw new NullReferenceException("Model can't be null", new Exception("Post_CreateLookupTable"));
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTables, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_LookupTable(int orgId, string tableId)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Delete_LookupTable"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTable, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="includeLookupTableValues">Set to true to return Lookup Table Values. Default value is false.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_InformationAboutLookupTable(int orgId, string tableId, bool? includeLookupTableValues)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Get_InformationAboutLookupTable"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId }
            };

            if (includeLookupTableValues != null) { _oQuery.Add(nameof(includeLookupTableValues), includeLookupTableValues); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTable, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="model">Model for the Lookup Table.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyLookupTable(int orgId, string tableId, LookupTablesModel model)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Put_ModifyLookupTable"));
            if (model == null) throw new NullReferenceException("Model can't be null", new Exception("Put_ModifyLookupTable"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                { 2, tableId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTable, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Export a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ExportLookupTable(int orgId, string tableId)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Get_ExportLookupTable"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_ExportLookupTable, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return exported Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="exportId">ID of the export request.</param>
        /// <returns>non-JSON String</returns>
        public string Get_ReturnExportedLookupTable(int orgId, string tableId, int exportId)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Get_ReturnExportedLookupTable"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId },
                { 3, exportId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_RetrieveExportedLookupTable, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Import a CSV file into a Lookup Table
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="commaSeperatedValues">Lookup Table values. Requires text/csv format, not JSON.</param>
        /// <param name="hasHeaderRow">Set to true if first row of comma separated values is column headings. Default value is true.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ImportCSVIntoLookupTable(int orgId, string tableId, string commaSeperatedValues, bool? hasHeaderRow)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Post_ImportCSVIntoLookupTable"));
            if (commaSeperatedValues == null || commaSeperatedValues == string.Empty) throw new NullReferenceException("commaSeperatedValues can't be null", new Exception("Post_ImportCSVIntoLookupTable"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId }
            };

            if (hasHeaderRow != null) { _oQuery.Add(nameof(hasHeaderRow), hasHeaderRow); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_RetrieveExportedLookupTable, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(commaSeperatedValues));
        }

        /// <summary>
        /// Return a list of Lookup Table Values
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="offset">Number of Lookup Table Values to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Lookup Table Values. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_LostOfLookupTableValues(int orgId, string tableId, int? offset, int? limit)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Get_LostOfLookupTableValues"));
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTableValues, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a Lookup Table Value
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="model">Model for the Lookup Table Value.</param>
        /// <returns>JSON String</returns>
        public string Post_CreateLookupTableValue(int orgId, string tableId, LookupTablesModel model)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Post_CreateLookupTableValue"));
            if (model == null) throw new NullReferenceException("Model can't be null", new Exception("Post_CreateLookupTableValue"));
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                { 2, tableId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTableValues, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a Lookup Table Value
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="valueId">ID of the Lookup Table Value.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_RemoveLookupTableValue(int orgId, string tableId, string valueId)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Delete_RemoveLookupTableValue"));
            if (valueId == null || valueId == string.Empty) throw new NullReferenceException("valueId can't be null", new Exception("Post_CreateLookupTableValue"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId },
                { 3, valueId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTableValue, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a Lookup Table Value
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="valueId">ID of the Lookup Table Value.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ReturnInfoAboutLookupTableValue(int orgId, string tableId, string valueId)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Get_ReturnInfoAboutLookupTableValue"));
            if (valueId == null || valueId == string.Empty) throw new NullReferenceException("valueId can't be null", new Exception("Get_ReturnInfoAboutLookupTableValue"));
            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableId },
                { 3, valueId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTableValue, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a Lookup Table Value
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableId">ID of the Lookup Table.</param>
        /// <param name="valueId">ID of the Lookup Table Value.</param>
        /// <param name="model">Model for the Lookup Table Value.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyLookupTableValue(int orgId, string tableId, string valueId, LookupTablesModel model)
        {
            if (tableId == null || tableId == string.Empty) throw new NullReferenceException("tableId can't be null", new Exception("Put_ModifyLookupTableValue"));
            if (valueId == null || valueId == string.Empty) throw new NullReferenceException("valueId can't be null", new Exception("Put_ModifyLookupTableValue"));
            if (model == null) throw new NullReferenceException("Model can't be null", new Exception("Put_ModifyLookupTableValue"));
            _oPath = new Dictionary<int, string>()
            {
                { 1, orgId.ToString() },
                { 2, tableId },
                { 3, valueId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.DELETE_LookupTables_DeleteLookupTableValue, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return Lookup Table Value1 based on Lookup Table Value2
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableOrName">ID or Name of the Lookup Table.</param>
        /// <param name="value2">Value of Lookup Table Value2.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_LookupTableValue1BasedOnValue2(int orgId, string tableOrName, string value2)
        {
            if (tableOrName == null || tableOrName == string.Empty) throw new NullReferenceException("tableOrName can't be null", new Exception("Get_LookupTableValue1BasedOnValue2"));
            if (value2 == null || value2 == string.Empty) throw new NullReferenceException("value2 can't be null", new Exception("Get_LookupTableValue1BasedOnValue2"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableOrName },
                { 3, value2 }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTableValueValue1, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return Lookup Table Value2 based on Lookup Table Value1
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="tableOrName">ID or Name of the Lookup Table.</param>
        /// <param name="value1">Value of Lookup Table Value1.</param>
        /// <returns>Non-JSON String</returns>
        public string GetLookupTableValue2BasedOnValue1(int orgId, string tableOrName, string value1)
        {
            if (tableOrName == null || tableOrName == string.Empty) throw new NullReferenceException("tableOrName can't be null", new Exception("GetLookupTableValue2BasedOnValue1"));
            if (value1 == null || value1 == string.Empty) throw new NullReferenceException("value1 can't be null", new Exception("GetLookupTableValue2BasedOnValue1"));

            _oPath = new Dictionary<int, string>
            {
                { 1, orgId.ToString() },
                { 2, tableOrName },
                { 3, value1 }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlLookupTablesSettings.GET_LookupTables_GetLookupTableValueValue2, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Managed Connectors
        /// <summary>
        /// Return a list of Managed Connectors
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="showCustomers">Set to true to return customer Organizations using the Connector. Default value is false.</param>
        /// <param name="name">Name of the Managed Connector.</param>
        /// <param name="offset">Number of Managed Connectors to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Managed Connectors. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfManagedConnectors(int orgId, bool? showCustomers, string name, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
            };

            if (showCustomers != null) { _oQuery.Add(nameof(showCustomers), showCustomers); }
            if(name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlManagedConnectorsSettings.GET_ManagedConnectors_GetByOrg, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about a Managed Connector
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="id">ID of the Managed Connector.</param>
        /// <param name="showCustomers">Set to true to return customer Organizations using the Connector. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutManagedConnector(int orgId, string id, bool? showCustomers)
        {
            if (id == null || id == string.Empty) throw new NullReferenceException("Id can't be null.", new Exception("Get_InformationAboutManagedConnector"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, id}
            };

            if (showCustomers != null) { _oQuery.Add(nameof(showCustomers), showCustomers); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlManagedConnectorsSettings.GET_ManagedConnectors_GetById, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Maps
        /// <summary>
        /// Upgrade and return a submitted Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="model">Model of the Map.</param>
        /// <returns>JSON String</returns>
        public string Post_UpgradeAndReturnSubmittedMap(int orgId, string solutionId, MapsModel model)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_UpgradeAndReturnSubmittedMap"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_UpgradeAndReturnSubmittedMap"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_UpgradeMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return a Map link
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <returns>JSON String</returns>
        public string Get_MapLink(int orgId, string solutionId, int mapId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_MapLink"));
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetMapLinkById, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Maps
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="name">Name of the Map.</param>
        /// <param name="offset">Number of Maps to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Maps. Default value is 100.	</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ListOfMaps(int orgId, string solutionId, string name, int? offset, int? limit)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_ListOfMaps"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            if (name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetMaps, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Delete a Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_Map(int orgId, string solutionId, int mapId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Delete_Map"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.DELETE_Maps_DeleteMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="lockMap">Set to true to return Map in a locked state or false to return Map without locking it.</param>
        /// <param name="revision">Revision number of the Map.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutAMap(int orgId, string solutionId, int mapId, bool? lockMap, int? revision)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_InformationAboutAMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (lockMap != null) { _oQuery.Add(nameof(lockMap), lockMap); }
            if (revision != null) { _oQuery.Add(nameof(revision), revision); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.DELETE_Maps_DeleteMap, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify Block type
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="blockId">ID of the Block.</param>
        /// <param name="action">Known action type.</param>
        /// <param name="operationName">Name of operation.</param>
        /// <param name="model">Model of the Map.</param>
        /// <returns>JSON String</returns>
        public string Post_ModifyBlockType(int orgId, string solutionId, int mapId, string blockId, string action, string operationName, MapsModel model)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_ModifyBlockType"));
            if (blockId == null || blockId == string.Empty) throw new NullReferenceException("blockId can't be null", new Exception("Post_ModifyBlockType"));
            if (action == null || action == string.Empty) throw new NullReferenceException("action can't be null", new Exception("Post_ModifyBlockType"));
            if (operationName == null || operationName == string.Empty) throw new NullReferenceException("operationName can't be null", new Exception("Post_ModifyBlockType"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_ModifyBlockType"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (blockId != null && blockId != string.Empty) { _oQuery.Add(nameof(blockId), blockId); }
            if (action != null && action != string.Empty) { _oQuery.Add(nameof(action), action); }
            if (operationName != null && operationName != string.Empty) { _oQuery.Add(nameof(operationName), operationName); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_ChangeBlockType, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Copy a Map within a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <returns>JSON String</returns>
        public string Post_CopyMapInSolution(int orgId, string solutionId, int mapId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_CopyMapInSolution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_CloneMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Modify Map Enabled/Disabled state
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="enableState">Set to true to enable or false to disable Map.</param>
        /// <returns>JSON String</returns>
        public string Put_EnableDisableMap(int orgId, string solutionId, int mapId, bool? enableState)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Put_EnableDisableMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (enableState != null) { _oQuery.Add(nameof(enableState), enableState); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.PUT_Maps_EnableDisableMap, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Return information about an Event Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <returns>JSON String</returns>
        public string Get_EventMapInformation(int orgId, string solutionId, int mapId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_EventMapInformation"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetMapEventInfo, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify Map Locked/Unlocked state
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="lockState">Set to true to lock or false to unlock Map.</param>
        /// <returns>JSON String</returns>
        public string Put_LockUnlockMap(int orgId, string solutionId, int mapId, bool? lockState)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Put_LockUnlockMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (lockState != null) { _oQuery.Add(nameof(lockState), lockState); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.PUT_Maps_LockMap, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Return the results of a Native Query Block test
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="nativeQueryBlockId">ID of the Native Query Block.</param>
        /// <returns>JSON String</returns>
        public string Get_NativeQueryBlockTestResults(int orgId, string solutionId, int mapId, string nativeQueryBlockId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_NativeQueryBlockTestResults"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (nativeQueryBlockId != null && nativeQueryBlockId != string.Empty) { _oQuery.Add(nameof(nativeQueryBlockId), nativeQueryBlockId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetNativeQueryResults, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Test Native Query Block properties
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="model">Model for the Block.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_TestNativeQueryBlockProperties(int orgId, string solutionId, int mapId, BlockModel model)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_TestNativeQueryBlockProperties"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_TestNativeQueryBlockProperties"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetNativeQueryResults, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Run Query preview
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="model">Model for the Block.</param>
        /// <param name="blockId">ID of the Fetch Block. If blank, Query preview results are returned.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_RunQueryPreview(int orgId, string solutionId, int mapId, MapsModel model, string blockId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_RunQueryPreview"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_RunQueryPreview"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (blockId != null && blockId != string.Empty) { _oQuery.Add(nameof(blockId), blockId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_PreviewQuery, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return Query preview results
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="previewId">The previewId returned from the Query preview.</param>
        /// <returns></returns>
        public string Get_QueryPreviewResults(int orgId, string solutionId, int mapId, string previewId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_QueryPreviewResults"));
            if (previewId == null || previewId == string.Empty) throw new NullReferenceException("previewId can't be null", new Exception("Get_QueryPreviewResults"));
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() },
                {4, previewId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetPreviewQueryResults, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of Relationships for a Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="blockId">ID of the Block.</param>
        /// <param name="agentId">ID of the Agent to load the metadata, if necessary.</param>
        /// <param name="offset">Number of Relationships to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Relationships. Default value is 100.	</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfRelationhipsForAMap(int orgId, string solutionId, int mapId, string blockId, string agentId, int? offset, int? limit)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_ListOfRelationhipsForAMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (blockId != null && blockId != string.Empty) { _oQuery.Add(nameof(blockId), blockId); }
            if (agentId != null && agentId != string.Empty) { _oQuery.Add(nameof(agentId), agentId); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetRelationships, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify Block name
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="blockId">ID of the Block.</param>
        /// <param name="model">Model of the Map.</param>
        /// <param name="newName">Block name.</param>
        /// <returns>JSON String</returns>
        public string Post_ModifyBlockName(int orgId, string solutionId, int mapId, string blockId, MapsModel model, string newName)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_ModifyBlockName"));
            if (blockId == null || blockId == string.Empty) throw new NullReferenceException("blockId can't be null", new Exception("Post_ModifyBlockName"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_ModifyBlockName"));
            if (newName == null || newName == string.Empty) throw new NullReferenceException("newName can't be null", new Exception("Post_ModifyBlockName"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (blockId != null && blockId != string.Empty) { _oQuery.Add(nameof(blockId), blockId); }
            if (newName != null && newName != string.Empty) { _oQuery.Add(nameof(newName), newName); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_RenameBlock, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Revert a Map to a prior version
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="revision">Number of Map revision to restore.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_RevertMapToPriorVersion(int orgId, string solutionId, int mapId, int? revision)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_RevertMapToPriorVersion"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (revision != null) { _oQuery.Add(nameof(revision), revision); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_RevertMap, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return a list of Map revisions
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="offset">Number of Maps to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Maps. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfMapRevisions(int orgId, string solutionId, int mapId, int? offset, int? limit)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_ListOfMapRevisions"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetMapRevisions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Run a single Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <returns>JSON String</returns>
        public string Post_RunSingleMap(int orgId, string solutionId, int mapId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_RunSingleMap"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_RunMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return the status of the Run Map Now command
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="commandId">ID returned by the Run Single Map command.</param>
        /// <returns>JSON String</returns>
        public string Get_StatusOfRunMapNowCommand(int orgId, string solutionId, int mapId, string commandId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_StatusOfRunMapNowCommand"));
            if (commandId == null || commandId == string.Empty) throw new NullReferenceException("commandId can't be null", new Exception("Get_StatusOfRunMapNowCommand"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() },
                {4, commandId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetRunMapResult, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Validate a Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="model">Model for the Map.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ValidateMap(int orgId, string solutionId, int mapId, MapsModel model)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_ValidateMap"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_ValidateMap"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_ValidateMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Validate a Formula in a Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map link.</param>
        /// <param name="model">Model for the Formula.</param>
        /// <returns>JSON String</returns>
        public string Post_validateFormula(int orgId, string solutionId, int mapId, FormulaModel model)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_validateFormula"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_validateFormula"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_ValidateFormula, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return Map validation results
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="validationId"></param>
        /// <param name="offset">Number of Maps to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Maps. Default value is 100.</param>
        /// <param name="filterByBlockId">ID of the Block. Return errors only for a specific Block.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnMapValidationResults(int orgId, string solutionId, int validationId, int? offset, int? limit, string filterByBlockId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_ReturnMapValidationResults"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, validationId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }
            if (filterByBlockId != null && filterByBlockId != string.Empty) { _oQuery.Add(nameof(filterByBlockId), filterByBlockId); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetValidationMapResults, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create an Advanced Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="model">Model for the Advanced Map.</param>
        /// <param name="comment">Revision comment.	</param>
        /// <returns>JSON String</returns>
        public string Post_CreateAdvancedMap(int orgId, string solutionId, MapsModel model, string comment)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_CreateAdvancedMap"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_CreateAdvancedMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            if (comment != null && comment != string.Empty) { _oQuery.Add(nameof(comment), comment); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_CreateAdvMap, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Modify an Advanced Map
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapId">ID of the Map.</param>
        /// <param name="model">Model for the Advanced Map.</param>
        /// <param name="unlockMap">Set to true to unlock Map after update.</param>
        /// <param name="validateMap">Set to true to run validation against the Map being updated. Default is false.</param>
        /// <param name="prepareMap">Set to true to run prepare against the Map being updated. Default is false.</param>
        /// <param name="oldConnectionId">ID of the Connection to be reassigned.</param>
        /// <param name="newConnectionId">ID of the replacement Connection.</param>
        /// <param name="updateBlockConnectionOnly">Set to true to leave reassigned Connection in the Map. Set to false to remove reassigned Connection from the Map.</param>
        /// <param name="comment">Revision comment.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyAdvancedMap(int orgId, string solutionId, int mapId, MapsModel model, bool? unlockMap, bool? validateMap, bool? prepareMap, string oldConnectionId, string newConnectionId, bool? updateBlockConnectionOnly, string comment)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Put_ModifyAdvancedMap"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Put_ModifyAdvancedMap"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, mapId.ToString() }
            };
            if (unlockMap != null) { _oQuery.Add(nameof(unlockMap), unlockMap); }
            if (validateMap != null) { _oQuery.Add(nameof(validateMap), validateMap); }
            if (prepareMap != null) { _oQuery.Add(nameof(prepareMap), prepareMap); }
            if (oldConnectionId != null && oldConnectionId != string.Empty) { _oQuery.Add(nameof(oldConnectionId), oldConnectionId); }
            if (newConnectionId != null && newConnectionId != string.Empty) { _oQuery.Add(nameof(newConnectionId), newConnectionId); }
            if (updateBlockConnectionOnly != null) { _oQuery.Add(nameof(updateBlockConnectionOnly), updateBlockConnectionOnly); }
            if (comment != null && comment != string.Empty) { _oQuery.Add(nameof(comment), comment); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.PUT_Maps_UpdateAdvMap, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Export Maps from a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="mapIds">Comma delimited list of Map IDs to export.</param>
        /// <param name="revision">Map revision to export when one mapId is specified.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ExportsMapsFromSolution(int orgId, string solutionId, string mapIds, int? revision)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_ExportsMapsFromSolution"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
            };

            if (mapIds != null && mapIds != string.Empty) { _oQuery.Add(nameof(mapIds), mapIds); }
            if (revision != null) { _oQuery.Add(nameof(revision), revision); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_Export, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return exported Maps
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="exportId">ID of the export request.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_ExportedMaps(int orgId, string solutionId, int exportId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Get_ExportedMaps"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {3, exportId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.GET_Maps_GetExportedMaps, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Import Maps into a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="model">Models for one or more Maps.</param>
        /// <param name="importedMapOrder">When Map names are the same, append imported Maps or merge with existing Maps. Default is True. True = Append. False = Merge.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_ImportMapsIntoSolution(int orgId, string solutionId, ImportMapModel model, bool? importedMapOrder)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionid can't be null", new Exception("Post_ImportMapsIntoSolution"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_ImportMapsIntoSolution"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            if (importedMapOrder != null) { _oQuery.Add(nameof(importedMapOrder), importedMapOrder); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlMapsSettings.POST_Maps_ImportMaps, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }
        #endregion

        #region Marketplace
        /// <summary>
        /// Return a list of Marketplace Connectors
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="filterByName">Name of the Connector.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfMarketplaceConnectors(int? orgId, string filterByName)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            if (filterByName != null && filterByName != string.Empty) { _oQuery.Add(nameof(filterByName), filterByName); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlManagedConnectorsSettings.GET_ManagedConnectors_GetByOrg, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Organizations
        /// <summary>
        /// Return a list of Organizations
        /// </summary>
        /// <param name="parentId">ID of the parent Organization. Only child Organizations are returned.</param>
        /// <param name="name">Name of the Organization, exact match.</param>
        /// <param name="nameContains">Filter by Name of the Organization with Contains parameter.</param>
        /// <param name="offset">Number of Organizations to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Organizations. Default value is 100.</param>
        /// <param name="noPagination">Set to true to ignore limit and offset, and return all Organizations without pagination. Default value is false.</param>
        /// <param name="status">Filter by Organization status.</param>
        /// <param name="expandStatus">Return Organization status. Default value is false.</param>
        /// <param name="filterByAPIAccess">True returns only Organizations with your IP address in the range of IPs with API access. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnListOfOrganizations(int? parentId, string name, string nameContains, int? offset, int? limit, bool? noPagination, string status, bool? expandStatus, bool? filterByAPIAccess)
        {
            _oQuery = new Dictionary<string, object>();

            if (parentId != null) { _oQuery.Add(nameof(parentId), parentId); }
            if (name != null && name != string.Empty) { _oQuery.Add(nameof(name), name); }
            if (nameContains != null && nameContains != string.Empty) { _oQuery.Add(nameof(nameContains), nameContains); }
            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }
            if (noPagination != null) { _oQuery.Add(nameof(noPagination), noPagination); }
            if (status != null && status != string.Empty) { _oQuery.Add(nameof(status), status); }
            if (expandStatus != null) { _oQuery.Add(nameof(expandStatus), expandStatus); }
            if (filterByAPIAccess != null) { _oQuery.Add(nameof(filterByAPIAccess), filterByAPIAccess); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetOrganizations, null, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a child Organization
        /// </summary>
        /// <param name="model">Model for the child Organization. Required: Name, ParentId</param>
        /// <returns>JSON String</returns>
        public string Post_CreateChildOrganization(OrgsModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_CreateChildOrganization"));
            if (model.Name == null || model.Name == string.Empty) throw new NullReferenceException("model name can't be null", new Exception("Post_CreateChildOrganization"));

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetOrganizations, null, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a child Organization
        /// </summary>
        /// <param name="orgId">ID of the child Organization.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_DeleteChildOrganization(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteOrganization, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about an Organization
        /// </summary>
        /// <param name="orgId">ID of the child Organization.</param>
        /// <returns>Non-JSON String</returns>
        public string Get_InformationAboutAnOrganization(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteOrganization, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify an Organization
        /// </summary>
        /// <param name="orgId">ID of the child Organization.</param>
        /// <param name="model">Model for the Organization.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyAnOrganization(int orgId, OrgsModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Put_ModifyAnOrganization"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteOrganization, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return a list of Message Endpoints
        /// </summary>
        /// <param name="orgId">ID of the child Organization.</param>
        /// <param name="offset">Number of endpoints to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of endpoints. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfMessageEndpoints(int orgId, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetEndpoints, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Clear the Message queue
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="endpointid">ID of the Message Endpoint.</param>
        /// <returns>Non-JSON String</returns>
        public string Put_ClearMessageQueue(int orgId, int endpointid)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() },
                {2, endpointid.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.PUT_Organizations_clearendpoint, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Return a list of Functions
        /// </summary>
        /// <param name="orgId">ID of the parent Organization.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnListOfFunctions(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetFunctions, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return a list of security rules
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="offset">Number of security rules to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of security rules. Default value is 100</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnListOfSecurityRules(int orgId, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetSecurityRules, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a security rule
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="model">Model for the security rule.</param>
        /// <returns>JSON String</returns>
        public string Post_CreateSecurityRule(int orgId, SecurityRuleModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Post_CreateSecurityRule"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetSecurityRules, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a security rule
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="ruleId">ID of the security rule.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_SecurityRule(int orgId, string ruleId)
        {
            if (ruleId == null || ruleId == string.Empty) throw new NullReferenceException("ruleId can't be null", new Exception("Delete_SecurityRule"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() },
                {2, ruleId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteSecurityRules, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a security rule
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="ruleId">ID of the security rule.</param>
        /// <returnsJSON String></returns>
        public string Get_InformationAboutSecurityRule(int orgId, string ruleId)
        {
            if (ruleId == null || ruleId == string.Empty) throw new NullReferenceException("ruleId can't be null", new Exception("Delete_SecurityRule"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() },
                {2, ruleId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteSecurityRules, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a security rule
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="ruleId">ID of the security rule.</param>
        /// <param name="model">Model for the security rule.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifySecurityRule(int orgId, string ruleId, SecurityRuleModel model)
        {
            if (ruleId == null || ruleId == string.Empty) throw new NullReferenceException("ruleId can't be null", new Exception("Put_ModifySecurityRule"));
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Put_ModifySecurityRule"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() },
                {2, ruleId }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteSecurityRules, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return a list of security settings
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnListOfSecuritySettings(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetSecuritySettings, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify security settings
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="model">Model for the security settings.</param>
        /// <returns></returns>
        public string Put_ModifySecuritySettings(int orgId, SecuritySettingModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Put_ModifySecuritySettings"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetSecuritySettings, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Reset the Event Solution access token
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>JSON String</returns>
        public string Put_ResetEventSolutionAccessToken(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.PUT_Organizations_resetaccesstoken, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Reset the API encryption token
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <returns>JSON String</returns>
        public string Put_ResetAPIEncryptionToken(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.PUT_Organizations_resetcryptotoken, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }

        /// <summary>
        /// Return a list of settings
        /// </summary>
        /// <param name="orgId">ID of the parent Organization.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfSettings(int orgId)
        {
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.GET_Organizations_GetSettings, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="email">Email address of the User.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_User(int orgId, string email)
        {
            if (email == null || email == string.Empty) throw new NullReferenceException("email can't be null", new Exception("Delete_User"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            if (email != null && email != string.Empty) { _oQuery.Add(nameof(email), email); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteUser, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return a list of Users
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="offset">Number of Users to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of Users. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfUsers(int orgId, int? offset, int? limit)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteUser, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a User
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="model">Model for the User.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyUser(int orgId, UserModel model)
        {
            if (model == null) throw new NullReferenceException("model can't be null", new Exception("Put_ModifyUser"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString() }
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlOrganizationsSettings.DELETE_Organizations_DeleteUser, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }
        #endregion

        #region Register
        /// <summary>
        /// Provision a New Organization for a User
        /// </summary>
        /// <param name="model">Model for the Organization.</param>
        /// <returns>JSON String</returns>
        public string Post_CreateNewOrganization(RegisterModel model)
        {
            if (model == null) throw new NullReferenceException("Model can't be null", new Exception("Post_CreateNewOrganization"));
            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlRegisterSettings.POST_Register_Create, null, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }
        #endregion

        #region Solutions
        /// <summary>
        /// Return a list of Solutions
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="offset">Number of Solutions to skip before returning results. Default value is 0.	</param>
        /// <param name="limit">Maximum number of Solutions. Default value is 100.</param>
        /// <param name="solutionNameFilter">Filter by Solution name.</param>
        /// <param name="solutionType">Filter by Solution type.	</param>
        /// <param name="solutionStatusFilter">Filter by Solution status</param>
        /// <param name="sortName">Sort by selected parameter. Default value is Name.</param>
        /// <param name="sortOrder">Sort ascending or descending. Default value is ascending.	</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfSolutions(int orgId, int? offset, int? limit, string solutionNameFilter, string solutionType, string solutionStatusFilter, string sortName, string sortOrder)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }
            if (solutionNameFilter != null && solutionNameFilter != string.Empty) { _oQuery.Add(nameof(solutionNameFilter), solutionNameFilter); }
            if (solutionType != null && solutionType != string.Empty) { _oQuery.Add(nameof(solutionType), solutionType); }
            if (solutionStatusFilter != null && solutionStatusFilter != string.Empty) { _oQuery.Add(nameof(solutionStatusFilter), solutionStatusFilter); }
            if (sortName != null && sortName != string.Empty) { _oQuery.Add(nameof(sortName), sortName); }
            if (sortOrder != null && sortOrder != string.Empty) { _oQuery.Add(nameof(sortOrder), sortOrder); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Create a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.	</param>
        /// <param name="model">Model for the Solution. Required: Name, AgentId, and SolutionType.</param>
        /// <param name="checkForBulk">Enables bulk operations if supported by Connection. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Post_CreateSolution(int orgId, SolutionsModel model, bool? checkForBulk)
        {
            if (model == null) throw new NullReferenceException("model can't be null.", new Exception("Post_CreateSolution"));
            if (model.Name == null || model.Name == string.Empty) throw new NullReferenceException("Name in model can't be null.", new Exception("Post_CreateSolution"));
            if (model.AgentId == null || model.AgentId == string.Empty) throw new NullReferenceException("AgentId in model can't be null.", new Exception("Post_CreateSolution"));
            if (model.SolutionType == null || model.SolutionType == string.Empty) throw new NullReferenceException("SolutionType in model can't be null.", new Exception("Post_CreateSolution"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
            };

            if (checkForBulk != null) { _oQuery.Add(nameof(checkForBulk), checkForBulk); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Delete a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Delete_Solution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Delete_Solution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.DELETE_Solutions_DeleteSolution, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.DeleteMethod);
        }

        /// <summary>
        /// Return information about a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutSolution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Get_InformationAboutSolution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.DELETE_Solutions_DeleteSolution, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="model">Model for the Solution.</param>
        /// <param name="checkForBulk">Enables bulk operations if supported by Connection. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifySolution(int orgId, string solutionId, SolutionsModel model, bool? checkForBulk)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Put_ModifySolution"));
            if (model == null) throw new NullReferenceException("model can't be null.", new Exception("Put_ModifySolution"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            if (checkForBulk != null) { _oQuery.Add(nameof(checkForBulk), checkForBulk); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutions, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Copy an entire Solution into another Organization
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="destOrgId">ID of the destination Organization.</param>
        /// <param name="destAgentId">ID of the destination Agent.</param>
        /// <returns>JSON String</returns>
        public string Post_CopySolutionToAnotherOrganization(int orgId, string solutionId, int destOrgId, string destAgentId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_CopySolutionToAnotherOrganization"));
            if (destAgentId == null || destAgentId == string.Empty) throw new NullReferenceException("destAgentId can't be null.", new Exception("Post_CopySolutionToAnotherOrganization"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };
            _oQuery = new Dictionary<string, object>()
            {
                {nameof(destOrgId), destOrgId},
                {nameof(destAgentId), destAgentId}
            };


            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_CloneSolution, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return information about Connections for a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>JSON String</returns>
        public string Get_InformationAboutConnectionsForSolution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Get_InformationAboutConnectionsForSolution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutionConnections, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Prepare a Solution to run
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>JSON String</returns>
        public string Post_PrepareSolutionToRun(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_PrepareSolutionToRun"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_PrepareSolution, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Return the status of the Solution prepare command
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="prepareId">ID of the prepare Solution command.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnSolutionPrepareCommandStatus(int orgId, string solutionId, string prepareId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Get_ReturnSolutionPrepareCommandStatus"));
            if (prepareId == null || prepareId == string.Empty) throw new NullReferenceException("prepareId can't be null.", new Exception("Get_ReturnSolutionPrepareCommandStatus"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId},
                {2, prepareId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_PrepareSolutionStatus, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Return information about a schedule for a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>JSON String</returns>
        public string Get_SolutionScheduleInformation(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Get_SolutionScheduleInformation"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutionSchedule, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify a schedule for a Solution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <param name="model">Model for the Schedule. See the RecurringModel TimeZone topic in the Scribe API Help for information on the TimeZone field.</param>
        /// <param name="checkForBulk">	Enables bulk operations if supported by Connection. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifySolutionSchedule(int orgId, string solutionId, ScheduleModel model, bool? checkForBulk)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Put_ModifySolutionSchedule"));
            if (model == null) throw new NullReferenceException("model can't be null.", new Exception("Put_ModifySolutionSchedule"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            if (checkForBulk != null) { _oQuery.Add(nameof(checkForBulk), checkForBulk); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.GET_Solutions_GetSolutionSchedule, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Start a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_StartSolutionExecution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_StartSolutionExecution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_Start, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Start monitoring a Solution when it runs
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_StartMonitoringSolutionExectuion(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_StartMonitoringSolutionExectuion"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_StartMonitorSolution, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Stop a Solution execution
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_StopSolutionExecution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_StopSolutionExecution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_Stop, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }

        /// <summary>
        /// Stop monitoring a Solution when it runs
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="solutionId">ID of the Solution.</param>
        /// <returns>Non-JSON String</returns>
        public string Post_StopMonitoringSolutionExecution(int orgId, string solutionId)
        {
            if (solutionId == null || solutionId == string.Empty) throw new NullReferenceException("solutionId can't be null.", new Exception("Post_StopMonitoringSolutionExecution"));

            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()},
                {2, solutionId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSolutionsSettings.POST_Solutions_StopMonitorSolution, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PostMethod);
        }
        #endregion

        #region Subscriptions
        /// <summary>
        /// Return a list of Subscriptions
        /// </summary>
        /// <param name="orgId">ID of the Organization.</param>
        /// <param name="showMonthlyUsage">	Set to true to return monthly Subscription usage. Default value is false.</param>
        /// <returns>JSON String</returns>
        public string Get_ListOfSubscriptions(int orgId, bool? showMonthlyUsage)
        {
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, orgId.ToString()}
            };

            if (showMonthlyUsage != null) { _oQuery.Add(nameof(showMonthlyUsage), showMonthlyUsage); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlSubscriptionsSettings.GET_Subscriptions_GetByOrg, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }
        #endregion

        #region Users
        /// <summary>
        /// Return your User information
        /// </summary>
        /// <returns>JSON String</returns>
        public string Get_ReturnUserInfo()
        {
            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.GET_Users_GetUser, null, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify your User information
        /// </summary>
        /// <param name="userModel">Model for the User.</param>
        /// <returns>JSON String</returns>
        public string Put_ModifyUserInfo(UserModel userModel)
        {
            if (userModel == null) throw new NullReferenceException("userModel can't be null", new Exception("Put_ModifyUserInfo"));
            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.GET_Users_GetUser, null, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod, _jsonService.ConvertObjectToJSON(userModel));
        }

        /// <summary>
        /// Return a list of email notification settings
        /// </summary>
        /// <param name="userId">ID of the User.</param>
        /// <param name="offset">Number of settings to skip before returning results. Default value is 0.</param>
        /// <param name="limit">Maximum number of settings. Default value is 100.</param>
        /// <returns>JSON String</returns>
        public string Get_UserNotificationSettings(string userId, int? offset, int? limit)
        {
            if (userId != null && userId != string.Empty) throw new NullReferenceException("userId can't be null", new Exception("Get_UserNotificationSettings"));
            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, userId}
            };

            if (offset != null) { _oQuery.Add(nameof(offset), offset); }
            if (limit != null) { _oQuery.Add(nameof(limit), limit); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.GET_Users_GetAlertSettings, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify email notification settings
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns>Non-JSON String</returns>
        public string Put_ModifyNotificationSettings(string userId, EmailNotificationModel model)
        {
            if (userId != null && userId != string.Empty) throw new NullReferenceException("userId can't be null", new Exception("Put_ModifyNotificationSettings"));
            if (model == null) throw new NullReferenceException("userModel can't be null", new Exception("Put_ModifyNotificationSettings"));

            _oPath = new Dictionary<int, string>()
            {
                {1, userId}
            };

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.GET_Users_GetAlertSettings, _oPath, null);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod, _jsonService.ConvertObjectToJSON(model));
        }

        /// <summary>
        /// Return a list of Invitations
        /// </summary>
        /// <param name="email">Email address of the User.</param>
        /// <returns>JSON String</returns>
        public string Get_ReturnListOfInvites(string email)
        {
            _oQuery = new Dictionary<string, object>();

            if (email != null && email != string.Empty) { _oQuery.Add(nameof(email), email); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.GET_Users_invitations, null, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.GetMethod);
        }

        /// <summary>
        /// Modify Invitation status
        /// </summary>
        /// <param name="inviteId">ID of the Invitation.</param>
        /// <param name="accept">Set to true to accept or false to decline the Invitation.</param>
        /// <returns>Non-JSON String</returns>
        public string Put_ModifyInvitationStatus(string inviteId, bool? accept)
        {
            if (inviteId == null || inviteId == string.Empty) throw new NullReferenceException("Invite Id can't be null.", new Exception("Put_ModifyInvitationStatus"));

            _oQuery = new Dictionary<string, object>();
            _oPath = new Dictionary<int, string>()
            {
                {1, inviteId}
            };

            if (accept != null) { _oQuery.Add(nameof(accept), accept); }

            _url = _urlBuilderService.BuildUrl(BaseUrl + UrlUsersSettings.PUT_Users_AcceptInvitations, _oPath, _oQuery);

            return _WebServiceDAO.SendRequest(UserName, Password, _url, GeneralSettings.PutMethod);
        }
        #endregion
    }
}