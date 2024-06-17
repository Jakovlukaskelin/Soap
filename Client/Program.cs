using System;
using System.ServiceModel;
using System.Xml.Linq;
using System.Threading.Tasks;
using Soap.Service;
using Soap.Interface;


class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Unesite termin za pretraživanje: ");
        string searchTerm = Console.ReadLine();

        var binding = new BasicHttpBinding();
        binding.Security.Mode = BasicHttpSecurityMode.Transport;
        var endpoint = new EndpointAddress("https://localhost:44378/SearchService.asmx");

        var client = new SearchServiceClient(binding, endpoint);

        try
        {
            var result = await client.SearchEntitesAsyn(searchTerm);

            Console.WriteLine("Rezultati pretraživanja:");
            Console.WriteLine(FormatXml(result));

            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Greška prilikom pretraživanja: {ex.Message}");
        }
        finally
        {
            if (client.State == CommunicationState.Faulted)
            {
                client.Abort();
            }
            else
            {
                client.Close();
            }
        }
    }

    static string FormatXml(string xml)
    {
        var doc = XDocument.Parse(xml);
        return doc.ToString();
    }
}
