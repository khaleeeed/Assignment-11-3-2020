using Assignment.Domain.Models;
using System;
using System.IO;

namespace Assignment.UI.Logic
{
    public interface IReadFileLogic
    {
        Result ImportDataFromFile(Stream openReadStream, string fileExtension);
    }
}