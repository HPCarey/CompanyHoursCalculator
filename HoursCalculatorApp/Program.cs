namespace HoursCalculatorApp
{
    internal class Program
    {
        // File Paths
        const string DATAFILEPATH = @"C:\Users\ThinkPad\Desktop\visualstudioapps\";
        const string COMPANYHOURSFILE = "hourscalculatorapp.csv";
        const string SUMMARY = "summary.txt";

        // Arrays
        static string[] departments = {"finance", "sales", "accounting", "management"};
        static double[] totalHours = new double[4]; //per department
        static double[] highestHoursValue = new double[4]; // store the highest value for number of hours per department
        static string[] personHighestHours = new string[4]; 
        static int[] employeeCount = new int[4];
        static double totalOverallHours = 0; // Total overall hours worked in the company
        static int totalOverallEmployees = 0; // Total overall employees in the company

        // data lines
        static string[] allDataLines;
        static void Main(string[] args)
        {
            try
            {
                allDataLines = File.ReadAllLines(DATAFILEPATH + COMPANYHOURSFILE);
                for (int i = 1; i < allDataLines.Length; i++) // i = 1 to skip the header
                {
                    ProcessDataLine(allDataLines[i]);
                }
                DisplayResults();
                //foreach (var line in allDataLines)
                //{
                //    Console.WriteLine(line);
                //}
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DisplayResults()
        {
            string outputLine;
            if (File.Exists(DATAFILEPATH + SUMMARY)) 
            {
                File.Delete(DATAFILEPATH + SUMMARY);
            }
            for (int i = 0; i < departments.Length; i++)
            {
                outputLine = departments[i];
                DisplayAndWrite(outputLine);
                outputLine = $" Total hours worked : {totalHours[i]}";
                DisplayAndWrite(outputLine);
                outputLine = $"Average hours worked per employee {totalHours[i] / employeeCount[i]:F2}";
                DisplayAndWrite(outputLine);
                outputLine = $"The employee who worked the most hours : {personHighestHours[i]} with {highestHoursValue[i]}";
                DisplayAndWrite(outputLine);
                outputLine = $"===============";
                DisplayAndWrite(outputLine);
                // Display overall company totals

            }
            outputLine = "Overall Company Totals";
            DisplayAndWrite(outputLine);
            outputLine = $" Total overall hours worked in the company: {totalOverallHours}";
            DisplayAndWrite(outputLine);
            outputLine = $" Average across all departments: {totalOverallHours / totalOverallEmployees:F2}";
            DisplayAndWrite(outputLine);
        }
        static void DisplayAndWrite(string outputLine)
        {
            Console.WriteLine(outputLine);
            File.AppendAllText(DATAFILEPATH + SUMMARY, outputLine + "\n");
        }
        static void ProcessDataLine(string dataLines)
        {
            //Split the data lines into the datafields array
            string[] datafields = dataLines.Split(',');

            // declare variables to store the specific department and the sales value for that department
            string department = datafields[1];
            double departmentHoursValue = 0;
            // increment the departmentSalesValue with the Sales data
            // This programme assumes the sales data is in the 3rd - 7th columns
            for (int i = 2; i < datafields.Length; i++)
            {
                departmentHoursValue += double.Parse(datafields[i]);
            }
            // Get the index for the department
            int deparmentIndex = Array.IndexOf(departments, department);
            totalHours[deparmentIndex] += departmentHoursValue;
            totalOverallHours += departmentHoursValue;

            // Check highest hours value against departmentHours and replace if lower
            if (departmentHoursValue > highestHoursValue[deparmentIndex]) 
            {
                highestHoursValue[deparmentIndex] = departmentHoursValue;
                personHighestHours[deparmentIndex] = datafields[0];
            }
            employeeCount[deparmentIndex]++;
            totalOverallEmployees++;
        }

    }
}