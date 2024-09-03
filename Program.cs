// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    //file reader
    StreamReader sr = new("data.txt");

    //create variables for use in parsing data
    String[] lineSplit;
    String[] dateArray;
    String[] dayStrings;
    int[] days = {0, 0, 0, 0, 0, 0, 0};
    DateTime date;

    while(!sr.EndOfStream){
        //separate date from data
        lineSplit = (sr.ReadLine() ?? "").Split(',');
        //separate out individual dates in data
        dayStrings = lineSplit[1].Split('|');
        //convert data from string to integers
        for (int i = 0; i < dayStrings.Length; i++){
            days[i] = Int32.Parse(dayStrings[i]);
        }
        //split elements of the date
        dateArray = lineSplit[0].Split('/');
        //convert string representations of date components into a DateTime Obejct
        date = new DateTime(Int32.Parse(dateArray[2]), Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]));

        //Display Interface
        Console.WriteLine($"   Week of {date:MMM d, yyyy}");
        Console.WriteLine("===========================");
        Console.WriteLine("Sun|Mon|Tue|Wed|Thu|Fri|Sat");
        Console.WriteLine("———————————————————————————");
        Console.Write($"{days[0], 3}");
        for (int i = 1; i < days.Length; i++){
            Console.Write($"|{days[i],3}");
        }
        Console.WriteLine("\n———————————————————————————");
        Console.WriteLine($"Total: {days.Sum()}");
        //Does fancy math to output minutes instad of fractions of an hour
        Console.WriteLine($"Average: {days.Sum()/7} hours, {(double)(days.Sum()%7)/7*60:n0} minutes");
        Console.WriteLine('\n');
    }
}
