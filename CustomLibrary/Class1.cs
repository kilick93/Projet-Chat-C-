using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Drawing;

namespace CustomLibrary
{
    public class msg
    {
        public string texte { get; set; }
        public string pseudo { get; set; }
        public int canal { get; set; }
        public DateTime time { get; set; }
        // 1 pour 1ere connexion
        // 2 pour texte
        // 3 pour image
        public int type { get; set; }
        public byte[] image { get; set; }


    }
}
