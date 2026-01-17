using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;
// Declaração das bibliotecas que serão utilizadas


namespace PrimeiroConsole // O código está definido no namespace "ImportarMaquinas".
{
 public class Company // A classe "Company" é definida.
{
    public void CreateCompany(ServiceClient service) // O método "CreateCompany" é definido.Ele usa um objeto
 {
        try
        {
                using (StreamReader sr = new StreamReader(@"C:\Temp\Companies.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) // Repetição de leitura de cada linha enquanto for diferente de vazio
                         {
                        Entity company = new Entity("account"); // Referência tabela conta pelo nome lógico
                         {
                            company.Attributes["name"] = line.Split(';')[0];
                            company.Attributes["telephone1"] =
                           line.Split(';')[1];
                            company.Attributes["address1_line1"] =
                           line.Split(';')[2];
                            company.Attributes["address1_city"] =
                           line.Split(';')[3];
                            company.Attributes["address1_stateorprovince"] =
                           line.Split(';')[4];
                            company.Attributes["address1_postalcode"] =
                           line.Split(';')[5];
                            company.Attributes["address1_country"] =
                           line.Split(';')[6];
                        }
                         ;
                        // Cria o serviço para adicionar o registro na tabela
                        Guid IDaccount = Guid.NewGuid();
                        IDaccount = service.Create(company);
                        Console.WriteLine("Conta criada com sucesso! " +
                       IDaccount.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void CreateCompanyMultiple(ServiceClient service)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                ExecuteMultipleRequest multipleRequest = new ExecuteMultipleRequest()
                {
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = true,
                        ReturnResponses = false
                    },
                    Requests = new OrganizationRequestCollection()
                };

                using (StreamReader sr = new StreamReader(@"C:\Temp\Companies.csv"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(';');

                        Entity company = new Entity("account");
                        company["name"] = data[0];
                        company["telephone1"] = data[1];
                        company["address1_line1"] = data[2];
                        company["address1_city"] = data[3];
                        company["address1_stateorprovince"] = data[4];
                        company["address1_postalcode"] = data[5];
                        company["address1_country"] = data[6];

                        CreateRequest createRequest = new CreateRequest
                        {
                            Target = company
                        };

                        multipleRequest.Requests.Add(createRequest);
                    }
                }

                service.Execute(multipleRequest);

                stopwatch.Stop();

                Console.WriteLine("✅ Contas criadas com Multiple Request");
                Console.WriteLine("⏱ Tempo de execução: " + stopwatch.ElapsedMilliseconds + " ms");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro no Multiple Request:");
                Console.WriteLine(ex.Message);
            }
        }


        public void CreateCompanyWithContact(ServiceClient service)
        {
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\Temp\Companies.csv"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(';');

                        // 1️⃣ Criar CONTA
                        Entity account = new Entity("account");
                        account["name"] = data[0];
                        account["telephone1"] = data[1];
                        account["address1_line1"] = data[2];
                        account["address1_city"] = data[3];
                        account["address1_stateorprovince"] = data[4];
                        account["address1_postalcode"] = data[5];
                        account["address1_country"] = data[6];

                        Guid accountId = service.Create(account);

                        // 2️⃣ Criar CONTATO
                        Entity contact = new Entity("contact");
                        contact["firstname"] = data[7];
                        contact["lastname"] = data[8];
                        contact["emailaddress1"] = data[9];
                        contact["telephone1"] = data[10];

                        // Associação contato → conta
                        contact["parentcustomerid"] =
                            new EntityReference("account", accountId);

                        Guid contactId = service.Create(contact);

                        // 3️⃣ Atualizar conta com contato primário
                        Entity accountUpdate = new Entity("account");
                        accountUpdate.Id = accountId;
                        accountUpdate["primarycontactid"] =
                            new EntityReference("contact", contactId);

                        service.Update(accountUpdate);

                        Console.WriteLine("✅ Conta e Contato criados com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro ao criar conta e contato:");
                Console.WriteLine(ex.Message);
            }
        }


        public void FindOrCreateCompany(ServiceClient service)
        {
            try
            {
                Console.Write("Digite o telefone da conta: ");
                string phone = Console.ReadLine();

                QueryExpression query = new QueryExpression("account");
                query.ColumnSet = new ColumnSet("accountid", "name");
                query.Criteria.AddCondition("telephone1", ConditionOperator.Equal, phone);

                EntityCollection results = service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    // ✅ Conta existe
                    Entity account = results.Entities[0];

                    Console.WriteLine("Conta encontrada: " + account["name"]);

                    Console.Write("Nova Cidade: ");
                    string city = Console.ReadLine();

                    Console.Write("Novo Estado: ");
                    string state = Console.ReadLine();

                    Console.Write("Novo País: ");
                    string country = Console.ReadLine();

                    Entity update = new Entity("account");
                    update.Id = account.Id;
                    update["address1_city"] = city;
                    update["address1_stateorprovince"] = state;
                    update["address1_country"] = country;

                    service.Update(update);

                    Console.WriteLine("✅ Conta atualizada com sucesso!");
                }
                else
                {
                    // ❌ Conta não existe
                    Console.WriteLine("Conta não encontrada. Criando nova...");

                    Console.Write("Nome da Conta: ");
                    string name = Console.ReadLine();

                    Console.Write("Cidade: ");
                    string city = Console.ReadLine();

                    Console.Write("Estado: ");
                    string state = Console.ReadLine();

                    Console.Write("País: ");
                    string country = Console.ReadLine();

                    Entity account = new Entity("account");
                    account["name"] = name;
                    account["telephone1"] = phone;
                    account["address1_city"] = city;
                    account["address1_stateorprovince"] = state;
                    account["address1_country"] = country;

                    Guid id = service.Create(account);

                    Console.WriteLine("✅ Conta criada com sucesso!");
                    Console.WriteLine("ID da conta: " + id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Erro na tarefa 7:");
                Console.WriteLine(ex.Message);
            }
        }


    }
}