# MockingBird
Mobile app game that will test your memory to mimic the longest tune

## Background
This was the first mobile app I attempted to create. Before committing to its development, I wanted to ensure that the scope of the game was not too large for a solo project and created an outline. This game invites the player to observe ordered auditory and visual cues and then replay them, with increasing difficulty the game tests memory to mimic the longest tune. Your scores are saved in a graph visible in the Settings section of the game.

The project consisted of various aspects: asynchronous programming, 2D sprite asset generation, generalized mobile UI organization, audio asset generation, as well as general design principles.

## About the Code
Most of the code controlling the game is within the 'GameManager' script of the Scripts folder. The methods that play the randomized sequence are asynchronous task types such that they could be cancelled with cancellation token sources. The game data is organized so that a random list of strings { "a", "b", "c", "d" } is assembled with a count corresponding to the level, and then those strings determine which button fires. The user is to tap on the buttons which add the correct string to a list which is compared to the randomly generated one at runtime.  

## Screenshots 
> The Main Menu
![The Main Menu](https://github.com/TognaBologna09/MockingBird/blob/main/MBMain.PNG)

# How to Play
When you click play, the following screen will appear. Letters fade in and out saying, "Repeat the Sequence" as a sound and accompanying animation highlights a button for the user to repeat. As the sequence plays, the player can 'interrupt' and begin to copy the sequence which cancels the already playing sequence. The same 'interrupt' mechanic occurs if a player copies the wrong tone which causes an error tone to play and the random sequence to restart.

> Play Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/MBPlay4.PNG)

#
After failing to copy the sequence, the player loses a 'life'. The sequence repeats itself again after a slight pause, but should the player lose their last life the next screen will appear. 
> Score screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/MBScorecard.PNG)

