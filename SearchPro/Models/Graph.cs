using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchPro.Models
{
    public class Graph
    {
        public string State;
        public Freq freq;
        public Graph(string state, Freq freq)
        {
            this.State = state;
            this.freq = freq;
        }

    }
    public class Freq
    {

        public int low, mid, high;
        public Freq(int low, int mid, int high)
        {
            this.low = low;
            this.mid = mid;
            this.high = high;
        }
        public Freq(Freq f)
        {
            this.low = f.low;
            this.mid = f.mid;
            this.high = f.high;
        }

    }
}