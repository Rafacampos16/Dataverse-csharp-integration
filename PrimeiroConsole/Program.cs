using Microsoft.PowerPlatform.Dataverse.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiroConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceClient service = new Connection().ConnectionClientCredential();

                if (service.IsReady)
                {
                    Console.WriteLine("✅ Conectado");

                    //Company company = new Company();
                    //company.CreateCompany(service); // método simples

                    //Company company = new Company();
                    //company.CreateCompanyMultiple(service); // Multiple Request

                    //Company company = new Company();
                    //company.CreateCompanyWithContact(service); //com contato relacionado

                    Company company = new Company();
                    company.FindOrCreateCompany(service); //buscar contato



                }
                else
                {
                    Console.WriteLine("❌ Falha na conexão:");
                    Console.WriteLine(service.LastError);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro:");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

    }
}
