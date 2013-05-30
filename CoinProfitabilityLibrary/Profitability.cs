using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

// Coin Profitability Calculator
//
// Copyright © 2013 Scott Alfter
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

namespace ScottAlfter.CoinProfitabilityLibrary
{
    public class Profitability
    {
        public decimal ProfitOnInterval(long lInterval, // seconds
            decimal dHashRate, // hashes per second
            decimal dReward, // coins
            decimal dDifficulty)
        {
            BigInteger interval = new BigInteger(lInterval);
            BigInteger hashrate = new BigInteger(dHashRate);
            BigInteger reward = new BigInteger(dReward) * 100000000;
            decimal difficulty = dDifficulty;
            BigInteger target = ((new BigInteger(65535) << 208) * 100000000000) / new BigInteger(difficulty * 100000000000);
            BigInteger revenue = interval * target * hashrate * reward / (new BigInteger(1) << 256);
            return (decimal)revenue / (decimal)100000000;
        }

        public decimal ProfitOnIntervalBTC(long lInterval, // seconds
            decimal dHashRate, // hashes per second
            decimal dReward, // coins
            decimal dDifficulty, 
            decimal dExchangeRate) // bitcoins per coin
        {
            return ProfitOnInterval(lInterval, dHashRate, dReward, dDifficulty) * dExchangeRate;
        }
    }
}
