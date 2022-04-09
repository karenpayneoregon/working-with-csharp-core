using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupProject.Classes
{
    public class SqlStatements
    {
        public static string ByPhoneByContactTypeSelect => @"SELECT Cust.CustomerIdentifier, 
       Cust.CompanyName AS [Company name], 
       Cust.City, 
       Cust.PostalCode, 
       Contacts.ContactId, 
       Contacts.FirstName + ' ' + Contacts.LastName AS ContactName, 
       Countries.CountryIdentifier, 
       Countries.[Name] AS Country, 
       Cust.Phone AS [Customer Phone], 
       Devices.PhoneTypeIdentifier, 
       Devices.PhoneNumber AS [Contact Phone],
	   FORMAT(Cust.ModifiedDate, 'MM/dd/yyyy') AS Modified
FROM Customers AS Cust
     INNER JOIN ContactType AS CT ON Cust.ContactTypeIdentifier = CT.ContactTypeIdentifier
     INNER JOIN Countries ON Cust.CountryIdentifier = Countries.CountryIdentifier
     INNER JOIN Contacts ON Cust.ContactId = Contacts.ContactId
     INNER JOIN ContactDevices AS Devices ON Contacts.ContactId = Devices.ContactId
WHERE Devices.PhoneTypeIdentifier = @PhoneType
      AND Cust.ContactTypeIdentifier = @ContactType;";
    }
}