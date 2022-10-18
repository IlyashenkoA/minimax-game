# Game with UI, based on MiniMax algorithm

At the start of the game, a random string of numbers is generated,
consisting of 0 and 1, for example "101011" (only the longest string of 10-15 characters should be taken).
Players perform moves in turn. The move involves replacing any two adjacent numbers,
based on the following conditions: the pair of numbers 00 gives 1, 01 -> 0, 10 -> 1, 11 -> 0.
Only one pair of numbers can be substituted per turn.
The game ends when 2 numbers are obtained. If both numbers are the same (11 or 00), then the player wins
who started the game. If they are different (10 or 01), then the second player wins.
