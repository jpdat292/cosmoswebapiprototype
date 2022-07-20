namespace WebApplication1
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApplication1.Models;
    using Microsoft.Azure.Cosmos;

    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;

        public CosmosDbService(
            CosmosClient cosmosClient,
            string databaseName,
            string containerName)
        {
            this._container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Student> AddItemAsync(Student student)
        {
            var item = await this._container.CreateItemAsync<Student>(student, new PartitionKey(student.Id));
            return item;
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<Student>(id, new PartitionKey(id));
        }

        public async Task<Student> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Student> response = await this._container.ReadItemAsync<Student>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Student>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Student>(new QueryDefinition(queryString));
            List<Student> results = new List<Student>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string id, Student student)
        {
            await this._container.UpsertItemAsync<Student>(student, new PartitionKey(id));
        }
    }
}