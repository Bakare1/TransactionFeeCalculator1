using System;
using System.IO;
using Newtonsoft.Json;

namespace TransactionSurcharge
{
    public class Surchage
    {
       
        public CustomerModel FileReader(int input)
        {

           
            if (input <= 0)
            {
                return null;
            }

            
            string path= @"C:\Parkway\fees.config.json";
            

            string filePath = path;

            string fileContent = String.Empty;

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
                serialized = JsonConvert.DeserializeObject<FeesAndCharges>(fileContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
            var customer = new CustomerModel();


            foreach (var elem in serialized.fees)
            {
                if (input<elem.feeAmount)
                {
                    customer = null;
                }

                if (input >= elem.minAmount && input <= elem.maxAmount)
                {
                    customer.Amount = input;
                    customer.Charge = elem.feeAmount;
                    customer.TransferAmount = input - elem.feeAmount;
                    customer.DebitAmount = customer.TransferAmount + elem.feeAmount;
                    
                }

            }
            return customer;
        }
    }
}