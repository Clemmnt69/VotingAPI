using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace VotingAPI
{
    public class Tour
    {
       
        public List<Candidats> candidats;
        public int votesTotal { get; set; }
        public int votesBlanc { get; set; }
        public bool fermer { get; set; }

        public int minPourGagner = 50;

        public Tour()
        {
            candidats = new List<Candidats>();
            votesTotal = 0;
            votesBlanc = 0;
            fermer = false;
        }

        public bool verificationCandidatExistant(string nom)
        {
            foreach (Candidats unCandidat in candidats)
            {
                if (unCandidat.nom == nom)
                {
                    return true;
                }
            }
            return false;
        }

        public Candidats recupereCandidat(string nom)
        {
            foreach (Candidats unCandidat in candidats)
            {
                if (unCandidat.nom == nom)
                    return unCandidat;
            }
            return null;
        }

        public int recupereIndexCandidat(string name)
        {
            int res = 0;
            foreach (Candidats unCandidat in candidats)
            {
                if (unCandidat.nom == name)
                {
                    break;
                }
                res += 1;
            }
            return res;
        }

        public void ajouterCandidats(string nom)
        {
            if (!verificationCandidatExistant(nom) && !fermer)
                candidats.Add(new Candidats(nom));
        }

        public void AddCandidates(List<string> noms)
        {
            if(!fermer)
            {
                foreach (string unNom in noms)
                {
                    ajouterCandidats(unNom);
                }
            }
        }

        public string ajouterVotesPourCandidat(string nom, int valeur)
        {
            if (verificationCandidatExistant(nom) && !fermer)
            {
                candidats[recupereIndexCandidat(nom)].parts += valeur;
                votesTotal += valeur;
                return null;
            }
            else
            {
                return "Impossible d'ajouter un vote (candidat inconnu)";
            }
        }
        
        public string ajouterVotesBlanc(int valeur)
        {
            votesBlanc += valeur;
            return "";
        }
        
        public string recupererGagnant(bool voteClos, int numTour)
        {
            if (votesTotal == 0)
            {
                return "Aucun votes";
            }

             if (numTour == 1)
             {
                foreach (Candidats unCandidat in candidats)
                {
                    if (obtenueLaMajorite(unCandidat))                  
                        return unCandidat.nom;            
                }

                Candidats candidatUn = recupererMeilleurCandidat(candidats);

                List<Candidats> listSansPremierGagnant = candidats;
                listSansPremierGagnant.Remove(candidatUn);

                Candidats candidatDeux = recupererMeilleurCandidat(listSansPremierGagnant);

                return candidatUn.nom + " et " + candidatDeux.nom;
             }
             else if(numTour == 2 && voteClos)
             {
                 return recupererMeilleurCandidat(candidats).nom;
             }
             else
             {
                if (candidats[0].parts > candidats[1].parts)
                {
                    return candidats[0].nom;
                }
                else if (candidats[0].parts < candidats[1].parts)
                {
                    return candidats[1].nom;
                }
                else
                {
                    return "Aucun gagnants dans le second tour";
                }
             }
        }

        public bool obtenueLaMajorite(Candidats unCandidat)
        {
            if (recupererPourcentageCandidat(unCandidat) > minPourGagner)
            {
                return true;
            }
            return false;
        }

        public Candidats recupererMeilleurCandidat(List<Candidats> list)
        {
            Candidats gagnant = new Candidats("");
            foreach (Candidats unCandidat in list)
            {
                if (unCandidat.parts >= gagnant.parts)
                    gagnant = unCandidat;
            }
            return gagnant;
        }

        public float recupererPourcentageCandidat(Candidats unCandidat)
        {
            float res = (float)unCandidat.parts / votesTotal * 100;
            return res;
        }

        public string creationSecondTour(Vote unVote)
        {
            string res = unVote.tourSuivant();
            if(unVote.clos  && String.IsNullOrEmpty(res))
            {
                Tour tourUn = unVote.tour[0];
                Tour tourClos = unVote.tour[1];
                Tour tourDeux = unVote.tour[2];
                
                Candidats candidat1 = tourUn.recupererMeilleurCandidat(tourUn.candidats);
                Candidats candidat2 = tourClos.recupererMeilleurCandidat(tourClos.candidats);

                candidat1.parts = 0;
                candidat2.parts = 0;
                tourUn.candidats.Add(candidat1);
                tourDeux.candidats.Add(candidat2);
                return "";
            }
            else if(String.IsNullOrEmpty(res))
            {
                Tour tourUn = unVote.tour[0];
                Candidats candidat1 = tourUn.recupererMeilleurCandidat(candidats);

                List<Candidats> lesCandidats = candidats;
                lesCandidats.Remove(candidat1);

                Candidats candidat2 = tourUn.recupererMeilleurCandidat(lesCandidats);
            
                Tour tourDeux = unVote.tour[1];
                candidat1.parts = 0;
                candidat2.parts = 0;
                tourDeux.candidats.Add(candidat1);
                tourDeux.candidats.Add(candidat2);

                return "";
            }
            else      
                return res;           
        }
        public string recupererToutLesVotes()
        {
            try
            { 
               return (votesTotal + votesBlanc).ToString();
            }
            catch
            {
                return null;
            }
            
        }
    }
}
