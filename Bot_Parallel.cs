using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var threads = new HashSet<Task<Tuple<Turn, double>>>();
            for (int i = 0; i < threadsCount; i++)
                threads.Add(Task.Run(() =>
                    SearchBestMove(rocket, new Random(random.Next()), iterationsCount / threadsCount)));
            Task.WhenAll(threads);

            var bestMove = threads.OrderBy(t => t.Result.Item2).FirstOrDefault().Result;
            var newRocket = rocket.Move(bestMove.Item1, level);
            return newRocket;
        }
    }
}