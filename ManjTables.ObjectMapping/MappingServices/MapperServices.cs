using ManjTables.DataModels.Models;
using ManjTables.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManjTables.ObjectMapping.MappingServices
{
    public class MapperServices
    {
        public static string GetProgramText(string program)
        {
            string programText = string.Empty;

            if (program == null)
            {
                return "No Program listed";
            }

            if (program.Contains("Primary"))
            {
                programText = "Primary";
                if (program.Contains("Full") || program.Contains("full"))
                {
                    programText += " Full Day";
                }
                if (program.Contains("Extension") || program.Contains("extension"))
                {
                    programText += " Morning Extension";
                }
                else if (program.Contains("Half"))
                {
                    programText += " 1/2 Day";
                }
                else if (program.Contains("Extended") || program.Contains("extended"))
                {
                    programText += " ECP";
                }
            }
            else if (program.Contains("Toddler"))
            {
                programText = "Toddler";
                if (program.Contains("Extension"))
                {
                    programText += " Morning Extension";
                }
                else if (program.Contains("All"))
                {
                    programText += " ECP";
                }
                else if (program.Contains("Half"))
                {
                    programText += " 1/2 Day";
                }
                else if (program.Contains("Extended") || program.Contains("extended"))
                {
                    programText += " ECP";
                }
            }
            else if (program.Contains("Elementary"))
            {
                programText = "Elementary";
                if (program.Contains("After") || program.Contains("after"))
                {
                    programText += " with ECP";
                }
            }
            else if (program.Contains("All Day Montessori"))
            {
                programText = "Primary ECP";
            }

            return programText;
        }

        public static bool IsEcp(string programmeName)
        {
            if (programmeName.Contains("ECP"))
            {
                return true;
            }
            return false;
        }
        public static string GetDismissalTime(string hoursString)
        {
            if (string.IsNullOrEmpty(hoursString))
            {
                return string.Empty;
            }

            string dismissal = string.Empty;
            string[] parts = hoursString.Split('-');
            if (parts.Length > 1)
            {
                string? endTimePart = parts[1]?.Trim();
                if (!string.IsNullOrEmpty(endTimePart))
                {
                    int indexSpace = endTimePart.IndexOf(' ');
                    if (indexSpace != -1)
                    {
                        dismissal = endTimePart[..indexSpace];
                    }
                }
            }
            return dismissal;
        }
    }
}
