using System.Xml.Serialization;
using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Services.Common;
using Services.Common.Interfaces;
using Services.ParserServices.Interfaces;

namespace Services.ParserServices.Imps;

public class XmlParserService:IParserService
{
    private readonly IValidateValueService _validateValueService;

    public XmlParserService(IValidateValueService validateValueService)
    {
        _validateValueService = validateValueService;
    }
    public async Task<ICollection<Operation>> Parse(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Transactions));
            var result = (Transactions) serializer.Deserialize(stream)!;
            if (result == null)
            {
                throw new Exception("No data");
            }
            var listTransaction = new List<Operation>();
            foreach (var res in result.XmlTransactions)
            {
                if (string.IsNullOrEmpty(res.Id))
                {
                    throw new Exception($"Id is null");
                }
                    
                if (string.IsNullOrEmpty(res.TransactionDate))
                {
                    throw new Exception($"TransactionDate is null");
                }
                var date = res.TransactionDate.ParseDate( "yyyy-MM-ddTHH:mm:ss");
                    
                if (string.IsNullOrEmpty(res.PaymentDetails.CurrencyCode))
                {
                    throw new Exception($"CurrencyCode is null");
                }
                if (!_validateValueService.ValidateCurrency(res.PaymentDetails.CurrencyCode))
                {
                    throw new Exception("Currency format is not correct");
                }
                    
                if (string.IsNullOrEmpty(res.Status))
                {
                    throw new Exception($"Status is null");
                }
                var status = ConvertStatus(res.Status);
                    
                listTransaction.Add(new Operation()
                {
                    Id = res.Id,
                    Amount = res.PaymentDetails.Amount,
                    CurrencyCode = res.PaymentDetails.CurrencyCode,
                    Date = date,
                    Status = status
                });
            }

            return await Task.FromResult<ICollection<Operation>>(listTransaction);
        }
    }

    public OperationStatus ConvertStatus(string status)
    {
        switch (status)
        {
            case "Approved":
                return OperationStatus.Approved;
            case "Rejected":
                return OperationStatus.Rejected;
            case "Done":
                return OperationStatus.Done;
            default:
                throw new Exception($"Status :{status} is not supported");

            
        }
    }
    
    [XmlRoot(ElementName="PaymentDetails")]
    public class PaymentDetails {
        [XmlElement(ElementName="Amount")]
        public decimal Amount { get; set; }
        [XmlElement(ElementName="CurrencyCode")]
        public string CurrencyCode { get; set; }
    }

    [XmlRoot(ElementName="Transaction")]
    public class XmlTransaction {
        [XmlElement(ElementName="TransactionDate")]
        public string TransactionDate { get; set; }
        
        [XmlElement(ElementName="PaymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }
        
        [XmlElement(ElementName="Status")]
        public string Status { get; set; }
        
        [XmlAttribute(AttributeName="id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName="Transactions")]
    public class Transactions {
        [XmlElement(ElementName="Transaction")]
        public List<XmlTransaction> XmlTransactions { get; set; }
    }
}