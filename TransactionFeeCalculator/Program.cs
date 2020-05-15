using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace TransactionFeeCalculator
{
    class Program
    {
        
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Write("Please input Amount to be transferred: ");
                var input = Console.ReadLine();

                var inputValue = int.TryParse(input, out int result);

                if (!inputValue)
                {
                    Console.WriteLine($"Error Please check your input detils");
                    continue;

                }

                string pathToFile = @"C:\Parkway\fees.config.json";
                
                var content = FileReader(pathToFile, result);

                Console.Write("Your Charges is: ");
                Console.WriteLine(content); 
            }

        }

        static int FileReader(string Parkway, int input)
        {

            if (input <= 0)
            {
                return 0;
            }


            string filePath = Parkway;

            string fileContent=String.Empty;

            try
            {
                StreamReader reader = new StreamReader(filePath);
                fileContent = reader.ReadToEnd();
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            
            FeesAndCharges serialized;
            try
            {
                serialized = JsonConvert.DeserializeObject<FeesAndCharges>(fileContent);//.DeserializeObject<FeesAndCharges>(fileContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            int feeToPay = 0;
            

            foreach (var elem in serialized.fees)
            {
                if (input>=elem.minAmount && input <= elem.maxAmount)
                {
                    feeToPay = elem.feeAmount;
                }

            }
            return feeToPay;
        }
    }
}
