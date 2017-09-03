using System;

namespace GameLoop
{
    internal class Stat
    {
        public string Name { get; set; }
        public int Value { get; private set; }
        public int HeroicValue { get; private set; }
        public string Parent { get; set; }
        public int RowNo {get; set;}
        public int GroupNo { get; set; }

        public Stat (string name, int value )
        {
            Name = name;
            if (value <1||value >10)
            {
                throw new Exception("Invalid Stat");
            }
            Value = value;
            HeroicValue = 0;
        }

        public Stat (string name, int value , int heroicValue) : this (name,value)
        {
            if (heroicValue > value|| heroicValue < 0  )
            {
                throw new Exception("Invalid HeroicStat");
                
            }
            HeroicValue = heroicValue;
        }

        public void setValue (int value)
        {
            if (value > 10 || value < 1)
                throw new Exception("Invalid Stat");
            if (value < HeroicValue)
                HeroicValue = value;
            Value = value;
        }

        public void setHeroicValue (int heroicValue)
        {
            if (heroicValue < 0)
                throw new Exception("Invalid heroic stat");
            if (heroicValue > Value)
            {
                HeroicValue = Value;
            }
            else
            {
                HeroicValue = heroicValue;
            }
        }

        public bool Hero()
        {
            return (HeroicValue != 0);
        }

        public int roll()
        {
            return HeroicValue * (HeroicValue + 1) / 2 + Rolled();
        }

        public int Bar()
        {
            return HeroicValue * (HeroicValue + 1) / 2;
        }

        private int Rolled()
        {
            Random random = new Random();
            int counter = 1;
            int score = 0;
            int roll;
            while (counter <= Value)            {
                roll = random.Next(10);
                if (roll >= 6)
                    score++;
                if (roll == 9)
                    score++;
                counter++;
            }
            return score;
        }
    }
}