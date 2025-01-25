// See https://aka.ms/new-console-template for more information
using System.Linq.Expressions;

Console.WriteLine("Hello, World!");

/*int[] nums = { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 };

   int k = 0;
    int lastInsertNumber;
    for (int i = 0; i < nums.Length; i++)
    {
        int inserted = 0;
        do
        {
            nums[k] = nums[i];
            lastInsertNumber = nums[i];
            k++;
            inserted = 1;
        } while (lastInsertNumber != nums[i] && inserted == 0);
    }
    return k;*/

/*string romanNumber = "IIII";

int result = 0;

Dictionary<char, int> map = new Dictionary<char, int>()
{
    {'I',1 },
    {'V',5 },
    {'X',10 },
    {'L',50 },
    {'C',100 },
    {'D',500 },
    {'M',1000 }
};

for(int i = 0; i< romanNumber.Length; i++)
{
    int currentValue = map[romanNumber[i]];

    if(i+1 < romanNumber.Length && map[romanNumber[i+1]] > currentValue)
    {
        result -= currentValue;
    }
    else
    {
        result += currentValue;
    }
}*/