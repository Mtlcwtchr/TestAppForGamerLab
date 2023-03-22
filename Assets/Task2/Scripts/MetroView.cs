using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Task2.Scripts
{
    public class MetroView : MonoBehaviour
    {
        private Dictionary<Color, MetroStation<Vector3, Color>> _heads;

        private List<MetroStation<Vector3, Color>> _route;

        private List<Vector3> _path;
        private int _nextPath;
        private Vector3 _spherePos;
        private Color _sphereColorCurrent;

        private const float AnimVelocity = 10f;

        private void Awake()
        {
            _heads = new Dictionary<Color, MetroStation<Vector3, Color>>();

            var o = new MetroStation<Vector3, Color>(new Vector3(7, 6.5f, 0));
            var j = new MetroStation<Vector3, Color>(new Vector3(5, 6, 0));
            var d = new MetroStation<Vector3, Color>(new Vector3(3, 3, 0));
            var l = new MetroStation<Vector3, Color>(new Vector3(1, 1, 0));
            var n = new MetroStation<Vector3, Color>(new Vector3(0, 0, 0));

            var g = new MetroStation<Vector3, Color>(new Vector3(9, 1, 0));
            var f = new MetroStation<Vector3, Color>(new Vector3(9, 3, 0));
            var h = new MetroStation<Vector3, Color>(new Vector3(3.5f, 9, 0));
            var b = new MetroStation<Vector3, Color>(new Vector3(0, 8.5f, 0));

            var e = new MetroStation<Vector3, Color>(new Vector3(6.5f, 3, 0));
            var c = new MetroStation<Vector3, Color>(new Vector3(1, 4, 0));
            var a = new MetroStation<Vector3, Color>(new Vector3(0, 10, 0));

            var k = new MetroStation<Vector3, Color>(new Vector3(0, 3, 0));
            var m = new MetroStation<Vector3, Color>(new Vector3(3.7f, 1, 0));

            o.AddRoute(Color.blue, j, null);
            j.AddRoute(Color.blue, d, o);
            d.AddRoute(Color.blue, l, j);
            l.AddRoute(Color.blue, n, d);
            n.AddRoute(Color.blue, null, l);

            g.AddRoute(Color.black, f, null);
            f.AddRoute(Color.black, j, g);
            j.AddRoute(Color.black, h, f);
            h.AddRoute(Color.black, b, j);
            b.AddRoute(Color.black, null, h);

            f.AddRoute(Color.red, e, null);
            e.AddRoute(Color.red, d, f);
            d.AddRoute(Color.red, c, e);
            c.AddRoute(Color.red, b, d);
            b.AddRoute(Color.red, a, c);
            a.AddRoute(Color.red, null, b);

            j.AddRoute(Color.green, c, e);
            c.AddRoute(Color.green, k, j);
            k.AddRoute(Color.green, l, c);
            l.AddRoute(Color.green, m, k);
            m.AddRoute(Color.green, e, l);
            e.AddRoute(Color.green, j, m);

            _heads.Add(Color.blue, o);
            _heads.Add(Color.black, g);
            _heads.Add(Color.red, f);
            _heads.Add(Color.green, j);

            var (path, changes, routed) = a.RouteTo(m);
            _route = path;
            _path = _route.Select(station => station.ID).ToList();
            _spherePos = _path[0];
            _nextPath = 1;
            _sphereColorCurrent = _route[0].GetRoute(_route[1]);

            Debug.Log("Shortest Route: ");
            Debug.Log(String.Join(" -> ", _route));
            Debug.Log($"Route Changes: {changes}");
        }

        private void Update()
        {
            float step = AnimVelocity * Time.deltaTime;
            Vector3 newPosition = Vector3.MoveTowards(_spherePos, _path[_nextPath], step);
            if (_spherePos == _path[_nextPath])
            {
                ++_nextPath;
                if (_nextPath == _path.Count)
                {
                    _nextPath = 1;
                    _spherePos = _path[0];
                    _sphereColorCurrent = _route[0].GetRoute(_route[1]);
                    return;
                }

                var station = _route.FirstOrDefault(stat => stat.ID == _path[_nextPath - 1]);
                var nextStation = _route.FirstOrDefault(stat => stat.ID == _path[_nextPath]);
                if (station != null && nextStation != null)
                {
                    _sphereColorCurrent = station.GetRoute(nextStation);
                }
            }

            _spherePos = newPosition;
        }

        private void OnDrawGizmos()
        {
            if (_heads == null)
            {
                return;
            }

            foreach (var (route, head) in _heads)
            {
                var node = head;
                if (head == null)
                {
                    continue;
                }

                do
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(node.ID, .2f);

                    var directions = node.GetDirectionsByRoute(route);
                    var next = directions.Next;
                    if (next != null)
                    {
                        Gizmos.color = route;
                        Gizmos.DrawLine(node.ID, next.ID);
                    }

                    node = next;
                } while (node != null && node != head);
            }

            if (_route != null)
            {
                for (var i = 0; i < _route.Count - 1; ++i)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(_route[i].ID, _route[i + 1].ID);
                }
            }

            Gizmos.color = _sphereColorCurrent;
            Gizmos.DrawSphere(_spherePos, 0.5f);
        }
    }
}