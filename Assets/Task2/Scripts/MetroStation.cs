using System.Collections.Generic;
using System.Linq;

namespace Task2.Scripts
{
    public class MetroStation<TID, TRouteID>
    {
        public TID ID { get; set; }

        private readonly Dictionary<TRouteID, Pair<MetroStation<TID, TRouteID>, MetroStation<TID, TRouteID>>> _routes;

        public MetroStation(TID id) : this()
        {
            ID = id;
        }

        public MetroStation()
        {
            _routes = new Dictionary<TRouteID, Pair<MetroStation<TID, TRouteID>, MetroStation<TID, TRouteID>>>();
        }

        public List<TRouteID> GetRoutes() => _routes.Keys.ToList();

        public TRouteID GetRoute(MetroStation<TID, TRouteID> destination) => _routes.FirstOrDefault(pair => pair.Value.Next == destination || pair.Value.Prev == destination).Key;

        public Pair<MetroStation<TID, TRouteID>, MetroStation<TID, TRouteID>> GetDirectionsByRoute(TRouteID route)
        {
            return _routes.TryGetValue(route, out var directions) ? directions : default;
        }

        public void AddRoute(TRouteID route, MetroStation<TID, TRouteID> next = null, MetroStation<TID, TRouteID> prev = null)
        {
            if (_routes.ContainsKey(route))
            {
                return;
            }

            _routes.Add(route, 
                new Pair<MetroStation<TID, TRouteID>, MetroStation<TID, TRouteID>>() {
                Next = next,
                Prev = prev 
                });
        }

        public (List<MetroStation<TID, TRouteID>>, int, bool) RouteTo(MetroStation<TID, TRouteID> destination) => Route(destination, new List<MetroStation<TID, TRouteID>>(), -1);

        private (List<MetroStation<TID, TRouteID>>, int, bool) Route(MetroStation<TID, TRouteID> destination, List<MetroStation<TID, TRouteID>> currentPath, int routeChanges, TRouteID prevRoute = default)
        {
            var path = new List<MetroStation<TID, TRouteID>>();
            path.AddRange(currentPath);

            var changes = routeChanges;

            path.Add(this);

            if (this == destination)
            {
                return (path, changes, true);
            }

            var routes = GetRoutes();
            var neighbours = new Dictionary<MetroStation<TID, TRouteID>, TRouteID>();

            var shortestPath = path;
            var shortestPathRouteChanges = changes;
            var shortestPathRouted = false;

            foreach (var route in routes)
            {
                var next = GetDirectionsByRoute(route).Next;
                var prev = GetDirectionsByRoute(route).Prev;

                if (next != null && !currentPath.Contains(next))
                {
                    neighbours.Add(next, route);
                }

                if (prev != null && !currentPath.Contains(prev))
                {
                    neighbours.Add(prev, route);
                }
            }

            foreach (var (neighbour, route) in neighbours)
            {
                var (stations, rChanges, isRouted) = neighbour.Route(destination, path, route.Equals(prevRoute) ? routeChanges : routeChanges + 1, route);

                if (isRouted && (!shortestPathRouted || stations.Count <= shortestPath.Count))
                {
                    if (stations.Count == shortestPath.Count)
                    {
                        if (rChanges < shortestPathRouteChanges)
                        {
                            shortestPath = stations;
                            shortestPathRouteChanges = rChanges;
                        }
                    }
                    else
                    {
                        shortestPathRouted = true;
                        shortestPath = stations;
                        shortestPathRouteChanges = rChanges;
                    }
                }
            }

            return (shortestPath, shortestPathRouteChanges, shortestPathRouted);
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
