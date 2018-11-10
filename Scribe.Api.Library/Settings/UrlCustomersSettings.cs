namespace Scribe.Api.Library.Settings
{
	public class UrlCustomersSettings
	{
		public const string GET_Customers_GetConnectorCustomers = "/v1/orgs/{orgId}/managedconnectors/{connectorId}/customers";
		public const string DELETE_Customers_RemoveCustomer = "/v1/orgs/{orgId}/managedconnectors/{connectorId}/customers/{id}";
	}
}