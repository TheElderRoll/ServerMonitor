using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerMonitor.Domain
{
    public class WPair<T, K>
    {
        public T First { get; set; }
        public K Second { get; set; }

        public WPair(T first, K second){
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"({First}, {Second})";
        }
    }
}