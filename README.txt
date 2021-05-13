To run:

1. Load the API project in VS 2019 and start debug.  An empty page will load but you use the following in the browser to make sure the api is running:
	https://localhost:44317/deal?playerOne=Tom&playerTwo=Dick
	
	you should get and json object returned that looks something like:
	
	[{"playerName":"Tom","hand":"9H 2D 6H 6D 7S","discard":null,"winner":false},{"playerName":"Dick","hand":"KS 10C 9S AH 5H","discard":null,"winner":false}]
	
	the difference will be in the "hand" returned for each player.
	
2. Load the PokerUI project in VS Code and execute a yarn start.
	
	The Draw Poker Table page should render.  If it doesn't, I'm screwed.
	
Issues:

1.  The api call from the JavaScript fetch method connects and seems to return data.  I can see it in the browesrs network activity, but I cannot get
	it to render because the return object is empty.  Something fundemental is wrong, but I don't have time right now to dig into the problem.
	
2.	"Who Won" button isn't wired up because the render from the issues with getting any results from the api calls.