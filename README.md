# MockingBird
Mobile app game that will test your memory to mimic the longest tune

## Background
This game invites the player to observe ordered auditory and visual cues and then replay them, with increasing difficulty the game tests memory to mimic the longest tune. Your scores are saved in a list visible in the Settings section of the game.

The project consisted of various aspects: asynchronous programming, 2D sprite asset generation, generalized mobile UI organization, audio asset generation, as well as general design principles.

#### How to Play
In the main menu screen there are two buttons: Play and Settings. The settings menu allows the user to adjust game parameters such as tempo, six mode, a mute button, and the ability to view scores from previous playthroughs. When you click play with 'six mode' disabled letters fade in and out saying, "Repeat the Sequence" as a sound and accompanying animation highlights one of the four buttons for the user to repeat. As the sequence plays, the player can 'interrupt' and begin to copy the sequence which cancels the already playing sequence. The same 'interrupt' mechanic occurs if a player copies the wrong tone which causes an error tone to play and the random sequence to restart. 


> The Main Menu
![The Main Menu](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1675.PNG)
> Play Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1679.PNG)
> Six Play Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1678.PNG)

 #
After failing to copy the sequence, the player loses a 'life'. The sequence repeats itself again after a slight pause, but should the player lose their last life the next screen will appear. 
> Score Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1680.PNG)

# 
When the player adjusts the settings, the following image appears. It displays a slider to control the tempo of the buttons, should the User find the speed to be too fast or slow. There are buttons to control the gamemode and to mute the game, as well as a button at the bottom to view the previous scores.
> Settings Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1676.PNG)
> Scoreboard Screen
![](https://github.com/TognaBologna09/MockingBird/blob/main/IMG-1677.PNG)

## About the Code
Most of the code controlling the game is within the 'GameManager' script of the Scripts folder. The methods that play the randomized sequence are asynchronous task types such that they could be cancelled with cancellation token sources. The game data is organized so that a random list of strings { "a", "b", "c", "d" } is assembled with a count corresponding to the level, and then those strings determine which button fires. The user is to tap on the buttons which add the correct string to a list which is compared to the randomly generated one at runtime.  
