using System;
using System.Collections.Generic;
using DevOpsInspector.Api;
using DevOpsInspector.Data.Models;
using DevOpsInspector.Data.Models.ApiModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace DevOpsInspector
{
    public static class DOFunction
    {
#if DEBUG
        // schedulazione ogni 30 secondi
        private const string schedulazione = "*/30 * * * * *";
#else
        // schedulazione ogni ora del giorno
         private const string schedulazione = "0 0 * * * *";
#endif

        [FunctionName("DOFunction")]
        public static void Run([TimerTrigger(schedulazione)] TimerInfo myTimer, ILogger log)
        {
            // Load global configuration
            Global global = new Global();

            // Azure DevOps REST Api worker instance
            Worker apiWorker = new Worker();

            try
            {
                // Get Projects
                List<Projects> projects = apiWorker.GetProjects(global.Configuration.OrganizzationBaseUri + global.Configuration.ProjectsUri,
                                                                global.Configuration.AccessToken, global.Configuration.ConfigurationProjects).Result;

                // Get Root Classification Nodes 
                Dictionary<RootClassificationNodes, Guid> rootClassificationNodes = apiWorker.GetRootClassificationNodes(projects, global.Configuration.OrganizzationBaseUri + global.Configuration.ClassificationNodesRootUri,
                    global.Configuration.AccessToken).Result;

                // Get Teams
                List<Teams> teams = apiWorker.GetTeams(global.Configuration.OrganizzationBaseUri + global.Configuration.TeamsUri,
                                                       global.Configuration.AccessToken, global.Configuration.ConfigurationProjects).Result;
                // Get Teams Members
                Dictionary<Members, Guid> teamsMembers = apiWorker.GetTeamsMembers(teams, global.Configuration.AccessToken).Result;

                // Get Team Iterations
                List<Iterations> iterations = apiWorker.GetIterations(teams, global.Configuration.OrganizzationBaseUri + global.Configuration.IterationsUri, global.Configuration.AccessToken).Result;

                // Get Iteration Capacities
                List<JSONCapacities> capacities = apiWorker.GetCapacities(teams, iterations, global.Configuration.OrganizzationBaseUri + global.Configuration.CapacitiesUri, global.Configuration.AccessToken).Result;

                if (projects != null)
                {
                    if (projects.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving projects in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateProjects(projects);
#if DEBUG
                        Console.WriteLine("----------- Saving of projects in the db was successful -----------");
#endif
                    }
                }

                if (rootClassificationNodes != null)
                {
                    if (rootClassificationNodes.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving root nodes in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateRootClassificationNodes(rootClassificationNodes);
#if DEBUG
                        Console.WriteLine("----------- Saving of root nodes in the db was successful -----------");
#endif
                    }
                }

                if (teams != null)
                {
                    if (teams.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving teams in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateTeams(teams);
#if DEBUG
                        Console.WriteLine("----------- Saving of teams in the db was successful -----------");
#endif
                    }
                }

                if (teamsMembers != null)
                {
                    if (teamsMembers.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving members in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateMembers(teamsMembers);
#if DEBUG
                        Console.WriteLine("----------- Saving of members in the db was successful -----------");
#endif
                    }
                }

                if (iterations != null)
                {
                    if (iterations.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving iterations in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateIterations(iterations);
#if DEBUG
                        Console.WriteLine("----------- Saving of iterations in the db was successful -----------");
#endif
                    }
                }

                if (capacities != null)
                {
                    if (capacities.Count > 0)
                    {
#if DEBUG
                        Console.WriteLine("----------- Saving capacities in the db -----------");
#endif
                        Core.UpdateDatabase.UpdateCapacities(capacities);
#if DEBUG
                        Console.WriteLine("----------- Saving of capacities in the db was successful -----------");
#endif
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }




#if DEBUG
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
#endif
        }
    }
}
