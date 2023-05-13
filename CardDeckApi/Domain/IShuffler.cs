using System;
using System.Collections;
using System.Collections.Generic;

namespace CardDeckApi.Domain
{
    public interface IShuffler
    {
        public IEnumerable<T> OrderByRandom<T>(IEnumerable<T> items);
        public IEnumerable<T> FanShuffle<T>(T[] items);
    }
}
