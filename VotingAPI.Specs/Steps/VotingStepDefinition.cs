using FluentAssertions;
using System;
using TechTalk.SpecFlow;

namespace VotingAPI.Specs.Steps
{
    [Binding]
    public sealed class VotingStepDefinition
    {
        private readonly ScenarioContext scenarioCtxt;
        private Vote unVote;
        private string unResultat = "";

        public VotingStepDefinition(ScenarioContext scenarioContext)
        {
            scenarioCtxt = scenarioContext;
            unVote = new Vote();
        }

        #region Given
        [Given("le candidat est '(.*)'")]
        public void ajouterCandidat(string nom)
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unTour.ajouterCandidats(nom);
        }

        [Given(@"le tableau de candidats est")]
        public void ajouterTableauCandidats(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                Tour unTour = unVote.tour[unVote.tourEnCours - 1];
                unTour.ajouterCandidats(row["Nom"]);
            }
        }

        [Given("(.*) votes pour '(.*)'")]
        public void ajouterVotesPour(int votesNumber, string name)
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.ajouterVotesPourCandidat(name, votesNumber);
        }
        
        [Given("(.*) votes blanc")]
        public void ajouterVotesBlanc(int votesBlancs)
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.ajouterVotesBlanc(votesBlancs);
        }

        [Given("changement de tour")]
        public void changementTour()
        {
            unResultat = unVote.tourSuivant();
        }
        #endregion

        #region When
        [When("vote pour '(.*)'")]
        public void ajouterVotePour(string nom)
        {
            if (String.IsNullOrEmpty(unResultat))
            {
                Tour unTour = unVote.tour[unVote.tourEnCours - 1];
                unResultat = unTour.recupereCandidat(nom).parts.ToString();
            }
                
        }

        [When("compte de tout les votes")]
        public void compteurVotes()
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.recupererToutLesVotes();
        }

        [When("compte des candidats")]
        public void compteurCandidats()
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.candidats.Count.ToString();
        }     
        
        [When("compte des votes blancs")]
        public void compteurVoteBlancs()
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.votesBlanc.ToString();
        }     

        [When("compte du pourcentage pour '(.*)'")]
        public void compteurPourcentagePour(string nom)
        {
            if (String.IsNullOrEmpty(unResultat))
            {
                Tour unTour = unVote.tour[unVote.tourEnCours - 1];
                unResultat = unTour.recupererPourcentageCandidat(unTour.recupereCandidat(nom)).ToString();
            }
                
        }

        [When("votes clos")]
        public void votesClos()
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.recupererGagnant(unVote.clos, unVote.tourEnCours);
        }
        
        [When("votes clos et creation d'un tour")]
        public void votesClosEtCreationTour()
        {
            Tour unTour = unVote.tour[unVote.tourEnCours - 1];
            unResultat = unTour.creationSecondTour(unVote);
        }
        #endregion

        #region Then
        [Then("le candidat a (.*) votes")]
        public void recupereCandidatVotes(string result)
        {
            unResultat.Should().Be(result);

        }

        [Then("il y a (.*) candidats")]
        public void recupereNombreCandidats(string result)
        {
            unResultat.Should().Be(result);

        }  

        [Then("il y a (.*) votes au total")]
        public void recupereVotesTotal(string result)
        {
            unResultat.Should().Be(result);
        }

        [Then("le gagnant est '(.*)'")]
        public void leGagnantEst(string result)
        {
            unResultat.Should().Be(result);
        }

        [Then("impossible de cree un nouveau tour")]
        public void creationImpossibleTour()
        {
            unResultat.Should().Be("Impossible d'avoir plus de 2 tours");
        }

        [Then("il na pas de gagnant")]
        public void aucunGagnant()
        {
            unResultat.Should().Be("Aucun gagnants dans le second tour");
        }

        [Then("erreur '(.*)'")]
        public void erreurMessage(string result)
        {
            unResultat.Should().Be(result);
        }
        #endregion
    }
}
