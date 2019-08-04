using Microsoft.Azure.Cosmos;
using System;
using System.Net;
using System.Threading.Tasks;
using WeatherHub.Models.Search;

namespace WeatherHub.Logic
{
    public class CosmosDB
    {
        // The Azure Cosmos DB endpoint 
        private string EndpointUrl = "https://weatherhub-db.documents.azure.com:443/";
        // The primary key for the Azure DocumentDB account.
        private string PrimaryKey = "QjERfskbSmoFVzSaypLZRiW2MoM3lIjhKYRX6keHMyCuyHH3O2oiWGHyHpX0xEn2N28LgQw7WiHMZiEg617CuA==";
        // The Cosmos client instance
        private CosmosClient cosmosClient;
        // The database we will create or use
        private Database database;
        // The container we will create or use
        private Container container;
        // The name of the database and container we will create
        private string databaseId = "WeatherHubDB";
        private string containerId = "SearchContainer";

        /// <summary>
        /// Will log the passed in location that the user searched for to the CosmosDB
        /// </summary>
        public async Task LogToCosmosDB(string location)
        {
            try
            {
                cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
                await CreateDatabaseAsync();
                await CreateContainerAsync();
                await AddItemsToContainerAsync(location);
            }
            catch (CosmosException de)
            {
                // Log Error
            }
            catch (Exception e)
            {
                // Log Error
            }
        }

        /// <summary>
        /// Creates a database if it doesnt already exist based on the databaseId
        /// </summary>
        private async Task CreateDatabaseAsync()
        {
            database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        }

        /// <summary>
        /// Creates a container if it doesnt already exist. 
        /// Specifiy "/SearchValue" as the partition key since we're storing user search values, to ensure good distribution of requests and storage.
        /// </summary>
        private async Task CreateContainerAsync()
        {
            container = await this.database.CreateContainerIfNotExistsAsync(new ContainerProperties() { Id = containerId, PartitionKeyPath = "/SearchValue" });
        }

        private async Task AddItemsToContainerAsync(string location)
        {
            UserSearch userSearch = new UserSearch
            {
                Id = new Guid().ToString(),
                SearchValue = location,
                TimeSearched = DateTime.Now
            };
            try
            {
                // Read the item to see if it exists. ReadItemAsync will throw an exception if the item does not exist and return status code 404 (Not found).
                ItemResponse<UserSearch> searchResponse = await this.container.ReadItemAsync<UserSearch>(userSearch.TimeSearched.ToString(), new PartitionKey(userSearch.SearchValue));
                Console.WriteLine("Item in database with id: {0} already exists\n", searchResponse.Resource.TimeSearched.ToString());
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the users search. Note we provide the value of the partition key for the item
                ItemResponse<UserSearch> searchResponse = await container.CreateItemAsync(userSearch, new PartitionKey(userSearch.SearchValue));
            }
        }
    }
}
