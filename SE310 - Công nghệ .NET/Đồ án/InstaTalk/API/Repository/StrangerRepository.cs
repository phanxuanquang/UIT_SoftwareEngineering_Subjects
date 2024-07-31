using API.Data;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class StrangerRepository : IStrangerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StrangerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<List<AppUser>>> StrangerFindMatch()
        {
            var queryUser = await _context.Users.Include(x => x.Rooms)
                .Include(x => x.StrangerFilter)
                .ThenInclude(x => x.CurrentRoom)
                .AsNoTracking()
                .Where(user => !user.Locked)
                .ToListAsync();

            Graph g = new Graph();

            foreach (var user in queryUser)
            {
                var listUserMatch = queryUser.Where(item => user.Id != item.Id &&
                    user.StrangerFilter != null &&
                    //user.StrangerFilter.FindType.Any(subItem => item.Type.Contains(subItem)) &&
                    user.StrangerFilter.MinAge <= item.Age &&
                    user.StrangerFilter.MaxAge >= item.Age &&
                    user.StrangerFilter.FindGender.Contains(item.Gender ?? string.Empty) &&
                    user.StrangerFilter.FindRegion.Contains(item.Nationality ?? string.Empty))
                    .ToList();
                foreach (var item in listUserMatch)
                {
                    g.AddEdge(user, item);
                }
            }

            return g.ConnectedComponents();
        }
    }

    class Graph
    {
        Dictionary<AppUser, List<AppUser>> adjList = new Dictionary<AppUser, List<AppUser>>();

        public void AddEdge(AppUser u, AppUser v)
        {
            if (!adjList.ContainsKey(u))
                adjList[u] = new List<AppUser>();
            adjList[u].Add(v);
        }

        void DFSUtil(AppUser v, Dictionary<AppUser, bool> visited, List<AppUser> component)
        {
            visited[v] = true;
            component.Add(v);

            List<AppUser> vList;
            adjList.TryGetValue(v, out vList);

            if (vList != null)
            {
                foreach (var n in vList)
                {
                    if (visited.TryGetValue(n, out bool status) && !status)
                        DFSUtil(n, visited, component);
                }
            }
        }

        public List<List<AppUser>> ConnectedComponents()
        {
            Dictionary<AppUser, bool> visited = new Dictionary<AppUser, bool>();
            foreach (var i in adjList.Keys)
                visited[i] = false;

            List<List<AppUser>> components = new List<List<AppUser>>();

            foreach (var v in adjList.Keys)
            {
                if (visited.TryGetValue(v, out bool status) && !status)
                {
                    List<AppUser> component = new List<AppUser>();
                    DFSUtil(v, visited, component);
                    components.Add(component);
                }
            }

            return components;
        }
    }
}
