using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCor
{
    public class ArrayModel
    {
        private List<ColoredNumber> nums;
        public ColoredNumber findValue;
        public int borderOne, borderTwo;
        private int numsSize = 64;


        public ArrayModel(int listSize = 64)
        {
            this.findValue = new ColoredNumber(1);
            nums = initNums(listSize);
            numsSize = nums.Count;
            borderOne = 0;
            borderTwo = listSize - 1;

        }

        public List<ColoredNumber> GetNums()
        {
            return nums;
        }


        private List<ColoredNumber> initNums(int size = 64)
        {
            var listnums = new List<ColoredNumber>();
            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                var num = new ColoredNumber(random.Next(0, 100));
                listnums.Add(num);
            }
            return listnums;
        }


        public void SortArray()
        {

            var len = nums.Count;
            for (var i = 1; i < len; i++)
            {
                for (var j = 0; j < len - i; j++)
                {
                    if (nums[j].value > nums[j + 1].value)
                    {
                        ColoredNumber temp = nums[j];
                        nums[j] = nums[j+1];
                        nums[j+1] = temp;
                    }
                }
            }
             
        }

        public int GetMidIndex()
        {
            return (borderOne + borderTwo) / 2;
        }

        public bool BinaryRecolor()
        {
            bool isFinished = false;

            if ((borderTwo - borderOne) != 1)
            {
                int midIndex = GetMidIndex();

                if (findValue.value < nums[midIndex].value)
                {
                    borderTwo = midIndex;

                    for (int i = borderTwo; i < numsSize; i++)
                    {
                        nums[i].color = nums[i].lessColor;
                    }
                }
                else if (findValue.value > nums[midIndex].value)
                {
                    borderOne = midIndex;

                    for (int i = 0; i < borderOne+1; i++)
                    {
                        nums[i].color = nums[i].lessColor;
                    }
                }
                else if (findValue.value == nums[midIndex].value)
                {
                    isFinished = true;
                    borderOne = 0;
                    borderTwo = nums.Count - 1;
                    
                }
            }
            else
            {
                if (nums[borderOne].value == findValue.value)
                {
                    isFinished = true;
                    borderOne = 0;
                    borderTwo = nums.Count - 1;
                    
                }
                else
                {
                    borderOne = 0;
                    borderTwo = nums.Count - 1;
                    
                    throw new Exception("Число не найдено!");
                }
                    
                
            }

            return isFinished;
        }
    }
}
