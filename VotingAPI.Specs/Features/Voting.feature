Feature: Voting

Scenario: Ajouter 4 candidats
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	When compte des candidats
	Then il y a 4 candidats

Scenario: Candidat en double
	Given le candidat est 'Tom Holland'
	And le candidat est 'Tom Holland'
	When compte des candidats
	Then il y a 1 candidats

Scenario: Ajouter plusieurs candidats
	Given le tableau de candidats est
	| Nom			|
	| Zendaya	|
	| Tom Holland	|
	| Andrew Garfield	|
	| Tobey Maguire	|
	When compte des candidats
	Then il y a 4 candidats

Scenario: Ajouter un vote pour un candidat
	Given le candidat est 'Tobey Maguire'
	Given 5 votes pour 'Tobey Maguire'
	When vote pour 'Tobey Maguire'
	Then le candidat a 5 votes

Scenario: Ajouter un vote pour un candidat invalide
	Given 2 votes pour 'Tom Holland'
	When vote pour 'Tom Hollandr'
	Then erreur 'Impossible d'ajouter un vote (candidat inconnu)'

Scenario Outline: Ajouter des votes pour plusieurs candidats
	Given le candidat est '<candidate>'
	Given <votes1> votes pour '<candidate>'
	Given <votes2> votes pour '<candidate>'
	When vote pour '<candidate>'
	Then le candidat a <total> votes
	Examples: 
	| candidate    | votes1 | votes2 | total |
	| Zendaya   | 1      | 4      | 5     |
	| Tom Holland | 5      | 3      | 8     |

Scenario: Compteur des votes blancs
	Given le tableau de candidats est
		 | Nom         |
		 | Zendaya   |
		 | Tom Holland |
		 | Andrew Garfield |
		 | Tobey Maguire |
	Given 1 votes pour 'Zendaya'
	Given 3 votes pour 'Tom Holland'
	Given 5 votes pour 'Tobey Maguire'
	Given 7 votes blanc
	When compte des votes blancs
	Then il y a 7 votes au total
		
Scenario: Compteur de tout les votes
	Given le tableau de candidats est
		 | Nom         |
		 | Zendaya  |
		 | Tom Holland |
		 | Andrew Garfield |
		 | Tobey Maguire |
	Given 1 votes pour 'Zendaya'
	Given 3 votes pour 'Tom Holland'
	Given 5 votes pour 'Tobey Maguire'
	And 7 votes blanc
	When compte de tout les votes
	Then il y a 16 votes au total
	
Scenario: Verification d'un gagnant sur le 1er tour avec la majorité absolue
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	Given 7 votes pour 'Zendaya'
	And 35 votes pour 'Tom Holland'
	And 4 votes pour 'Andrew Garfield'
	And 1 votes pour 'Tobey Maguire'
	When votes clos
	Then le gagnant est 'Tom Holland'

Scenario: Verification d'un gagnant sur le 1er tour
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	Given 7 votes pour 'Zendaya'
	And 9 votes pour 'Tom Holland'
	And 4 votes pour 'Andrew Garfield'
	And 1 votes pour 'Tobey Maguire'
	When votes clos
	Then le gagnant est 'Tom Holland et Zendaya'

Scenario: Verification d'un gagnant sur le 2eme tour
	Given changement de tour
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	Given 7 votes pour 'Zendaya'
	And 9 votes pour 'Tom Holland'
	When votes clos
	Then le gagnant est 'Tom Holland'
	
Scenario: Verification d'un candidat sur le changement de tour
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	Given 7 votes pour 'Zendaya'
	And 9 votes pour 'Tom Holland'
	And 4 votes pour 'Andrew Garfield'
	And 1 votes pour 'Tobey Maguire'
	When votes clos et creation d'un tour
	And compte des candidats 
	Then il y a 2 candidats

Scenario: Verification si il y a un gagnant sur le changement de tour
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	Given 7 votes pour 'Zendaya'
	And 9 votes pour 'Tom Holland'
	And 4 votes pour 'Andrew Garfield'
	And 1 votes pour 'Tobey Maguire'
	When votes clos et creation d'un tour
	Given 4 votes pour 'Tom Holland'
	And 2 votes pour 'Zendaya'
	When votes clos
	Then le gagnant est 'Tom Holland'

Scenario: Verification si il y a aucun gagnant sur le changement de tour
	Given le candidat est 'Zendaya'
	And le candidat est 'Tom Holland'
	And le candidat est 'Andrew Garfield'
	And le candidat est 'Tobey Maguire'
	Given 7 votes pour 'Zendaya'
	And 9 votes pour 'Tom Holland'
	And 4 votes pour 'Andrew Garfield'
	And 1 votes pour 'Tobey Maguire'
	When votes clos et creation d'un tour
	Given 2 votes pour 'Tom Holland'
	And 2 votes pour 'Zendaya'
	When votes clos
	Then il na pas de gagnant

	