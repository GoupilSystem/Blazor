#language: no-NO

Egenskap: Henvisning side

@smoketest
Scenario: Bruker kan åpne Henvisning side og registrere bestilling
	Gitt en bruker åpner Bestillingsveiviser
	Når brukeren velger Henvisning flis
	Og klikker på Neste steg knapp
	Så lander applikasjonen på Henvisning side

	Når brukeren velger kommune og bydel
	Og klikker på Fullfør registrering
	Så får brukeren antall bestillinger


