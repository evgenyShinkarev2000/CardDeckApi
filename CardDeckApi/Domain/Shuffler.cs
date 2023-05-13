using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDeckApi.Domain
{
    public class Shuffler : IShuffler
    {
        public IEnumerable<T> FanShuffle<T>(T[] items)
        {
            if (items.Length < 2)
            {
                foreach(var item in items)
                {
                    yield return item;
                }
            }

            var middle = items.Length / 2;
            var leftIndex = 0;
            var rightIndex = middle;
            var random = new Random();

            foreach(var i in Enumerable.Range(0, items.Length))
            {
                var needReturnLeft = random.Next(2) == 0;
                if (leftIndex < middle)
                {
                    if (needReturnLeft || rightIndex >= items.Length)
                    {
                        yield return items[leftIndex];
                        ++leftIndex;
                    }
                    else
                    {
                        yield return items[rightIndex];
                        ++rightIndex;
                    }
                }
                else
                {
                    yield return items[rightIndex];
                    ++rightIndex;
                }
            }
        }

        public IEnumerable<T> OrderByRandom<T>(IEnumerable<T> items)
        {
            var random = new Random();

            return items.OrderBy(select => random.Next());
        }
    }
}
