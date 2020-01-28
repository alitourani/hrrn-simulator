# HRRN Algorithm Simulator
This application is a simulator for **Highest Response Ratio Next (HRRN)** Algorithm which is a known method in Operating Systems to handle their processes.  In the fields of process management and CPU scheduling, there are a wide variety of algorithms to make sure the incoming processes are done in the desired time.
Considering ***Arrival Time*** as the time at which the process arrives in the ready queue and Burst Time as the time required by a process for CPU execution, Response Ratio in HRRN algorithm is:

```csharp
 Response Ratio = (W + S)/S
```

Where ***S*** refers to Burst Time and ***W*** refers to the time difference between turn around time and burst time. Read more about different times for scheduling [here](https://www.geeksforgeeks.org/gate-notes-operating-system-process-scheduling/ "here").
In HRRN shorter processes are favoured and longer jobs can get past shorter jobs.

### Implementation of HRRN Scheduling
1. Input the number of processes, their arrival times and burst times.
2. Sort them according to their arrival times.
3. At any given time calculate the response ratios and select the appropriate process to be scheduled.
4. Calculate the turn around time as completion time – arrival time.
5. Calculate the waiting time as turn around time – burst time.
6. Turn around time divided by the burst time gives the normalized turn around time.
7. Sum up the waiting and turn around times of all processes and divide by the number of processes to get the average waiting and turn around time.

### References:
1.  Wikipedia - Highest response ratio next ([Link](https://en.wikipedia.org/wiki/Highest_response_ratio_next "Link"))
2.  How does Highest Response Ratio Next work? ([Link](https://www.quora.com/How-does-Highest-Response-Ratio-Next-work-as-a-process-scheduling-algorithm-in-operating-systems "Link"))
