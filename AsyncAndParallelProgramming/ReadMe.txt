Async => start an operation and then return to UI (responsiveness)
Parallel => divide workload (between cores) so you finish sooner (performance)

1. Task Scheduler - what it is, what it does, what it is used for
2. Task t = new Task() vs. Task t = Task.TaskFactory.StartNew()
3. Task.FromCurrentSynchronizationContext
4. Task t = t.ContinueWith((antecedent) => {} // wait for first task to finish, fire off this one after
5. Task.WaitAny(new Task[]{ tasks list here })
6. Task<int> t = Task.Factory.StartNew(() => { code; return int; }); t.Result; // Implicitly will wait for result

SUMMARY
1. Introduction - why use async/parallel programming? 
		- Reponsiveness (async)
		- Performance (parallel)
	a. Task Parallel Library (TPL) -(.net 4)
		- why use TPL if we already have Threads, async/await, event based async, queueUserWorkItem
		- New evolutionary model: canceling, easier exeption handling, higher level constructs, much more
	b. Task - unit of work; an object denoting an ongoing operation or computation being performed by machine
	c. Code-based tasks are executed by a thread on some processor
	d. Thread is dedicated to task until task completes
2. Task = new Task(code) (2 concurrent processes)
3. Hardware - (1 core vs 8 core cpu)
	a. 1 core (main and Task t) switching/swapping executing of two processes - does it slow down the CPU?
		- no, because main thread (ex.) processes UI (doesn't need much cpu power/cycles) to execute Task t in the background
	b. 8 cores (main thread on 1 core and Task t on another core, Task t2 on yet another), all using different cores (gole of parallel programming)
4. AsiaOptions WPF application
	- Splitting task into 2 tasks and stargin one after first completes
	- t1 task (new worker thread: t.Start()) => complete => t2.ContinueWith (from UI Thread): t.Start(fromContext)
	- Task t = new Task(code) vs Task t = Task.Factory.StartNew(code) => StartNew is a little more efficient
	- Adding counter - shared resource - not thread safe (who does what?)
		- Click event => adds 1 => t2.ContinueWith => substracts 1 (both in UI context/main thread) => this is OK => UI can't be in both at the same time
		- However, adding in t1 is in a seperate t1 (shared resources). This is bad
5. Lambda expressions => (parameters) => { code } (params passed into block of code)
6. Closure == code + supporting data envirenment => IMPORTANT! => always passed by reference int x = 123 => used by Task.Factory.StartNew()
	- variables are ALWAYS passed by reference to TASKS => Implication? => variables can become SHARED variables (two or more code streams changes - unsafe)
	- be careful when variables are shared, different tasks can change data
7. Reflector => using Reflector tool
8. Common operations on Tasks - Async/Parallel components
	- TPL - provides functionality for tasks (Data structures: Concurrent dictionaries etc, Parallel LINQ)
	- Task Scheduler - mapping tasks to available worker threads
	- Resource manager - responsible for managing pool of threads
9. Tasks - what is a task? => object representing an ongoing computeation
	- provide means to check status, wait, harvest results, store exceptions etc
	- IMPORTANT - Tasks use a worker Thread! to perform an operation
	- Facade tasks => va op = new TaskCompletionSource<T>(); Task t = op.Task
10. StockHistory
	- Implementing 3 tasks to call 3 services
	- Task.WaitAny(new Task[] { t_yahoo, t_msn, t_nasdaq }) => wait for any task to finish first, takes in array of tasks to wait on
11. Task Operations
	a. t.Wait() = wait until task completes
		- t.Status => RanToCompletion, Cancelled, Faulted
		- Race condition - who finishes first/any errors?
	b. t.WaitAny(new Task[] { yahoo, msn }) = wait for any task to finish
		- int index = Task.WaitAny(tasks);
		- Task first = tasks[index]; // Now you can use index to grab result of task that finished first
	c. WaitAll(tasks) - wait for ALL tasks to finish in WHATEVER order they finish
	d. WaitAllOneByOne - wait for all but in order
	d. Harvesting results
		- Task<int> t = Task.Factory.StartNew(() => { code; return result; }); int r = t.Result = IMPORTANT = no need to call Wait() = implicitly Waiting for result t.Result;
	e. Task composition - ordering of tasks, one needing result from first
		- Task<decimal> t2 = t1.ContinueWith((antecedent) => { code } => wait for t1 to finish before starting t2
	f. ContinueWhenAll(tasks, (setofTasks) => { code }) - continue when ALL have finished and result is needed