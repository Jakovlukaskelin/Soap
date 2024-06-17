using CsvHelper;
using Soap.Entity;
using Soap.Interface;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Soap.Service
{
    public class SearchService : IsearchService
    {
        private const string XmlFilePath = "SuperStore.xml";
        private const string CsvFilePath = "Super_Store_data.csv";
        public SearchService()
        {
            GenerateXmlFile();
        }
        public async Task<string> SearchEntitesAsyn(string search)
        {
            var result = SearchXmlFile(search);
            return await Task.FromResult(result);


       

            }

        private string SearchXmlFile(string search)
        {
            var document = XDocument.Load(XmlFilePath);
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            var xpath = $"//SuperStore[contains(OrderID, '{search}') or contains(CustomerName, '{search}') or contains(ProductName, '{search}')]";

            var matchingElements = document.XPathSelectElements(xpath, namespaceManager);
            return new XElement("SearchResults", matchingElements).ToString();
        }

        private void GenerateXmlFile()
        {
            using var reader = new StreamReader(CsvFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<SuperStore>().ToList();

            var entites = new XElement("Entities",
                records.Select(records =>
                new XElement("SuperStore",
                new XElement("RowID", records.RowID),
                 new XElement("OrderID", records.OrderID),
                  new XElement("OrderDate", records.OrderDate),
                   new XElement("ShipDate", records.ShipDate),
                    new XElement("ShipMode", records.ShipMode),
                     new XElement("CustomerID", records.CustomerID),
                      new XElement("Segment", records.Segment),
                       new XElement("CustomerName", records.CustomerName),
                        new XElement("Country", records.Country),
                         new XElement("City", records.City),
                          new XElement("State", records.State),
                           new XElement("PostalCode", records.PostalCode),
                            new XElement("Region", records.Region),
                             new XElement("ProductID", records.ProductID),
                              new XElement("Category", records.Category),
                               new XElement("SubCategory", records.SubCategory),
                                new XElement("ProductName", records.ProductName),
                                 new XElement("Sales", records.Sales),
                                  new XElement("Quantity", records.Quantity),
                                   new XElement("Discount", records.Discount),
                                    new XElement("Profit", records.Profit)
                )));

            entites.Save(XmlFilePath);
        }
    }
}
