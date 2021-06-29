using DevOpsInspector.Data.Models;
using DevOpsInspector.Data.Models.ApiModels;
using DevOpsInspector.Data.Models.AppBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevOpsInspector.Api
{
    public class Worker
    {
        #region private fields
        private HttpClient Client { get; set; }
        #endregion private fields

        #region constructor
        public Worker()
        {
            Client = new HttpClient();
        }
        #endregion constructor

        #region public methods
        /// <summary>
        /// 
        /// </summary>
        public async Task<List<Projects>> GetProjects(string uri, string token, List<string> ConfigurationProjects)
        {
            List<Projects> retList = new List<Projects>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseString = await response.Content.ReadAsStringAsync();
                        ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);

                        for (int i = 0; i < responseBody.count; i++)
                        {
                            var jsn = JsonSerializer.Deserialize<JSONProject>(responseBody.value.ElementAt(i).ToString());

                            //if (ConfigurationProjects != null)
                            //{
                            //    if (ConfigurationProjects.Count > 0)
                            //    {
                            //        if (ConfigurationProjects.Any(q =>
                            //                    q.ToUpper().Trim().Equals(
                            //                        jsn.name.ToUpper().Trim())))
                            //        {
                                        retList.Add(new Projects
                                        {
                                            Description = jsn.description,
                                            Id = Guid.Parse(jsn.id),
                                            LastUpdateTime = jsn.lastUpdateTime,
                                            Name = jsn.name,
                                            Revision = jsn.revision,
                                            State = jsn.state,
                                            Url = jsn.url,
                                            Visibility = jsn.visibility
                                        });
#if DEBUG
                                        Console.WriteLine("Found project: " + jsn.name);
#endif
                            //        }
                            //    }
                            //}
                        }

                        return retList;
                    }
                }

            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetProjects) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Dictionary<RootClassificationNodes, Guid>> GetRootClassificationNodes(List<Projects> projects, string uri, string token)
        {
            Dictionary<RootClassificationNodes, Guid> retList = new Dictionary<RootClassificationNodes, Guid>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (Projects project in projects)
                    {
                        var cleanUri = uri;
                        cleanUri = cleanUri.Replace("{project}", project.Id.ToString());

                        using (HttpResponseMessage response = await client.GetAsync(cleanUri))
                        {
                            response.EnsureSuccessStatusCode();
                            string responseString = await response.Content.ReadAsStringAsync();
                            ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);

                            for (int i = 0; i < responseBody.count; i++)
                            {
                                var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                if (cleanResponseBody.Contains("ValueKind"))
                                {
                                    cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                    cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                }

                                var jsm = JsonSerializer.Deserialize<JSONRootClassificationNode>(responseBody.value.ElementAt(i).ToString());

                                retList.Add(new RootClassificationNodes
                                {
                                    Id = Guid.Parse(jsm.Identifier),
                                    Identifier = jsm.Id,
                                    DateCreation = DateTime.Now,
                                    HasChildren = jsm.HasChildren,
                                    Name = jsm.Name,
                                    Path = jsm.Path,
                                    StructureType = jsm.StructureType,
                                    Url = jsm.Url
                                }, project.Id);
#if DEBUG
                                Console.WriteLine("Found root: " + jsm.Name);
#endif
                            }
                        }
                    }
                    return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetRootClassificationNodes) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projects"></param>
        /// <param name="rootClassificationNodes"></param>
        /// <param name="v"></param>
        /// <param name="dEVOPS_PERSONAL_ACCESS_TOKEN"></param>
        public async void GetChildsClassificationNodes(List<Projects> projects, Dictionary<RootClassificationNodes, Guid> rootClassificationNodes, string uri, string token)
        {
            //List<JSONCapacities> retList = new List<JSONCapacities>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (var rootClassificationNode in rootClassificationNodes)
                    {
                        var cleanUri = uri;
                        cleanUri = cleanUri.Replace("{project}", rootClassificationNode.Value.ToString());
                        cleanUri = cleanUri.Replace("{ids}", rootClassificationNode.Key.Identifier.ToString());

                        using (HttpResponseMessage response = await client.GetAsync(cleanUri))
                        {
                            response.EnsureSuccessStatusCode();
                            string responseString = await response.Content.ReadAsStringAsync();
                            ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);
#if DEBUG
                            Console.WriteLine("Classification Node of Root: " + responseBody.count + ", ");
#endif
                            for (int i = 0; i < responseBody.count; i++)
                            {
                                var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                if (cleanResponseBody.Contains("ValueKind"))
                                {
                                    cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                    cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                }

                                var jsi = JsonSerializer.Deserialize<JSONClassificationNode>(responseBody.value.ElementAt(i).ToString());

                                //jsi.iterationId = iteration.Id;
                                //jsi.teamId = team.Id;

                                //retList.Add(jsi);
                            }
                        }


                    }
                    //return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<List<Teams>> GetTeams(string uri, string token, List<string> ConfigurationProjects)
        {
            List<Teams> retList = new List<Teams>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();
                        string responseString = await response.Content.ReadAsStringAsync();
                        ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);

                        for (int i = 0; i < responseBody.count; i++)
                        {
                            var jsn = JsonSerializer.Deserialize<JSONTeam>(responseBody.value.ElementAt(i).ToString());

                            if (ConfigurationProjects != null)
                            {
                                if (ConfigurationProjects.Count > 0)
                                {
                                    if (ConfigurationProjects.Any(q =>
                                                q.ToUpper().Trim().Equals(
                                                    jsn.projectName.ToUpper().Trim())))
                                    {
                                        retList.Add(new Teams
                                        {
                                            Description = jsn.description,
                                            Id = Guid.Parse(jsn.id),
                                            IdentityUrl = jsn.identityUrl,
                                            Name = jsn.name,
                                            ProjectId = Guid.Parse(jsn.projectId),
                                            ProjectName = jsn.projectName,
                                            Url = jsn.url
                                        });
#if DEBUG
                                        Console.WriteLine("Found team: " + jsn.name + " of project " + jsn.projectName);
#endif
                                    }
                                }
                            }
                        }
                        return retList;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetTeams) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<Dictionary<Members, Guid>> GetTeamsMembers(List<Teams> teams, string token)
        {
            Dictionary<Members, Guid> retList = new Dictionary<Members, Guid>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (Teams team in teams)
                    {
                        string uri = team.Url + "/members";

                        using (HttpResponseMessage response = await client.GetAsync(uri))
                        {
                            response.EnsureSuccessStatusCode();
                            string responseString = await response.Content.ReadAsStringAsync();
                            ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);
#if DEBUG
                            Console.WriteLine("Found " + responseBody.count + " members of team: " + team.Name);
#endif
                            for (int i = 0; i < responseBody.count; i++)
                            {
                                var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                if (cleanResponseBody.Contains("ValueKind"))
                                {
                                    cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                    cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                }

                                var jsm = JsonSerializer.Deserialize<JSONMember>(responseBody.value.ElementAt(i).ToString());

                                retList.Add(new Members
                                {
                                    Id = Guid.Parse(jsm.identity.Id),
                                    AvatarUrl = jsm.identity.Links.avatar.href,
                                    Descriptor = jsm.identity.Descriptor,
                                    DisplayName = jsm.identity.DisplayName,
                                    ImageUrl = jsm.identity.ImageUrl,
                                    IsTeamAdmin = jsm.isTeamAdmin,
                                    UniqueName = jsm.identity.UniqueName,
                                    Url = jsm.identity.Url
                                }, team.Id);
                            }
                        }
                    }
                    return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetTeamsMembers) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teams"></param>
        /// <param name="dEVOPS_PERSONAL_ACCESS_TOKEN"></param>
        /// <returns></returns>
        public async Task<List<Iterations>> GetIterations(List<Teams> teams, string uri, string token)
        {
            List<Iterations> retList = new List<Iterations>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (Teams team in teams)
                    {
                        var cleanUri = uri;
                        cleanUri = cleanUri.Replace("{project}", team.ProjectId.ToString());
                        cleanUri = cleanUri.Replace("{team}", team.Id.ToString());

                        using (HttpResponseMessage response = await client.GetAsync(cleanUri))
                        {
                            response.EnsureSuccessStatusCode();
                            string responseString = await response.Content.ReadAsStringAsync();
                            ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);
#if DEBUG
                            Console.WriteLine("Found " + responseBody.count + " Iterations of team: " + team.Name);
#endif
                            for (int i = 0; i < responseBody.count; i++)
                            {
                                var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                if (cleanResponseBody.Contains("ValueKind"))
                                {
                                    cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                    cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                }

                                var jsi = JsonSerializer.Deserialize<JSONIteration>(responseBody.value.ElementAt(i).ToString());

                                // importo solo le iter nuove
                                if (jsi.attributes.finishDate >= DateTime.Now)
                                {
                                    retList.Add(new Iterations
                                    {
                                        Id = Guid.Parse(jsi.id),
                                        Name = jsi.name,
                                        Path = jsi.path,
                                        TeamId = team.Id,
                                        Url = jsi.url,
                                        FinishDate = jsi.attributes.finishDate,
                                        StartDate = jsi.attributes.startDate,
                                        TimeFrame = jsi.attributes.timeFrame
                                    });
                                }
                            }
                        }
                    }

                    return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetIterations) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teams"></param>
        /// <param name="iterations"></param>
        /// <param name="uri"></param>
        /// <param name="token"></param>
        public async void GetTeamsDaysOff(List<Teams> teams, List<Iterations> iterations, string uri, string token)
        {
            //List<JSONCapacities> retList = new List<JSONCapacities>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (Teams team in teams)
                    {
                        foreach (Iterations iteration in iterations)
                        {
                            if (iteration.TeamId == team.Id)
                            {
                                var cleanUri = uri;
                                cleanUri = cleanUri.Replace("{project}", team.ProjectId.ToString());
                                cleanUri = cleanUri.Replace("{team}", team.Id.ToString());
                                cleanUri = cleanUri.Replace("{iterationId}", iteration.Id.ToString());

                                using (HttpResponseMessage response = await client.GetAsync(cleanUri))
                                {
                                    response.EnsureSuccessStatusCode();
                                    string responseString = await response.Content.ReadAsStringAsync();
                                    ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);
#if DEBUG
                                    Console.WriteLine("Found " + responseBody.count + " days off of team: " + team.Name + " for iteration " + iteration.Name);
#endif
                                    for (int i = 0; i < responseBody.count; i++)
                                    {
                                        var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                        if (cleanResponseBody.Contains("ValueKind"))
                                        {
                                            cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                            cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                        }

                                        var jsi = JsonSerializer.Deserialize<JSONCapacities>(responseBody.value.ElementAt(i).ToString());

                                        //jsi.iterationId = iteration.Id;
                                        //jsi.teamId = team.Id;

                                        //retList.Add(jsi);
                                    }
                                }
                            }
                        }
                    }
                    //return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetTeamsDaysOff) + " " + ex.Message);
#endif
                throw ex;
            }
        }

        public async Task<List<JSONCapacities>> GetCapacities(List<Teams> teams, List<Iterations> iterations, string uri, string token)
        {
            List<JSONCapacities> retList = new List<JSONCapacities>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", token))));

                    foreach (Teams team in teams)
                    {
                        foreach (var iteration in iterations)
                        {
                            if (iteration.TeamId == team.Id)
                            {
                                var cleanUri = uri;
                                cleanUri = cleanUri.Replace("{project}", team.ProjectId.ToString());
                                cleanUri = cleanUri.Replace("{team}", team.Id.ToString());
                                cleanUri = cleanUri.Replace("{iterationId}", iteration.Id.ToString());

                                using (HttpResponseMessage response = await client.GetAsync(cleanUri))
                                {
                                    response.EnsureSuccessStatusCode();
                                    string responseString = await response.Content.ReadAsStringAsync();
                                    ApiResponseBody responseBody = JsonSerializer.Deserialize<ApiResponseBody>(responseString);
#if DEBUG
                                    Console.WriteLine("Found " + responseBody.count + " capacities of team: " + team.Name + " for iteration " + iteration.Name);
#endif
                                    for (int i = 0; i < responseBody.count; i++)
                                    {
                                        var cleanResponseBody = responseBody.value.ElementAt(i).ToString();
                                        if (cleanResponseBody.Contains("ValueKind"))
                                        {
                                            cleanResponseBody = cleanResponseBody.Remove(0, 22);
                                            cleanResponseBody = cleanResponseBody.Remove(cleanResponseBody.Length - 1, cleanResponseBody.Length);
                                        }

                                        var jsi = JsonSerializer.Deserialize<JSONCapacities>(responseBody.value.ElementAt(i).ToString());

                                        jsi.iterationId = iteration.Id;
                                        jsi.teamId = team.Id;

                                        retList.Add(jsi);
                                    }
                                }
                            }
                        }
                    }
                    return retList;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(nameof(GetCapacities) + " " + ex.Message);
#endif
                throw ex;
            }
        }
        #endregion public methods
    }
}