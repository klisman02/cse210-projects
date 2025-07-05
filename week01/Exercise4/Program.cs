using System;

class Program
{
    static void Main(string[] args)
    {
 List<int> numbers = new List<int>();
        
        // Part 0: Get the numbers from the user
        // We will use a while loop to get the numbers from the user until they enter   
        int userNumber = -1;
        while (userNumber != 0)
        {
            Console.Write("Enter a number (0 to quit): ");
            
            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);
            
            // If the user entered a number other than 0, we will add it to the list
            // If the user entered 0, we will exit the loop.    
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }
        // Part 1: Compute the sum
        // We will use a foreach loop to iterate through the list of numbers and compute the sum    
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");
        // Part 2: Compute the average
        // The average is the sum divided by the number of elements in the list.            
        // We can use the Count property of the list to get the number of elements
        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
            return; // Exit the program if no numbers were entered
        }   

        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Part 3: Find the max
        // We will use a foreach loop to iterate through the list of numbers and find the maximum       
        
        int max = numbers[0];

        foreach (int number in numbers)
        {
            if (number > max)
            {
                // If the current number is greater than the max, we update the max
                // We assume the first number is the max, so we start with numbers[0]   
                max = number;
            }
        }

        Console.WriteLine($"The max is: {max}");
    }
}