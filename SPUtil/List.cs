using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace SPUtil
{
    public class List
    {
        public static void Run(SPListItemCollection items)
        {
            Console.WriteLine($"count items:\t{items.Count}");
            Console.WriteLine($"list ID:\t{items.List.ID}");

            StringCollection fieldNames = items.QueryFieldNames;

            foreach (SPListItem item in items)
            {
                /*
                foreach (string fieldname in fieldNames)
                {
                    var val = item[fieldname];
                    Console.WriteLine($"{fieldname}\t{val}");
                }
                */
                //Console.WriteLine($"{item["ID"]}");
                Console.WriteLine("--------------------------------------------------------");
                foreach (SPField f in item.Fields)
                {
                    Console.WriteLine($"{f.Title}\t{item[f.Title]}");
                }
                Console.WriteLine("========================================================");

            }
        }
        public static IEnumerable<string> GetAllIDs(SPListItemCollection items)
        {
            foreach (SPListItem item in items)
            {
                yield return item["ID"].ToString();
            }
        }
    }
}
