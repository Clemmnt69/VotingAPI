using System;
using System.Collections.Generic;

namespace VotingAPI
{
    public class Vote
    {
        public List<Tour> tour { get; set; }
        public int tourEnCours { get; set; }
        public Boolean clos { get; set; }

        public Vote()
        {
            tour = new List<Tour>();
            tour.Add(new Tour());
            tourEnCours = 1;
            clos = false;
        }

        public string tourSuivant()
        {
            string resultat = "";
            if (clos == false && tourEnCours > 1)
            {
                resultat = "Impossible d'avoir plus de deux tour";
            }       
            else
            {
                tour.Add(new Tour());
                tourEnCours++;
            }
            return resultat;
        }
    }
}
