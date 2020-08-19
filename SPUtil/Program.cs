using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPUtil
{
    class Program
    {
        static int Main(string[] args)
        {
            if (!Opts.ParseOpts(args, out Opts opts))
            {
                return 99;
            }

            
            if (opts.command.Count == 0)
            {
                Console.Error.WriteLine("command missing. l|d");
                return 4;
            }
            try
            {
                using (SPSite site = new SPSite(opts.SiteURL))
                using (SPWeb spWeb = site.OpenWeb())
                {
                    SPList spList = spWeb.Lists[opts.listname];


                    List<string> param = opts.command.Count > 1 ? opts.command.Skip(1).ToList() : new List<string>();
                    string cmd = opts.command[0];
                    if ("l".Equals(cmd))
                    {
                        SPQuery myquery = new SPQuery() { Query = String.Empty };
                        SPListItemCollection items = spList.GetItems(myquery);
                        List.Run(items);
                    }
                    else if ("d".Equals(cmd))
                    {
                        string deletingBatch = Delete.CreateBatch(spList.ID.ToString(), param);
                        string result = spWeb.ProcessBatchData(deletingBatch);
                        Console.WriteLine(result);
                        spList.Update();
                    }
                    else if ("da".Equals(cmd))
                    {
                        SPQuery myquery = new SPQuery() { Query = String.Empty };
                        SPListItemCollection items = spList.GetItems(myquery);

                        var IDs = List.GetAllIDs(items).ToList();
                        
                        string deletingBatch = Delete.CreateBatch(spList.ID.ToString(), IDs);
                        DateTime start = DateTime.Now;
                        string result = spWeb.ProcessBatchData(deletingBatch);
                        TimeSpan deleteDuration = DateTime.Now - start;
                        Console.WriteLine($"deleted {IDs.Count} items in {Misc.NiceDuration2(deleteDuration)}");
                        System.IO.File.WriteAllText(".\\results.txt", result, Encoding.UTF8);
                    }
                    else
                    {
                        Console.Error.WriteLine($"invalid command [{cmd}]");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return 8;
            }
            return 0;
        }
    }
}
