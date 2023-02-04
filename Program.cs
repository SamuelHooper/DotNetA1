namespace DotNetA1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Declarations
            string ticketFile = "tickets.csv";
            string menuOption;

            do
            {
                // Gets user input for menu navigation
                Console.WriteLine("1) Read data from file.");
                Console.WriteLine("2) Create data from file.");
                Console.WriteLine("Enter any other key to exit.");
                menuOption = Console.ReadLine();

                if (menuOption == "1" && File.Exists(ticketFile))
                {
                    // Logic to read
                    StreamReader sr = new(ticketFile);

                    sr.ReadLine(); // Skips header line
                    while (!sr.EndOfStream)
                    {
                        var ticketLine = sr.ReadLine().Split(',');
                        var watchingLine = ticketLine[6].Split('|');

                        string ticketOutput = $"\nTicketID: {ticketLine[0]}\n" +
                            $"  Summery: {ticketLine[1]}\n" +
                            $"  Status: {ticketLine[2]}\n" +
                            $"  Priority: {ticketLine[3]}\n" +
                            $"  Submitter: {ticketLine[4]}\n" +
                            $"  Assigned: {ticketLine[5]}\n" +
                            $"  Watched by: ";
                        for (int i = 0; i < watchingLine.Length; i++)
                        {
                            // Add proper punctuation and grammer to tickets with 1 or 2 watchers
                            switch (watchingLine.Length)
                            {
                                case 1: ticketOutput += watchingLine[0]; break;
                                case 2: ticketOutput += $"{watchingLine[0]} and {watchingLine[1]}"; i++; break;
                                default:
                                    if (i == watchingLine.Length - 1)
                                    {
                                        ticketOutput += "and " + watchingLine[i];
                                    }
                                    else
                                    {
                                        ticketOutput += watchingLine[i] + ", ";
                                    }
                                    break;
                            }
                        }

                        // Output info of each ticket
                        Console.WriteLine(ticketOutput);
                    }

                    sr.Close();
                }
                else if (menuOption == "2")
                {
                    // Logic to write
                    StreamWriter sw = new(ticketFile);
                    List<string> watchers = new();
                    string contineLoop;
                    int ticket = 1;

                    // Header Creation
                    sw.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");

                    // Loop to add tickets
                    do
                    {
                        Console.Write("Enter the ticket summery: ");
                        var summary = Console.ReadLine();

                        Console.Write("Enter the ticket status: ");
                        var status = Console.ReadLine();

                        Console.Write("Enter the ticket priority: ");
                        var priority = Console.ReadLine();

                        Console.Write("Enter the ticket submitter: ");
                        var submitter = Console.ReadLine();

                        Console.Write("Enter the ticket assigned: ");
                        var assigned = Console.ReadLine();

                        // Loop to add watchers
                        for (int i = 0; i < 10; i++)
                        {
                            Console.Write("Enter a ticket watcher: ");
                            var watcher = Console.ReadLine();

                            watchers.Add(watcher + "|");

                            Console.Write("Enter another ticket watcher (Y/N)? ");
                            string addWatcher = Console.ReadLine().ToUpper();
                            if (addWatcher != "Y")
                            {
                                watchers[i] = watchers[i].TrimEnd('|');
                                break;
                            }
                        }

                        // String concatination for saving to file
                        string entryLine = $"{ticket++},{summary},{status},{priority},{submitter},{assigned},";

                        foreach (string watcher in watchers)
                        {
                            entryLine += watcher;
                        }

                        sw.WriteLine(entryLine);

                        Console.Write("\nAdd another ticket (Y/N)? ");
                        contineLoop = Console.ReadLine().ToUpper();
                    } while (contineLoop == "Y");

                    sw.Close();
                }
                else if (!File.Exists(ticketFile)) // Gives error message if option 1 is picked with no avalible file
                {
                    Console.WriteLine("File does not exist");
                }
            } while (menuOption == "1" || menuOption == "2");
        }
    }
}