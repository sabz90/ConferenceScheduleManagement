using ConferenceScheduleManagement.CoreClasses;
using System;
using System.Collections.Generic;

/*
 * AUTHOR: SABARISH K R E
 * EMAIL: sabarish90@outlook.com
 * */
namespace ConferenceScheduleManagement.SchedulingLogic
{
    class KnapSackSchedulingLogic : ISchedulingLogic
    {
        ///<summary>
        ///Schedules some talks to the gien session from the list of talks
        ///</summary>
        public void Schedule(List<Talk> listOfTalks, Session session)
        { 
            //input to knapsack algorithm is Total capacity (here: no of minutes in session) and the different weights (here: talks of specified durations)

            //this is the capacity i.e., no of max minutes of the session
            double totalMinutesOfSession = (session.EndTime - session.StartTime).TotalMinutes;

            //no if items
            int noOfItems = listOfTalks.Count;

            //Initialize Some data required by Knapsack Algorithm
            int[] profit = new int[noOfItems + 1];
            int[] weight = new int[noOfItems + 1];

            int i = 1;
            // profit and weight are the same here
            foreach (Talk proft in listOfTalks)
            {
                profit[i] = weight[i] = (int)proft.TimeDurationInMinutes;
                i++;
            }

            bool[] indexOfTalksToSelect = RunKnapSackAlgorithm((int)totalMinutesOfSession, noOfItems, profit, weight);

            PopulateSessionWithSelectedTalks(listOfTalks, indexOfTalksToSelect, session);
        }

        ///<summary>
        ///Fills the session with some selected talks from the given list of talks.
        ///</summary>
        private void PopulateSessionWithSelectedTalks(List<Talk> listOfTalks, bool[] indexOfTalksToSelect, Session session)
        {
            session.ScheduledTalks = (session.ScheduledTalks == null) ? new List<Talk>() : session.ScheduledTalks;

            for (int i = 1; i < indexOfTalksToSelect.Length; i++)
            {
                if (indexOfTalksToSelect[i])
                {
                    Talk talkToAdd = listOfTalks[i - 1];
                    session.ScheduledTalks.Add(talkToAdd);
                }
            }
        }

        ///<summary>
        ///Runs the knapsack algorithm to find the best fit.
        ///</summary>
        private bool[] RunKnapSackAlgorithm(int knapSackSize, int noOfItems, int[] profit, int[] weight)
        {
            int W = knapSackSize;
            int N = noOfItems;

            // opt[n][w] = maximum profit of packing the items 1..n with weight limit w
            // sol[n][w] = does the optimal soln to pack items 1..n with weight limit w include item n?
            int[,] opt = new int[N + 1, W + 1];
            bool[,] sol = new bool[N + 1, W + 1];

            for (int n = 1; n <= N; n++)
            {
                for (int w = 1; w <= W; w++)
                {
                    // Leave the item N
                    int option1 = opt[n - 1, w];

                    // Select item n
                    int option2 = int.MinValue;
                    if (weight[n] <= w)
                        option2 = profit[n] + opt[n - 1, w - weight[n]];

                    // select the better one of two options
                    opt[n, w] = Math.Max(option1, option2);
                    sol[n, w] = (option2 > option1);
                }
            }

            // determine which items to select n take
            bool[] take = new bool[N + 1];
            for (int n = N, w = W; n > 0; n--)
            {
                if (sol[n, w])
                {
                    take[n] = true;
                    w = w - weight[n];
                }
                else
                {
                    take[n] = false;
                }
            }
            return take;
        }        
    }
}
