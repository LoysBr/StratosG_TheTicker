I implemented two different Ticker classes. To switch, in the Ticker scene, on the "GameManager" game object, 
simply select the Ticker instance than you want (both are already in the scene).

The Asynchronized Ticker simply creates one Coroutine for each registered Tickable.
The Synchronized Ticker creates one Coroutine per different "time" (or tick duration). Thus, you can have a lot of 
different Tickable that have the same tick duration and will all tick in the same frame.

You can notice in my example, in case you choose Sync Ticker, the Tickable3 is ticking only 9 times instead 
of 10 times with Async Ticker. 
This is because it misses the first tick, triggered only with Tickable1 registered, 
because the Coroutine is created when the first element of this tickDuration is added.