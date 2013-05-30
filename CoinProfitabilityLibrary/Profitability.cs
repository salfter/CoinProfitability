using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ScottAlfter.CoinProfitabilityLibrary
{
    public class Profitability
    {
        public decimal ProfitOnInterval(long lInterval, decimal dHashRate, decimal dReward, decimal dDifficulty)
        {
            BigInteger interval = new BigInteger(lInterval);
            BigInteger hashrate = new BigInteger(dHashRate);
            BigInteger reward = new BigInteger(dReward) * 100000000;
            decimal difficulty = dDifficulty;
            BigInteger target = ((new BigInteger(65535) << 208) * 100000000000) / new BigInteger(difficulty * 100000000000);
            BigInteger revenue = interval * target * hashrate * reward / (new BigInteger(1) << 256);
            return (decimal)revenue / (decimal)100000000;
        }

        public decimal ProfitOnIntervalBTC(long lInterval, decimal dHashRate, decimal dReward, decimal dDifficulty, decimal dExchangeRate)
        {
            return ProfitOnInterval(lInterval, dHashRate, dReward, dDifficulty) * dExchangeRate;
        }
    }
}
