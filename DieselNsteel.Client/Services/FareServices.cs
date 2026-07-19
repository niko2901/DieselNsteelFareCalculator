using System;
using DieselNsteel.Client.Models;

namespace DieselNsteel.Client.Services
{
    public class FareServices
    {
        public List<string> BalagtasRoute { get; } = new()
        {
            "Bulakan-Matungao",
            "Bulakan-Panginay Guiguinto",
            "Bulakan-Panginay Balagtas",
            "Bulakan-Wawa"
        };

        public List<string> GuiguintoRoute = new()
        {
            "Bulakan-Matungao",
            "Bulakan-MacArthur Highway",
            "Bulakan-BSU",
            "Bulakan-Tuktukan"
        };

        public List<string> BulakanToMalolosRoute = new()
        {
            "Maysantol/San Nicolas/Pitpitan",
            "Mambog",
            "Matimbo",
            "Panasahan",
            "Bagna",
            "Atlag",
            "San Juan/Sto. Rosario"
        };

        public List<string> MalolosToBulakanRoute = new()
        {
            "Atlag/Bagna/Panasahan",
            "Matimbo",
            "Mambog",
            "Pitpitan",
            "San Nicolas",
            "Maysantol",
            "Bagumbayan/San Jose"
        };

        public List<string> GetStationsForRoute(string RouteName) => RouteName switch
        {
            "Balagtas" => BalagtasRoute,
            "Guiguinto" => GuiguintoRoute,
            "BulakanMalolos" => BulakanToMalolosRoute,
            "MalolosBulakan" => MalolosToBulakanRoute,
            _ => BalagtasRoute
        };

        public FareResult ComputeAndChange(
            string routeName,
            string origin,
            string destination,
            int passengerCount,
            bool isDiscounted,
            decimal cashReceived
        )
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
                return new FareResult(0, 0, 0, "");

            List<string> activeRoute = GetStationsForRoute(routeName);

            // 1. Calculate station hops using list indices
            int startIndex = activeRoute.IndexOf(origin);
            int endIndex = activeRoute.IndexOf(destination);
            Console.Write( startIndex);
            Console.Write( endIndex);

            if (startIndex == -1 || endIndex == -1)
                return new FareResult(0, 0, 0, "Selected stations do not exist on this route line.");

            int stationHops = Math.Abs(endIndex - startIndex);
            decimal singlePassengerFare = 0;

            if (origin == destination)
                singlePassengerFare = 13;

            // Apply rules based on route matrix thresholds
            if (routeName == "BulakanMalolos" || routeName == "MalolosBulakan")
            {
                if(startIndex >= 1)
                {
                    singlePassengerFare = (endIndex-startIndex) < 4 ? 13 :  (13 + (((endIndex-startIndex) - 3) * 2));
                }
                else
                {
                    singlePassengerFare = 13 + (endIndex * 2);
                }
            }
            else
            {
                // Balagtas & Guiguinto standard scaling rules: 1-4 hops = 13, 5+ hops = 15
                if (routeName == "Balagtas")
                {
                    if(endIndex == 3 && startIndex == 0)
                    {
                        singlePassengerFare = 15;
                    }
                    else
                    {
                        singlePassengerFare = stationHops <= 3 ? 13 : 15;
                    }
                }
                else
                {
                    singlePassengerFare = stationHops <= 3 ? 13 : 15;
                }
            }

            // Apply 2 Pesos discount for Senior/Student
            if (isDiscounted)
            {
                singlePassengerFare -= 2;
            }

            // 3. Compute group grand total
            decimal totalFare = singlePassengerFare * passengerCount;

            // 4. Calculate change from cash bill taps
            decimal change = 0;
            string errorMessage = "";

            if (cashReceived > 0)
            {
                if (cashReceived < totalFare)
                    errorMessage = "Insufficient bill amount!";
                else
                    change = cashReceived - totalFare;
            }

            return new FareResult(singlePassengerFare, totalFare, change, errorMessage);
        }
    }
}
