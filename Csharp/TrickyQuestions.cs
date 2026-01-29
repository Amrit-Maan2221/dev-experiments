// 1

int a = 5;
int b = a;
b = 10;
Console.WriteLine(a);


// 2
double x = 10 / 3;
Console.WriteLine(x);


// 3
int i = 1;
Console.WriteLine(i++ + ++i);

// 4
/*
int number = 0;

if (number) // this won't compile
{
    Console.WriteLine("True");
}
*/

/*
int y = 2;

switch (y)
{
    case 1:
        Console.WriteLine("One");// won't compile break is mandatory
    case 2:
        Console.WriteLine("Two");
        break;
}
*/

/* this also won't compile as n is foreach iterator
int[] nums = {1,2,3};

foreach (var n in nums)
{
    n = n * 2;
}
Console.WriteLine(nums[0]);
*/

static void Print(string s)
{
    s = "Hello";
}

string msg = "Hi";
Print(msg);
Console.WriteLine(msg);

/*
var l = 10;
l = "Hello";
*/



Console.WriteLine((int)Status.Rejected);
Status s = (Status)10; // why did not this throw exception ?
Console.WriteLine(s);


object obj = 10;
long y = (long)obj; // Runtime exception

enum Status
{
    Pending = 1,
    Approved = 5,
    Rejected
}

