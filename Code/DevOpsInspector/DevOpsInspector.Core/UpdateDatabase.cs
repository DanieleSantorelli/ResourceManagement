using DevOpsInspector.Data.Models;
using DevOpsInspector.Data.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace DevOpsInspector.Core
{
    public static class UpdateDatabase
    {
        public static void UpdateProjects(List<Projects> projects)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (Projects project in projects)
                    {
                        if (dbContext.Projects.Contains(project))
                        {
                            var prj = dbContext.Projects.Where(q => q.Id == project.Id).FirstOrDefault();
                            prj.DateModify = DateTime.Now;
                            dbContext.Projects.Update(prj);
                        }
                        else
                        {
                            dbContext.Projects.Add(project);
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateTeams(List<Teams> teams)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (Teams team in teams)
                    {
                        if (dbContext.Teams.Contains(team))
                        {
                            var tm = dbContext.Teams.Where(q => q.Id == team.Id).FirstOrDefault();
                            tm.DateModify = DateTime.Now;
                            dbContext.Teams.Update(tm);
                        }
                        else
                        {
                            dbContext.Teams.Add(team);
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateMembers(Dictionary<Members, Guid> members)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (var member in members)
                    {
                        if (dbContext.Members.Contains(member.Key))
                        {
                            var mb = dbContext.Members.Where(q => q.Id == member.Key.Id).FirstOrDefault();
                            mb.DateModify = DateTime.Now;
                            dbContext.Members.Update(mb);

                            // controllo su associazione tra team e membro del team
                            if (!dbContext.MembersTeams.Any(q => q.MemberId == member.Key.Id && q.TeamId == member.Value))
                            {
                                dbContext.MembersTeams.Add(new MembersTeams
                                {
                                    MemberId = member.Key.Id,
                                    TeamId = member.Value
                                });

                            }
                            // se è già presente, controllo che il membro sia ancora attivo nel team
                            else
                            {
                                var teamMembers = members.Where(q => q.Value == member.Value).ToList();
                                var teamMembersInDb = dbContext.MembersTeams.Where(q => q.TeamId == member.Value).ToList();

                                foreach (var memberInTeamDb in teamMembersInDb)
                                {
                                    if (!teamMembers.Any(q => q.Key.Id == memberInTeamDb.MemberId))
                                    {
                                        memberInTeamDb.IsInTeam = false;
                                    }
                                }
                            }

                            dbContext.SaveChanges();
                        }
                        else
                        {
                            dbContext.Members.Add(member.Key);
                            dbContext.MembersTeams.Add(new MembersTeams
                            {
                                MemberId = member.Key.Id,
                                TeamId = member.Value
                            });
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateRootClassificationNodes(Dictionary<RootClassificationNodes, Guid> rootClassificationNodes)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (var classificationNode in rootClassificationNodes)
                    {
                        if (dbContext.RootClassificationNodes.Contains(classificationNode.Key))
                        {
                            var mb = dbContext.RootClassificationNodes.Where(q => q.Id == classificationNode.Key.Id).FirstOrDefault();
                            mb.DateModify = DateTime.Now;
                            dbContext.RootClassificationNodes.Update(mb);

                            // controllo su associazione tra project e area path
                            if (!dbContext.ProjectAreaPath.Any(q => q.RootClassificationNodeId == classificationNode.Key.Id && q.ProjectId == classificationNode.Value))
                            {
                                dbContext.ProjectAreaPath.Add(new ProjectAreaPath
                                {
                                    ProjectId = classificationNode.Value,
                                    RootClassificationNodeId = classificationNode.Key.Id
                                });

                            }

                            dbContext.SaveChanges();
                        }
                        else
                        {
                            dbContext.RootClassificationNodes.Add(classificationNode.Key);
                            dbContext.ProjectAreaPath.Add(new ProjectAreaPath
                            {
                                ProjectId = classificationNode.Value,
                                RootClassificationNodeId = classificationNode.Key.Id
                            });
                            dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateCapacities(List<JSONCapacities> capacities)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (JSONCapacities jsCapacity in capacities)
                    {

                        Capacities capacity = new Capacities()
                        {
                            Url = jsCapacity.Url,
                            TeamId = jsCapacity.teamId,
                            MemberId = Guid.Parse(jsCapacity.TeamMember.Id),
                            IterationId = jsCapacity.iterationId,
                            DateCreation = DateTime.Now
                        };
                        // salvataggio capacities
                        var dbCapacity = dbContext.Capacities.Where(q => q.Url == capacity.Url).FirstOrDefault();
                        if (dbCapacity != null)
                        {
                            capacity = dbCapacity;
                            dbCapacity.DateModify = DateTime.Now;
                            dbContext.Capacities.Update(dbCapacity);
                        }
                        else
                        {
                            dbContext.Capacities.Add(capacity);
                        }
                        var ins = dbContext.SaveChanges();

                        if (ins > 0)
                        {
                            foreach (Activity activity in jsCapacity.Activities)
                            {
                                if (activity.CapacityPerDay > 0)
                                {
                                    Activities newActivity = new Activities()
                                    {
                                        CapacityId = capacity.Id,
                                        CapacityPerDay = activity.CapacityPerDay,
                                        Name = activity.Name,
                                        DateCreation = DateTime.Now
                                    };
                                    // salvataggio activity
                                    var dbActivity = dbContext.Activities.Where(q => q.Id == newActivity.Id).FirstOrDefault();
                                    if (dbActivity != null)
                                    {
                                        newActivity = dbActivity;
                                        dbActivity.DateModify = DateTime.Now;
                                        dbContext.Activities.Update(dbActivity);
                                    }
                                    else
                                    {
                                        dbContext.Activities.Add(newActivity);
                                    }
                                    dbContext.SaveChanges();
                                }

                            }

                            foreach (JSONDaysOff dayOff in jsCapacity.DaysOff)
                            {

                                DaysOff newDayOff = new DaysOff()
                                {
                                    CapacityId = capacity.Id,
                                    DateCreation = DateTime.Now,
                                    Start = dayOff.Start,
                                    End = dayOff.End.Value
                                };
                                // salvataggio activity
                                var dbDayOff = dbContext.DaysOff.Where(q => q.Id == newDayOff.Id).FirstOrDefault();
                                if (dbDayOff != null)
                                {
                                    newDayOff = dbDayOff;
                                    dbDayOff.DateModify = DateTime.Now;
                                    dbDayOff.Start = newDayOff.Start;
                                    dbDayOff.End = newDayOff.End;
                                    dbContext.DaysOff.Update(dbDayOff);
                                }
                                else
                                {
                                    dbContext.DaysOff.Add(newDayOff);
                                }
                                dbContext.SaveChanges();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateIterations(List<Iterations> iterations)
        {
            try
            {
                using (AlpitourDbContext dbContext = new AlpitourDbContext())
                {
                    foreach (Iterations iteration in iterations)
                    {
                        if (dbContext.Iterations.Contains(iteration))
                        {
                            var tm = dbContext.Iterations.Where(q => q.Id == iteration.Id).FirstOrDefault();
                            tm.DateModify = DateTime.Now;
                            dbContext.Iterations.Update(tm);
                        }
                        else
                        {
                            dbContext.Iterations.Add(iteration);
                        }

                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
