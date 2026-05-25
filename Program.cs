using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RuleEnginePOC.Models;
using RuleEnginePOC.Services;
using System;

namespace RuleEnginePOC
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

                services.AddDbContext<RuleEngineContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("RuleEngineDb"))
                );

                services.AddTransient<RuleEngineService>();

                var serviceProvider = services.BuildServiceProvider();

                var ruleEngine = serviceProvider.GetService<RuleEngineService>();
                using var context = new RuleEngineContext();

                //var ruleEngine = new RuleEngineService(context);

                var input = new RuleInput
                {
                    UseCaseCode = "IT_SALARY_DECISION",
                    Fields = new Dictionary<string, string>
                            {
                                { "Experience","6" },
                                { "Skill",".NET" }
                            }
                };

                var input2 = new RuleInput
                {
                    UseCaseCode = "IT_SALARY_DECISION",
                    Inputs = new List<Dictionary<string, string>>
                             {
                                 new Dictionary<string,string>
                                 {
                                     { "Experience", "10" },
                                     { "Skill", "AI" }
                                 },
                                 new Dictionary<string,string>
                                 {
                                     { "Experience", "6" },
                                     { "Skill", ".NET" }
                                 }
                             }
                };

                var result = ruleEngine.Evaluate(input2);

                if (result != null)
                {
                    Console.WriteLine("Rule Matched\n");

                    foreach (var item in result)
                    {
                        Console.WriteLine($"{item.Key} : {item.Value}");
                    }
                }
                else
                {
                    Console.WriteLine("No rule matched");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Console.ReadKey();
        }
    }
}