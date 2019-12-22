I implemented two different Ticker classes. To switch, in the Ticker scene, on the "GameManager" game object, 
simply select the Ticker instance than you want (both are already in the scene).

The Asynchronized Ticker simply creates one Coroutine for each registered Tickable.
The Synchronized Ticker creates one Coroutine per different "time" (or tick duration). Thus, you can have a lot of 
different Tickable that have the same tick duration and will all tick in the same frame.

You can notice in my example, in case you choose Sync Ticker, the Tickable3 is ticking only 9 times instead 
of 10 times with Async Ticker. 
This is because it misses the first tick, triggered only with Tickable1 registered, 
because the Coroutine is created when the first element of this tickDuration is added.


Additional exercise : 
We can notice some strange outputs : 
If we ask Coroutine to "tick" every second for example, instead of ticking the very same frame Unity time reaches a new second, 
this is slowly shifting to the 2nd frame, then 3rd frame after detecting the new second is reached etc...

The Coroutine is partially executed each frame (so each Update()). This means Coroutine yields at the first frame the statement
Timer > timeDuration is true. Then we reset the Timer and start counting again. BUT the exact duration of this "loop" has taken
slightly more time than the real timeDuration. After a lot of Update loops, this overtime can represents more than 1 frame duration.

This explains why we have this strange result. 