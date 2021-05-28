using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.GraphQlApi.Mutations;
using Nucleus.GraphQlApi.Queries;

namespace Nucleus.Extensions.ServicesProviders
{
    public static class GraphQlServiceProvider
    {
        public static void AddAppGraphQl(this IServiceCollection services)
        {
            var graphQlServices = services.AddGraphQLServer();
            
            addQuery(graphQlServices);
            addMutation(graphQlServices);
            
            graphQlServices.AddAuthorization();
        }

        private static void addQuery(IRequestExecutorBuilder builder) =>
            builder
                .AddQueryType<MainQuery>()
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<AccountQuery>();
        

        private static void addMutation(IRequestExecutorBuilder builder) =>
            builder
                .AddMutationType<MainMutation>()
                .AddTypeExtension<AccountMutation>();
    }
}