using System;
using System.Collections.Generic;
using System.Text;

namespace VotingAPI
{
    public class Candidats
    {
        public string nom { get; set; }
        public int parts { get; set; }

        public Candidats(string nom, int parts)
        {
            this.nom = nom;
            this.parts = parts;
        }

        public Candidats(string nom)
        {
            this.nom = nom;
            this.parts = 0;
        }
    }
}
