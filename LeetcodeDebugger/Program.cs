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

/*string[] strs = { "hello", "wello", "bello" };

var box = strs[0];
string result = string.Empty;

foreach (char a in box)
{

    for (int i = 1; i < strs.Length; i++)
    {
        string first = string.Empty;
        bool flag = true;
        while (flag)
        {
            foreach (char b in strs[i])
            {
                if (a == b)
                {
                    if (first == "")
                    {
                        first += b;
                    }
                    if (first[0] == b)
                    {
                        result += b;
                        flag = false;
                    }
                }
            }
        }
    }
s
}
*/
/*
int[] digits = { 9,9 };

 
        int[] result = new int[digits.Length + 1];
        int[] realResult = new int[digits.Length];
        bool thisBecomeOneMoreLonger = false;
        int upper = 0;
if (digits.Length == 1)
{
    if (digits[0] == 9)
    {
        int[] first = { 1, 0 };

    }
    int[] second = { digits[0] + 1 };
}
        for (int i = digits.Length-1; i > -1; i--)
        {
    
            if (i == digits.Length-1)
            {
                if (digits[i] == 9)
                {
                    digits[i] = 0;
                    upper = 1;
            result[i] = digits[i];
        }
        else
        {
            digits[i] = digits[i] + 1;
            result[i] = digits[i];
        }

    }
    else {
        if (upper > 0)
        {
            if (i == 0)
            {
                if (digits[i] == 9)
                {
                    digits[i] = 0;
                    result[i] = 0;
                    for (int j = result.Length - 2; j > 0; j--)
                    {
                        result[j] = result[j + 1];
                    }
                    result[0] = 1;
                    thisBecomeOneMoreLonger = true;
                }
                else
                {
                    result[i] = digits[i]+upper;
                }
                
            }
            else
            {
                if (digits[i] == 9)
                {
                    digits[i] = 0;
                    upper = 1;
                    result[i] = 0;
                }
                else
                {
                    digits[i] = digits[i] + upper;
                    upper = 0;
                    result[i] = digits[i];
                }
            }
        }
        else
        {
            result[i] = digits[i];
        }
    }

        }

        if (result[result.Length-1] == 0 && !thisBecomeOneMoreLonger)
        {
            for(int k = 0; k < result.Length -1; k++)
            {
                realResult[k] = result[k];
            }

    foreach (var i in realResult)
    {
        Console.WriteLine(i);
    }


}
else
{
    foreach (var i in result)
    {
        Console.WriteLine(i);
    }

}





Console.WriteLine(result.ToString());
  
 */



int[] input = { -10, -3, 0, 5, 9, 10, 11 };


var root = input.Length / 2;
int[] result = new int[input.Length];
result[0] = root;
input[root] = 0;

if (input[root - 1] > input[root] && input[root + 1] < input[root])
{
    result[2] = input[root - 1];
    result[1] = input[root + 1];
}

if (input[root + 1] > input[root] && input[root - 1] < input[root])
{
    result[2] = input[root + 1];
    result[1] = input[root - 1];
}

 
 


